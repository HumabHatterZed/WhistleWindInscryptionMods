using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void DreamOfABlackSwan_F0270()
        {
            List<Ability> abilities = new()
            {
                Protector.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                DreamOfBlackSwan.specialAbility
            };

            WstlUtils.Add(
                "wstl_dreamOfABlackBird", "Dream of a Black Swan",
                "Its eyes light up the darkness like stars.",
                2, 5, 3, 0,
                Resources.dreamOfABlackSwan, Resources.dreamOfABlackSwan_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true);
        }
    }
}