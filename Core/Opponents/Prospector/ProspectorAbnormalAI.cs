using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class ProspectorAbnormalAI : ProspectorAI
    {
		public static readonly string ID = AIManager.Add(WstlPlugin.pluginGuid, "ProspectorAbnormalAI", typeof(ProspectorAbnormalAI)).Id;

		// Identical to vanilla but with card name replaced
		public override List<CardSlot> SelectSlotsForCards(List<CardInfo> cards, CardSlot[] slots)
		{
			List<CardSlot> list = new(slots);
			if (cards.Exists((CardInfo x) => x.name == "wstl_RUDOLTA_MULE"))
			{
				CardSlot openSlot = list.Find((CardSlot x) => x.Card == null);
				if (openSlot != null)
				{
					list.Sort((CardSlot a, CardSlot b) => (!(a == openSlot)) ? 1 : (-1));
					return list;
				}
			}
			else
			{
				List<CardSlot> list2 = list.FindAll((CardSlot x) => x.Card == null && x.opposingSlot.Card != null && x.opposingSlot.Card.Info.name == "GoldNugget");
				if (list2.Count > 0)
				{
					return base.SelectSlotsForCards(cards, list2.ToArray());
				}
			}
			return base.SelectSlotsForCards(cards, slots);
		}
	}
}
