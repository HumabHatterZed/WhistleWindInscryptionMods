using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections.Generic;

namespace WhistleWindLobotomyMod.Opponents.Prospector
{
    public class ProspectorAbnormalAI : ProspectorAI
    {
        public static readonly string ID = AIManager.Add(LobotomyPlugin.pluginGuid, "ProspectorAbnormalAI", typeof(ProspectorAbnormalAI)).Id;

        // Identical to vanilla but with card name replaced
        public override List<CardSlot> SelectSlotsForCards(List<CardInfo> cards, CardSlot[] slots)
        {
            List<CardSlot> list = new(slots);
            if (cards.Exists((x) => x.name == "wstl_RUDOLTA_MULE"))
            {
                CardSlot openSlot = list.Find((x) => x.Card == null);
                if (openSlot != null)
                {
                    list.Sort((a, b) => !(a == openSlot) ? 1 : -1);
                    return list;
                }
            }
            else
            {
                List<CardSlot> list2 = list.FindAll((x) => x.Card == null && x.opposingSlot.Card != null && x.opposingSlot.Card.Info.name == "GoldNugget");
                if (list2.Count > 0)
                {
                    return base.SelectSlotsForCards(cards, list2.ToArray());
                }
            }
            return base.SelectSlotsForCards(cards, slots);
        }
    }
}
