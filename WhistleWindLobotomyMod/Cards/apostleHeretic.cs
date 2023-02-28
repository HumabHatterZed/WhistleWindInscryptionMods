using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleHeretic_T0346()
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
                "wstl_apostleHeretic", "Heretic",
                "The time has come.",
                atk: 0, hp: 7,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleHeretic, Artwork.apostleHeretic_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances, modTypes: ModCardType.EventCard);
        }
    }
}