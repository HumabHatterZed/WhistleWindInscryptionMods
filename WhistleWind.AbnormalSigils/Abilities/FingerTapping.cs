using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FingerTapping()
        {
            const string rulebookName = "Finger Tapping";
            const string rulebookDescription = "When [creature] is played, create Fingers on adjacent empty spaces. [define:wstl_finger]";
            const string dialogue = "Resentment bursts forth like a weed.";
            const string triggerText = "Sharp thorns shoot out around [creature]!";
            FingerTapping.ability = AbnormalAbilityHelper.CreateAbility<FingerTapping>(
                "sigilFingerTapping",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class FingerTapping : CreateCardsAdjacent
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string SpawnedCardId => "wstl_finger";
        public override string CannotSpawnDialogue => "Not enough hands to go around.";
    }
}
