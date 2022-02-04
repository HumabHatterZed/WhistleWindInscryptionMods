using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenBee_T0450()
        {
            List<Ability> abilities = new List<Ability>
            {
                QueenNest.ability
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Insect
            };

            WstlUtils.Add(
                "wstl_queenBee", "Queen Bee",
                "A monstrous amalgam of a hive and a bee.",
                2, 0, 2, 0,
                Resources.queenBee,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}