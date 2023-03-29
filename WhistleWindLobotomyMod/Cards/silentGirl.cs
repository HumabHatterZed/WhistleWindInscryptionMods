using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentGirl_O010()
        {
            List<Ability> abilities = new() { Ability.TriStrike };
            List<Tribe> tribes = new() { TribeAnthropoid };

            CreateCard(
                "wstl_silentGirl", "Silent Girl",
                "A girl wielding a hammer and nail.",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.silentGirl, Artwork.silentGirl_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Teth,
                modTypes: ModCardType.Ruina);
        }
    }
}