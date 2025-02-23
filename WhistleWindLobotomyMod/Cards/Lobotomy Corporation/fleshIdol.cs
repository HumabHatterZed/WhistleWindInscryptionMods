using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string fleshIdol = "wstl_fleshIdol";
        public const string fleshIdolGood = "wstl_fleshIdolGood";
        private static void FleshIdol_T0979()
        {
            string idolName = "Flesh Idol";
            string desc = "Prayer inevitably ends with the worshipper's despair.";
            string textureName = "fleshIdol";
            string pixelName2 = "fleshIdolGood";
            Tribe[] tribes = new[] { TribeDivine };

            CardInfo fleshIdolGoodCard = CardManager.New(LobotomyPlugin.pluginPrefix, fleshIdolGood, idolName,
                attack: 0, health: 4)
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TeamLeader.ability, TeamLeader.ability, Ability.Transformer)
                .AddTribes(tribes)
                .Build();

            CardInfo fleshIdolCard = CardManager.New(LobotomyPlugin.pluginPrefix, fleshIdol, idolName,
                attack: 0, health: 4, desc)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Aggravating.ability, Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(fleshIdolGoodCard, 2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            fleshIdolGoodCard.SetEvolve(fleshIdolCard, 1);

            CardInfo fleshIdolGoodCard2 = CardManager.New(LobotomyPlugin.pixelPrefix, pixelName2, idolName,
                attack: 0, health: 4)
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(TeamLeader.ability, TeamLeader.ability, Ability.Transformer)
                .AddTribes(tribes)
                .Build();

            CardInfo fleshIdolCard2 = CardManager.New(LobotomyPlugin.pixelPrefix, textureName, idolName,
                attack: 0, health: 4, desc)
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Aggravating.ability, Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(fleshIdolGoodCard, 2)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);

            fleshIdolGoodCard2.SetEvolve(fleshIdolCard2, 1);
        }
    }
}