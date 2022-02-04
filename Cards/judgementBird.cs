using System.Collections.Generic;
using DiskCardGame;
using APIPlugin;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private void JudgementBird_O0262()
        {
            List<Ability> abilities = new List<Ability>
            {
                Ability.Sniper,
                Ability.Deathtouch
            };

            List<Tribe> tribes = new List<Tribe>
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_judgementBird", "Judgement Bird",
                "A judge whose scales are always tipped to one side.",
                1, 1, 2, 0,
                Resources.judgementBird,
                abilities: abilities, new List<SpecialAbilityIdentifier>(),
                tribes: tribes, metaCategory: CardMetaCategory.Rare,
                appearanceBehaviour: CardUtils.getRareAppearance, onePerDeck: true);
        }
    }
}