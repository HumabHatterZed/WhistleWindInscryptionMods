using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WhiteNight_T0346()
        {
            const string whiteNight = "whiteNight";

            NewCard(whiteNight, "WhiteNight", "The time has come.",
                attack: 0, health: 66)
                .SetPortraits(whiteNight, titleName: "whiteNight_title")
                .AddAbilities(Ability.Flying, Idol.ability, TrueSaviour.ability)
                .AddTribes(TribeDivine)
                .AddTraits(ImmuneToInstaDeath, Trait.Uncuttable)
                .AddAppearances(ForcedWhiteEmission.appearance, CardAppearanceBehaviour.Appearance.TerrainLayout)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard);
        }
    }
}