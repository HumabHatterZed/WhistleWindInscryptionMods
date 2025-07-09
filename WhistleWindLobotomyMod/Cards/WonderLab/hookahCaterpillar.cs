using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string hookahCaterpillar = "wstlWonder_hookahCaterpillar";
        public const string hookahButterfly = "wstlWonder_hookahButterfly";
        private static void HookahCaterpillar() {
            string textureName = "hookahButterfly";
            string textureName2 = "hookahCaterpillar";
            CardInfo butterfly = CardManager.New(LobotomyPlugin.wonderlabPrefix, hookahButterfly, "Hookah Butterfly",
                attack: 2, health: 2)
                .SetEnergyCost(4).SetBonesCost(8)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(ReturnToNihil.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.wonderlabPrefix, hookahCaterpillar, "Hookah Caterpillar",
                attack: 0, health: 2, "A gluttonous worm, it fattens itself on doubt and despair.")
                .SetEnergyCost(2).SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Scorching.ability, Ability.Evolve)
                .SetEvolve(butterfly, 2)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}