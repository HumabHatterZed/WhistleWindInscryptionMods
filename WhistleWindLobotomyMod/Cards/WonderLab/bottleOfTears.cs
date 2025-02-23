using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string bottleOfTears = "wstl_bottleOfTears";
        private static void BottleOfTears()
        {
            return;
            string textureName = "bottleOfTears";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, bottleOfTears, "Bottle of Tears",
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Spilling.ability, Ability.Morsel)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}