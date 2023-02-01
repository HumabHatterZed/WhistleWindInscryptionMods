using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class SigilPower : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        public override int[] GetStatValues()
        {
            List<AbilityInfo> infos = AbilityManager.AllAbilityInfos
                .FindAll(x => x.ability != Ability.RandomAbility && base.PlayableCard.AllAbilities().Contains(x.ability));

            infos.Sort((AbilityInfo a, AbilityInfo b) => b.powerLevel - a.powerLevel);

            return new int[2] { infos[0].powerLevel, 0 };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_SigilPower()
        {
            const string rulebookName = "Sigil Power";
            const string rulebookDescription = "The value represented by this sigil will be equal to the powerlevel of this card's strongest sigil.";
            SigilPower.icon = AbilityHelper.CreateStatIcon<SigilPower>(
                pluginGuid, rulebookName, rulebookDescription,
                TextureLoader.LoadTextureFromBytes(Artwork.sigilSigilPower),
                TextureLoader.LoadTextureFromBytes(Artwork.sigilSigilPower_pixel), true, false).Id;
        }
    }
}
