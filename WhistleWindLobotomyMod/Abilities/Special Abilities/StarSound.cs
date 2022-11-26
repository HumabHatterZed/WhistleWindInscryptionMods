using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class RulebookEntryStarSound : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_StarSound()
        {
            const string rName = "Sound of a Star";
            const string rDesc = "If there are no opposing cards that can be attacked, Blue Star strikes all slots.";
            RulebookEntryStarSound.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryStarSound>(rName, rDesc).Id;
        }
    }
}
