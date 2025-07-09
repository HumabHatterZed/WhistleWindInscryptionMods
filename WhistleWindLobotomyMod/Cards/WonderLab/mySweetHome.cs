using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string mySweetHome = "wstlWonder_mySweetHome";
        private static void MySweetHome() {
            string textureName = "mySweetHome";
            CardManager.New(LobotomyPlugin.wonderlabPrefix, mySweetHome, "My Sweet Home",
                attack: 0, health: 0)
                .SetBonesCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.MadeOfStone, Ability.Reach, GiveSigils.AbilityID)
                .SetSpellType(SpellType.TargetedSigils)
                .Build(CardHelper.CardType.Common, RiskLevel.Teth, true);
        }
    }
}