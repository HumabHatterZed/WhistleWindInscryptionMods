using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public class Bless : PlagueDoctorClass
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Bless";
        public const string rDesc = "Upon successfully healing a card using the Healer ability, Plague Doctor changes its appearance.";

        private readonly string eventIntro3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private readonly string eventIntroRepeat = "[c:bR]The time has come again. I will be thy guide.[c:]";

        public override IEnumerator TriggerBlessing()
        {
            if (LobotomyConfigManager.Instance.NoEvents || LobotomySaveManager.TriggeredWhiteNightThisBattle)
                yield break;

            SaviourBossUtils.UpdateBlessings(base.PlayableCard, 1);

            yield return HelperMethods.ChangeCurrentView(View.Board);

            base.PlayableCard.Anim.LightNegationEffect();
            base.PlayableCard.ApplyAppearanceBehaviours(base.PlayableCard.Info.appearanceBehaviour);
            base.PlayableCard.RenderCard();
            yield return new WaitForSeconds(0.2f);

            int blessings = SaviourBossUtils.Blessings(base.PlayableCard);

            // play hint dialogue if it hasn't been yet, or if this is the first run since it triggered
            if (!DialogueEventsData.EventIsPlayed("PlagueDoctorBless"))
                yield return DialogueHelper.PlayDialogueEvent("PlagueDoctorBless", card: base.PlayableCard);

            else if (!LobotomySaveManager.TriggeredWhiteNightThisRun && blessings == 0)
                yield return DialogueHelper.PlayAlternateDialogue("The hands of the Clock move towards salvation.");

            else if (blessings == 9)
                yield return DialogueHelper.PlayAlternateDialogue("[c:bR]When the day comes, find me.[c:]");

            else if (blessings == 10)
                yield return DialogueHelper.PlayAlternateDialogue("[c:bR]I will save your life from destruction and raise you from the end of the world.[c:]");
        }
        public override IEnumerator TriggerClock() => CheckTheClock();

        private IEnumerator CheckTheClock()
        {
            // can only trigger WhiteNight once per battle
            if (LobotomyConfigManager.Instance.NoEvents || LobotomySaveManager.TriggeredWhiteNightThisBattle)
                yield break;

            LobotomyPlugin.Log.LogDebug("Checking the Clock");
            int blessings = SaviourBossUtils.Blessings(base.PlayableCard);
            if (blessings >= 0 && blessings < 12)
                yield break;

            LobotomyPlugin.Log.LogDebug("Clock has struck twelve");
            if (Singleton<BoardManager>.Instance.AllSlotsCopy.Exists(x => x.Card != null && x.Card.Info.name == "wstl_whiteNight"))
            {
                yield return base.PlayableCard.DieTriggerless();
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]You shall have no other gods before me.[c:]");
                yield break;
            }

            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);
            yield return new WaitForSeconds(0.5f);

            // If blessings are in the negatives (aka someone altered the config value), wag a finger and go 'nuh-uh-uh!'
            if (LobotomyConfigManager.Instance.NumOfBlessings < 0)
                yield return DialogueHelper.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou cannot stop my ascension. Even the [c:]tutelary[c:bR] bows to my authority.[c:]");

            // Change Leshy's eyes to red
            if (SaveManager.SaveFile.IsPart1)
                LeshyAnimationController.Instance?.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));

            LobotomyPlugin.Log.LogDebug("Transforming into WhiteNight");

            // need to do this or it breaks later
            bool isOpponent = base.PlayableCard.OpponentCard;
            CardSlot baseSlot = base.PlayableCard.Slot;

            // Transform the Doctor into Him
            yield return base.PlayableCard.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            yield return new WaitForSeconds(0.5f);

            // Play dialogue depending on whether this is the first time this has happened this run
            if (!LobotomySaveManager.TriggeredWhiteNightThisRun)
            {
                LobotomySaveManager.TriggeredWhiteNightThisRun = true;
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightEventIntro");
            }
            else
                yield return DialogueHelper.ShowUntilInput(eventIntroRepeat, speaker: DialogueEvent.Speaker.Bonelord);

            yield return DialogueHelper.ShowUntilInput(eventIntro3, speaker: DialogueEvent.Speaker.Bonelord);
            yield return new WaitForSeconds(0.2f);

            LobotomyPlugin.Log.LogDebug("Determining whether player owns One Sin or has the Heretic");

            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            string oneSinName = "wstl_oneSin";
            bool playerHasOneSin;
            if (SaveManager.SaveFile.IsPart2)
                playerHasOneSin = SaveManager.SaveFile.gbcData.deck.Cards.Exists(info => info.name == oneSinName);
            else
                playerHasOneSin = RunState.DeckList.Exists(info => info.name == oneSinName);

            LobotomyPlugin.Log.LogDebug("Creating Apostles");
            foreach (CardSlot slot in BoardManager.Instance.GetSlotsCopy(!isOpponent).Where(slot => slot.Card != null && slot != baseSlot))
            {
                if (slot.Card.Info.name != oneSinName)
                    yield return SaviourBossUtils.ConvertCardToApostle(slot.Card, base.GetRandomSeed());
            }
            
            if (playerHasOneSin)
            {
                LobotomyPlugin.Log.LogDebug("Player has One Sin");
                yield return new WaitForSeconds(0.5f);

                if (BoardManager.Instance.GetPlayerCards(x => x.Info.name == oneSinName).Count > 0)
                {
                    LobotomyPlugin.Log.LogDebug("One Sin is on the board");
                    foreach (PlayableCard card in BoardManager.Instance.GetPlayerCards(x => x.Info.name == oneSinName))
                    {
                        // Transform the first One Sin into Heretic
                        // Remove the rest
                        if (!SaviourBossUtils.PlayerHasHeretic)
                        {
                            yield return card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                            SaviourBossUtils.PlayerHasHeretic = true;
                        }
                        else
                            card.RemoveFromBoard();
                    }
                }
                else if (CardDrawPiles3D.Instance.Deck.cards.Exists(x => x.name == oneSinName))
                {
                    LobotomyPlugin.Log.LogDebug("One Sin is in the deck");
                    CardInfo oneSin = CardDrawPiles3D.Instance.Deck.cards.Find(x => x.name == oneSinName);
                    yield return HelperMethods.ChangeCurrentView(View.Hand, 0f);
                    yield return CardDrawPiles3D.Instance.DrawCardFromDeck(oneSin);
                    yield return new WaitForSeconds(0.5f);
                }
                
                if (!SaviourBossUtils.PlayerHasHeretic)
                {
                    if (PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == oneSinName))
                    {
                        LobotomyPlugin.Log.LogDebug("One Sin is in the player's hand");
                        foreach (PlayableCard card in PlayerHand.Instance.CardsInHand.Where(c => c.Info.name == oneSinName))
                        {
                            if (!SaviourBossUtils.PlayerHasHeretic)
                            {
                                yield return card.TransformIntoCardInHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                                SaviourBossUtils.PlayerHasHeretic = true;
                            }
                            else
                                card.DieTriggerless();
                        }
                    }
                    else
                    {
                        LobotomyPlugin.Log.LogDebug("Forcing One Sin into the hand");
                        yield return HelperMethods.ChangeCurrentView(View.Hand, 0f);
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        yield return new WaitForSeconds(0.5f);
                        SaviourBossUtils.PlayerHasHeretic = true;
                    }
                }
            }

            // for future use
            LobotomySaveManager.TriggeredWhiteNightThisBattle = true;
            LobotomyConfigManager.Instance.SetHasSeenHim();

            yield return new WaitForSeconds(0.2f);
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }
    }
    public class RulebookEntryBless : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Bless()
            => RulebookEntryBless.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryBless>(Bless.rName, Bless.rDesc).Id;
        private void SpecialAbility_Bless()
            => Bless.specialAbility = AbilityHelper.CreateSpecialAbility<Bless>(pluginGuid, Bless.rName).Id;
    }
}
