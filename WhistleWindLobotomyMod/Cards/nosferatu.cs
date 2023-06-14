using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Nosferatu_F01113()
        {
            const string nosferatuName = "Nosferatu";
            const string nosferatu = "nosferatu";
            const string nosferatuBeast = "nosferatuBeast";
            Tribe[] tribes = new[] { TribeFae };

            CardInfo nosferatuBeastCard = NewCard(
                nosferatuBeast,
                nosferatuName,
                attack: 3, health: 2, blood: 2)
                .SetPortraits(nosferatuBeast)
                .AddAbilities(Bloodfiend.ability, Bloodfiend.ability)
                .AddTribes(tribes);

            CardInfo nosferatuCard = NewCard(
                nosferatu,
                nosferatuName,
                "A creature of the night, noble and regal. Will you help sate its thirst?",
                attack: 1, health: 2, blood: 2)
                .SetPortraits(nosferatu)
                .AddAbilities(Bloodfiend.ability, Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolveInfo("wstl_nosferatuBeast");

            CreateCard(nosferatuBeastCard, cardType: ModCardType.Ruina);
            CreateCard(nosferatuCard, CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Ruina);
        }
    }
}