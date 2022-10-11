using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_SilentGirl_O010()
        {
            List<Ability> abilities = new()
            {
                Ability.TriStrike
            };
            List<CardHelper.MetaType> metaTypes = new()
            {
                CardHelper.MetaType.RuinaCard
            };

            CardHelper.CreateCard(
                "wstl_silentGirl", "Silent Girl",
                "A girl wielding a hammer and nail.",
                2, 2, 2, 0,
                Artwork.silentGirl, Artwork.silentGirl_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.ChoiceType.Rare, riskLevel: CardHelper.RiskLevel.Teth,
                metaTypes: metaTypes);
        }
    }
}