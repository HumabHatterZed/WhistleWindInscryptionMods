using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Core.Opponents.PirateSkull
{
    public class PirateSkullAbnormalAI : AI
    {
        public static readonly string ID = AIManager.Add(LobotomyPlugin.pluginGuid, "PirateSkullAbnormalAI", typeof(PirateSkullAbnormalAI)).Id;
        public override List<CardSlot> SelectSlotsForCards(List<CardInfo> cards, CardSlot[] slots)
        {
            List<CardSlot> list = new List<CardSlot>(slots);
            if (cards.Exists((x) => x.name == "wstl_yin"))
            {
                CardSlot openSlot = list.Find((x) => x.Card == null);
                if (openSlot != null)
                {
                    list.Sort((a, b) => !(a == openSlot) ? 1 : -1);
                    return list;
                }
            }
            return base.SelectSlotsForCards(cards, slots);
        }
    }
}
