using DiskCardGame;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core
{
    public class StatusEffectIconsManager : ManagedBehaviour
    {
        public List<GameObject> statusEffectIconGroups = new();
        public readonly List<AbilityIconInteractable> part1AbilityIcons = new();
        
        public List<AbilityIconInteractable> abilityIcons = new();
        public Material statusEffectMat = null;

        public bool cacheForceEmission = false;

        public int currentPage = 0;
    }

    public class PixelStatusEffectAbilityIcons : ManagedBehaviour
    {
        public List<GameObject> statusEffectIcons = new();
    }
}