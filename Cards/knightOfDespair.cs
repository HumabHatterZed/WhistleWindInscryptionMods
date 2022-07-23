using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void KnightOfDespair_O0173()
        {
            List<Ability> abilities = new()
            {
                Ability.SplitStrike,
                Piercing.ability
            };

            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };
            CardHelper.CreateCard(
                "wstl_knightOfDespair", "The Knight of Despair",
                "Failing to protect, she realised nothing was ever truly upheld.",
                2, 4, 2, 0,
                Resources.knightOfDespair, Resources.knightOfDespair_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances);
        }
    }
}