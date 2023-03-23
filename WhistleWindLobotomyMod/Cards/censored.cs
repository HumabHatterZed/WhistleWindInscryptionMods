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
        private void Card_CENSORED_O0389()
        {
            List<Ability> abilities = new() { Bloodfiend.ability };
            List<SpecialTriggeredAbility> specialAbilities = new() { CensoredSpecial.specialAbility };

            CreateCard(
                "wstl_censored", "CENSORED",
                "It's best you never learn what it looks like.",
                atk: 4, hp: 3,
                blood: 3, bones: 0, energy: 0,
                Artwork.censored, Artwork.censored_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Aleph,
                evolveName: "CENSORED {0}");
        }
    }
}