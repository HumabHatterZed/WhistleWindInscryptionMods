using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public class Bless : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Bless";
        public static readonly string rDesc = "Upon successfully healing a card using the Healer ability, Plague Doctor changes its appearance.";

        private bool HasHeretic =
            new List<PlayableCard>(Singleton<PlayerHand>.Instance.CardsInHand).FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count != 0
            || new List<CardSlot>(Singleton<BoardManager>.Instance.AllSlotsCopy.Where((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_apostleHeretic")).Count != 0;

        private readonly string eventIntro3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private readonly string eventIntroRepeat = "[c:bR]The time has come again. I will be thy guide.[c:]";

        private IEnumerator CheckTheClock()
        {
            // If Blessings are between (0,11), break
            if (0 < ConfigManager.Instance.NumOfBlessings && ConfigManager.Instance.NumOfBlessings < 12)
                yield break;

            yield return new WaitForSeconds(0.5f);

            // If blessings are in the negatives (aka someone altered tge config value), wag a finger and go 'nuh-uh-uh!'
            if (ConfigManager.Instance.NumOfBlessings < 0)
                yield return HelperMethods.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou cannot stop my ascension. Even the [c:]tutelary[c:bR] bows to my authority.[c:]");

            // Change Leshy's eyes to red
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));

            // Transform the Doctor into Him
            yield return base.PlayableCard.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            base.PlayableCard.Status.hiddenAbilities.Add(Ability.Flying);
            base.PlayableCard.AddTemporaryMod(new CardModificationInfo(Ability.Flying));
            yield return new WaitForSeconds(0.5f);

            // reset the Doctor's sprites
            CardLoader.GetCardByName("wstl_plagueDoctor").SetPortrait(TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor), TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor_emission));

            // Play dialogue depending on whether this is the first time this has happened this run
            if (!WstlSaveManager.TriggeredWhiteNightThisRun)
            {
                WstlSaveManager.TriggeredWhiteNightThisRun = true;
                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightEventIntro");
            }
            else
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventIntroRepeat, speaker: DialogueEvent.Speaker.Bonelord);

            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventIntro3, speaker: DialogueEvent.Speaker.Bonelord);
            yield return new WaitForSeconds(0.2f);

            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            bool sinful = new List<CardInfo>(RunState.DeckList).FindAll((CardInfo info) => info.name == "wstl_oneSin").Count() > 0;

            // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.PlayableCard.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                    yield return ConvertToApostle(slot.Card, sinful);
            }
            // If the player has One Sin
            if (sinful)
            {
                yield return new WaitForSeconds(0.5f);
                // if there is a One Sin on the board
                if (Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot slot) => slot.Card != null && slot.Card.Info.name == "wstl_oneSin").Count > 0)
                {
                    foreach (CardSlot slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where(s => s.Card != null && s.Card.Info.name == "wstl_oneSin"))
                    {
                        // Transform the first One Sin into Heretic
                        // Remove the rest
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
                            yield return slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        }
                        else
                        {
                            slot.Card.Dead = true;
                            slot.Card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = slot.Card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                                components[i].OnCleanUp();

                            slot.Card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }
                }
                else
                {
                    // Transform into Heretic
                    yield return new WaitForSeconds(0.25f);
                    foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_oneSin"))
                    {
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
                            yield return card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                            yield return new WaitForSeconds(0.5f);
                        }
                        else
                        {
                            card.Dead = true;
                            card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }

                }
                // Spawn card to hand if One Sin is in the deck or dead
                if (!HasHeretic)
                {
                    HasHeretic = true;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        // check that blessing and/or clock is being triggered
        public override bool RespondsToAttackEnded() => base.PlayableCard.GetComponent<Healer>().TriggerBless || base.PlayableCard.GetComponent<Healer>().TriggerClock;

        public override IEnumerator OnAttackEnded()
        {
            // Trigger increasing blessing and clock if applicable
            if (base.PlayableCard.GetComponent<Healer>().TriggerBless)
            {
                base.PlayableCard.Anim.LightNegationEffect();
                ConfigManager.Instance.UpdateBlessings(1);
                UpdatePortrait();
                yield return new WaitForSeconds(0.2f);
            }
            if (base.PlayableCard.GetComponent<Healer>().TriggerClock)
                yield return CheckTheClock();
        }

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn()
        {
            this.DisguiseInBattle();
            yield break;
        }
        private void DisguiseInBattle()
        {
            this.UpdatePortrait();
            if (ConfigManager.Instance.NumOfBlessings >= 11)
                base.PlayableCard.ApplyAppearanceBehaviours(new() { ForcedWhite.appearance });
        }

        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            this.UpdatePortrait();
            yield break;
        }
        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.UpdatePortrait();
            yield break;
        }
        public override void OnShownInDeckReview()
        {
            this.UpdatePortrait();
        }
        public override void OnShownForCardChoiceNode()
        {
            this.UpdatePortrait();
        }

        public IEnumerator ConvertToApostle(PlayableCard otherCard, bool HasOneSin = false)
        {
            bool isOpponent = otherCard.OpponentCard;
            // null check should be done elsewhere
            if (otherCard.HasAnyOfTraits(Trait.Pelt, Trait.Terrain) || otherCard.HasSpecialAbility(SpecialTriggeredAbility.PackMule))
            {
                yield return otherCard.DieTriggerless();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            }
            else
            {
                int randomSeed = base.GetRandomSeed();
                CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
                {
                    0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                    1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                    _ => CardLoader.GetCardByName("wstl_apostleStaff")
                };
                if (!HasHeretic && !HasOneSin)
                {
                    if (new System.Random().Next(0, 12) == 0)
                    {
                        HasHeretic = true;
                        if (!isOpponent)
                            randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
                        else
                        {
                            otherCard.RemoveFromBoard();
                            yield return new WaitForSeconds(0.5f);
                            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                            {
                                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                                yield return new WaitForSeconds(0.2f);
                            }
                            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"), null, 0.25f, null);
                            yield return new WaitForSeconds(0.45f);
                        }
                    }
                }
                if (otherCard != null)
                    yield return otherCard.TransformIntoCard(randApostle);

                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleHeretic");

                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.2f);
            }
        }
        private void UpdatePortrait()
        {
            Texture2D portrait;
            Texture2D emissive;

            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11);
                    emissive = TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor11_emission);
                    base.Card.ApplyAppearanceBehaviours(new() { ForcedWhite.appearance });
                    break;
            }
            base.Card.ClearAppearanceBehaviours();
            base.Card.Info.SetPortrait(portrait, emissive);
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
        {
            RulebookEntryBless.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryBless>(Bless.rName, Bless.rDesc).Id;
        }
        private void SpecialAbility_Bless()
        {
            Bless.specialAbility = AbilityHelper.CreateSpecialAbility<Bless>(pluginGuid, Bless.rName).Id;
        }
    }
}
