using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class BigEyes : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_BigEyes()
        {
            const string rulebookName = "Big Eyes";
            BigEyes.ability = LobotomyAbilityHelper.CreateAbility<BigEyes>(
                "sigilBigEyes", rulebookName,
                "While this card is on the board, all creatures on the board are unaffected by Power-changing effects.",
                null, powerLevel: 0,
                canStack: false).Id;
        }
    }
}
