using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ArmyInBlack_D01106()
        {
            CreateCard(
                "wstl_armyInBlack", "Army in Black",
                "Duty-bound.",
                atk: 3, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.armyInBlack, Artwork.armyInBlack_emission,
                abilities: new() { Volatile.ability, Ability.Brittle }, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice);

            CreateCard(
                "wstl_armyInBlackSpell", "Army in Black",
                "Duty-bound.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 0,
                Artwork.armyInBlack, Artwork.armyInBlack_emission,
                abilities: new() { Volatile.ability }, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(),
                choiceType: CardHelper.CardChoiceType.Rare, metaTypes: CardHelper.CardMetaType.NonChoice,
                spellType: SpellType.Targeted);
        }
    }
}