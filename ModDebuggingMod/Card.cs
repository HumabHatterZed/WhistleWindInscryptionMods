using DiskCardGame;
using InscryptionAPI.Card;
using ModDebuggingMod.Properties;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void CARD_DEBUG()
        {
            List<Ability> abilities = new()
            {
                Sporogenic.ability
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
            CardHelper.CreateCard(
                "wstl", "wstlcard", "Debug",
                "You shouldn't see this.",
                1, 99, 0, 0, 0,
                Resources.allAroundHelper_emission, Resources.allAroundHelper_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits,
                evolveName: "Buggy {0}")
                //.SetExtendedProperty("AffectedByTidalLock", true)
                //.SetGemsCost(new() { GemType.Blue })
                //.SetExtendedProperty("ForbiddenMoxCost", 1)
                .SetEvolve("wstl_silentGirl", 1);
        }
    }
}
