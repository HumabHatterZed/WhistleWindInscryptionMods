using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_OneSin_O0303()
        {
            List<Ability> abilities = new()
            {
                Martyr.ability
            };
            CardHelper.CreateCard(
                "wstl_oneSin", "One Sin and Hundreds of Good Deeds",
                "A floating skull. Its hollow sockets see through you.",
                0, 1, 0, 2,
                Resources.oneSin, Resources.oneSin_emission, gbcTexture: Resources.oneSin_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 1);
        }
    }
}