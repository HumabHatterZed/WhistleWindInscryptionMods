using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Aggravating.ability,
                TargetGainSigils.ability
            };

            CardHelper.CreateCard(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                0, 0, 0, 3,
                Artwork.shelterFrom27March, Artwork.shelterFrom27March_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.He,
                spellType: CardHelper.SpellType.TargetedStats);
        }
    }
}