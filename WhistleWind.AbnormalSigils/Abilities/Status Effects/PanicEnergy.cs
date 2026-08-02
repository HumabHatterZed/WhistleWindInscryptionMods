using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class PanicEnergy : PanicBase, IPassiveAttackBuff {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int Priority => -9000;
        public override List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            return new();
        }

        public int GetPassiveAttackBuff(PlayableCard target) {
            if (BoardManager.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Contains(target.Slot)) {
                return -1;
            }
            return 0;
        }
    }

    public partial class AbnormalPlugin {
        private void Panic_Energy() {
            const string rName = "Languishing";
            const string rDesc = "A card in this state will not attack during combat, instead reducing adjacent cards' Power by 1. At the end of its owner's turn, remove all Sinking from this card and stop Panicking.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PanicEnergy>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.darkPurple,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            PanicEnergy.specialAbility = data.Id;
            PanicEnergy.iconId = data.IconInfo.ability;
        }
    }
}
