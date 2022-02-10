using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System;

namespace WhistleWindLobotomyMod
{
    public static partial class PersistentValues
    {
        #region Plague Doctor
        public static int NumberOfBlessings // Keeps track of the number of times Plague Doctor has healed a card
        {
            get
            {
                return SaveGameHelper.GetInt("NumberOfBlessings", 0);
            }
            set
            {
                SaveGameHelper.SetValue("NumberOfBlessings", value.ToString());
            }
        }
        public static bool ClockThisRun // Keeps track of whether Plague Doctor activated this run (Does nothing currently)
        {
            get
            {
                return SaveGameHelper.GetBool("ClockThisRun");
            }
            set
            {
                SaveGameHelper.SetValue("ClockThisRun", value.ToString());
            }
        }
        public static bool ApostleDowned // Keeps track of whether this is the first time an Apostle has been downed
        {
            get
            {
                return SaveGameHelper.GetBool("ApostleDowned");
            }
            set
            {
                SaveGameHelper.SetValue("ApostleDowned", value.ToString());
            }
        }
        public static bool ApostleKilled // Keeps track of whether this is the first time the player has tried to kill an Apostle
        {
            get
            {
                return SaveGameHelper.GetBool("ApostleKilled");
            }
            set
            {
                SaveGameHelper.SetValue("ApostleKilled", value.ToString());
            }
        }
        public static bool ApostleHeretic // Keeps track of whether this is the first time the player has tried to kill WhiteNight
        {
            get
            {
                return SaveGameHelper.GetBool("ApostleHeretic");
            }
            set
            {
                SaveGameHelper.SetValue("ApostleHeretic", value.ToString());
            }
        }
        public static bool WhiteNightKilled // Keeps track of whether this is the first time WhiteNight has been killed by a card
        {
            get
            {
                return SaveGameHelper.GetBool("WhiteNightKilled");
            }
            set
            {
                SaveGameHelper.SetValue("WhiteNightKilled", value.ToString());
            }
        }
        public static bool WhiteNightHammer // Keeps track of whether this is the first time the player has tried to kill WhiteNight
        {
            get
            {
                return SaveGameHelper.GetBool("WhiteNightHammer");
            }
            set
            {
                SaveGameHelper.SetValue("WhiteNightHammer", value.ToString());
            }
        }
        #endregion

