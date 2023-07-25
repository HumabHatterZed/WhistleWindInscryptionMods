using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ScaredyCat_F02115()
        {
            const string catName = "Scaredy Cat";
            const string scaredyCat = "scaredyCat";
            const string scaredyCatStrong = "scaredyCatStrong";
            Trait[] traits = new[] { TraitEmeraldCity };
            SpecialTriggeredAbility[] specialAbilities = new[] { Cowardly.specialAbility };

            NewCard(scaredyCatStrong, catName,
                attack: 2, health: 6, blood: 2)
                .SetPortraits(scaredyCatStrong)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .Build(cardType: ModCardType.Ruina);

            NewCard(scaredyCat, catName,
                attack: 0, health: 1, blood: 1)
                .SetPortraits(scaredyCat)
                .AddSpecialAbilities(specialAbilities)
                .AddTraits(traits)
                .Build(cardType: ModCardType.Ruina);
        }
    }
}