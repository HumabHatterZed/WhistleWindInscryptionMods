using DiskCardGame;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using System.Reflection;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Enchanted : ModifyOnUpkeepStatusEffectBehaviour, IModifyDamageTaken, ISetupAttackSequence
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override Ability IconAbility => iconId;

        private bool IsEnchanted => EffectPotency > 0;
        public override int PotencyModification => -1;

        public override List<string> EffectDecalIds() => new();

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return IsEnchanted && attacker == base.Card && target != null && target.HasAbility(Dazzling.ability);
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => 0;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => int.MinValue;

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return IsEnchanted && card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            List<CardSlot> slots = BoardManager.Instance.AllSlotsCopy.FindAll(x => x.Card != null && x.Card.HasAbility(Dazzling.ability));

            if (slots.Count <= 1)
                return slots;

            slots.Randomize();
            slots.RemoveRange(1, slots.Count - 1);
            return slots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return int.MinValue;
        }
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Enchanted()
        {
            const string rName = "Enchanted";
            const string rDesc = "This card will only target Dazzling cards, and will perish when striking one. At the start of the owner's turn, lose 1 Potency.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Enchanted>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.gold,
                TextureLoader.LoadTextureFromFile("sigilEnchanted.png", ModAssembly),
                TextureLoader.LoadTextureFromFile("sigilEnchanted_pixel.png", ModAssembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            data.IconInfo.SetAbilityRedirect("Dazzling", Dazzling.ability, GameColors.Instance.gold);
            Enchanted.specialAbility = data.Id;
            Enchanted.iconId = data.IconInfo.ability;
        }
    }
}
