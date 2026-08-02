using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class PanicMox : PanicBase, IOnPostSlotAttackSequence {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            return new();
        }

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) {
            return attackingSlot == base.PlayableCard.Slot && !attackingSlot.Card.HasAbility(Unyielding.ID) && !attackingSlot.Card.HasTrait(Trait.Giant);
        }
        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot) {
            yield return new WaitForSeconds(0.5f);
            yield return ReturnCard.RecallCard(attackingSlot, 0.5f, false);
        }
    }

    public partial class AbnormalPlugin {
        private void Panic_Mox() {
            const string rName = "Flighty";
            const string rDesc = "A card in this state will not attack during combat, instead retreating to the owner's hand. At the end of its owner's turn, remove all Sinking from this card and stop Panicking.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PanicMox>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.blue,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            PanicMox.specialAbility = data.Id;
            PanicMox.iconId = data.IconInfo.ability;
        }
    }
}
