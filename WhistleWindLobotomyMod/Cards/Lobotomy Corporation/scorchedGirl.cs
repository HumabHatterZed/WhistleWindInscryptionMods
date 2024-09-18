using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ScorchedGirl_F0102()
        {
            const string scorchedGirl = "scorchedGirl";
            CardManager.New(pluginPrefix, scorchedGirl, "Scorched Girl",
                attack: 1, health: 1, "Though there's nothing left to burn, the fire won't go out.")
                .SetBonesCost(2)
                .SetPortraits(ModAssembly, scorchedGirl)
                .AddAbilities(Ability.ExplodeOnDeath)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}