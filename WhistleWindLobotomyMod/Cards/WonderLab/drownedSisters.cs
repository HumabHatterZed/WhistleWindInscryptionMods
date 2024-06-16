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
        private void XCard_DrownedSisters()
        {
            const string drownedSisters = "drownedSisters";
            
            NewCard(drownedSisters, "The Drowned Sisters",
                attack: 0, health: 2, bones: 2)
                .SetPortraits(drownedSisters)
                .AddAbilities(Ability.DebuffEnemy)
                .AddTribes(AbnormalPlugin.TribeAnthropoid)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.WonderLab);
        }
    }
}