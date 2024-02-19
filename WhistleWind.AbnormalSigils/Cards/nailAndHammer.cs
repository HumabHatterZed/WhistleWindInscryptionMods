using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_NailHammer_O010()
        {
            const string nail = "nail";
            const string hammer = "hammer";
            CreateCard(MakeCard(
                cardName: nail,
                "Nail",
                attack: 1, health: 2)
                .SetPortraits(nail)
                .AddAbilities(Piercing.ability, RightStrike.ability));
            CreateCard(MakeCard(
                cardName: hammer,
                "Hammer",
                attack: 1, health: 2)
                .SetPortraits(hammer)
                .AddAbilities(Ability.MadeOfStone, LeftStrike.ability));
        }
    }
}