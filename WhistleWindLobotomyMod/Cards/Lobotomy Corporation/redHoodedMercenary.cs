using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string redHoodedMercenary = "wstl_redHoodedMercenary";
        private static void RedHoodedMercenary_F0157()
        {
            string name = "Little Red Riding Hooded Mercenary";
            string name2 = "Red Riding Hooded Mercenary";
            string desc = "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.";
            string textureName = "redHoodedMercenary";

            CardManager.New(LobotomyPlugin.pluginPrefix, redHoodedMercenary, name,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BitterEnemies.ability, Ability.Sniper)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(TribeAnthropoid)
                .SetDefaultEvolutionName(name2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}