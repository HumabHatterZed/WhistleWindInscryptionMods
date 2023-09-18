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

            NewCard("yinYangHead")
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHeadBackground.appearance)
                .SetHideStats()
                .Build();

            NewCard("yinYangHorns")
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHornsBackground.appearance)
                .SetHideStats()
                .Build();

            NewCard("yinYangBody")
                .AddAppearances(DragonBodyBackground.appearance)
                .SetHideStats()
                .Build();
        }
    }
}