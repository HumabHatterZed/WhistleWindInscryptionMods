using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowWhitesApple_F0442()
        {
            List<Ability> abilities = new() { Roots.ability };
            List<Tribe> tribes = new() { TribeBotanic };
            List<Trait> traits = new() { Trait.KillsSurvivors };
            
            CreateCard(
                "wstl_snowWhitesApple", "Snow White's Apple",
                "A poisoned apple brought to life, on a fruitless search for its own happily ever after.",
                atk: 1, hp: 1,
                blood: 0, bones: 3, energy: 0,
                Artwork.snowWhitesApple, Artwork.snowWhitesApple_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                evolveName: "[name]Snow White's Rotted Apple");
        }
    }
}