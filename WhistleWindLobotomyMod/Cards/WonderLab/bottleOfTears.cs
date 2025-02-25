using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string bottleOfTears = "wstlWonder_bottleOfTears";
        private static void BottleOfTears()
        {
            string textureName = "bottleOfTears";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, bottleOfTears, "Bottle of Tears",
                attack: 0, health: 1)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FizzyLifter.ability, Spilling.ability)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout)
                .Build(CardHelper.CardType.Common, RiskLevel.Zayin, true);
        }
    }
}