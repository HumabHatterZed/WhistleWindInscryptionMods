using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Yin_O05102()
        {
            List<Ability> abilities = new()
            {
                Ability.Strafe,
                Ability.Submerge
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                BlackFish.appearance
            };
            CardHelper.CreateCard(
                "wstl_yin", "Yin",
                "A black pendant in search of its missing half.",
                2, 3, 2, 0,
                Resources.yin, Resources.yin_emission,
                altTexture: Resources.yinAlt, emissionAltTexture: Resources.yinAlt_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances,
                onePerDeck: true, riskLevel: 4);
        }
    }
}