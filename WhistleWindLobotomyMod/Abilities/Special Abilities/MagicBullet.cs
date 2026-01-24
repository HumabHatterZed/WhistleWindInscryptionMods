using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionCommunityPatch.Card;
using System.Collections;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class RulebookEntryMagicBullet : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities {
        private static void Rulebook_MagicBullet() {
            const string rName = "Magic Bullet";
            const string rDesc = "Der Freischutz gains 2 Power whenever triggering Gun For Hire, until its next attack ends. On the 7th activation, it will attack a random space on the board.";
            RulebookEntryMagicBullet.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMagicBullet>(rName, rDesc).Id;
        }
    }
}
