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
        private void Card_FleshIdol_T0979()
        {
            const string idolName = "Flesh Idol";
            const string fleshIdol = "fleshIdol";
            Tribe[] tribes = new[] { TribeDivine };


            CardInfo fleshIdolGoodCard = NewCard("fleshIdolGood", idolName,
                attack: 0, health: 4, bones: 2, temple: CardTemple.Undead)
                .SetPortraits(fleshIdol)
                .AddAbilities(TeamLeader.ability, TeamLeader.ability, Ability.Transformer)
                .AddTribes(tribes)
                .Build();

            CardInfo fleshIdolCard = NewCard(fleshIdol, idolName, "Prayer inevitably ends with the worshipper's despair.",
                attack: 0, health: 4, bones: 2, temple: CardTemple.Undead)
                .SetPortraits(fleshIdol)
                .AddAbilities(Aggravating.ability, Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(fleshIdolGoodCard, 2)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw);

            fleshIdolGoodCard.SetEvolve(fleshIdolCard, 1);
        }
    }
}