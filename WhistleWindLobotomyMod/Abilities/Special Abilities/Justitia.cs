using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class RulebookEntryJustitia : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Justitia()
        {
            const string rName = "Justitia";
            const string rDesc = "Cards targeted by Judgement Bird are killed regardless of Health. Judgement Bird is not affected by abilities like Sharp and Punisher.";
            RulebookEntryJustitia.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryJustitia>(rName, rDesc).Id;
        }
    }
}
