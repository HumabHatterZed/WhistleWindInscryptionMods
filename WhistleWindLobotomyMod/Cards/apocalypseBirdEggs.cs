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
            Trait[] traits = new Trait[3] {
                Trait.Uncuttable, Trait.Terrain, AbnormalPlugin.ImmuneToInstaDeath
            };

            NewCard(bigEgg, "Aspect of Big Bird",
                attack: 1, health: 100)
                .SetPortraits("bigBird", "bigBird_emission", "")
                .AddAbilities(ApocalypseAbility.ability, BigEyes.ability, Dazzling.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            NewCard(littleEgg, "Aspect of Small Bird",
                attack: 1, health: 100)
                .SetPortraits("punishingBird", "punishingBird_boss_emission", "")
                .AddAbilities(ApocalypseAbility.ability, SmallBeak.ability, Misdeeds.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            NewCard(longEgg, "Aspect of Long Bird",
                attack: 1, health: 100)
                .SetPortraits("judgementBird", "judgementBird_boss_emission", "")
                .AddAbilities(ApocalypseAbility.ability, LongArms.ability, UnjustScale.ability, Ability.MadeOfStone)
                .AddTraits(traits)
                .AddAppearances(ForcedEmission.appearance)
                .Build();
            NewCard("!GIANTCARD_ApocalypseBird", "",
                attack: 2, health: 40)
                .AddAbilities(ApocalypseAbility.ability, UnjustScale.ability, Ability.Reach, Ability.MadeOfStone)
                .AddTraits(Trait.Uncuttable, Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath)
                .AddSpecialAbilities(SpecialTriggeredAbility.GiantCard)
                .AddAppearances(GiantBirdAppearance.appearance)
                .Build();
        }
    }
}