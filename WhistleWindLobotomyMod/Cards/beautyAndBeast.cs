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

            NewCard(beautyAndBeast, "Beauty and the Beast", "A pitiable creature. Death would be a mercy for it.",
                attack: 0, health: 1, blood: 1)
                .SetPortraits(beautyAndBeast)
                .AddAbilities(Cursed.ability)
                .AddTribes(Tribe.Hooved, Tribe.Insect)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth);
        }
    }
}