using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;

namespace WhistleWind.AbnormalSigils.Core.Helpers
{
    public static class AbnormalHelperExtensions
    {
        /// <summary>
        /// Checks the Card for the number of stacks of the given ability it has.
        /// </summary>
        /// <param name="ability">Ability to check for.</param>
        /// <returns>Number of stacks of the given ability.</returns>
        public static int GetAbilityStacks(this PlayableCard card, Ability ability)
        {
            return card.AllAbilities().FindAll(ab => ab == ability).Count;
        }
    }
}
