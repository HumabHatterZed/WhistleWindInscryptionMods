using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void YoureBald_BaldIsAwesome()
        {
            List<Ability> abilities = new()
            {
                Ability.DrawCopy
            };
            CardHelper.CreateCard(
                "wstl_youreBald", "You're Bald...",
                "I've always wondered what it was like to be bald.",
                1, 1, 0, 3,
                Resources.youreBald, Resources.youreBald_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(), riskLevel: 1);
        }
    }
}