using DiskCardGame;
using ModDebuggingMod.Properties;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {

            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                //StarSound.specialAbility
            };
            List<Tribe> tribes = new()
            {
                //AbnormalPlugin.TribeDivine
            };
            List<Trait> traits = new()
            {

            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {

            };
            CardInfo card = CardHelper.CreateCard(
                "wstl", "wstlcard", "Debug",
                "You shouldn't see this.",
                0, 99, 0, 0, 0,
                Resources.allAroundHelper_emission, Resources.allAroundHelper_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits,
                evolveName: "Buggy {0}");

            // card.Mods.Add(new() { gemify = true });
        }
    }
}
