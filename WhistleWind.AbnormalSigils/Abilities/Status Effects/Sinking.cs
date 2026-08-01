using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// When a card bearing this effect has 5 or more Sinking, enter a Panicked state based on its play cost: Blood: Murder; Bone: Suicide; Energy: Languish; Mox: Defile.
    public class Sinking : StatusEffectBehaviour {
        public const int MAX_POTENCY = 5;
        
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override int MaxPotency => MAX_POTENCY;

        public override bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus) {
            return target == base.PlayableCard && statusEffect.IconAbility == this.IconAbility;
        }
        public override IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus) {
            // apply panic
            List<SpecialTriggeredAbility> possiblePanics = new(4);
            if (target.BloodCost() > 0) {
                possiblePanics.Add(PanicBlood.specialAbility);
            }
            if (target.BonesCost() > 0) {
                possiblePanics.Add(PanicBones.specialAbility);
            }
            if (target.EnergyCost > 0) {
                possiblePanics.Add(PanicEnergy.specialAbility);
            }
            if (target.GemsCost().Count > 0) {
                possiblePanics.Add(PanicMox.specialAbility);
            }
            yield return target.AddStatusEffect(possiblePanics[SeededRandom.Range(0, possiblePanics.Count, base.GetRandomSeed())], 1);
            yield return DialogueHelper.PlayDialogueEvent("LearnPanic");
        }
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_Sinking() {
            const string rName = "Sinking";
            const string rDesc = "When a card has 5 or more Sinking, it enters a Panicked state based on its play cost: Blood: Murderous; Bone: Suicidal; Energy: Languishing; Mox: Traitorous.";
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
