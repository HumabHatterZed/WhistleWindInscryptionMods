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
        private void Card_WorldPortrait_O0991()
        {
            List<Ability> abilities = new() { Reflector.ability };
            
            CreateCard(
                "wstl_worldPortrait", "Portrait of Another World",
                "This portrait captures a moment, one we're destined to lose.",
                atk: 0, hp: 4,
                blood: 1, bones: 0, energy: 0,
                Artwork.worldPortrait, Artwork.worldPortrait_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}