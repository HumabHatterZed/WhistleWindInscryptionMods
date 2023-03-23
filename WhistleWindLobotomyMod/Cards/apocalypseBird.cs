using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApocalypseBird_O0263()
        {
            List<Ability> abilities = new()
            {
                Ability.AllStrike,
                Ability.SplitStrike
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                BoardEffects.specialAbility
            };
            List<Tribe> tribes = new() { Tribe.Bird };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_apocalypseBird", "Apocalypse Bird",
                "There was no moon, no stars. Just a bird, alone in the Black Forest.",
                atk: 3, hp: 12,
                blood: 4, bones: 0, energy: 0,
                Artwork.apocalypseBird, Artwork.apocalypseBird_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Rare,
                modTypes: ModCardType.EventCard, metaTypes: CardHelper.CardMetaType.NonChoice,
                evolveName: "Greater {0}");
        }
    }
}