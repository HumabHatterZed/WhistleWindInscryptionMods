using DiskCardGame;
using InscryptionAPI.RuleBook;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public partial class Abilities
    {
        private static void AddLongArms()
        {
            const string rulebookName = "Long Arms";
            LongArms.ability = AbilityHelper.New<LongArms>(LobotomyPlugin.pluginGuid, "sigilLongArms", rulebookName,
                "[creature] is immune to status ailments. While this card is on the board, time cannot be altered.",
                0, true)
                .SetItemRedirect("time cannot be altered", "Hourglass", GameColors.Instance.red).Id;
        }
    }

    public class LongArms : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
