using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class PotshotPopEffect : SodaEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override string SingletonId => PotshotPop.id;
        public override Ability AbilityToAdd => PotshotPop.abilityToAdd;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_PotshotPopEffect()
        {
            const string rName = "Potshot Popped";
            const string rDesc = "A card bearing this effect has Sentry. At the end of the owner's turn, reduce this effect's Potency by 1.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PotshotPopEffect>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.darkRed,
                TextureLoader.LoadTextureFromFile("sigilPotshotPop.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilPotshotPop_2_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            PotshotPopEffect.specialAbility = data.Id;
            PotshotPopEffect.iconId = data.IconInfo.ability;
        }
    }
}
