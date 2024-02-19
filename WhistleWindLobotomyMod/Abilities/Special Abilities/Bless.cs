using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
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

        internal const string rName = "Bless";
        internal const string rDesc = "Whenever Plague Doctor successfully heals another card using the Healer sigil, it changes its appearance.";

        private const string eventIntro3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private const string eventIntroRepeat = "[c:bR]The time has come again. I will be thy guide.[c:]";

        private IEnumerator CheckTheClock()
        {
            int blessings = SaviourBossUtils.Blessings(base.PlayableCard);
            if (LobotomyConfigManager.Instance.NoEvents || (blessings >= 0 && blessings < 12)) // [0, 12)
                yield break;

            if (LobotomySaveManager.TriggeredWhiteNightThisBattle || BoardManager.Instance.CardsOnBoard.Exists(x => x.HasAbility(TrueSaviour.ability)))
            {
                yield return base.PlayableCard.DieTriggerless();
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou shalt have no other gods before me.[c:]");
                yield break;
            }

            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);
            yield return new WaitForSeconds(0.5f);

            // Change Leshy's eyes to red
            if (SaveManager.SaveFile.IsPart1)
                LeshyAnimationController.Instance?.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));

            // Negative blessing values will forcefully trigger the event
            if (LobotomyConfigManager.Instance.NumOfBlessings < 0)
                yield return DialogueHelper.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou cannot stop my ascension.[c:]");

            LobotomyPlugin.Log.LogDebug("Transforming into WhiteNight");

            // need to do this or it breaks later
            bool isOpponent = base.PlayableCard.OpponentCard;
            CardSlot baseSlot = base.PlayableCard.Slot;

            // Transform the Doctor into Him
            yield return base.PlayableCard.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"), () => base.PlayableCard.Status.damageTaken = 0);
            MiracleWorkerAppearance app = base.PlayableCard.GetComponent<MiracleWorkerAppearance>();
            if (app != null)
            {
                app.ResetAppearance();
                DestroyImmediate(app);
            }
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

            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            bool playerHasOneSin;
            if (SaveManager.SaveFile.IsPart2)
                playerHasOneSin = SaveManager.SaveFile.gbcData.deck.Cards.Exists(info => info.name == SaviourBossUtils.ONESIN_NAME);
            else
                playerHasOneSin = RunState.DeckList.Exists(info => info.name == SaviourBossUtils.ONESIN_NAME);

            LobotomyPlugin.Log.LogDebug("Creating Apostles");
            yield return SaviourBossUtils.ConvertCardsOnBoard(!isOpponent, baseSlot.Card, base.GetRandomSeed());

            if (!SaviourBossUtils.PlayerHasHeretic && playerHasOneSin)
            {
                LobotomyPlugin.Log.LogDebug("Player has One Sin");
                yield return new WaitForSeconds(0.5f);

                List<PlayableCard> opposingCards = BoardManager.Instance.GetPlayerCards(x => x.Info.name == SaviourBossUtils.ONESIN_NAME);
                if (opposingCards.Count > 0)
                {
                    LobotomyPlugin.Log.LogDebug("One Sin is on the board");
                    yield return opposingCards[0].TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
                else if (CardDrawPiles3D.Instance.Deck.cards.Exists(x => x.name == SaviourBossUtils.ONESIN_NAME))
                {
                    LobotomyPlugin.Log.LogDebug("One Sin is in the deck");
                    CardInfo oneSin = CardDrawPiles3D.Instance.Deck.cards.Find(x => x.name == SaviourBossUtils.ONESIN_NAME);
                    yield return HelperMethods.ChangeCurrentView(View.Hand, 0f);
                    yield return CardDrawPiles3D.Instance.DrawCardFromDeck(oneSin);
                    yield return new WaitForSeconds(0.5f);
                }

                // if player still doesn't have the Heretic
                if (!SaviourBossUtils.PlayerHasHeretic)
                {
                    if (PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == SaviourBossUtils.ONESIN_NAME))
                    {
                        LobotomyPlugin.Log.LogDebug("One Sin is in the player's hand");
                        yield return PlayerHand.Instance.CardsInHand.Find(x => x.name == SaviourBossUtils.ONESIN_NAME).TransformIntoCardAboveHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                    }
                    else
                    {
                        LobotomyPlugin.Log.LogDebug("Forcing Heretic into the hand");
                        yield return HelperMethods.ChangeCurrentView(View.Hand, 0f);
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }

            // for future use
            LobotomySaveManager.TriggeredWhiteNightThisBattle = true;
            LobotomyConfigManager.Instance.SetHasSeenHim();

            yield return new WaitForSeconds(0.2f);
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }

        public override IEnumerator TriggerClock() => CheckTheClock();
        public override IEnumerator TriggerBlessing()
        {
            if (LobotomyConfigManager.Instance.NoEvents || LobotomySaveManager.TriggeredWhiteNightThisBattle)
                yield break;

            SaviourBossUtils.UpdateBlessings(base.PlayableCard, 1);

            yield return HelperMethods.ChangeCurrentView(View.Board);

            AudioController.Instance.PlaySound2D("bird_laser_fire", MixerGroup.TableObjectsSFX, 0.4f);
            base.PlayableCard.Anim.StrongNegationEffect();
            base.PlayableCard.UpdateAppearanceBehaviours();
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
