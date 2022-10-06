using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_Pinocchio_F01112()
        {
            List<Ability> abilities = new()
            {
                Copycat.ability
            };
            CardHelper.CreateCard(
                "wstl_pinocchio", "Pinocchio",
                "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                0, 0, 0, 2,
                Resources.pinocchio, Resources.pinocchio_emission, gbcTexture: Resources.pinocchio_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, isTerrain: true, riskLevel: 2);
        }
    }
}