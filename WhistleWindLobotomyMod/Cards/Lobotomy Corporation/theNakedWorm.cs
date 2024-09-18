using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_TheNakedWorm_O0274()
        {
            const string theNakedWorm = "theNakedWorm";

            CardManager.New(pluginPrefix, theNakedWorm, "Naked Worm",
                attack: 1, health: 1, "It can enter your body through any aperture.")
                .SetPortraits(ModAssembly, theNakedWorm)
                .AddTribes(Tribe.Insect)
                .AddTraits(NakedSerpent)
                .Build();
        }
    }
}