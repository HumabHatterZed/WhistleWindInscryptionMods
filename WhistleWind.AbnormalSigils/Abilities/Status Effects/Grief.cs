using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Rulebook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Grief : ModifyOnUpkeepStatusEffectBehaviour, IPassiveAttackBuff
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => 1;

        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (target.OnBoard && base.PlayableCard == target)
            {
                return -EffectPotency;
            }
            return 0;
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.PlayableCard.OpponentCard && otherCard.HasStatusEffect<Pebble>();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return base.RemoveFromCard(true);
            yield return new WaitForSeconds(0.3f);
        }
        public override bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            return target.OpponentCard == base.PlayableCard.OpponentCard && statusEffect.StatusEffect == Pebble.specialAbility;
        }
        public override IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            yield return this.OnOtherCardResolve(null);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Grief()
        {
            const string rName = "Grief";
            const string rDesc = "A card bearing this effect loses Power equal to its Grief. If there is an ally card with Pebble, remove this effect. Otherwise, gain 1 Grief at the start of the owner's turn.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Grief>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilGrief.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilGrief_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Grief.specialAbility = data.Id;
            Grief.iconId = data.IconInfo.ability;
            data.IconInfo.SetAbilityRedirect("Pebble", Pebble.iconId, GameColors.Instance.gray);
            Pebble.data.IconInfo.SetAbilityRedirect("Grief", Grief.iconId, new(0.25f, 0.25f, 0.25f));
        }
    }
}
