using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string eyeballChick = "wstl_eyeballChick";
        public const string forestKeeper = "wstl_forestKeeper";
        public const string runawayBird = "wstl_runawayBird";
        public const string eyeballChick_mook = "wstl_eyeballChick_mook";
        public const string forestKeeper_mook = "wstl_forestKeeper_mook";
        public const string runawayBird_mook = "wstl_runawayBird_mook";
        private static void ApocalypseBirdMinions() {
            string textureName = "eyeballChick";
            string textureName2 = "forestKeeper";
            string textureName3 = "runawayBird";
            string name = "Eyeball Chick";
            string name2 = "Keeper of the Black Forest";
            string name3 = "Runaway Bird";
            CardManager.New(LobotomyPlugin.pluginPrefix, eyeballChick, name,
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BindingStrike.ability, Piercing.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            CardManager.New(LobotomyPlugin.pluginPrefix, eyeballChick_mook, name,
                attack: 2, health: 1)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(BindingStrike.ability)
                .AddTribes(Tribe.Bird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, forestKeeper, name2,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            CardManager.New(LobotomyPlugin.pluginPrefix, forestKeeper_mook, name2,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddTribes(Tribe.Bird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, runawayBird, name3,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(HighStrung.ability, Persistent.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .AddTraits(PriorityMovement)
                .Build();
            CardManager.New(LobotomyPlugin.pluginPrefix, runawayBird_mook, name3,
                attack: 1, health: 2)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName3)
                .AddAbilities(HighStrung.ability)
                .AddTribes(Tribe.Bird)
                .Build();
        }
    }
}