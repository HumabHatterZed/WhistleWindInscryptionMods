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
        private void Card_RedHoodedMercenary_F0157()
        {
            const string redHoodedMercenary = "redHoodedMercenary";

            CardManager.New(pluginPrefix, redHoodedMercenary, "Little Red Riding Hooded Mercenary",
                attack: 2, health: 5, "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, redHoodedMercenary)
                .AddAbilities(Ability.Sniper, BitterEnemies.ability)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName("Red Riding Hooded Mercenary")
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}