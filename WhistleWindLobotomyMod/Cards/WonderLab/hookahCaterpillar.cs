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
            const string hookahCaterpillar = "hookahCaterpillar";
            const string hookahButterfly = "hookahButterfly";

            CardInfo butterfly = NewCard(hookahButterfly, "Hookah Butterfly",
                attack: 2, health: 3, energy: 6)
                .SetPortraits(hookahButterfly)
                .AddAbilities(ReturnToNihil.ability)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.ChoiceType.Rare, nonChoice: true);

            NewCard(hookahCaterpillar, "Hookah Caterpillar",
                attack: 0, health: 3, energy: 3)
                .SetPortraits(hookahCaterpillar)
                .AddAbilities(Scorching.ability, Ability.Evolve)
                .SetEvolve(butterfly, 2)
                .AddTribes(Tribe.Insect)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Waw, ModCardType.WonderLab);
        }
    }
}