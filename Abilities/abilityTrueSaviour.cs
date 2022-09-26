using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            string rulebookDescription = ConfigManager.Instance.RevealWhiteNight ? "Cannot die. Transforms non-Terrain and non-Pelt cards into Apostles." : "My story is nowhere, unknown to all.";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ability = AbilityHelper.CreateAbility<TrueSaviour>(
                Resources.sigilTrueSaviour, Resources.sigilTrueSaviour_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class TrueSaviour : BaseDoctor
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string oneSinDialogue = "[c:bR]What are you doing?[c:]";
        private readonly string killedDialogue = "[c:bR]Do not deny me.[c:]";
        private readonly string hammerDialogue = "[c:bR]I shall not leave thee until I have completed my mission.[c:]";

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // Return owner's turn and whether the player has Heretic in their hand
            return base.Card.OpponentCard != playerUpkeep && Singleton<PlayerHand>.Instance.CardsInHand.FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count() != 0;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return MakeRoomForOneSin();
        }

        public override bool RespondsToResolveOnBoard()
        {
            return base.Card.OpponentCard;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            foreach (var slot in Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(slot => slot.Card != base.Card))
            {
                // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                {
                    yield return base.ConvertToApostle(slot.Card);
                }
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null && otherCard != base.Card)
            {
                if (!otherCard.Info.name.StartsWith("wstl_apostle") && otherCard.Info.name != "wstl_oneSins" && otherCard.Info.name != "wstl_hundredsGoodDeeds")
                {
                    return base.Card.OnBoard && base.Card.OpponentCard == otherCard.OpponentCard;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.ConvertToApostle(otherCard);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
            {
                yield return KilledByNonNull(killer);
            }
            else
            {
                yield return KilledByNull();
            }
        }

        public IEnumerator MakeRoomForOneSin()
        {
            // If all slots on the owner's side are full
            yield return new WaitForSeconds(0.2f);
            if (Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(s => s.Card != null).Count() == 4)
            {
                int randomSeed = base.GetRandomSeed();
                List<CardSlot> cardsToKill = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).FindAll((CardSlot s) => s.Card != null && s.Card != base.Card);
                PlayableCard cardToKill = cardsToKill[SeededRandom.Range(0, 3, randomSeed++)].Card;
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_apostleHeretic"))
                {
                    card.Anim.StrongNegationEffect();
                }
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered);
                cardToKill.Anim.SetShaking(true);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(oneSinDialogue, -0.65f, 0.4f, emotion: Emotion.Curious, speaker: DialogueEvent.Speaker.Bonelord);
                yield return cardToKill.Die(false, base.Card);
            }
        }
        public IEnumerator KilledByNonNull(PlayableCard killer)
        {
            // If killed by Heretic, die and break
            if (killer.Info.name == "wstl_apostleHeretic")
            {
                AudioController.Instance.PlaySound2D("mycologist_scream");
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);
                yield break;
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
            yield return new WaitForSeconds(0.2f);
            if (!WstlSaveManager.WhiteNightKilled)
            {
                WstlSaveManager.WhiteNightKilled = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                yield return new WaitForSeconds(0.2f);
            }
            yield return killer.DieTriggerless();
        }
        public IEnumerator KilledByNull()
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
            if (!WstlSaveManager.WhiteNightHammer)
            {
                yield return new WaitForSeconds(0.2f);
                WstlSaveManager.WhiteNightHammer = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammerDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                yield return new WaitForSeconds(0.2f);

            }
            yield return new WaitForSeconds(0.2f);
            yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase += 1;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
        }
    }
}
