using DiskCardGame;
using InscryptionAPI.Boons;
using InscryptionAPI.RuleBook;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class Boons {
        public static BoonData.Type RoseCurse { get; private set; }
        //public static BoonData.Type RoseCurseWeak { get; private set; }
        //public static BoonData.Type RoseBoon { get; private set; }
        internal static void Initialise() {
            RoseCurse = BoonManager.New<StainingRoseBoon>(LobotomyPlugin.pluginGuid,
                "Curse of the Staining Rose", "You will start the battle with a Staining Rose in your hand.",
                TextureLoader.LoadTextureFromFile("boonRoseIcon.png", LobotomyPlugin.ModAssembly),
                TextureLoader.LoadTextureFromFile("boonRoseBackground.png", LobotomyPlugin.ModAssembly),
                false, true);

            //RoseCurseWeak = BoonManager.New<StainingRoseBoon>(LobotomyPlugin.pluginGuid,
            //    "Thirst of the Rose", "At the end of your turn, all cards on your side of the board without Paper Rose gain 1 Paper Rose.",
            //    TextureLoader.LoadTextureFromFile("boonRoseIcon2.png", LobotomyPlugin.ModAssembly), background,
            //    false, true);

            //RoseBoon = BoonManager.New<StainingRoseBoon>(LobotomyPlugin.pluginGuid,
            //    "Boon of the Rose", "When the scales are tipped towards you, gain 1 life at the start of your turn.",
            //    TextureLoader.LoadTextureFromFile("boonRoseIcon3.png", LobotomyPlugin.ModAssembly), background,
            //    false, true);

            //BoonManager.AllBoonsCopy.Find(x => x.type == RoseCurse)
            //    .SetAbilityRedirect("Paper Rose", Ability.None, Color.red);


            //BoonManager.AllBoonsCopy.Find(x => x.type == RoseCurseWeak)
            //    .SetAbilityRedirect("Paper Rose", Ability.None, Color.red);
        }
    }
}
