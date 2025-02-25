using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class FizzyLifterEffect : ModifyOnTurnEndStatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int PotencyModification => -1;

        public override bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect)
        {
            return target == base.PlayableCard && statusEffect.GetType() == this.GetType();
        }
        public override IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect)
        {
            CardModificationInfo mod = base.PlayableCard.TemporaryMods.Find(x => HelperMethods.CompareSingleton(x.singletonId, "FizzyLifted"));
            if (mod != null)
                base.PlayableCard.RemoveTemporaryMod(mod);
            base.PlayableCard.Status.hiddenAbilities.Remove(Ability.Flying);
            return base.OnStatusEffectRemoved(target, statusEffect);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_FizzyLifterEffect()
        {
            const string rName = "Fizzy Lifted";
            const string rDesc = "While a card bears this effect, it will be Airborne. At the end of the owner's turn, reduce this effect's Potency by 1.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<FizzyLifterEffect>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.seafoam,
                TextureLoader.LoadTextureFromFile("sigilFizzyLifter.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilFizzyLifter_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            FizzyLifterEffect.specialAbility = data.Id;
            FizzyLifterEffect.iconId = data.IconInfo.ability;
        }
    }
}
