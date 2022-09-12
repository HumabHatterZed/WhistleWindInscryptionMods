using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void ApostleGuardianDown_T0346()
        {
            List<Ability> abilities = new()
            {
                Ability.Evolve
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_apostleGuardianDown", "Guardian Apostle",
                "The time has come.",
                0, 2, 0, 0,
                Resources.apostleGuardianDown, Resources.apostleGuardianDown_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, evolveName: "wstl_apostleGuardian");
        }
    }
}