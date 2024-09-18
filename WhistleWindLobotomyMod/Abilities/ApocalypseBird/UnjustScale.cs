using DiskCardGame;
using InscryptionAPI.RuleBook;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class UnjustScale : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_UnjustScale()
        {
            const string rulebookName = "Unjust Scale";
            UnjustScale.ability = AbilityHelper.New<UnjustScale>(pluginGuid, "sigilUnjustScale", rulebookName,
                "At the end of the owner's turn, all other cards gain 1 Sin. At the start of the owner's turn, cards with 3+ Sin will perish. If Long Arms is defeated, this effect changes.",
                0, true)
                .SetAbilityRedirect("Sin", Sin.iconId, GameColors.Instance.red).Id;
        }
    }
}
