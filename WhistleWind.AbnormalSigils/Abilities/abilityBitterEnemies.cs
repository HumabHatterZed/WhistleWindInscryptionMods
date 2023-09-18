using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_BitterEnemies()
        {
            const string rulebookName = "Bitter Enemies";
            const string rulebookDescription = "This card gains 1 Power for each other card on the board that also bears this sigil.";
            const string dialogue = "A bitter grudge laid bare.";

            BitterEnemies.ability = AbnormalAbilityHelper.CreateAbility<BitterEnemies>(
                "sigilBitterEnemies",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class BitterEnemies : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => ActivateOnPlay();
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);

        // Gives +1 Attack to all cards with Bitter Enemies when two or more exist on the board (including the base)
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (!this.Card.OnBoard || target == base.Card || target.Info.LacksAbility(ability))
                return 0;

            List<CardSlot> slotsToCount = BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard).FindAll(x => x.Card != null && x.Card.HasAbility(ability));
            slotsToCount.Remove(base.Card.Slot);

            return slotsToCount.Count > 0 ? 1 : 0;
        }

        public bool ActivateOnPlay()
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot != base.Card.Slot))
            {
                if (slot.Card != null && slot.Card.HasAbility(ability))
                    return true;
            }
            return false;
        }
    }
}
