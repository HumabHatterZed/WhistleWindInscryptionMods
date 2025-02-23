using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string theNakedWorm = "wstl_theNakedWorm";
        private static void TheNakedWorm_O0274()
        {
            string textureName = "theNakedWorm";
            CardManager.New(LobotomyPlugin.pluginPrefix, theNakedWorm, "Naked Worm",
                attack: 1, health: 1, "It can enter your body through any aperture.")
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddTribes(Tribe.Insect)
                .AddTraits(NakedSerpent)
                .Build();
        }
    }
}