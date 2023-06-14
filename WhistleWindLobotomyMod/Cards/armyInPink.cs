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
        private void Card_ArmyInPink_D01106()
        {
            const string armyInPink = "armyInPink";

            CardInfo army = NewCard(
                armyInPink,
                "Army in Pink",
                "A friendly soldier the colour of the human heart. It will protect you wherever you go.",
                attack: 3, health: 3, blood: 2)
                .SetPortraits(armyInPink)
                .AddAbilities(Protector.ability, Ability.MoveBeside)
                .AddSpecialAbilities(Pink.specialAbility)
                .AddTribes(TribeAnthropoid);

            CreateCard(army, CardHelper.ChoiceType.Rare, RiskLevel.Zayin, ModCardType.Donator);
        }
    }
}