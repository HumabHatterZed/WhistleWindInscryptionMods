using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Slime()
        {
            const string rulebookName = "Made of Slime";
            const string rulebookDescription = "If this card is not a Slime, take 1 damage at turn's end and transform into a Slime on death. Adjacent cards gain this sigil at the end of the owner's turn.";
            const string dialogue = "Its army grows everyday.";
            const string triggerText = "[creature] melts into slime!";
            Slime.ability = AbnormalAbilityHelper.CreateAbility<Slime>(
                "sigilSlime",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Slime : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // infect adjacent cards
            CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true);
            CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false);

            bool leftValid = CheckIsValid(leftSlot);
            bool rightValid = CheckIsValid(rightSlot);

            if (leftValid)
            {
                bool faceDown = leftSlot.Card.FaceDown;
                yield return leftSlot.Card.FlipFaceUp(faceDown);
                leftSlot.Card.Anim.StrongNegationEffect();
                leftSlot.Card.AddTemporaryMod(new(this.Ability) { nonCopyable = true });
                yield return new WaitForSeconds(0.6f);
                yield return leftSlot.Card.TakeDamage(1, null);
                yield return new WaitForSeconds(0.4f);
                yield return leftSlot.Card.FlipFaceDown(faceDown, rightValid ? 0.1f : 0.3f);
            }
            if (rightValid)
            {
                bool faceDown = rightSlot.Card.FaceDown;
                yield return rightSlot.Card.FlipFaceUp(faceDown);
                rightSlot.Card.Anim.StrongNegationEffect();
                rightSlot.Card.AddTemporaryMod(new(this.Ability) { nonCopyable = true });
                yield return new WaitForSeconds(0.6f);
                yield return rightSlot.Card.FlipFaceDown(faceDown);
            }

            yield return base.LearnAbility(0.4f);

            if (base.Card.LacksTrait(AbnormalPlugin.LovingSlime))
                yield return base.Card.TakeDamage(1, null);
        }

        private bool CheckIsValid(CardSlot slot)
        {
            if (slot?.Card != null && slot.Card.LacksAbility(this.Ability) && slot.Card.LacksAllTraits(Trait.Pelt, Trait.Uncuttable, Trait.Giant))
            {
                return slot.Card.LacksTrait(AbnormalPlugin.LovingSlime);
            }
            return false;
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => base.Card.LacksTrait(AbnormalPlugin.LovingSlime);
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion").Clone() as CardInfo;
            cardInfo.baseHealth = Mathf.Max(1, base.Card.MaxHealth - 1);
            cardInfo.SetCost(base.Card.BloodCost(), base.Card.BonesCost(), base.Card.EnergyCost, base.Card.GemsCost());
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Copy merged sigils and the like
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.attackAdjustment = 0;

                cardInfo.Mods.Add(cardModificationInfo);
            }
            // Copy base sigils
            foreach (Ability item in base.Card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                cardInfo.Mods.Add(new CardModificationInfo(item));
            }

            yield return BoardManager.Instance.CreateCardInSlot(cardInfo, base.Card.Slot);
        }
    }
}
