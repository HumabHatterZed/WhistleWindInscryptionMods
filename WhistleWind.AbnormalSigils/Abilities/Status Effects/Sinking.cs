using DiskCardGame;
using InscryptionAPI.Triggers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// A card bearing this effect loses Power equal to its Sinking divided by its max Health, rounded down.
    /// </summary>
    public class Sinking : StatusEffectBehaviour, IPassiveAttackBuff {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public int GetPassiveAttackBuff(PlayableCard target) {
            if (target == base.PlayableCard) {
                int powerDown = this.EffectPotency / target.MaxHealth;
                if (powerDown > 0) {
                    return -powerDown;
                }
            }
            return 0;
        }
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_Sinking() {
            const string rName = "Sinking";
            const string rDesc = "A card bearing this effect loses Power equal to its Sinking divided by its max Health, rounded down.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Sinking>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.glowSeafoam,
                TextureLoader.LoadTextureFromFile("sigilSinking.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilSinking_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.Part3StatusEffect);

            Sinking.specialAbility = data.Id;
            Sinking.iconId = data.IconInfo.ability;
        }
    }
}
