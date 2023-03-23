using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WallLady_F0118()
        {
            List<Ability> abilities = new() { Ability.Sharp };
            List<Tribe> tribes = new() { TribeAnthropoid };

            CreateCard(
                "wstl_wallLady", "The Lady Facing the Wall",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                atk: 1, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.wallLady, Artwork.wallLady_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                evolveName: "[name]The Elder Lady Facing the Wall");
        }
    }
}