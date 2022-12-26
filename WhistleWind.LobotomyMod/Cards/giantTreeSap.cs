using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_GiantTreeSap_T0980()
        {
            List<Ability> abilities = new()
            {
                Ability.Morsel,
                Ability.Sacrificial
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Sap.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_giantTreeSap", "Giant Tree Sap",
                "Sap from a tree at the end of the world. It is a potent healing agent.",
                atk: 0, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.giantTreeSap, Artwork.giantTreeSap_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                customTribe: TribePlant);
        }
    }
}