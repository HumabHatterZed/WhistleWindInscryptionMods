using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ShelterFrom27March_T0982()
        {
            List<Ability> abilities = new()
            {
                Ability.PreventAttack,
                Aggravating.ability,
                GiveSigils.AbilityID
            };
            CreateCard(
                "wstl_shelterFrom27March", "Shelter From the 27th of March",
                "It makes itself the safest place in the world by altering the reality around it.",
                atk: 0, hp: 0,
                blood: 0, bones: 0, energy: 3,
                Artwork.shelterFrom27March, Artwork.shelterFrom27March_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He,
                spellType: SpellType.TargetedStats);
        }
    }
}