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
        private void XCard_PiscineMermaid()
        {
            const string piscineMermaid = "piscineMermaid";
            
            NewCard(piscineMermaid, "Piscine Mermaid",
                attack: 1, health: 1, blood: 1)
                .SetPortraits(piscineMermaid)
                .AddAbilities(GiftGiver.ability, Ability.Submerge)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He, ModCardType.WonderLab);
        }
    }
}