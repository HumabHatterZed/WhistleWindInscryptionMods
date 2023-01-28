using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentOrchestra_T0131()
        {
            List<Ability> abilities = new()
            {
                Conductor.ability
            };

            LobotomyCardHelper.CreateCard(
                "wstl_silentOrchestra", "The Silent Orchestra",
                "A conductor of the apocalypse.",
                atk: 1, hp: 5,
                blood: 3, bones: 0, energy: 0,
                Artwork.silentOrchestra, Artwork.silentOrchestra_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph);
        }
    }
}