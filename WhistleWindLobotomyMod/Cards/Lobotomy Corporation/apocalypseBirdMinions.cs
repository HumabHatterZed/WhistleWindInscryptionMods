using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApocalypseBirdMinions()
        {
            const string eyeballChick = "eyeballChick", forestKeeper = "forestKeeper", runawayBird = "runawayBird";
            CardManager.New(pluginPrefix, eyeballChick, "Eyeball Chick",
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, eyeballChick)
                .AddAbilities(BindingStrike.ability, Piercing.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            CardManager.New(pluginPrefix, "eyeballChick_mook", "Eyeball Chick",
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, eyeballChick)
                .AddAbilities(BindingStrike.ability, Piercing.ability)
                .AddTribes(Tribe.Bird)
                .Build();

            CardManager.New(pluginPrefix, forestKeeper, "Keeper of the Black Forest",
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, forestKeeper)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            CardManager.New(pluginPrefix, "forestKeeper_mook", "Keeper of the Black Forest",
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, forestKeeper)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddTribes(Tribe.Bird)
                .Build();

            CardManager.New(pluginPrefix, runawayBird, "Runaway Bird",
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, runawayBird)
                .AddAbilities(HighStrung.ability, Persistent.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            CardManager.New(pluginPrefix, "runawayBird_mook", "Runaway Bird",
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, runawayBird)
                .AddAbilities(HighStrung.ability, Persistent.ability)
                .AddTribes(Tribe.Bird)
                .Build();
        }
    }
}