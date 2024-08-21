using DiskCardGame;
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
            UnjustScale.ability = LobotomyAbilityHelper.CreateAbility<UnjustScale>(
                "sigilUnjustScale", rulebookName,
                "At the end of the owner's turn, inflict 1 Sin on all other cards. At the start of the owner's turn, kill cards with 3+ Sin. If Long Arms is defeated, this effect changes.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
