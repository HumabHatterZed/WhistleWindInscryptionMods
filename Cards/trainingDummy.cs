using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void TrainingDummy_00000()
        {
            WstlUtils.Add(
                "wstl_trainingDummy", "Standard Training-Dummy Rabbit",
                "A creature in the shape of a training dummy. Perhaps the safest of these strange beasts.",
                0, 1, 0, 1,
                Resources.trainingDummy, Resources.trainingDummy_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true);
        }
    }
}