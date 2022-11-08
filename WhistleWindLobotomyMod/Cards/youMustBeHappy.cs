using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YouMustBeHappy_T0994()
        {
            List<Ability> abilities = new()
            {
                Scrambler.ability
            };
            CardHelper.CreateCard(
                "wstl_youMustBeHappy", "You Must be Happy",
                "Those that undergo the procedure find themselves rested and healthy again.",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 2,
                Artwork.youMustBeHappy, Artwork.youMustBeHappy_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Zayin,
                spellType: CardHelper.SpellType.TargetedStats);
        }
    }
}