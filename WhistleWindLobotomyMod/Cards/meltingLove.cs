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
        private void Card_MeltingLove_D03109()
        {
            List<Ability> abilities = new() { Slime.ability };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Adoration.specialAbility
            };
            List<Tribe> tribes = new() { TribeAnthropoid };
            List<Trait> traits = new() { Trait.KillsSurvivors };

            CreateCard(
                "wstl_meltingLove", "Melting Love",
                "Don't let your beasts get too close now.",
                atk: 4, hp: 2,
                blood: 3, bones: 0, energy: 0,
                Artwork.meltingLove, Artwork.meltingLove_emission,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: tribes, traits: traits,
                choiceType: CardHelper.CardChoiceType.Rare, riskLevel: RiskLevel.Aleph,
                modTypes: ModCardType.Donator);
        }
    }
}