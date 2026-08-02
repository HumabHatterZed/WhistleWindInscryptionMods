using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class PanicBlood : PanicBase {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            int seed = base.GetRandomSeed();
            List<CardSlot> slots = new();
            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(card.Slot);
            for (int i = 0; i < attackCount; i++) {
                slots.Add(adjacentSlots.GetSeededRandom(seed++));
            }
            return slots;
        }
    }

    public partial class AbnormalPlugin {
        private void Panic_Blood() {
            const string rName = "Murderous";
            const string rDesc = "A card in this state will strike adjacent spaces during combat. At the end of its owner's turn, remove all Sinking from this card and stop Panicking.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PanicBlood>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.glowRed,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            PanicBlood.specialAbility = data.Id;
            PanicBlood.iconId = data.IconInfo.ability;
        }
    }
}
