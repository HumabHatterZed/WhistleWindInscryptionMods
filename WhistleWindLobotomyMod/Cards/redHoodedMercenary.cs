using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedHoodedMercenary_F0157()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability,
                BitterEnemies.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_redHoodedMercenary", "Little Red Riding Hooded Mercenary",
                "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.",
                atk: 2, hp: 3,
                blood: 3, bones: 0, energy: 0,
                Artwork.redHoodedMercenary, Artwork.redHoodedMercenary_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw);
        }
    }
}