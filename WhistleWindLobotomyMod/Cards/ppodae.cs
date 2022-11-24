using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Ppodae_D02107()
        {
            List<Ability> abilities = new() { Ability.DebuffEnemy };
            List<Tribe> tribes = new() { Tribe.Canine };

            CardHelper.CreateCard(
                "wstl_ppodaeBuff", "Ppodae",
                "",
                atk: 3, hp: 2,
                blood: 0, bones: 8, energy: 0,
                Artwork.ppodaeBuff, Artwork.ppodaeBuff_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());

            abilities.Add(Ability.Evolve);

            CardHelper.CreateCard(
                "wstl_ppodae", "Ppodae",
                "An innocent little puppy.",
                atk: 1, hp: 1,
                blood: 0, bones: 4, energy: 0,
                Artwork.ppodae, Artwork.ppodae_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth,
                metaTypes: CardHelper.MetaType.Donator, evolveName: "wstl_ppodaeBuff");
        }
    }
}