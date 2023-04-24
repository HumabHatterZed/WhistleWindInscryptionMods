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
                Ability.RandomAbility
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
                5, 99, 0, 0, 0,
                Resources.allAroundHelper_emission, Resources.allAroundHelper_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits,
                evolveName: "Buggy {0}");

            card.Mods.Add(new() { gemify = true });

            /*            CardInfo moxOrange1 = CardManager.New("wstl", "wstlmoxblue", "Orange", 0, 1);
                        moxOrange1.SetGemsCost(new() { GemType.Blue });
                        moxOrange1.SetDefaultPart1Card();

                        CardInfo moxOrange2 = CardManager.New("wstl", "wstlmoxgreen", "Orange", 0, 1);
                        moxOrange2.SetGemsCost(new() { GemType.Green, GemType.Orange });
                        moxOrange2.SetDefaultPart1Card();*/
        }
    }
}
