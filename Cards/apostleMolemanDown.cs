using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ApostleMolemanDown_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.Reach,
                Ability.Evolve
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance,
                CardAppearanceBehaviour.Appearance.RareCardBackground
            };
            CardHelper.CreateCard(
                "wstl_apostleMolemanDown", "Moleman Apostle",
                "The time has come.",
                0, 2, 0, 0,
                Resources.apostleMolemanDown, Resources.apostleMolemanDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, evolveName: "wstl_apostleMoleman", numTurns: 2);
        }
    }
}