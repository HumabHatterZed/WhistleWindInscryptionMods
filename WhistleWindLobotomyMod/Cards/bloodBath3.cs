using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Bloodbath3_T0551()
        {
            List<Ability> abilities = new()
            {
                Ability.TripleBlood,
                Ability.QuadrupleBones
            };
            CreateCard(
                "wstl_bloodBath3", "Bloodbath",
                "A tub of blood. The hands of people you once loved wait inside.",
                atk: 1, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.bloodBath3, Artwork.bloodBath3_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                statIcon: SpecialStatIcon.SacrificesThisTurn);
        }
    }
}