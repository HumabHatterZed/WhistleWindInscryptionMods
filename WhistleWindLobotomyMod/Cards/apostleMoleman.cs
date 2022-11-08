using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.CardHelper;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ApostleMoleman_T0346()
        {
            List<Ability> abilities = new()
            {
                Apostle.ability,
                Ability.Reach,
                Ability.WhackAMole
            };
            List<Trait> traits = new()
            {
                Trait.Uncuttable,
                Trait.Terrain
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_apostleMoleman", "Moleman Apostle",
                "The time has come.",
                atk: 1, hp: 8,
                blood: 0, bones: 0, energy: 0,
                Artwork.apostleMoleman, Artwork.apostleMoleman_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: traits,
                appearances: appearances,
                cardType: CardHelper.CardType.Rare, metaTypes: MetaType.NonChoice | MetaType.EventCard);
        }
    }
}