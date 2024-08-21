using DiskCardGame;
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

        public override bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            return target.OpponentCard == base.PlayableCard.OpponentCard && statusEffect.StatusEffect == Pebble.specialAbility;
        }
        public override IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus)
        {
            base.PlayableCard.Anim.StrongNegationEffect();
            base.DestroyStatusEffect();
            yield return new WaitForSeconds(0.3f);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Grief()
        {
            const string rName = "Grief";
            const string rDesc = "This card loses Power equal to its Grief. At the start of the owner's turn, if there is no allied card with Pebble, gain 1 Grief. Otherwise, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Grief>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilGrief.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilGrief_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Grief.specialAbility = data.Id;
            Grief.iconId = data.IconInfo.ability;
        }
    }
}
