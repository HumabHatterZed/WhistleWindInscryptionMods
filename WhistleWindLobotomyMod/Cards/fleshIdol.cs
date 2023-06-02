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
        private void Card_FleshIdol_T0979()
        {
            List<Tribe> tribes = new() { TribeDivine };

            CreateCard(
                "wstl_fleshIdolGood", "Flesh Idol",
                "Perhaps this ordeal will bring recovery.",
                atk: 0, hp: 3,
                blood: 0, bones: 2, energy: 0,
                Artwork.fleshIdol, Artwork.fleshIdol_emission,
                abilities: new() { TeamLeader.ability, GroupHealer.ability, Ability.Evolve }, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                evolveName: "wstl_fleshIdol");

            CreateCard(
                "wstl_fleshIdol", "Flesh Idol",
                "Prayer inevitably ends with the worshipper's despair.",
                atk: 0, hp: 3,
                blood: 0, bones: 2, energy: 0,
                Artwork.fleshIdol, Artwork.fleshIdol_emission,
                abilities: new() { Aggravating.ability, Ability.Evolve }, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                evolveName: "wstl_fleshIdolGood", numTurns: 2);
        }
    }
}