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
                attack: 0, health: 5, bones: 5, temple: CardTemple.Undead)
                .SetPortraits(meltingLove)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddSpecialAbilities(Adoration.specialAbility)
                .AddTraits(Trait.KillsSurvivors, AbnormalPlugin.LovingSlime)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph, ModCardType.Donator);
        }
    }
}