using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Shaver() {
            const string rulebookName = "Hair Loss Serum";
            const string rulebookDescription = "Choose one of your cards and remove all its sigils, then double its Power and Health.";
            const string dialogue = "Bald is beautiful.";
            Shaver.ability = AbnormalAbilityHelper.CreateAbility<Shaver>(
                "sigilShaver",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// Choose one of your cards and remove all its sigils then double its Power and Health.
    /// </summary>
    [HarmonyPatch]
    public class Shaver : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard() => base.Card.Info.IsGlobalSpell();
        public override IEnumerator OnResolveOnBoard() {
            List<CardSlot> slots = BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard);
            foreach (CardSlot slot in slots.Where(CheckValid)) {
                CardModificationInfo mod = new(slot.Card.Attack, slot.Card.Health);
                mod.AddNegateAbilities(slot.Card.AllAbilities().ToArray());
                yield return HelperMethods.ChangeCurrentView(View.Board);
                slot.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.15f);
                slot.Card.AddTemporaryMod(mod);
            }
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) => base.Card == attacker && CheckValid(slot);
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker) {
            CardModificationInfo mod = new(slot.Card.Attack, slot.Card.Health);
            mod.AddNegateAbilities(slot.Card.AllAbilities().ToArray());

            yield return HelperMethods.ChangeCurrentView(View.Board);
            slot.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            slot.Card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.4f);
            yield return base.LearnAbility();
        }

        private bool CheckValid(CardSlot target) {
            if (target.IsOpponentSlot() == base.Card.OpponentCard && target.Card != null && target.Card.LacksAbility(Bleachproof.ability)) {
                return target.Card.LacksAllTraits(Trait.Giant, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath) && target.Card.AllAbilities().Count > 0;
            }
            return false;
        }
    }
}
