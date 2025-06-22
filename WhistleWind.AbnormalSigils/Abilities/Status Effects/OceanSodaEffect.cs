using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class OceanSodaEffect : SodaEffectBehaviour {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override string SingletonId => OceanSoda.id;
        public override Ability AbilityToAdd => OceanSoda.abilityToAdd;
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_OceanSodaEffect() {
            const string rName = "Ocean Soda";
            const string rDesc = "A card bearing this effect is Waterborne. At the end of the owner's turn, reduce this effect's Potency by 1.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<OceanSodaEffect>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.seafoam,
                TextureLoader.LoadTextureFromFile("sigilOceanSoda.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilOceanSoda_2_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            OceanSodaEffect.specialAbility = data.Id;
            OceanSodaEffect.iconId = data.IconInfo.ability;
        }
    }
}
