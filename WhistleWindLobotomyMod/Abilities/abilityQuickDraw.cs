using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_QuickDraw()
        {
            const string rulebookName = "Quick Draw";
            const string rulebookDescription = "When a card moves into the space opposing this card, deal 1 damage.";
            const string dialogue = "The early bird gets the worm.";

            QuickDraw.ability = AbilityHelper.CreateAbility<QuickDraw>(
                Resources.sigilQuickDraw, Resources.sigilQuickDraw_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: true, isPassive: false).Id;
        }
    }
    // ripped from Sentry code
    public class QuickDraw : Sentry
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
}
