﻿using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ExpressHellTrain_T0986()
        {
            List<Ability> abilities = new()
            {
                GroupHealer.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                TicketTaker.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                atk: 0, hp: 4,
                blood: 0, bones: 4, energy: 0,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Waw,
                customTribe: TribeMachine);

            abilities = new() { TheTrain.ability };
            LobotomyCardHelper.CreateCard(
                "wstl_BottledExpressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                atk: 0, hp: 1,
                blood: 1, bones: 0, energy: 0,
                Artwork.expressHellTrain, Artwork.expressHellTrain_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}