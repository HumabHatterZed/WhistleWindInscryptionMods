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
        private void Card_Nosferatu_F01113()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability,
                Bloodfiend.ability
            };
            List<Tribe> tribes = new() { TribeFae };

            CreateCard(
                "wstl_nosferatuBeast", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 3, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.nosferatuBeast, Artwork.nosferatuBeast_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                modTypes: ModCardType.Ruina);

            abilities = new() { Bloodfiend.ability, Ability.Evolve};

            CreateCard(
                "wstl_nosferatu", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 1, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Waw,
                modTypes: ModCardType.Ruina, evolveName: "wstl_nosferatuBeast");
        }
    }
}