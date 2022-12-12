using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentGirl_O010()
        {
            List<Ability> abilities = new()
            {
                Ability.TriStrike
            };
            LobotomyCardHelper.CreateCard(
                "wstl_silentGirl", "Silent Girl",
                "A girl wielding a hammer and nail.",
                atk: 2, hp: 1,
                blood: 2, bones: 0, energy: 0,
                Artwork.silentGirl, Artwork.silentGirl_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Teth,
                modTypes: LobotomyCardHelper.ModCardType.Ruina, customTribe: TribeHumanoid);
        }
    }
}