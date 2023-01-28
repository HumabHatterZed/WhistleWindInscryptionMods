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
                .FindAll(x => base.PlayableCard.AllAbilities().Contains(x.ability));
            infos.Sort((AbilityInfo a, AbilityInfo b) => b.powerLevel - a.powerLevel);

            int sigilPower = infos[0].powerLevel;

            // if sigil power is negative
            if (sigilPower < 0)
            {
                // if sigil power would kill the card, initiate death
                if (base.PlayableCard.Health - sigilPower <= 0)
                    base.Card.StartCoroutine(base.PlayableCard.Die(false));
                
                return new int[2] { 0, sigilPower };
            }

            return new int[2] { sigilPower, sigilPower };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_SigilPower()
        {
            const string rulebookName = "Sigil Power";
            const string rulebookDescription = "The value represented by this sigil will be equal to the powerlevel of this card's strongest base sigil.";
            SigilPower.icon = AbilityHelper.CreateStatIcon<SigilPower>(
                pluginGuid, rulebookName, rulebookDescription,
                (UnityEngine.Texture2D)AbilitiesUtil.LoadAbilityIcon(Ability.RandomAbility.ToString()),
                TextureLoader.LoadTextureFromBytes(Artwork.sigilSigilPower_pixel), true, true).Id;
        }
    }
}
