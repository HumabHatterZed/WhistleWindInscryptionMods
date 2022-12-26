using DiskCardGame;
using WhistleWind.Core.Helpers;
using ModDebuggingMod.Properties;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {
                Debug.ability,
                FrostRuler.ability
                //Ability.LatchExplodeOnDeath
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {

            };
            List<Tribe> tribes = new()
            {
                DebugTribe
            };
            List<Trait> traits = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {

            };
            CardHelper.CreateCard(
                "wstl", "wstlcard", "DEBUG CARD",
                "You shouldn't see this.",
                0, 10, 0, 0, 0,
                Resources.fairyFestival, Resources.fairyFestival_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}
