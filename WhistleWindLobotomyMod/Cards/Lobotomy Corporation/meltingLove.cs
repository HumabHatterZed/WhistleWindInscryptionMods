using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string meltingLove = "wstl_meltingLove";
        private static void MeltingLove_D03109() {
            string name = "Melting Love";
            string desc = "Don't let your beasts get too close now.";
            string textureName = "meltingLove";
            CardManager.New(LobotomyPlugin.pluginPrefix, meltingLove, name,
                attack: 0, health: 5, desc)
                .SetBonesCost(7)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddSpecialAbilities(Adoration.specialAbility)
                .AddTraits(Trait.KillsSurvivors, AbnormalPlugin.LovingSlime)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph);

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 0, health: 5, desc)
                .SetBonesCost(7)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .SetStatIcon(SlimeIcon.Icon)
                .AddAbilities(Slime.ability)
                .AddSpecialAbilities(Adoration.specialAbility)
                .AddTraits(Trait.KillsSurvivors, AbnormalPlugin.LovingSlime)
                .AddMetaCategories(DonatorCard)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);
        }
    }
}