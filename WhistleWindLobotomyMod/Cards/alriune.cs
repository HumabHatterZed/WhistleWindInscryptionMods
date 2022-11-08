using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Alriune_T0453()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };
            CardHelper.CreateCard(
                "wstl_alriune", "Alriune",
                "A doll yearning to be a human. A human yearning to be a doll.",
                atk: 4, hp: 5,
                blood: 3, bones: 0, energy: 0,
                Artwork.alriune, Artwork.alriune_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw);
        }
    }
}