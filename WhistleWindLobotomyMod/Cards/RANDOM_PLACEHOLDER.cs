using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RANDOM_PLACEHOLDER()
        {
            const string RANDOM_PLACEHOLDER = "RANDOM_PLACEHOLDER";
            CardManager.New(pluginPrefix, RANDOM_PLACEHOLDER, RANDOM_PLACEHOLDER,
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, RANDOM_PLACEHOLDER)
                .AddAbilities(DiskCardGame.Ability.RandomAbility)
                .SetStatIcon(SigilPower.Icon)
                .Build();
        }
    }
}