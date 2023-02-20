using DiskCardGame;
using EasyFeedback.APIs;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Nihil : VariableStatBehaviour
    {
        public static SpecialStatIcon icon;
        public static SpecialStatIcon Icon => icon;
        public override SpecialStatIcon IconType => icon;
        public override int[] GetStatValues()
        {
            List<CardSlot> emptySlots = Singleton<BoardManager>.Instance.AllSlotsCopy.FindAll(s => s.Card == null);

            return new int[2] { emptySlots.Count, 0 };
        }
    }

    public partial class AbnormalPlugin
    {
        private void StatIcon_Nihil()
        {
            const string rulebookName = "Nihil";
            const string rulebookDescription = "The value represented by this sigil will be equal to the number of empty spaces on the board.";
            Nihil.icon = AbilityHelper.CreateStatIcon<Nihil>(pluginGuid,
                rulebookName, rulebookDescription, Artwork.sigilNihil, Artwork.sigilNihil_pixel, true, false).Id;
        }
    }
}
