using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void TodaysShyLook_O0192()
        {
            List<SpecialAbilityIdentifier> specialAbilities = new()
            {
                TodaysShyLook.GetSpecialAbilityId
            };

            WstlUtils.Add(
                "wstl_todaysShyLook", "Today's Shy Look",
                "A shy one. She dons a different face whenever drawn. Just don't look at her when she does.",
                2, 1, 1, 0,
                Resources.todaysShyLook,
                new List<Ability>(), specialAbilities: specialAbilities,
                new List<Tribe>(), metaCategory: CardMetaCategory.ChoiceNode);
        }
    }
}