using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SkinProphecy_T0990()
        {
            List<Ability> abilities = new()
            {
                Witness.ability
            };
            CardHelper.CreateCard(
                "wstl_skinProphecy", "Skin Prophecy",
                "A holy book. Its believers wrapped it in skin to preserve its sanctity.",
                0, 2, 1, 0,
                Resources.skinProphecy, Resources.skinProphecy_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, riskLevel: 2);
        }
    }
}