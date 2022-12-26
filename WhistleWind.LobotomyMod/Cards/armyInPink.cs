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
        private void Card_ArmyInPink_D01106()
        {
            List<Ability> abilities = new()
            {
                Protector.ability,
                Ability.MoveBeside
            };
            List<SpecialTriggeredAbility> specialAbilties = new()
            {
                PinkTears.specialAbility
            };
            LobotomyCardHelper.CreateCard(
                "wstl_armyInPink", "Army in Pink",
                "A friendly soldier the colour of the human heart. It will protect you wherever you go.",
                atk: 3, hp: 3,
                blood: 2, bones: 0, energy: 0,
                Artwork.armyInPink, Artwork.armyInPink_emission,
                abilities: abilities, specialAbilities: specialAbilties,
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: LobotomyCardHelper.RiskLevel.Aleph,
                modTypes: LobotomyCardHelper.ModCardType.Donator);
        }
    }
}