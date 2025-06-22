using DiskCardGame;
using InscryptionAPI.RuleBook;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddUnjustScale() {
            const string rulebookName = "Unjust Scale";
            UnjustScale.ability = AbilityHelper.New<UnjustScale>(LobotomyPlugin.pluginGuid, "sigilUnjustScale", rulebookName,
                "At the end of the owner's turn, all other cards gain 1 Sin. At the start of the owner's turn, cards with 3+ Sin will perish. If Long Arms is defeated, this effect changes.",
                0, true)
                .SetAbilityRedirect("Sin", Sin.iconId, GameColors.Instance.red).Id;
        }
    }

    public class UnjustScale : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
