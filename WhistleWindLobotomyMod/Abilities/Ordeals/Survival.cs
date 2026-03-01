using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSurvival() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Survival";
            info.rulebookDescription = "At the start of every other turn for the owner, this card creates a Food Chain in empty adjacent spaces. [define:wstl_foodChain]";
            info.powerLevel = 4;
            Survival.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Survival), TextureLoader.LoadTextureFromFile("sigilSurvival.png")).Id;
        }
    }

    public class Survival : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        private int turnsOnBoard;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) {
            return otherCard == base.Card;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            yield return base.PreSuccessfulTriggerSequence();
            turnsOnBoard = Mathf.Max(1, 4 - RunState.CurrentRegionTier - RunState.Run.DifficultyModifier);
            LobotomyPlugin.Log.LogDebug($"[Survival] Assigned to slot, turnsOnBoard = {turnsOnBoard}");
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd) {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            turnsOnBoard--;
            if (turnsOnBoard == 0) {
                LobotomyPlugin.Log.LogDebug("[Survival] End Turns On Board");
                yield return base.PreSuccessfulTriggerSequence();
                yield return HelperMethods.ChangeCurrentView(View.Board);

                
                foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy) {
                    if (slot.Card == base.Card) {
                        slot.Card = null;
                    }
                }
                LobotomyPlugin.Log.LogDebug("[Survival] Move below board");
                yield return MoveEternalMealBelowBoard(base.Card, base.Card.Slot);
                LobotomyPlugin.Log.LogDebug("[Survival] Summon worms");
                yield return SummonCards();
            }

        }

        private IEnumerator MoveEternalMealBelowBoard(PlayableCard card, CardSlot anchorSlot) {
            base.Card.Slot = null;
            AudioController.Instance.PlaySound3D("giant_stones_falling", MixerGroup.CardPaperSFX, card.transform.position);
            card.transform.parent = anchorSlot.transform;
            card.transform.rotation = anchorSlot.transform.GetChild(0).rotation;
            Tween.LocalPosition(card.transform, new Vector3(0.7f, -0.025f, 1.05f), 0.3f, 0.05f, Tween.EaseOut, Tween.LoopType.None, null, card.Anim.PlayRiffleSound);
            yield return OrdealUtils.ShakeBoard();
        }

        private IEnumerator SummonCards() {
            int rand = base.GetRandomSeed();
            AudioController.Instance.PlaySound3D("goovoice_calm#2", MixerGroup.CardPaperSFX, base.transform.position);
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(Cards.foodChain), BoardManager.Instance.GetOpponentOpenSlots());
            yield return CombatHelpers.CreateCardInRandomSlot(CardLoader.GetCardByName(Cards.foodChain), BoardManager.Instance.GetOpponentOpenSlots());
            yield return new WaitForSeconds(0.5f);
        }
    }
}
