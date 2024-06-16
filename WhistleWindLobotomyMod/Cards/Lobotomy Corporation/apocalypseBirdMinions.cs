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
            const string eyeballChick = "eyeballChick";
            const string forestKeeper = "forestKeeper";
            const string runawayBird = "runawayBird";

            NewCard(eyeballChick, "Eyeball Chick",
                attack: 2, health: 1, blood: 2)
                .SetPortraits(eyeballChick)
                .AddAbilities(BindingStrike.ability, Piercing.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();

            NewCard(forestKeeper, "Keeper of the Black Forest",
                attack: 1, health: 2, blood: 2)
                .SetPortraits(forestKeeper)
                .AddAbilities(Ability.GainAttackOnKill)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();

            NewCard(runawayBird, "Runaway Bird",
                attack: 1, health: 2, blood: 2)
                .SetPortraits(runawayBird)
                .AddAbilities(HighStrung.ability, Persistent.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
        }
    }
}