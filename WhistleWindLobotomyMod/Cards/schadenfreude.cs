using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Schadenfreude_O0576()
        {
            List<Ability> abilities = new() { Ability.Sniper };
            List<Tribe> tribes = new() { TribeMechanical };

            CreateCard(
                "wstl_schadenfreude", "Schadenfreude",
                "A strange machine. You can feel someone's persistent gaze through the keyhole.",
                atk: 1, hp: 1,
                blood: 0, bones: 0, energy: 2,
                Artwork.schadenfreude, Artwork.schadenfreude_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.He);
        }
    }
}