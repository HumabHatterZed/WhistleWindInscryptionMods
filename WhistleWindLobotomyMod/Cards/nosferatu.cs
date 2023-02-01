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
        private void Card_Nosferatu_F01113()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability,
                Bloodfiend.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_nosferatuBeast", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 3, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                modTypes: LobotomyCardHelper.ModCardType.Ruina);

            abilities.Remove(Bloodfiend.ability);
            abilities.Add(Ability.Evolve);

            LobotomyCardHelper.CreateCard(
                "wstl_nosferatu", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 1, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                modTypes: LobotomyCardHelper.ModCardType.Ruina, evolveName: "wstl_nosferatuBeast",
                customTribe: TribeHumanoid);
        }
    }
}