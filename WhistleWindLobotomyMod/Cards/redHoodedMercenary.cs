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
        private void Card_RedHoodedMercenary_F0157()
        {
            List<Ability> abilities = new()
            {
                Ability.Sniper,
                BitterEnemies.ability
            };
            List<Tribe> tribes = new() { TribeAnthropoid };

            CreateCard(
                "wstl_redHoodedMercenary", "Little Red Riding Hooded Mercenary",
                "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.",
                atk: 2, hp: 5,
                blood: 3, bones: 0, energy: 0,
                Artwork.redHoodedMercenary, Artwork.redHoodedMercenary_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                evolveName: "[name]Red Riding Hooded Mercenary");
        }
    }
}