using DiskCardGame;
using InscryptionAPI.Card;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangDragon_O07103()
        {
            SpecialTriggeredAbility[] specialAbilities = new[] { DragonHead.specialAbility };

            CardManager.New(pluginPrefix, "yinYangHead", "",
                attack: 0, health: 0)
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHeadBackground.appearance)
                .SetHideStats()
                .Build();

            CardManager.New(pluginPrefix, "yinYangHorns", "",
                attack: 0, health: 0)
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHornsBackground.appearance)
                .SetHideStats()
                .Build();

            CardManager.New(pluginPrefix, "yinYangBody", "",
                attack: 0, health: 0)
                .AddAppearances(DragonBodyBackground.appearance)
                .SetHideStats()
                .Build();
        }
    }
}