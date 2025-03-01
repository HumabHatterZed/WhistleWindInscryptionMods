using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string redQueen = "wstlWonder_redQueen";
        private static void RedQueen()
        {
            string name = "Red Queen";
            string desc = "A royal figure that decapitates those that fail to please it.";
            string textureName = "redQueen";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, redQueen, name,
                attack: 2, health: 2, desc)
                .SetBloodCost(2)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Deathtouch, Unyielding.ability)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 2, health: 2, desc)
                .SetGemsCost(GemType.Green, GemType.Orange)
                .SetCardTemple(CardTemple.Wizard)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.Deathtouch, Unyielding.ability)
                .AddTraits(AbnormalPlugin.SodaLover)
                .AddTribes(AbnormalPlugin.TribeFae)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}