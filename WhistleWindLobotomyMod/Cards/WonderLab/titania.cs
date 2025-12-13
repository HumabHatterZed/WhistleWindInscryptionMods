using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string titania = "wstlWonder_titania";
        private static void Titania() {
            string textureName = "titania";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, titania, "Titania",
                attack: 0, health: 4, "The queen of faeries, searching always for her traitorous husband.")
                .SetBloodCost(2)
                .SetStatIcon(FlowerPower.Icon)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FlowerQueen.ability, Ability.StrafeSwap)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}