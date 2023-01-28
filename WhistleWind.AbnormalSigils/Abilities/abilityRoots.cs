using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Roots()
        {
            const string rulebookName = "Roots";
            const string rulebookDescription = "When this card is played, create Thorny Vines on adjacent empty spaces. [define:wstl_snowWhitesVine]";
            const string dialogue = "Resentment bursts forth like a weed.";
            Roots.ability = AbnormalAbilityHelper.CreateAbility<Roots>(
                Artwork.sigilRoots, Artwork.sigilRoots_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
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
