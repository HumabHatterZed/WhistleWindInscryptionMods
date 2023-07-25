using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApocalypseBird_O0263()
        {
            const string apocalypseBird = "apocalypseBird";
            NewCard(apocalypseBird, "Apocalypse Bird",
                attack: 3, health: 9, blood: 4)
                .SetPortraits(apocalypseBird)
                .AddAbilities(Ability.AllStrike, Ability.SplitStrike, Ability.MadeOfStone)
                .AddSpecialAbilities(BoardEffects.specialAbility)
                .AddTribes(Tribe.Bird)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetNodeRestrictions(true, false, false, true)
                .SetDefaultEvolutionName("Greater Apocalypse Bird")
                .SetOnePerDeck()
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard);
        }
    }
}