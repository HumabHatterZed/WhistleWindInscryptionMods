using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string armyInPink = "wstl_armyInPink";
        private static void ArmyInPink_D01106()
        {
            string textureName = "armyInPink";
            CardManager.New(LobotomyPlugin.pluginPrefix, armyInPink, "Army in Pink",
                attack: 3, health: 3, "A friendly pink soldier. It will protect you wherever you go.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.MoveBeside, Protector.ability)
                .AddSpecialAbilities(Pink.specialAbility)
                .AddTribes(TribeAnthropoid)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Zayin, true);
        }
    }
}