using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void JudgementBird_O0262()
        {
            List<Ability> abilities = new()
            {
                Marksman.ability
            };

            List<Tribe> tribes = new()
            {
                Tribe.Bird
            };

            WstlUtils.Add(
                "wstl_judgementBird", "Judgement Bird",
                "A long bird that judges sinners with its tipped scales.",
                1, 1, 2, 0,
                Resources.judgementBird, Resources.judgementBird_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                isChoice: true, onePerDeck: true);
        }
    }
}