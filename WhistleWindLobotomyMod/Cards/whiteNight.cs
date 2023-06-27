using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
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

            CardInfo whiteNightCard = NewCard(
                whiteNight,
                "WhiteNight",
                "The time has come.",
                attack: 0, health: 66)
                .SetPortraits(whiteNight, titleName: "whiteNight_title")
                .AddAbilities(Ability.Flying, Idol.ability, TrueSaviour.ability)
                .AddTribes(TribeDivine)
                .AddTraits(ImmuneToInstaDeath, Trait.Uncuttable, TraitApostle)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetOnePerDeck();

            CreateCard(whiteNightCard, CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard);
        }
    }
}