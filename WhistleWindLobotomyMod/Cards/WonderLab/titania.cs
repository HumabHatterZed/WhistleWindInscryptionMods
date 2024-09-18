using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_Titania()
        {
            const string titania = "titania";
            CardManager.New(wonderlabPrefix, titania, "Titania",
                attack: 0, health: 4, "The queen of faeries, searching always for her traitorous husband.")
                .SetBloodCost(1)
                .SetStatIcon(FlowerPower.Icon)
                .SetPortraits(ModAssembly, titania)
                .AddAbilities(Ability.StrafeSwap)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}