using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Rudolta_Mule()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                SpecialTriggeredAbility.PackMule
            };
            List<Tribe> tribes = new()
            {
                Tribe.Hooved
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable
            };
            CardHelper.CreateCard(
                "wstl_RUDOLTA_MULE", "Rudolta of the Sleigh",
                "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                atk: 2, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.rudoltaSleigh, Artwork.rudoltaSleigh_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}