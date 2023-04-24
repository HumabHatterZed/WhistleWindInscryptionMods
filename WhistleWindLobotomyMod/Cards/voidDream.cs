using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_VoidDream_T0299()
        {
            List<Ability> abilities = new() { Ability.DebuffEnemy };

            List<Tribe> tribes = new()
            {
                Tribe.Hooved,
                Tribe.Bird
            };
            CreateCard(
                "wstl_voidDreamRooster", "Void Dream",
                "Quite the chimera.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.voidDreamRooster, Artwork.voidDreamRooster_emission, Artwork.voidDreamRooster_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new());

            abilities = new()
            {
                Ability.Flying,
                Ability.Evolve
            };
            tribes.Remove(Tribe.Bird);

            CreateCard(
                "wstl_voidDream", "Void Dream",
                "A sleeping goat. Or is it a sheep?",
                atk: 1, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.voidDream, Artwork.voidDream_emission, Artwork.voidDream_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                evolveName: "wstl_voidDreamRooster");
        }
    }
}