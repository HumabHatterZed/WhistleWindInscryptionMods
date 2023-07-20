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
        private void Card_ArmyInBlack_D01106()
        {
            const string blackName = "Army in Black";
            const string armyInBlack = "armyInBlack";

            NewCard(armyInBlack, blackName,
                attack: 3, health: 3, blood: 2)
                .SetPortraits(armyInBlack)
                .AddAbilities(Volatile.ability, Ability.Brittle)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard("armyInBlackSpell", blackName)
                .SetPortraits(armyInBlack)
                .AddAbilities(Volatile.ability)
                .SetSpellType(SpellType.Targeted)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);
        }
    }
}