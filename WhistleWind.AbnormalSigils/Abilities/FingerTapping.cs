using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_FingerTapping() {
            const string rulebookName = "Finger Tapping";
            const string rulebookDescription = "When [creature] is played, create Fingers on adjacent empty spaces. A Finger is defined as: 1 Power, 1 Health, Mind Strike.";
            const string dialogue = "Here comes the bride.";
            const string triggerText = "Floating fingers appear beside [creature]!";
            FingerTapping.ability = AbnormalAbilityHelper.CreateAbility<FingerTapping>(
                "sigilFingerTapping",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] is played, create Fingers on adjacent empty spaces. A Finger is defined as: 1 Power, 1 Health, Mind Strike.
    /// </summary>
    public class FingerTapping : CreateTwoCardsAdjacent {
        public static Ability ability;
        public override Ability Ability => ability;
        protected override string LeftSpawnedCardId => "wstl_finger_left";
        protected override string RightSpawnedCardId => "wstl_finger_right";
        protected override string CannotSpawnDialogue => "Not enough hands to go around.";
    }
}
