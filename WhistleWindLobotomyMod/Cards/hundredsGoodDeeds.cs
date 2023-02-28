using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HundredsGoodDeeds_O0303()
        {
            List<Ability> abilities = new()
            {
                Confession.ability
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhiteEmission.appearance
            };
            CreateCard(
                "wstl_hundredsGoodDeeds", "One Sin and Hundreds of Good Deeds",
                "Its hollow sockets see through you.",
                atk: 0, hp: 77,
                blood: 0, bones: 0, energy: 0,
                Artwork.hundredsGoodDeeds, Artwork.hundredsGoodDeeds_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard,
                hideStats: true);
        }
    }
}