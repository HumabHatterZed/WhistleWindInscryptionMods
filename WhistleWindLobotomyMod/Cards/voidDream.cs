using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_VoidDream_T0299()
        {
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Ability.Evolve
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };
            CardHelper.CreateCard(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.voidDream, Artwork.voidDream_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth,
                evolveName: "wstl_voidDreamRooster");
        }
    }
}