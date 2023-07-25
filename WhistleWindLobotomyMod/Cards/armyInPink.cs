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

            NewCard(armyInPink, "Army in Pink", "A friendly pink soldier. It will protect you wherever you go.",
                attack: 3, health: 3, blood: 2)
                .SetPortraits(armyInPink)
                .AddAbilities(Protector.ability, Ability.MoveBeside)
                .AddSpecialAbilities(Pink.specialAbility)
                .AddTribes(TribeAnthropoid)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Zayin, ModCardType.Donator);
        }
    }
}