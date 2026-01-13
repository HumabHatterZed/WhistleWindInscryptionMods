using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// At the end of the owner's turn, reduce this effect's Potency by 1. At 0 Potency, a card bearing this effect will kill itself.
    /// </summary>
    public class Decay : ModifyOnTurnEndStatusEffectBehaviour {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => -1;
        public override IEnumerator OnModifyOnTurnEnd() {
            yield return base.OnModifyOnTurnEnd();
            if (EffectPotency < 1) {
                yield return base.PlayableCard.Die(false, base.PlayableCard);
            }
        }
        public override int Priority => -9009;
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_Decay() {
            const string rName = "Decay";
            const string rDesc = "At the end of the owner's turn, reduce this effect's Potency by 1. At 0 Potency, a card bearing this effect will kill itself.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Decay>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilDecay.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilDecay_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect)
                .SetIrremovable(true);

            Decay.specialAbility = data.Id;
            Decay.iconId = data.IconInfo.ability;
        }
    }
}
