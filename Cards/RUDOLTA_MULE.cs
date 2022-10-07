using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
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
                2, 3, 2, 0,
                Artwork.rudoltaSleigh, Artwork.rudoltaSleigh_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits);
        }
    }
}