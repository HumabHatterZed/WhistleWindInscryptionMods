using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

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
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard
            };

            CardHelper.CreateCard(
                "wstl_pinocchio", "Pinocchio",
                "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                0, 0, 0, 2,
                Artwork.pinocchio, Artwork.pinocchio_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Teth,
                terrainType: CardHelper.TerrainType.Terrain, metaTypes: metaTypes);
        }
    }
}