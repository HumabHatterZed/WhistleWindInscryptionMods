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
        private void Card_JesterOfNihil_O01118()
        {
            const string jesterOfNihil = "jesterOfNihil";

            NewCard(jesterOfNihil, "The Jester of Nihil",
                attack: 0, health: 7, bones: 8)
                .SetPortraits(jesterOfNihil)
                .AddAbilities(ReturnToNihil.ability)
                .AddSpecialAbilities(BoardEffects.specialAbility)
                .SetStatIcon(Nihil.Icon)
                .AddTribes(TribeFae)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetNodeRestrictions(true, false, false, true)
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.Ruina | ModCardType.EventCard);
        }
    }
}