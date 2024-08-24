using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Sinking : StatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override bool RespondsToTakeDamage(PlayableCard source) => true;

        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            base.PlayableCard.HealDamage(-EffectPotency);
            base.DestroyStatusEffect();
            if (base.PlayableCard.Health >= 0)
            {
                yield return base.PlayableCard.Die(false, source);
            }
            yield break;
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Sinking()
        {
            const string rName = "Sinking";
            const string rDesc = "This card loses Power equal to its Sinking. When this card is struck, take additional damage equal to its Sinking then remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Sinking>(
                pluginGuid, rName, rDesc, 2, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilSinking.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilSinking_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Sinking.specialAbility = data.Id;
            Sinking.iconId = data.IconInfo.ability;
        }
    }
}
