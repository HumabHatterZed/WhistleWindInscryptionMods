using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PlagueDoctor_O0145()
        {
            CardInfo alriuneCard = NewCard(
                "plagueDoctor",
                "Plague Doctor",
                "A worker of miracles. He humbly requests to join you.",
                attack: 0, health: 3, bones: 3)
                .SetPortraits(UpdatePlagueSprites())
                .AddAbilities(Ability.Flying, Healer.ability)
                .AddSpecialAbilities(Bless.specialAbility)
                .AddTribes(TribeDivine)
                .SetOnePerDeck();

            CreateCard(alriuneCard, CardHelper.ChoiceType.Common, RiskLevel.Zayin);
        }
        public static string UpdatePlagueSprites()
        {
            // Update portrait and emission on loadup
            return LobotomyConfigManager.Instance.NumOfBlessings switch
            {
                0 => "plagueDoctor",
                1 => "plagueDoctor1",
                2 => "plagueDoctor2",
                3 => "plagueDoctor3",
                4 => "plagueDoctor4",
                5 => "plagueDoctor5",
                6 => "plagueDoctor6",
                7 => "plagueDoctor7",
                8 => "plagueDoctor8",
                9 => "plagueDoctor9",
                10 => "plagueDoctor10",
                _ => "plagueDoctor11",
            };
        }
    }
}