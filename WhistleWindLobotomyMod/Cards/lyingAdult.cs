using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.ComponentModel;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_AdultWhoTellsLies_F01117()
        {
            const string lyingAdult = "lyingAdult";

            NewCard(lyingAdult, "The Adult Who Tells Lies",
                attack: 1, health: 5, blood: 2)
                .SetPortraits(lyingAdult)
                .AddAbilities(FalseThrone.ability)
                .AddSpecialAbilities(BoardEffects.specialAbility)
                .AddTribes(TribeAnthropoid)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.Ruina | ModCardType.EventCard);
        }
    }
}