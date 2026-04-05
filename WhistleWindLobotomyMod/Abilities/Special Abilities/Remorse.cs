using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class Remorse : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Remorse";
        public const string rDesc = "When Silent Girl is played, create a Nail and Hammer in the adjacent left and right spaces respectively if they are empty.";

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;

            if (toLeftValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft, "wstl_nail");
            }
            if (toRightValid) {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight, "wstl_hammer");
            }
            if (toLeftValid || toRightValid) {
                yield return DialogueManager.PlayDialogueEventSafe("SilentGirlResolve", TextDisplayer.MessageAdvanceMode.Input);
            }
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot, string alternateSpawnCardId) {
            CardInfo cardByName = CardLoader.GetCardByName(alternateSpawnCardId);
            this.ModifySpawnedCard(cardByName);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }

        private void ModifySpawnedCard(CardInfo card) {
            StatusEffectPatches.ModifySpawnedCardStatusEffects(this.PlayableCard, card);
        }
    }

    public class RulebookEntryRemorse : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_Remorse() => RulebookEntryRemorse.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryRemorse>(Remorse.rName, Remorse.rDesc).Id;
        private static void AddSpecial_Remorse() {
            Remorse.specialAbility = AbilityHelper.CreateSpecialAbility<Remorse>(LobotomyPlugin.pluginGuid, Remorse.rName).Id;
        }
    }
}
