using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApocalypseBirdEggs()
        {
            const string littleEgg = "littleEgg";
            const string bigEgg = "bigEgg";
            const string longEgg = "longEgg";

            NewCard(littleEgg, "Small Beak",
                attack: 0, health: 20)
                .SetPortraits(littleEgg)
                .AddAbilities()
                .AddTraits(LittleEgg, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath)
                .SetTerrain()
                .Build();
            NewCard(bigEgg, "Big Eyes",
                attack: 0, health: 20)
                .SetPortraits(bigEgg)
                .AddAbilities()
                .AddTraits(BigEgg, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath)
                .SetTerrain()
                .Build();
            NewCard(longEgg, "Long Arms",
                attack: 0, health: 20)
                .SetPortraits(longEgg)
                .AddAbilities()
                .AddTraits(LongEgg, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath)
                .SetTerrain()
                .Build();
        }
    }
}