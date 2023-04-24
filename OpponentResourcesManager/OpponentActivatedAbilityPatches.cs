/*using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils.Patches
{
    internal class OpponentResourcesManager : ManagedBehaviour
    {
        private bool preventNextOpponentEnergyLoss;
        public int OpponentBones { get; private set; }
        public int OpponenMaxEnergy { get; private set; }
        public int OpponentEnergy { get; private set; }
        public bool OpponentEnergyAtMax => this.OpponenMaxEnergy >= 6;
        public IEnumerator AddBones(int amount, CardSlot slot = null)
        {
            OpponentBones += amount;
        }
    }
    [HarmonyPatch]
    internal class OpponentActivatedAbilityPatches
    {
        []
        private static IEnumerator AddOpponentResourceManager()
    }
}
*/