using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class SmallBeak : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_SmallBeak()
        {
            const string rulebookName = "Small Beak";
            SmallBeak.ability = LobotomyAbilityHelper.CreateAbility<SmallBeak>(
                "sigilSmallBeak", rulebookName,
                "At the start of the turn, target a random lane on the board.  At the start of the next turn, kill all cards in the targeted lane, excluding this card.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
