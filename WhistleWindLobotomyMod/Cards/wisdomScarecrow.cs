using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WisdomScarecrow_F0187()
        {
            List<Ability> abilities = new() { Bloodfiend.ability };
            List<Tribe> tribes = new() { TribeAnthropoid, TribeBotanic };
            List<Trait> traits = new() { TraitEmeraldCity };

            CreateCard(
                "wstl_wisdomScarecrow", "Scarecrow Searching for Wisdom",
                "A hollow-headed scarecrow. Blood soaks its straw limbs.",
                atk: 1, hp: 2,
                blood: 0, bones: 4, energy: 0,
                Artwork.wisdomScarecrow, Artwork.wisdomScarecrow_emission, pixelTexture: Artwork.wisdomScarecrow_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He); ;
        }
    }
}