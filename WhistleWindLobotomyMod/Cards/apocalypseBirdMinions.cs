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

            // spam card
            NewCard(eyeballChick, "Eyeball Chick",
                attack: 2, health: 2)
                .SetPortraits(eyeballChick)
                .AddAbilities(BindingStrike.ability, Piercing.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();

            // only 3
            NewCard(forestKeeper, "Keeper of the Black Forest",
                attack: 1, health: 5)
                .SetPortraits(forestKeeper)
                .AddAbilities(Ability.Sniper)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();

            // regular
            NewCard(runawayBird, "Runaway Bird",
                attack: 1, health: 3)
                .SetPortraits(runawayBird)
                .AddAbilities(Cycler.ability, HighStrung.ability)
                .AddTribes(Tribe.Bird)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
        }
    }
}