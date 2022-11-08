using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentOrchestra_T0131()
        {
            List<Ability> abilities = new()
            {
                Conductor.ability
            };

            CardHelper.CreateCard(
                "wstl_silentOrchestra", "The Silent Orchestra",
                "A conductor of the apocalypse.",
                atk: 0, hp: 5,
                blood: 3, bones: 0, energy: 0,
                Artwork.silentOrchestra, Artwork.silentOrchestra_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Rare, riskLevel: CardHelper.RiskLevel.Aleph);
        }
    }
}