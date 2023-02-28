using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_YinYangDragon_O07103()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                DragonHead.specialAbility
            };
            CreateCard(
                "wstl_yinYangHead", "", "",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                portrait: null,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new() { DragonHeadBackground.appearance }, hideStats: true);
            CreateCard(
                "wstl_yinYangHorns", "", "",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                portrait: null,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new() { DragonHornsBackground.appearance }, hideStats: true);
            CreateCard(
                "wstl_yinYangBody", "", "",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                portrait: null,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new() { DragonBodyBackground.appearance }, hideStats: true);
        }
    }
}