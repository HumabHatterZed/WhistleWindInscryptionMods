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
                Nettles.ability
            };
            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };
            WstlUtils.Add(
                "wstl_dreamOfABlackSwan", "Dream of a Black Swan",
                "What happens when the black swan wakes up from dreaming of a white swan?",
                2, 5, 3, 0,
                Resources.dreamOfABlackSwan, Resources.dreamOfABlackSwan_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isRare: true);
        }
    }
}