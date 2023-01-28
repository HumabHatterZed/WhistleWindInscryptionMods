using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;
using WhistleWind.LobotomyMod.Properties;

namespace WhistleWind.LobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Aggravating.ability,
                TargetGainSigils.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 2,
                Artwork.shelterFrom27March, Artwork.shelterFrom27March_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.He,
                spellType: LobotomyCardHelper.SpellType.TargetedStats);
        }
    }
}