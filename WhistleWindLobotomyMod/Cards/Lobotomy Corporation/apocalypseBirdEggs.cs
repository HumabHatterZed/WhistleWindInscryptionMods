using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string bigEgg = "wstl_apocalypseEgg_big";
        public const string littleEgg = "wstl_apocalypseEgg_small";
        public const string longEgg = "wstl_apocalypseEgg_long";
        public const string giantApocalypse = "wstl_!GIANTCARD_ApocalypseBird";
        public const string crisisChick = "wstl_crisisChick";
        private static void ApocalypseBirdEggs() {
            Trait[] traits = new Trait[2] { Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath };

            CardManager.New(LobotomyPlugin.pluginPrefix, bigEgg, "Aspect of Big Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "bigBird", "bigBird_emission.png", "")
                .AddAbilities(ApocalypseAbility.ID, BigEyes.ID, Dazzling.ID, Challenging.ID)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .SetUniqueCopycat(Cards.bigBird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, littleEgg, "Aspect of Small Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "punishingBird", "punishingBird_boss_emission.png", "")
                .AddAbilities(ApocalypseAbility.ID, SmallBeak.ID, Misdeeds.ID, Challenging.ID)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .SetUniqueCopycat(Cards.punishingBird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, longEgg, "Aspect of Long Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "judgementBird", "judgementBird_boss_emission.png", "")
                .AddAbilities(ApocalypseAbility.ID, LongArms.ID, UnjustScale.ID, Challenging.ID)
                .AddTraits(traits).AddTraits(AbnormalPlugin.ImmuneToAilments)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .SetUniqueCopycat(Cards.judgementBird)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, crisisChick, "Crisis Chick",
                attack: 1, health: 3)
                .SetBloodCost(3)
                .AddAbilities(IntenseVolley.ID)
                .SetPortraits(LobotomyPlugin.ModAssembly, "apocalypseBird")
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, giantApocalypse, "",
                attack: 2, health: 30)
                .AddAbilities(ApocalypseGiant.ID, Soulbound.ID, Ability.Reach, Challenging.ID)
                .AddTraits(Trait.Uncuttable, Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath)
                .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
                .AddAppearances(GiantBirdAppearance.appearance)
                .SetUniqueCopycat(crisisChick)
                .Build();
        }
    }
}