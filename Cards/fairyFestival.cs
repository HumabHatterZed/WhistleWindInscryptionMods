using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void FairyFestival_F0483()
        {
            List<Ability> abilities = new()
            {
                Bloodfiend.ability
            };

            CardHelper.CreateCard(
                "wstl_fairyFestival", "Fairy Festival",
                "Everything will be peaceful while you're under the fairies' care.",
                1, 1, 1, 0,
                Resources.fairyFestival, Resources.fairyFestival_emission, gbcTexture: Resources.fairyFestival_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 1);
        }
    }
}