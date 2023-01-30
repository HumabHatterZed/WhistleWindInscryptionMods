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
        private void Card_GraveOfBlossoms_O04100()
        {
            List<Ability> abilities = new()
            {
                Ability.Sharp,
                Bloodfiend.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_graveOfBlossoms", "Grave of Cherry Blossoms",
                "A blooming cherry tree. The more blood it has, the more beautiful it becomes.",
                atk: 0, hp: 3,
                blood: 1, bones: 0, energy: 0,
                Artwork.graveOfBlossoms, Artwork.graveOfBlossoms_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth,
                customTribe: TribePlant);
        }
    }
}