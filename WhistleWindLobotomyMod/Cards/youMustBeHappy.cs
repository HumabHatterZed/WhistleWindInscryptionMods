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
        private void Card_YouMustBeHappy_T0994()
        {
            List<Ability> abilities = new() { Scrambler.ability };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_youMustBeHappy", "You Must be Happy",
                "Those that undergo the procedure find themselves rested and healthy again.",
                atk: 0, hp: 2,
                blood: 0, bones: 0, energy: 2,
                Artwork.youMustBeHappy, Artwork.youMustBeHappy_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                evolveName: "{0}", spellType: SpellType.TargetedStats);
        }
    }
}