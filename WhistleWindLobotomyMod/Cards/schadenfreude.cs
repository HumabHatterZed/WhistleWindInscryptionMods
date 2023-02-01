using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Schadenfreude_O0576()
        {
            List<Ability> abilities = new()
            {
                QuickDraw.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_schadenfreude", "SchadenFreude",
                "A strange machine. You can feel someone's persistent gaze through the keyhole.",
                atk: 1, hp: 1,
                blood: 0, bones: 0, energy: 4,
                Artwork.schadenfreude, Artwork.schadenfreude_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                customTribe: TribeMachine);
        }
    }
}