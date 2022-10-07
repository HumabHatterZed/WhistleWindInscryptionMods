using InscryptionAPI;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class PirateSkullAbnormalAI : AI
    {
		public static readonly string ID = AIManager.Add(WstlPlugin.pluginGuid, "PirateSkullAbnormalAI", typeof(PirateSkullAbnormalAI)).Id;
		public override List<CardSlot> SelectSlotsForCards(List<CardInfo> cards, CardSlot[] slots)
		{
			List<CardSlot> list = new List<CardSlot>(slots);
			if (cards.Exists((CardInfo x) => x.name == "wstl_yin"))
			{
				CardSlot openSlot = list.Find((CardSlot x) => x.Card == null);
				if (openSlot != null)
				{
					list.Sort((CardSlot a, CardSlot b) => (!(a == openSlot)) ? 1 : (-1));
					return list;
				}
			}
			return base.SelectSlotsForCards(cards, slots);
		}
	}
}
