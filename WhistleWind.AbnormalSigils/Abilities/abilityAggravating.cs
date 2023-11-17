using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Aggravating()
        {
            const string rulebookName = "Aggravating";
            const string rulebookDescription = "While this card is on the board, all opposing creatures gain 1 Power.";
            const string dialogue = "My beasts are incensed by your creature's presence.";

            Aggravating.ability = AbnormalAbilityHelper.CreateAbility<Aggravating>(
                "sigilAggravating",
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Aggravating : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => ActivateOnPlay();
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            return this.Card.OnBoard && target.OpponentCard != this.Card.OpponentCard && target != base.Card ? 1 : 0;
        }
        public bool ActivateOnPlay()
        {
            if (base.Card.Slot != null)
                return Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard).Where(s => s.Card != null).Count() > 0;

            return false;
        }
    }
}
