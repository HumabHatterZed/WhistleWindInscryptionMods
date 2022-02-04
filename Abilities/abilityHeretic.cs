using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Heretic()
        {
            const string rulebookName = "Heretic";
            const string rulebookDescription = "Have I not chosen you, the Twelve? Yet one of you is a devil.";
            const string dialogue = "";

            return WstlUtils.CreateAbility<Heretic>(
                Resources.sigilHeretic,
                rulebookName, rulebookDescription, dialogue, 0);
        }
    }
    public class Heretic : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public CardModificationInfo mod = new CardModificationInfo(-1, 0);

        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();

            var slotsWithPCards = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card);
            var slotsWithLCards = Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot && slot.Card);

            if (base.Card.Slot.IsPlayerSlot) // If base Card is player-owned
            {
                foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
                {
                    slot.Card.AddTemporaryMod(mod);
                    yield return base.LearnAbility(0.25f);
                }
            }
            else if (!base.Card.Slot.IsPlayerSlot) // if base card is Leshy-owned
            {
                foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
                {
                    slot.Card.AddTemporaryMod(mod);
                }
            }

            yield break;
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null)
            {
                if ((otherCard.Slot.IsPlayerSlot && !base.Card.Slot.IsPlayerSlot) || (!otherCard.Slot.IsPlayerSlot && base.Card.Slot.IsPlayerSlot))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();

            var slotsWithPCards = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card);
            var slotsWithLCards = Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot && slot.Card);

            foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
            {
                slot.Card.RemoveTemporaryMod(mod);
            }
            foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
            {
                slot.Card.RemoveTemporaryMod(mod);
            }

            if (!otherCard.Slot.IsPlayerSlot && base.Card.Slot.IsPlayerSlot) // If base Card is player-owned, and card played is not
            {
                foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
                {
                    slot.Card.AddTemporaryMod(mod);
                }
            }
            else if (otherCard.Slot.IsPlayerSlot && !base.Card.Slot.IsPlayerSlot) // if base card is Leshy-owned and card played is not
            {
                foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
                {
                    slot.Card.AddTemporaryMod(mod);
                }
            }

            yield return base.LearnAbility(0.25f);
            yield break;
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return true;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();

            var slotsWithPCards = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card);
            var slotsWithLCards = Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot && slot.Card);

            if (base.Card.Slot.IsPlayerSlot) // Player-owned
            {
                foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
                {
                    slot.Card.RemoveTemporaryMod(mod);  // remove mod from player-owned cards, if that's somehow applicable (hook then hook back, I guess)
                }
                foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
                {
                    slot.Card.RemoveTemporaryMod(mod);
                    slot.Card.AddTemporaryMod(mod);
                }
            }
            else if (!base.Card.Slot.IsPlayerSlot) // Leshy-owned
            {
                foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
                {
                    slot.Card.RemoveTemporaryMod(mod);
                }
                foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
                {
                    slot.Card.RemoveTemporaryMod(mod);
                    slot.Card.AddTemporaryMod(mod);
                }
                yield return new WaitForSeconds(0.15f);
            }

            yield break;
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            var slotsWithPCards = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card);
            var slotsWithLCards = Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot && slot.Card);

            foreach (var slot in slotsWithPCards.Where(slot => slot.Card))
            {
                slot.Card.RemoveTemporaryMod(mod);
            }
            foreach (var slot in slotsWithLCards.Where(slot => slot.Card))
            {
                slot.Card.RemoveTemporaryMod(mod);
            }

            yield break;
        }

        public bool ActivateOnPlay()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(false))
            {
                if (slot.Card != null)
                {
                    num++;
                }
            }

            return num > 0;
        }
    }
}
