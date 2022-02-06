using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void PlagueDoctor_O0145()
        {
            byte[] resource = Resources.plagueDoctor;

            switch (PersistentValues.NumberOfBlessings)
            {
                case 0:
                    resource = Resources.plagueDoctor;
                    break;
                case 1:
                    resource = Resources.plagueDoctor1;
                    break;
                case 2:
                    resource = Resources.plagueDoctor2;
                    break;
                case 3:
                    resource = Resources.plagueDoctor3;
                    break;
                case 4:
                    resource = Resources.plagueDoctor4;
                    break;
                case 5:
                    resource = Resources.plagueDoctor5;
                    break;
                case 6:
                    resource = Resources.plagueDoctor6;
                    break;
                case 7:
                    resource = Resources.plagueDoctor7;
                    break;
                case 8:
                    resource = Resources.plagueDoctor8;
                    break;
                case 9:
                    resource = Resources.plagueDoctor9;
                    break;
                case 10:
                    resource = Resources.plagueDoctor10;
                    break;
                case 11:
                    resource = Resources.plagueDoctor11;
                    break;
                case 12:
                    resource = Resources.plagueDoctor12;
                    break;
            }

            List<Ability> abilities = new()
            {
                Ability.Flying,
                Healer.ability
            };

            WstlUtils.Add(
                "wstl_plagueDoctor", "Plague Doctor",
                "A worker of miracles.",
                3, 0, 0, 3,
                resource,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), onePerDeck: true);
        }
    }
}