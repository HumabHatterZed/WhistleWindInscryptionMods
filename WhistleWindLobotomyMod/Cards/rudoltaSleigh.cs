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
        private void Card_Rudolta_F0249()
        {
            const string sleighName = "Rudolta of the Sleigh";
            const string rudoltaSleigh = "rudoltaSleigh";
            Ability[] abilities = new[] { Ability.Strafe, GiftGiver.ability };
            Tribe[] tribes = new[] { Tribe.Hooved };

            NewCard(rudoltaSleigh, sleighName, "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.",
                attack: 2, health: 3, blood: 2)
                .SetPortraits(rudoltaSleigh)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);

            NewCard("RUDOLTA_MULE", sleighName,
                attack: 2, health: 3, blood: 2)
                .SetPortraits(rudoltaSleigh)
                .AddAbilities(abilities)
                .AddSpecialAbilities(SpecialTriggeredAbility.PackMule)
                .AddTribes(tribes)
                .AddTraits(Trait.Uncuttable)
                .Build();
        }
    }
}