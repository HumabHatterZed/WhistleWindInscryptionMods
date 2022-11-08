using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
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
            CardHelper.CreateCard(
                "wstl_nosferatu", "Nosferatu",
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.nosferatu, Artwork.nosferatu_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Basic, riskLevel: CardHelper.RiskLevel.Waw,
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}