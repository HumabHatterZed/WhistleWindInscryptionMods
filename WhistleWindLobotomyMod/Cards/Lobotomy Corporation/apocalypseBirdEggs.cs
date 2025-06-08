using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string bigEgg = "wstl_apocalypseEgg_big";
        public const string littleEgg = "wstl_apocalypseEgg_small";
        public const string longEgg = "wstl_apocalypseEgg_long";
        public const string giantApocalypse = "wstl_!GIANTCARD_ApocalypseBird";
        private static void ApocalypseBirdEggs()
        {
            Trait[] traits = new Trait[2] { Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath };

            CardManager.New(LobotomyPlugin.pluginPrefix, bigEgg, "Aspect of Big Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "bigBird", "bigBird_emission.png", "")
                .AddAbilities(ApocalypseAbility.ability, BigEyes.ability, Dazzling.ability, Challenging.ability)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, littleEgg, "Aspect of Small Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "punishingBird", "punishingBird_boss_emission.png", "")
                .AddAbilities(ApocalypseAbility.ability, SmallBeak.ability, Misdeeds.ability, Challenging.ability)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, longEgg, "Aspect of Long Bird",
                attack: 1, health: 90)
                .SetPortraits(LobotomyPlugin.ModAssembly, "judgementBird", "judgementBird_boss_emission.png", "")
                .AddAbilities(ApocalypseAbility.ability, LongArms.ability, UnjustScale.ability, Challenging.ability)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, giantApocalypse, "",
                attack: 3, health: 30)
                .AddAbilities(ApocalypseGiant.ability, Soulbound.ability, Ability.Reach, Challenging.ability)
                .AddTraits(Trait.Uncuttable, Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath)
                .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
                .AddAppearances(GiantBirdAppearance.appearance)
                .Build();
        }
    }
}