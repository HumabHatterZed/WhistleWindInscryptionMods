using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ArmyInBlack_D01106()
        {
            List<Ability> abilities = new()
            {
                Volatile.ability,
                Ability.Brittle
            };
            LobotomyCardHelper.CreateCard(
                "wstl_armyInBlack", "Army in Black",
                "Duty-bound.",
                atk: 2, hp: 1,
                blood: 0, bones: 0, energy: 0,
                Artwork.armyInBlack, Artwork.armyInBlack_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice);
        }
    }
}