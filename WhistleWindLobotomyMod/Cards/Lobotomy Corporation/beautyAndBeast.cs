using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_BeautyAndBeast_O0244()
        {
            const string beautyAndBeast = "beautyAndBeast";

            CardManager.New(pluginPrefix, beautyAndBeast, "Beauty and the Beast",
                attack: 0, health: 1, "A pitiable creature. Death would be a mercy for it.")
                .SetBonesCost(1)
                .SetPortraits(ModAssembly, beautyAndBeast)
                .AddAbilities(Cursed.ability)
                .AddTribes(Tribe.Hooved, Tribe.Insect)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}