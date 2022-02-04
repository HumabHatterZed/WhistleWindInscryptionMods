using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void ExpressHellTrain_T0986()
        {
            List<Ability> abilities = new List<Ability>
            {
                TheTrain.ability
            };

            WstlUtils.Add(
                "wstl_expressHellTrain", "Express Train to Hell",
                "When the time comes, the train will sound its mighty horn.",
                4, 0, 0, 7,
                Resources.expressHellTrain,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                new List<Tribe>(), metaCategory: CardMetaCategory.Rare,
                emissionTexture: Resources.expressHellTrain_emission,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}