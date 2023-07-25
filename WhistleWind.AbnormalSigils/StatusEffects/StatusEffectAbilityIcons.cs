using DiskCardGame;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWind.AbnormalSigils.Core
{
    public class StatusEffectAbilityIcons : ManagedBehaviour
    {
        public List<GameObject> statusEffectIconGroups = new();
        public List<AbilityIconInteractable> abilityIcons = new();
        public Material statusEffectMat = null;
    }
    public class PixelStatusEffectAbilityIcons : ManagedBehaviour
    {
        public List<GameObject> statusEffectIcons = new();
    }
}