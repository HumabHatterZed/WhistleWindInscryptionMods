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
        private void Card_DerFreischutz_F0169()
        {
            List<Ability> abilities = new()
            {
                Ability.Sniper,
                Ability.SplitStrike
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                MagicBullet.specialAbility
            };
            List<Tribe> tribes = new() { TribeFae };

            CreateCard(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a cruel gunsman to others. His bullets always hit their mark.",
                atk: 1, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.derFreischutz, Artwork.derFreischutz_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.He,
                evolveName: "[name]Der Ältere Freischütz");
        }
    }
}