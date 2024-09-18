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
            const string bigEgg = "apocalypseEgg_big";
            const string littleEgg = "apocalypseEgg_small";
            const string longEgg = "apocalypseEgg_long";
            Trait[] traits = new Trait[2] { Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath };

            CardManager.New(pluginPrefix, bigEgg, "Aspect of Big Bird",
                attack: 1, health: 100)
                .SetPortraits(ModAssembly, "bigBird", "bigBird_emission", "")
                .AddAbilities(ApocalypseAbility.ability, BigEyes.ability, Dazzling.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, littleEgg, "Aspect of Small Bird",
                attack: 1, health: 100)
                .SetPortraits(ModAssembly, "punishingBird", "punishingBird_boss_emission", "")
                .AddAbilities(ApocalypseAbility.ability, SmallBeak.ability, Misdeeds.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, longEgg, "Aspect of Long Bird",
                attack: 1, health: 100)
                .SetPortraits(ModAssembly, "judgementBird", "judgementBird_boss_emission", "")
                .AddAbilities(ApocalypseAbility.ability, LongArms.ability, UnjustScale.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .SetEventCard(false)
                .Build();

            CardManager.New(pluginPrefix, "!GIANTCARD_ApocalypseBird", "",
                attack: 2, health: 40)
                .AddAbilities(ApocalypseAbility.ability, GiantBlocker.ability, Ability.Reach, Ability.MadeOfStone)
                .AddTraits(Trait.Uncuttable, Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath)
                .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
                .AddAppearances(GiantBirdAppearance.appearance)
                .Build();
        }
    }
}