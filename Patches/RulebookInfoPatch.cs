using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    [HarmonyPatch(typeof(RuleBookInfo))]
<<<<<<< HEAD
    public class RuleBookInfoPatch
=======
    public static class RuleBookInfoPatch
>>>>>>> origin/development
    {
        // Adds select Kaycee Mod sigils to the Part 1 rulebook
        [HarmonyPostfix,HarmonyPatch(nameof(RuleBookInfo.AbilityShouldBeAdded))]
        private static void AddKayceeAbilities(ref int abilityIndex, ref AbilityMetaCategory rulebookCategory, ref bool __result)
        {
            AbilityInfo info = AbilitiesUtil.GetInfo((Ability)abilityIndex);
            if (!SaveFile.IsAscension && info.metaCategories.Contains(AbilityMetaCategory.AscensionUnlocked))
            {
                if (info.name.Equals("BoneDigger") || //info.name.Equals("DeathShield") ||
                    info.name.Equals("DoubleStrike") || //info.name.Equals("OpponentBones")
                    info.name.Equals("StrafeSwap") || info.name.Equals("Morsel"))
                {
                    __result = true;
                }
            }
        }
    }
}
