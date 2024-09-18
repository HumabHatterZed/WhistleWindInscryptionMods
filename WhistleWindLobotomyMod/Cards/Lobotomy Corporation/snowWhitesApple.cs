using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SnowWhitesApple_F0442()
        {
            const string snowWhitesApple = "snowWhitesApple";

            CardManager.New(pluginPrefix, snowWhitesApple, "Snow White's Apple",
                attack: 1, health: 1, "A poisoned apple brought to life, on a fruitless search for its own happily ever after.")
                .SetBonesCost(3)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, snowWhitesApple)
                .AddAbilities(Roots.ability)
                .AddTribes(TribeBotanic)
                .AddTraits(Trait.KillsSurvivors)
                .SetDefaultEvolutionName("Snow White's Rotted Apple")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}