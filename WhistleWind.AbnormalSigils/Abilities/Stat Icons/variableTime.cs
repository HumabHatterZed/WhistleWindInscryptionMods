using DiskCardGame;
using System.Collections;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Time : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        public override int[] GetStatValues()
        {
            if (base.PlayableCard.TurnPlayed == 0)
                return new int[2] { 0, 0 };

            return new int[2] { TurnManager.Instance.TurnNumber - base.PlayableCard.TurnPlayed, 0 };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_Time()
        {
            const string rulebookName = "Passing Time";
            const string rulebookDescription = "The value represented with this sigil will be equal to the number of turns that have passed since this card was placed on the board.";
            Time.icon = AbilityHelper.CreateStatIcon<Time>(pluginGuid,
                "sigilTime", rulebookName, rulebookDescription, true, false).Id;
        }
    }
}
