using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using UnityEngine.Animations;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_HookahCaterpillar()
        {
            const string hookahCaterpillar = "hookahCaterpillar", hookahButterfly = "hookahButterfly";

            CardInfo butterfly = CardManager.New(wonderlabPrefix, hookahButterfly, "Hookah Butterfly",
                attack: 2, health: 3)
                .SetEnergyCost(6)
                .SetPortraits(ModAssembly, hookahButterfly)
                .AddAbilities(ReturnToNihil.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(wonderlabPrefix, hookahCaterpillar, "Hookah Caterpillar",
                attack: 0, health: 3)
                .SetEnergyCost(3)
                .SetPortraits(ModAssembly, hookahCaterpillar)
                .AddAbilities(Scorching.ability, Ability.Evolve)
                .SetEvolve(butterfly, 2)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.CardType.Rare, RiskLevel.Waw, true);
        }
    }
}