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
            
            NewCard(pygmalion, "Pygmalion",
                attack: 2, health: 6, blood: 3)
                .SetPortraits(pygmalion)
                .AddAbilities(Ability.Sniper, Ability.WhackAMole)
                .AddTribes(AbnormalPlugin.TribeAnthropoid, AbnormalPlugin.TribeBotanic)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.WonderLab);
        }
    }
}