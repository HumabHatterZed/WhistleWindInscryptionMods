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
            
            CardManager.New(wonderlabPrefix, piscineMermaid, "Piscine Mermaid",
                attack: 1, health: 1)
                .SetBloodCost(1)
                .SetPortraits(ModAssembly, piscineMermaid)
                .AddAbilities(GiftGiver.ability, Ability.Submerge)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}