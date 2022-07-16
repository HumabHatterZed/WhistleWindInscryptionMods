using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void WallLady_F0118()
        {
            List<Ability> abilities = new()
            {
                Punisher.ability
            };
            List<Trait> traits = new()
            {
                Trait.KillsSurvivors
            };
            CardHelper.CreateCard(
                "wstl_wallLady", "The Lady Facing the Wall",
                "A deep sorrow, grown to obsession. Perhaps it's best to leave her be.",
                0, 2, 0, 4,
                Resources.wallLady, Resources.wallLady_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                isChoice: true, riskLevel: 2);
        }
    }
}