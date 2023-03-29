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
        private void Card_Pinocchio_F01112()
        {
            List<Ability> abilities = new() { Copycat.ability };
            List<Tribe> tribes = new() { TribeAnthropoid, TribeBotanic };

            CreateCard(
                "wstl_pinocchio", "Pinocchio",
                "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                atk: 0, hp: 1,
                blood: 0, bones: 1, energy: 0,
                Artwork.pinocchio, Artwork.pinocchio_emission, Artwork.pinocchio_pixel,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: tribes, traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Teth,
                modTypes: ModCardType.Ruina);
        }
    }
}