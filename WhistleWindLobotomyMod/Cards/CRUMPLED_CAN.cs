using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Crumpled_Can()
        {
            CardHelper.CreateCard(
                "wstl_CRUMPLED_CAN", "Crumpled Can of WellCheers",
                "Soda can can soda dota 2 electric boo.",
                atk: 0, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.skeleton_can, Artwork.skeleton_can_emission,
                abilities: new(), specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new());
        }
    }
}