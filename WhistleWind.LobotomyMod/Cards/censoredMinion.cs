using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_CENSOREDMinion_O0389()
        {
            LobotomyCardHelper.CreateCard(
                "wstl_censoredMinion", "CENSORED",
                "I think it's best you don't know what it looks like.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.censoredMinion, Artwork.censoredMinion_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}
