using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Roots()
        {
            const string rulebookName = "Roots";
            const string rulebookDescription = "When this card is played, create Thorny Vines on adjacent empty spaces. [define:wstl_snowWhitesVine]";
            const string dialogue = "Resentment bursts forth like a weed.";
            const string triggerText = "Sharp thorns shoot out around [creature]!";
            Roots.ability = AbnormalAbilityHelper.CreateAbility<Roots>(
                "sigilRoots",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Roots : CreateCardsAdjacent
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override string SpawnedCardId => "wstl_snowWhitesVine";
        public override string CannotSpawnDialogue => "Not enough space for the vines to grow.";
    }
}
