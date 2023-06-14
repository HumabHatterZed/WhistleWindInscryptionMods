using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangDragon_O07103()
        {
            SpecialTriggeredAbility[] specialAbilities = new[] { DragonHead.specialAbility };

            CardInfo yinYangHeadCard = NewCard(
                "yinYangHead")
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHeadBackground.appearance)
                .SetHideStats();

            CardInfo yinYangHornsCard = NewCard(
                "yinYangHorns")
                .AddSpecialAbilities(specialAbilities)
                .AddAppearances(DragonHornsBackground.appearance)
                .SetHideStats();

            CardInfo yinYangBodyCard = NewCard(
                "yinYangBody")
                .AddAppearances(DragonBodyBackground.appearance)
                .SetHideStats();

            CreateCards(yinYangHeadCard, yinYangHornsCard, yinYangBodyCard);
        }
    }
}