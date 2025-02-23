using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string hookahCaterpillar = "wstl_hookahCaterpillar";
        public const string hookahButterfly = "wstl_hookahButterfly";
        private static void HookahCaterpillar()
        {
            return;
            string textureName = "hookahButterfly";
            string textureName2 = "hookahCaterpillar";
            CardInfo butterfly = CardManager.New(LobotomyPlugin.wonderlabPrefix, hookahButterfly, "Hookah Butterfly",
                attack: 2, health: 3)
                .SetEnergyCost(6)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(ReturnToNihil.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.wonderlabPrefix, hookahCaterpillar, "Hookah Caterpillar",
                attack: 0, health: 3)
                .SetEnergyCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Scorching.ability, Ability.Evolve)
                .SetEvolve(butterfly, 2)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}