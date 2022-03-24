using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SingingMachine_O0530()
        {
            List<Ability> abilities = new()
            {
                TeamLeader.ability,
                Aggravating.ability
            };

            WstlUtils.Add(
                "wstl_singingMachine", "Singing Machine",
                "A wind-up music machine. The song it plays is to die for.",
                0, 4, 2, 0,
                Resources.singingMachine, Resources.singingMachine,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}