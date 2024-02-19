using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Idol()
        {
            const string rulebookName = "Idol";
            const string rulebookDescription = "While this card is on the board, all opposing creatures lose 1 Power.";
            const string dialogue = "My beasts defer to you.";

            Idol.ability = AbnormalAbilityHelper.CreateAbility<Idol>(
                "sigilIdol",
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class Idol : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.OpponentCard != base.Card.OpponentCard;

        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.5f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.5f);
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (this.Card.OnBoard && target.OpponentCard != base.Card.OpponentCard)
                return target.LacksAbility(Ability.MadeOfStone) ? -base.Card.GetAbilityStacks(Ability) : 0;

            return 0;
        }
        public bool ActivateOnPlay()
        {
            if (base.Card.OnBoard)
                return Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard)
                    .Where(s => s.Card != null && s.Card.Info.Attack > 0).Count() > 0;

            return false;
        }
    }
}
