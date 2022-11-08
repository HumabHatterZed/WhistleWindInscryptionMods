using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
            CardHelper.CreateCard(
                "wstl_silentGirl", "Silent Girl",
                "A girl wielding a hammer and nail.",
                atk: 2, hp: 2,
                blood: 2, bones: 0, energy: 0,
                Artwork.silentGirl, Artwork.silentGirl_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                cardType: CardHelper.CardType.Rare, riskLevel: CardHelper.RiskLevel.Teth,
                metaTypes: CardHelper.MetaType.Ruina);
        }
    }
}