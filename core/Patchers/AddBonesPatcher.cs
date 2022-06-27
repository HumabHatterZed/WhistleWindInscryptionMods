using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
	public class AddBonesPatcher
	{
		private static AddBonesPatcher w_instance;
		public static AddBonesPatcher Instance => w_instance ??= new AddBonesPatcher();

		// might be an easier method, but I'm too dumb to figure it out
		// also this works so YEEEEEEEEEEEEEEEEEEAAAAAAAAA
		// anyways, this prevents cards from dropping bones when killed by The Train ability
		[HarmonyPatch(typeof(ResourcesManager), nameof(ResourcesManager.AddBones))]
		[HarmonyPostfix]
		public static IEnumerator AddBones(IEnumerator enumerator, ResourcesManager __instance, int amount, CardSlot slot)
		{
			if (slot != null && slot.Card != null)
			{
				if (slot.Card.Info.GetExtendedProperty("killedByTrain") != "1")
                {
					yield return enumerator;
					yield break;
				}	
			}
			amount = 0;
			yield break;
		}
	}
}
