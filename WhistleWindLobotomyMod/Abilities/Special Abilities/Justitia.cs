using DiskCardGame;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class RulebookEntryJustitia : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_Justitia() {
            const string rName = "Justitia";
            const string rDesc = "If Judgement Bird targets a sacrificable creature, kill it ignoring sigil effects.";
            RulebookEntryJustitia.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryJustitia>(rName, rDesc).Id;
        }
    }
}
