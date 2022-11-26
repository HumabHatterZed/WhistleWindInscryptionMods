using DiskCardGame;
using System.Collections;
using WhistleWind.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public class Time : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        private int turns;
        public override int[] GetStatValues() => new int[2] { turns, turns };
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
            const string rulebookDescription = "The value represented by this sigil will be equal to the number of turns that have passed since this card resolved on the board.";
            Time.icon = AbilityBuilder.CreateStatIcon<Time>(pluginGuid,
                rulebookName, rulebookDescription, Artwork.sigilTime, Artwork.sigilTime_pixel, true, true).Id;
        }
    }
}
