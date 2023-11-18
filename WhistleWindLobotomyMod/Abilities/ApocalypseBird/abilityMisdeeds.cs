using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Misdeeds : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Misdeeds()
        {
            const string rulebookName = "Misdeeds Not Allowed";
            Misdeeds.ability = LobotomyAbilityHelper.CreateAbility<Misdeeds>(
                "sigilMisdeeds", rulebookName,
                "Whenever this card takes damage, gain 1 Power until the end of the owner's turn.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
