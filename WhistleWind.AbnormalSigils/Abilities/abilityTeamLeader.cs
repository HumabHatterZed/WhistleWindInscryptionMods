using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_TeamLeader()
        {
            const string rulebookName = "Team Leader";
            const string rulebookDescription = "While this card is on the board, all ally cards gain 1 Power.";
            const string dialogue = "Your beast emboldens its allies.";

            TeamLeader.ability = AbnormalAbilityHelper.CreateAbility<TeamLeader>(
                Artwork.sigilTeamLeader, Artwork.sigilTeamLeader_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class TeamLeader : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // Check if there are already other cards on the board
        // Used for LearnAbility dialogue
        public override bool RespondsToResolveOnBoard()
        {
            if (base.Card.Slot != null)
                return Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(s => s.Card != null).Count() > 0;

            return false;
        }
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);

        // Respond to other card resolving if it and this card are on the player's side of the board and
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (base.Card.OnBoard)
                return !base.Card.OpponentCard && base.Card.OpponentCard == otherCard.OpponentCard;

            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);

        // Gives +1 Power if on board and target card is on same side of the board
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            return this.Card.OnBoard && target.OpponentCard == this.Card.OpponentCard && target != base.Card ? 1 : 0;
        }
    }
}
