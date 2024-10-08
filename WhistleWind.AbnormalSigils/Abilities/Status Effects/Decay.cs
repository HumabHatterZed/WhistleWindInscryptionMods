using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Decay : ModifyOnUpkeepStatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => -1;
        public override IEnumerator OnModifyOnUpkeep()
        {
            yield return base.OnModifyOnUpkeep();
            if (EffectPotency <= 0)
            {
                yield return base.PlayableCard.Die(false, base.PlayableCard);
            }
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Decay()
        {
            const string rName = "Decay";
            const string rDesc = "At the start of the owner's turn, reduce this effect's Potency by 1. A card bearing this effect will perish when its Potency reaches 0.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Decay>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.lightPurple,
                TextureLoader.LoadTextureFromFile("sigilDecay.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilDecay_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect)
                .SetIrremovable(true);

            Decay.specialAbility = data.Id;
            Decay.iconId = data.IconInfo.ability;
        }
    }
}
