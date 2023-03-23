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
        private void Card_FragmentOfUniverse_O0360()
        {
            List<Ability> abilities = new() { Piercing.ability };
            List<Tribe> tribes = new() { TribeDivine };

            CreateCard(
                "wstl_fragmentOfUniverse", "Fragment of the Universe",
                "You see a song in front of you. It's approaching, becoming more colourful by the second.",
                atk: 1, hp: 2,
                blood: 1, bones: 0, energy: 0,
                Artwork.fragmentOfUniverse, Artwork.fragmentOfUniverse_emission, Artwork.fragmentOfUniverse_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth);
        }
    }
}