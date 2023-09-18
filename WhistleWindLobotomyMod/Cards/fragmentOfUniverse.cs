using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_FragmentOfUniverse_O0360()
        {
            const string fragmentOfUniverse = "fragmentOfUniverse";

            NewCard(fragmentOfUniverse, "Fragment of the Universe", "You see a song in front of you. It's approaching, becoming more colourful by the second.",
                attack: 1, health: 2, blood: 1)
                .SetPortraits(fragmentOfUniverse)
                .AddAbilities(Piercing.ability)
                .AddTribes(TribeDivine)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}