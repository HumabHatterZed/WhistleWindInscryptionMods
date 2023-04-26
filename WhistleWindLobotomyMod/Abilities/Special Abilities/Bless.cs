﻿using DiskCardGame;
using InscryptionAPI.Card;
using Pixelplacement;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
            if (LobotomyConfigManager.Instance.NoEvents)
                yield break;

            base.PlayableCard.Anim.LightNegationEffect();
            LobotomyConfigManager.Instance.UpdateBlessings(1);
            UpdatePortrait();
            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator TriggerClock() => CheckTheClock();

        private IEnumerator CheckTheClock()
        {
            LobotomyPlugin.Log.LogDebug("Checking the Clock");

            if (LobotomyConfigManager.Instance.NoEvents)
                yield break;

            // if in range [0, 12)
            if (0 <= LobotomyConfigManager.Instance.NumOfBlessings && LobotomyConfigManager.Instance.NumOfBlessings < 12)
                yield break;

            LobotomyPlugin.Log.LogDebug("Clock has struck twelve");

            if (Singleton<BoardManager>.Instance.AllSlotsCopy.Exists(x => x.Card != null && x.Card.Info.name == "wstl_whiteNight"))
            {
                yield return base.PlayableCard.DieTriggerless();
                yield return new WaitForSeconds(0.5f);
                yield return HelperMethods.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]You shall have no other gods before me.[c:]");
                yield break;
            }

            yield return new WaitForSeconds(0.5f);

            // If blessings are in the negatives (aka someone altered the config value), wag a finger and go 'nuh-uh-uh!'
            if (LobotomyConfigManager.Instance.NumOfBlessings < 0)
                yield return HelperMethods.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou cannot stop my ascension. Even the [c:]tutelary[c:bR] bows to my authority.[c:]");

            // Change Leshy's eyes to red
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));


            LobotomyPlugin.Log.LogDebug("Transforming into WhiteNight");

            // need to do this or it breaks later
            bool isOpponent = base.PlayableCard.OpponentCard;
            CardSlot baseSlot = base.PlayableCard.Slot;

            // Transform the Doctor into Him
            yield return base.PlayableCard.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            base.PlayableCard.Status.hiddenAbilities.Add(Ability.Flying);
            base.PlayableCard.AddTemporaryMod(new CardModificationInfo(Ability.Flying));

            yield return new WaitForSeconds(0.5f);


            LobotomyPlugin.Log.LogDebug("Resetting Plague Doctor's portrait");

            // reset the Doctor's sprites
            CardLoader.GetCardByName("wstl_plagueDoctor").SetPortrait(TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor), TextureLoader.LoadTextureFromBytes(Artwork.plagueDoctor_emission));

            // Play dialogue depending on whether this is the first time this has happened this run
            if (!LobotomySaveManager.TriggeredWhiteNightThisRun)
            {
                LobotomySaveManager.TriggeredWhiteNightThisRun = true;
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightEventIntro");
            }
            else
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventIntroRepeat, speaker: DialogueEvent.Speaker.Bonelord);

            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventIntro3, speaker: DialogueEvent.Speaker.Bonelord);
            yield return new WaitForSeconds(0.2f);

            LobotomyPlugin.Log.LogDebug("Determining whether player owns One Sin / has the Heretic");

            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            bool sinful = RunState.DeckList.FindAll(info => info.name == "wstl_oneSin").Count > 0;
            bool HasHeretic = Singleton<PlayerHand>.Instance.CardsInHand.FindAll(c => c.Info.name == "wstl_apostleHeretic").Count != 0 ||
                Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s.Card != null && s.Card.Info.name == "wstl_apostleHeretic").Count != 0;


            LobotomyPlugin.Log.LogDebug("Transforming cards");

            // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(isOpponent).Where(slot => slot.Card != null && slot != baseSlot))
            {
                if (slot.Card.Info.name != "wstl_oneSin")
                    yield return ConvertToApostle(slot.Card, HasHeretic);
            }
            // If the player has One Sin
            if (sinful)
            {
                LobotomyPlugin.Log.LogDebug("Player has One Sin");
                yield return new WaitForSeconds(0.5f);

                // if there is a One Sin on the board
                if (HelperMethods.GetSlotsCopy(false).FindAll(slot => slot.Card != null && slot.Card.Info.name == "wstl_oneSin").Count > 0)
                {
                    LobotomyPlugin.Log.LogDebug("One Sin is on the board");
                    foreach (CardSlot slot in HelperMethods.GetSlotsCopy(false).Where(s => s.Card != null && s.Card.Info.name == "wstl_oneSin"))
                    {
                        // Transform the first One Sin into Heretic
                        // Remove the rest
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
                            yield return slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        }
                        else
                            slot.Card.RemoveFromBoard();
                    }
                }
                else
                {
                    LobotomyPlugin.Log.LogDebug("Heretic is in the player's hand");
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
                            card.DieTriggerless();
                    }

                }
                // Spawn card to hand if One Sin is in the deck or dead
                if (!HasHeretic)
                {
                    LobotomyPlugin.Log.LogDebug("Forcing One Sin into the hand");
                    HasHeretic = true;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        private IEnumerator ConvertToApostle(PlayableCard otherCard, bool HasHeretic)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            if (otherCard.HasAnyOfTraits(Trait.Pelt, Trait.Terrain))
            {
                yield return otherCard.DieTriggerless();
                yield break;
            }

            bool isOpponent = otherCard.OpponentCard;

            if (otherCard.HasSpecialAbility(SpecialTriggeredAbility.PackMule))
            {
                Tween.LocalPosition(otherCard.transform, Vector3.up * (Singleton<BoardManager>.Instance.SlotHeightOffset), 0.1f, 0.05f, Tween.EaseOut, Tween.LoopType.None);
                Tween.Rotation(otherCard.transform, otherCard.Slot.transform.GetChild(0).rotation, 0.1f, 0f, Tween.EaseOut);

                if (otherCard.TriggerHandler.RespondsToTrigger(Trigger.Die, false, null))
                    yield return otherCard.TriggerHandler.OnTrigger(Trigger.Die, false, null);

                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            }

            int randomSeed = base.GetRandomSeed();
            CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
            {
                0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                _ => CardLoader.GetCardByName("wstl_apostleStaff")
            };
            if (!HasHeretic)
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

            if (HasHeretic)
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleHeretic");

            if (Singleton<ViewManager>.Instance.CurrentView == View.Hand)
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);

            yield return new WaitForSeconds(0.2f);
            LobotomySaveManager.UnlockedWhiteNight = true;
        }


        public override bool RespondsToDrawn() => true;
        public override void OnShownInDeckReview() => UpdatePortrait();
        public override void OnShownForCardChoiceNode() => UpdatePortrait();
        public override IEnumerator OnDrawn()
        {
            this.DisguiseInBattle();
            yield break;
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
        private void DisguiseInBattle()
        {
            this.UpdatePortrait();
            if (LobotomyConfigManager.Instance.NumOfBlessings >= 11)
                base.PlayableCard.ApplyAppearanceBehaviours(new() { ForcedWhiteEmission.appearance });
        }
        private void UpdatePortrait()
        {
            LobotomyPlugin.Log.LogDebug("Trying to update portrait");
            if (base.PlayableCard != null && base.PlayableCard.Info.name == "wstl_plagueDoctor")
            {
                CardInfo newInfo = (CardInfo)base.PlayableCard.Info.Clone();
                byte[][] portraits = LobotomyPlugin.UpdatePlagueSprites();

                newInfo.SetPortrait(
                    TextureLoader.LoadTextureFromBytes(portraits[0]), TextureLoader.LoadTextureFromBytes(portraits[1])
                    ).SetPixelPortrait(TextureLoader.LoadTextureFromBytes(portraits[2]));
                base.PlayableCard.SetInfo(newInfo);
            }
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