        #region Magical Girls
        public static bool HasSeenHatredTransformation // Keeps track of whether this is the first time Magical Girl H has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenHatredTransformation");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenHatredTransformation", value.ToString());
            }
        }
        public static bool HasSeenHatredTireOut // Keeps track of whether this is the first time Queen oF Hatred has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenHatredTireOut");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenHatredTireOut", value.ToString());
            }
        }
        public static bool HasSeenHatredRecover // Keeps track of whether this is the first time Queen oF Hatred (E) has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenHatredRecover");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenHatredRecover", value.ToString());
            }
        }
        public static bool HasSeenGreedTransformation // Keeps track of whether this is the first time Magical Girl D has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenGreedTransformation");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenGreedTransformation", value.ToString());
            }
        }
        public static bool HasSeenDespairTransformation // Keeps track of whether this is the first time Magical Girl S has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenDespairTransformation");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenDespairTransformation", value.ToString());
            }
        }
        public static bool HasSeenDespairProtect // Keeps track of whether this is the first time Magical Girl S has protected
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenDespairProtect");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenDespairProtect", value.ToString());
            }
        }
        #endregion

        #region ALEPHs
        public static bool HasSeenNothingTransformation // Keeps track of whether this is the first time Nothing There has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenNothingTransformation");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenNothingTransformation", value.ToString());
            }
        }
        public static bool HasSeenNothingTransformationTrue // Keeps track of whether this is the first time Nothing There True has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenNothingTransformationTrue");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenNothingTransformationTrue", value.ToString());
            }
        }
        public static bool HasSeenNothingTransformationEgg // Keeps track of whether this is the first time Nothing There Egg has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenNothingTransformationEgg");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenNothingTransformationEgg", value.ToString());
            }
        }
        public static bool HasSeenMountainGrow // Keeps track of whether this is the first time MoSB has grown
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenMountainGrow");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenMountainGrow", value.ToString());
            }
        }
        public static bool HasSeenMountainGrow2 // Keeps track of whether this is the first time MoSB-2 has grown
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenMountainGrow2");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenMountainGrow2", value.ToString());
            }
        }
        public static bool HasSeenMountainShrink2 // Keeps track of whether this is the first time MoSB-2 has shrunk
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenMountainShrink2");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenMountainShrink2", value.ToString());
            }
        }
        public static bool HasSeenMountainShrink3 // Keeps track of whether this is the first time MoSB-3 has shrunk
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenMountainShrink3");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenMountainShrink3", value.ToString());
            }
        }
        public static bool HasSeenCensoredKill // Keeps track of whether this is the first time Censored has <CENSORED>
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenCensoredKill");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenCensoredKill", value.ToString());
            }
        }
        public static bool HasSeenArmyBlacked // Keeps track of whether this is the first time Censored has <CENSORED>
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenArmyBlacked");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenArmyBlacked", value.ToString());
            }
        }
        #endregion

        #region HEs
        public static bool HasSeenDerFreischutzSeventh // Keeps track of whether this is the first time Freischutz has shot his seventh bullet
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenDerFreischutzSeventh");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenDerFreischutzSeventh", value.ToString());
            }
        }
        public static bool HasSeenCrumblingArmourKill // Keeps track of whether this is the first time Crumbling Armour has punished a coward
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenCrumblingArmourKill");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenCrumblingArmourKill", value.ToString());
            }
        }
        public static bool HasSeenCrumblingArmourFail // Keeps track of whether this is the first time Crumbling Armour hasn't given Power
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenCrumblingArmourFail");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenCrumblingArmourFail", value.ToString());
            }
        }
        public static bool HasSeenCrumblingArmourRefuse // Keeps track of whether this is the first time -Courageous- has refused to give Power
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenCrumblingArmourRefuse");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenCrumblingArmourRefuse", value.ToString());
            }
        }
        public static bool HasSeenSnowQueenFreeze // Keeps track of whether this is the first time Freischutz has shot his seventh bullet
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenSnowQueenFreeze");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenSnowQueenFreeze", value.ToString());
            }
        }
        #endregion

        #region TETHs
        public static bool HasSeenBloodbathHand // Keeps track of whether this is the first time Bloodbath has grown
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenBloodbathHand");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenBloodbathHand", value.ToString());
            }
        }
        public static bool HasSeenBloodbathHand1 // Keeps track of whether this is the first time Bloodbath has grown
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenBloodbathHand1");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenBloodbathHand1", value.ToString());
            }
        }
        public static bool HasSeenBloodbathHand2 // Keeps track of whether this is the first time Bloodbath has grown
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenBloodbathHand2");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenBloodbathHand3", value.ToString());
            }
        }
        public static bool HasSeenBeautyTransform // Keeps track of whether this is the first time a player card has transformed
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenBeautyTransform");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenBeautyTransform", value.ToString());
            }
        }
        public static bool HasSeenShyLookAngry // Keeps track of whether this is the first time Today's Shy Look became Angry
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenShyLookAngry");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenShyLookAngry", value.ToString());
            }
        }
        public static bool HasSeenShyLookHappy // Keeps track of whether this is the first time Today's Shy Look became Angry
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenShyLookHappy");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenShyLookHappy", value.ToString());
            }
        }
        public static bool HasSeenShyLookNeutral // Keeps track of whether this is the first time Today's Shy Look be indecisive
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenShyLookNeutral");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenShyLookNeutral", value.ToString());
            }
        }
        public static bool HasSeenRegeneratorExplode // Keeps track of whether this is the first time Regenerator caused explosions
        {
            get
            {
                return SaveGameHelper.GetBool("HasSeenRegeneratorExplode");
            }
            set
            {
                SaveGameHelper.SetValue("HasSeenRegeneratorExplode", value.ToString());
            }
        }
        #endregion
    }
}