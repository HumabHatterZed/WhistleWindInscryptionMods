using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_BehaviourAdjustment_O0996()
        {
            List<Ability> abilities = new()
            {
                Corrector.ability
            };

            CardHelper.CreateCard(
                "wstl_behaviourAdjustment", "Behaviour Adjustment",
                "A strange device. It was made to 'fix' any creature. I don't agree with the sentiment.",
                0, 1, 0, 4,
                Artwork.behaviourAdjustment, Artwork.behaviourAdjustment_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth);
        }
    }
}