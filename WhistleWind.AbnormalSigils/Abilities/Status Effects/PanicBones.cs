using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class PanicBones : PanicBase {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
    }

    public partial class AbnormalPlugin {
        private void Panic_Bones() {
            const string rName = "Suicidal";
            const string rDesc = "A card bearing this effect loses Speed equal to its Bind. At the end of the owner's turn, remove this effect.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<PanicBones>(
                pluginGuid, rName, rDesc, -3, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilBind.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilBind_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            PanicBones.specialAbility = data.Id;
            PanicBones.iconId = data.IconInfo.ability;
        }
    }
}
