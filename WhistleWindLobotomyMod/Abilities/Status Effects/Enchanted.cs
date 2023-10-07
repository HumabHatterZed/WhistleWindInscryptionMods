using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Enchanted : StatusEffectBehaviour, IGetOpposingSlots, IOnUpkeepInHand
    {
        public static SpecialTriggeredAbility specialAbility;
        public override string CardModSingletonName => "enchanted";

        private bool IsEnchanted => EffectSeverity > 1;
        public override List<string> EffectDecalIds() => new();
        public int TurnGained = -1;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public bool RespondsToUpkeepInHand(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (TurnManager.Instance.TurnNumber == TurnGained)
                yield break;

            UpdateStatusEffectCount(-EffectSeverity, false);
        }
        public IEnumerator OnUpkeepInHand(bool playerUpkeep)
        {
            if (TurnManager.Instance.TurnNumber == TurnGained)
                yield break;

            UpdateStatusEffectCount(-EffectSeverity, false);
        }

        public bool RemoveDefaultAttackSlot() => IsEnchanted;
        public bool RespondsToGetOpposingSlots() => IsEnchanted;

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            otherAddedSlots.Clear();
            List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy.FindAll(x => x.Card != null && x.Card.HasAbility(Dazzling.ability));

            if (slots.Count <= 1)
                return slots;

            slots.Randomize();
            slots.RemoveRange(1, slots.Count - 1);
            return slots;
        }
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Enchanted()
        {
            const string rName = "Enchanted";
            const string rDesc = "This card will only attack cards with the Dazzling ability. Lose this effect at the start of the owner's next turn.";

            Enchanted.specialAbility = StatusEffectManager.NewStatusEffect<Enchanted>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilEnchanted",
                powerLevel: -3, iconColour: GameColors.Instance.gold,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
