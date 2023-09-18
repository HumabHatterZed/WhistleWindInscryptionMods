using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_MeltingLove_D03109()
        {
            const string meltingLove = "meltingLove";

            NewCard(meltingLove, "Melting Love", "Don't let your beasts get too close now.",
                attack: 4, health: 3, blood: 3)
                .SetPortraits(meltingLove)
                .AddAbilities(Slime.ability)
                .AddSpecialAbilities(Adoration.specialAbility)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph, ModCardType.Donator);
        }
    }
}