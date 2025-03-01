using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class FizzyLifterEffect : SodaEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override string SingletonId => FizzyLifter.id;
        public override Ability AbilityToAdd => FizzyLifter.abilityToAdd;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_FizzyLifterEffect()
        {
            const string rName = "Fizzy Lifted";
            const string rDesc = "A card bearing this effect is Airborne. At the end of the owner's turn, reduce this effect's Potency by 1.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<FizzyLifterEffect>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.seafoam,
                TextureLoader.LoadTextureFromFile("sigilFizzyLifter.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilFizzyLifter_2_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            FizzyLifterEffect.specialAbility = data.Id;
            FizzyLifterEffect.iconId = data.IconInfo.ability;
        }
    }
}
