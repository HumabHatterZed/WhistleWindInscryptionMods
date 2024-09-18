using DiskCardGame;
using InscryptionAPI.Card;
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

            CardManager.New(pluginPrefix, armyInPink, "Army in Pink",
                attack: 3, health: 3, "A friendly pink soldier. It will protect you wherever you go.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, armyInPink)
                .AddAbilities(Ability.MoveBeside, Protector.ability)
                .AddSpecialAbilities(Pink.specialAbility)
                .AddTribes(TribeAnthropoid)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Zayin, true);
        }
    }
}