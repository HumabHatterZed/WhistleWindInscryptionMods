using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class SurefireDrinkEffect : SodaEffectBehaviour {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override string SingletonId => SurefireDrink.id;
        public override Ability AbilityToAdd => SurefireDrink.abilityToAdd;
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_SurefireDrinkEffect() {
            const string rName = "Surefired";
            const string rDesc = "A card bearing this effect has Sniper. At the end of the owner's turn, reduce this effect's Potency by 1.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<SurefireDrinkEffect>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.glowRed,
                TextureLoader.LoadTextureFromFile("sigilSurefireDrink.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilSurefireDrink_2_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            SurefireDrinkEffect.specialAbility = data.Id;
            SurefireDrinkEffect.iconId = data.IconInfo.ability;
        }
    }
}
