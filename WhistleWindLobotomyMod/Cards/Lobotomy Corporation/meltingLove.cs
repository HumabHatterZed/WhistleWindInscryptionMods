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

            CardManager.New(pluginPrefix, meltingLove, "Melting Love",
                attack: 0, health: 5, "Don't let your beasts get too close now.")
                .SetBonesCost(7)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, meltingLove)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddSpecialAbilities(Adoration.specialAbility)
                .AddTraits(Trait.KillsSurvivors, AbnormalPlugin.LovingSlime)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}