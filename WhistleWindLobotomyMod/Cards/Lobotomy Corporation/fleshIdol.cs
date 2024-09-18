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


            CardInfo fleshIdolGoodCard = CardManager.New(pluginPrefix, "fleshIdolGood", idolName,
                attack: 0, health: 4)
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, fleshIdol)
                .AddAbilities(TeamLeader.ability, TeamLeader.ability, Ability.Transformer)
                .AddTribes(tribes)
                .Build();

            CardInfo fleshIdolCard = CardManager.New(pluginPrefix, fleshIdol, idolName,
                attack: 0, health: 4, "Prayer inevitably ends with the worshipper's despair.")
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, fleshIdol)
                .AddAbilities(Aggravating.ability, Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(fleshIdolGoodCard, 2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);

            fleshIdolGoodCard.SetEvolve(fleshIdolCard, 1);
        }
    }
}