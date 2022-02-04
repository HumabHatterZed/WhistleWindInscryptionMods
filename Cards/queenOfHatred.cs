﻿using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void QueenOfHatred_O0104()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Flying
            };

            List<SpecialAbilityIdentifier> specialAbilities = new List<SpecialAbilityIdentifier>
            {
                QueenOfHatred.GetSpecialAbilityId
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Reptile
            };

            WstlUtils.Add(
                "wstl_queenOfHatred", "The Queen of Hatred",
                "Heroes exist to fight evil. In its absence, they must create it.",
                1, 5, 0, 0,
                Resources.queenOfHatred,
                abilities: abilities, specialAbilities: specialAbilities,
                tribes: tribes,
                appearanceBehaviour: CardUtils.getRareAppearance);
        }
    }
}