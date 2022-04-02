using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void MagicalGirlHeart_O0104()
        {
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                MagicalGirlHeart.specialAbility
            };

            WstlUtils.Add(
                "wstl_magicalGirlHeart", "Magical Girl",
                "A hero of love and justice, she will aid you on your journey.",
                2, 2, 1, 0,
                Resources.magicalGirlHeart, Resources.magicalGirlHeart_emission,
                abilities: new(), specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                isChoice: true, onePerDeck: true);
        }
    }
}