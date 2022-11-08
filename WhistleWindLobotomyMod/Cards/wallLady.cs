using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WallLady_F0118()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability
            };
            CardHelper.CreateCard(
                "wstl_wallLady", "The Lady Facing the Wall",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                atk: 0, hp: 3,
                blood: 0, bones: 5, energy: 0,
                Artwork.wallLady, Artwork.wallLady_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}