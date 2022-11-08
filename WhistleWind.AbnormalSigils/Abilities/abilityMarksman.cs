using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Marksman()
        {
            const string rulebookName = "Marksman";
            const string rulebookDescription = "You may choose which opposing space a card bearing this sigil strikes.";
            const string dialogue = "Your beast strikes with precision.";

            Marksman.ability = AbnormalAbilityHelper.CreateAbility<Marksman>(
                Artwork.sigilMarksman, Artwork.sigilMarksman_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Marksman : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
