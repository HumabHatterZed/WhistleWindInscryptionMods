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
        private void XCard_Pygmalion()
        {
            const string pygmalion = "pygmalion";
            
            CardManager.New(wonderlabPrefix, pygmalion, "Pygmalion",
                attack: 3, health: 4)
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, pygmalion)
                .AddAbilities(FingerTapping.ability)
                .AddTribes(AbnormalPlugin.TribeAnthropoid, AbnormalPlugin.TribeBotanic)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}