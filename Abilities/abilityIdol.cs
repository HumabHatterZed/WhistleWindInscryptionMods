using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Idol()
        {
            const string rulebookName = "Idol";
            const string rulebookDescription = "While this card is on the board, all opposing cards lose 1 Power.";
            const string dialogue = "My beasts defer to you.";

            Idol.ability = AbilityHelper.CreateAbility<Idol>(
                Resources.sigilIdol, Resources.sigilIdol_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Idol : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.5f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.5f);
        }
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            return this.Card.OnBoard && target.OpponentCard != this.Card.OpponentCard && target != base.Card ? -1 : 0;
        }
        public bool ActivateOnPlay()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.Slot.IsPlayerSlot))
            {
                if (slot.Card != null && slot.Card.Attack > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
