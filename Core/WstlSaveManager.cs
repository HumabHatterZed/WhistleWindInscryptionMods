using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using InscryptionAPI;
using InscryptionAPI.Saves;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static partial class WstlSaveManager
    {
        private static bool GetBool(string id)
        {
            return ModdedSaveManager.RunState.GetValueAsBoolean(WstlPlugin.pluginGuid, id);
        }
        private static void SetBool(string id, object value)
        {
            ModdedSaveManager.RunState.SetValue(WstlPlugin.pluginGuid, id, value);
        }

        public static bool AbnormalityChoiceIntro
        {
            get => GetBool("AbnormalityChoiceIntro");
            set => SetBool("AbnormalityChoiceIntro", value);
        }
        public static bool ClockThisRun
        {
            // Has the Clock struck twelve this run?
            get => GetBool("ClockStruckTwelve");
            set => SetBool("ClockStruckTwelve", value);
        }
        public static bool WhiteNightHammer
        {
            get => GetBool("WhiteNightMurderAttempted");
            set => SetBool("WhiteNightMurderAttempted", value);
        }
        public static bool WhiteNightKilled
        {
            get => GetBool("WhiteNightDefeated");
            set => SetBool("WhiteNightDefeated", value);
        }
        public static bool ApostleKilled
        {
            get => GetBool("ApostleMurderAttempted");
            set => SetBool("ApostleMurderAttempted", value);
        }
        public static bool ApostleDowned
        {
            get => GetBool("ApostleDowned");
            set => SetBool("ApostleDowned", value);
        }
        public static bool ApostleHeretic
        {
            get => GetBool("ApostleHereticAppeared");
            set => SetBool("ApostleHereticAppeared", value);
        }
        public static bool HasApocalypse
        {
            get => GetBool("HasApocalypseBird");
            set => SetBool("HasApocalypseBird", value);
        }
        public static bool HasSeenApocalypse
        {
            get => GetBool("HasSeenApocalypseStory");
            set => SetBool("HasSeenApocalypseStory", value);
        }
        public static bool HasSeenApocalypseEffects
        {
            get => GetBool("HasSeenApocalypseEffects");
            set => SetBool("HasSeenApocalypseEffects", value);
        }
        public static bool HasSeenBloodbathHand
        {
            get => GetBool("BloodBathHand1");
            set => SetBool("BloodBathHand1", value);
        }
        public static bool HasSeenBloodbathHand1
        {
            get => GetBool("BloodBathHand2");
            set => SetBool("BloodBathHand2", value);
        }
        public static bool HasSeenBloodbathHand2
        {
            get => GetBool("BloodBathHand3");
            set => SetBool("BloodBathHand3", value);
        }
        public static bool HasSeenBeautyTransform
        {
            get => GetBool("PlayerTransformedByCurse");
            set => SetBool("PlayerTransformedByCurse", value);
        }
        public static bool HasSeenShyLookAngry
        {
            get => GetBool("ShyLookedAngryToday");
            set => SetBool("ShyLookedAngryToday", value);
        }
        public static bool HasSeenShyLookHappy
        {
            get => GetBool("ShyLookedHappyToday");
            set => SetBool("ShyLookedHappyToday", value);
        }
        public static bool HasSeenShyLookNeutral
        {
            get => GetBool("ShyLookedNeutralToday");
            set => SetBool("ShyLookedNeutralToday", value);
        }
        public static bool HasSeenRegeneratorExplode
        {
            get => GetBool("RegeneratorCancer");
            set => SetBool("RegeneratorCancer", value);
        }
        public static bool HasSeenDerFreischutzSeventh
        {
            get => GetBool("FreischutzSeventhBullet");
            set => SetBool("FreischutzSeventhBullet", value);
        }
        public static bool HasSeenCrumblingArmourKill
        {
            get => GetBool("CowardnessPunished");
            set => SetBool("CowardnessPunished", value);
        }
        public static bool HasSeenCrumblingArmourFail
        {
            get => GetBool("CourageFailed");
            set => SetBool("CourageFailed", value);
        }
        public static bool HasSeenCrumblingArmourRefuse
        {
            get => GetBool("CourageRefused");
            set => SetBool("CourageRefused", value);
        }
        public static bool HasSeenSnowQueenFreeze
        {
            get => GetBool("SnowQueenFrozen");
            set => SetBool("SnowQueenFrozen", value);
        }
        public static bool HasSeenSnowQueenFail
        {
            get => GetBool("SnowQueenFail");
            set => SetBool("SnowQueenFail", value);
        }
        public static bool HasSeenNothingTransformation
        {
            get => GetBool("NothingThereRevealed");
            set => SetBool("NothingThereRevealed", value);
        }
        public static bool HasSeenNothingTransformationTrue
        {
            get => GetBool("NothingTherePupated");
            set => SetBool("NothingTherePupated", value);
        }
        public static bool HasSeenNothingTransformationEgg
        {
            get => GetBool("NothingThereHatched");
            set => SetBool("NothingThereHatched", value);
        }
        public static bool HasSeenMountainGrow
        {
            get => GetBool("MountainOfBodiesGrown");
            set => SetBool("MountainOfBodiesGrown", value);
        }
        public static bool HasSeenMountainShrink
        {
            get => GetBool("MountainOfBodiesShrunk");
            set => SetBool("MountainOfBodiesShrunk", value);
        }
        public static bool HasSeenCensoredKill
        {
            get => GetBool("CENSOREDCreatedMinion");
            set => SetBool("CENSOREDCreatedMinion", value);
        }
        public static bool HasSeenArmyBlacked
        {
            get => GetBool("ArmyTurnedBlack");
            set => SetBool("ArmyTurnedBlack", value);
        }
        public static bool HasSeenMeltingHeal
        {
            get => GetBool("MeltingLoveMinionAbsorbed");
            set => SetBool("MeltingLoveMinionAbsorbed", value);
        }
        public static bool HasSeenHatredTransformation
        {
            get => GetBool("TransformedIntoHatred");
            set => SetBool("TransformedIntoHatred", value);
        }
        public static bool HasSeenHatredTireOut
        {
            get => GetBool("HatredExhausted");
            set => SetBool("HatredExhausted", value);
        }
        public static bool HasSeenHatredRecover
        {
            get => GetBool("HatredRecovered");
            set => SetBool("HatredRecovered", value);
        }
        public static bool HasSeenGreedTransformation
        {
            get => GetBool("TransformedIntoGreed");
            set => SetBool("TransformedIntoGreed", value);
        }
        public static bool HasSeenDespairProtect
        {
            get => GetBool("ProtectedByKnight");
            set => SetBool("ProtectedByKnight", value);
        }
        public static bool HasSeenDespairTransformation
        {
            get => GetBool("TransformedIntoDespair");
            set => SetBool("TransformedIntoDespair", value);
        }
        public static bool HasSeenJester
        {
            get => GetBool("MagicalGirlsStory");
            set => SetBool("MagicalGirlsStory", value);
        }
        public static bool HasSeenJesterEffects
        {
            get => GetBool("MagicalGirlsEffects");
            set => SetBool("MagicalGirlsEffects", value);
        }
        public static bool HasJester
        {
            get => GetBool("HasJesterOfNihil");
            set => SetBool("HasJesterOfNihil", value);
        }
        public static bool HasUsedBackwardClock
        {
            get => GetBool("UsedBackwardClock");
            set => SetBool("UsedBackwardClock", value);
        }
        public static bool HasSeenSwanFail
        {
            get => GetBool("BlackSwanFail");
            set => SetBool("BlackSwanFail", value);
        }
        public static bool HasSeenDragon
        {
            get => GetBool("YinYangDragon");
            set => SetBool("YinYangDragon", value);
        }
        public static bool CopycatFailed
        {
            get => GetBool("CopycatFailed");
            set => SetBool("CopycatFailed", value);
        }
        public static bool HasSeenCowardlySelfLove
        {
            get => GetBool("CowardlySelfLove");
            set => SetBool("CowardlySelfLove", value);
        }
        public static bool HasSeenCowardlyWeaken
        {
            get => GetBool("CowardlyWeaken");
            set => SetBool("CowardlyWeaken", value);
        }
        public static bool GrownPumpkinJack
        {
            get => GetBool("GrownPumpkinJack");
            set => SetBool("GrownPumpkinJack", value);
        }
        public static bool HasAdult
        {
            get => GetBool("HasWizardOz");
            set => SetBool("HasWizardOz", value);
        }
        public static bool HasSeenAdult
        {
            get => GetBool("HasSeenWizardStory");
            set => SetBool("HasSeenWizardStory", value);
        }
        public static bool HasSeenAdultEffects
        {
            get => GetBool("WizardOzEffects");
            set => SetBool("WizardOzEffects", value);
        }
    }
}