using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Pinocchio_F01112()
        {
            List<Ability> abilities = new()
            {
                Copycat.ability
            };
            LobotomyCardHelper.CreateCard(
                "wstl_pinocchio", "Pinocchio",
                "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                atk: 0, hp: 0,
                blood: 0, bones: 1, energy: 0,
                Artwork.pinocchio, Artwork.pinocchio_emission,
                abilities: abilities, specialAbilities: new(),
                metaCategories: new(), tribes: new(), traits: new(),
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: LobotomyCardHelper.RiskLevel.Teth,
                 modTypes: LobotomyCardHelper.ModCardType.Ruina, customTribe: TribeHumanoid);
        }
    }
}