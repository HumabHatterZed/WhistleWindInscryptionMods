﻿using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_NamelessFetus_O0115()
        {
            const string fetusName = "Nameless Fetus";
            const string namelessFetus = "namelessFetus";
            const string namelessFetusAwake = "namelessFetusAwake";
            Tribe[] tribes = new[] { TribeAnthropoid };

            NewCard(namelessFetusAwake, displayName: fetusName,
                attack: 0, health: 1, bones: 3)
                .SetPortraits(namelessFetusAwake)
                .AddAbilities(Aggravating.ability, Ability.PreventAttack, Ability.Sacrificial)
                .AddTribes(tribes)
                .Build();

            NewCard(namelessFetus, fetusName, "A neverending supply of blood. Just don't wake it up.",
                attack: 0, health: 1, bones: 3)
                .SetPortraits(namelessFetus)
                .AddAbilities(Ability.TripleBlood, Ability.Sacrificial)
                .AddSpecialAbilities(Syrinx.specialAbility)
                .AddTribes(tribes)
                .AddTraits(Trait.Goat)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.He);
        }
    }
}