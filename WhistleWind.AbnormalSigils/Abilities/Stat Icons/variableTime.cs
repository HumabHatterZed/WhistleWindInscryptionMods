using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Time : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        private int turns;
        public override int[] GetStatValues() => new int[2] { turns, 0 };
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            turns += 1;
            yield break;
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_Time()
        {
            const string rulebookName = "Passing Time";
            const string rulebookDescription = "The value represented with this sigil will be equal to the number of turns that have passed since this card was played.";
            Time.icon = AbilityHelper.CreateStatIcon<Time>(pluginGuid,
                rulebookName, rulebookDescription, Artwork.sigilTime, Artwork.sigilTime_pixel, true, false).Id;
        }
    }
}
