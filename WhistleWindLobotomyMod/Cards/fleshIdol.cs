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
        private void Card_FleshIdol_T0979()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability,
                Ability.BuffEnemy
            };
            LobotomyCardHelper.CreateCard(
                "wstl_fleshIdol", "Flesh Idol",
                "Prayer inevitably ends with the worshipper's despair.",
                atk: 0, hp: 2,
                blood: 0, bones: 3, energy: 0,
                Artwork.fleshIdol, Artwork.fleshIdol_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                customTribe: TribeDivine);
        }
    }
}