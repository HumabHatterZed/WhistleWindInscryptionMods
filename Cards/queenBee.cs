using APIPlugin;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenBee_T0450()
        {
            List<Ability> abilities = new()
            {
                QueenNest.ability
            };

            List<Tribe> tribes = new()
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_queenBee", "Queen Bee",
                "A monstrous amalgam of a hive and a bee.",
                0, 3, 2, 0,
                Resources.queenBee,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode,
                emissionTexture: Resources.queenBee_emission);
        }
    }
}