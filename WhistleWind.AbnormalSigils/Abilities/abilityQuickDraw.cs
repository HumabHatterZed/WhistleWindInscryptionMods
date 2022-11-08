using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_QuickDraw()
        {
            const string rulebookName = "Quick Draw";
            const string rulebookDescription = "When a card moves into the space opposing this card, deal 1 damage.";
            const string dialogue = "The early bird gets the worm.";

            QuickDraw.ability = AbnormalAbilityHelper.CreateAbility<QuickDraw>(
                Artwork.sigilQuickDraw, Artwork.sigilQuickDraw_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: true, isPassive: false).Id;
        }
    }

    public class QuickDraw : Sentry
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
