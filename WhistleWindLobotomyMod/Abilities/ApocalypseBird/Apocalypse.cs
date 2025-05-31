using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public partial class Abilities
    {
        private static void AddApocalypse()
        {
            const string rulebookName = "Black Forest Guardians";
            AbilityManager.FullAbility ab = AbilityHelper.New<ApocalypseAbility>(LobotomyPlugin.pluginGuid, "sigilApocalypse", rulebookName,
                "This card will change its combat pattern every three turns. At 80/60/40 Health, change pattern and the previous pattern cannot used again.", 0, true);
            ab.Info.SetPassive();

            ApocalypseAbility.ability = ab.Id;
        }
    }

    public class ApocalypseAbility : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
