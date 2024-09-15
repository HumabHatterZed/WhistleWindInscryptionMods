using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Linq;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class SlimeIcon : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;

        public override int[] GetStatValues()
        {
            int slimeCount = BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard, (PlayableCard x) => x.HasTrait(AbnormalPlugin.LovingSlime)).Count;
            return new int[2] { slimeCount, 0 };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_Slime()
        {
            const string rulebookName = "Loving Slimes";
            const string rulebookDescription = "The value represented by this sigil will be equal to the number of Slimes that the owner has on their side of the table.";
            SlimeIcon.icon = AbilityHelper.CreateStatIcon<SlimeIcon>(pluginGuid,
                "sigilSlimeIcon", rulebookName, rulebookDescription, true, false).Id;
        }
    }
}
