using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string scorchedGirl = "wstl_scorchedGirl";
        private static void ScorchedGirl_F0102() {
            string name = "Scorched Girl";
            string desc = "Though there's nothing left to burn, the fire won't go out.";
            string textureName = "scorchedGirl";
            CardManager.New(LobotomyPlugin.pluginPrefix, scorchedGirl, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.ExplodeOnDeath)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 1, health: 1, desc)
                .SetBonesCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.ExplodeOnDeath)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Trait.KillsSurvivors)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}