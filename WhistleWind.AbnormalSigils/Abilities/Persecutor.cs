using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Persecutor() {
            const string rulebookName = "Persecutor";
            const string rulebookDescription = "When [creature] is played, create a Nail and Hammer in the adjacent left and right spaces respectively if they are empty.";
            const string dialogue = "Are you guilty of having a closed heart?";
            const string triggerText = "[creature] reveals its hidden tools!";
            Persecutor.ability = AbnormalAbilityHelper.CreateAbility<Persecutor>(
                "sigilPersecutor",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// When [creature] is played, create a Nail and Hammer in the adjacent left and right spaces respectively if they are empty.
    /// </summary>
    public class Persecutor : CreateTwoCardsAdjacent {
        public static Ability ability;
        public override Ability Ability => ability;

        protected override string LeftSpawnedCardId => "wstl_nail";
        protected override string RightSpawnedCardId => "wstl_hammer";
        protected override string CannotSpawnDialogue => "These tools remain hidden for now.";
    }
}
