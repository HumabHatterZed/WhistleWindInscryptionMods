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
        private void Card_DerFreischutz_F0169()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability,
                Ability.SplitStrike
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                MagicBullet.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_derFreischutz", "Der Freischütz",
                "A friendly hunter to some, a cruel gunsman to others. His bullets always hit their mark.",
                atk: 1, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.derFreischutz, Artwork.derFreischutz_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.He,
                customTribe: TribeFae);
        }
    }
}