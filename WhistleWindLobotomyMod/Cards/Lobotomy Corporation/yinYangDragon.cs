using DiskCardGame;
using InscryptionAPI.Card;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string yinYangHead = "wstl_yinYangHead";
        public const string yinYangHorns = "wstl_yinYangHorns";
        public const string yinYangBody = "wstl_yinYangBody";
        private static void YinYangDragon_O07103() {
            SpecialTriggeredAbility[] specialAbilities = new[] { DragonHead.specialAbility };

            CardManager.New(LobotomyPlugin.pluginPrefix, yinYangHead, string.Empty,
                attack: 0, health: 0)
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHeadBackground.appearance)
                .SetHideStats()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, yinYangHorns, string.Empty,
                attack: 0, health: 0)
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHornsBackground.appearance)
                .SetHideStats()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, yinYangBody, string.Empty,
                attack: 0, health: 0)
                .AddAppearances(DragonBodyBackground.appearance)
                .SetHideStats()
                .Build();
        }
    }
}