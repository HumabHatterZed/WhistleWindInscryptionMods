using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
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
            const string rulebookDescription = "At the end of the owner's turn, creatures adjacent to [creature] gain this sigil. If this card is not a Slime, also take 1 damage and transform into a Slime on death.";
            const string dialogue = "Its army grows everyday.";
            const string triggerText = "[creature] melts into slime!";
            Slime.ability = AbnormalAbilityHelper.CreateAbility<Slime>(
                "sigilSlime",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Slime : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.Card.LacksTrait(AbnormalPlugin.LovingSlime))
            {
                HelperMethods.ChangeCurrentView(View.Board);
                yield return base.Card.TakeDamage(1, null);
            }

            // infect adjacent cards
            CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true);
            CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false);

            bool leftValid = CheckIsValid(leftSlot);
            bool rightValid = CheckIsValid(rightSlot);

            if (!leftValid && !rightValid)
                yield break;

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
        }

        private bool CheckIsValid(CardSlot slot)
        {
            if (slot?.Card != null && slot.Card.LacksAbility(this.Ability) && slot.Card.LacksAllTraits(Trait.Pelt, Trait.Terrain, Trait.Uncuttable, Trait.Giant))
            {
                return slot.Card.LacksTrait(AbnormalPlugin.LovingSlime);
            }
            return false;
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && base.Card.LacksTrait(AbnormalPlugin.LovingSlime);
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");
            foreach (CardModificationInfo item in base.Card.Info.Mods.Where(x => !x.nonCopyable))
            {
                // Copy merged sigils and the like
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.attackAdjustment = 0;

                cardInfo.Mods.Add(cardModificationInfo);
            }
            // Copy base sigils
            foreach (Ability item in base.Card.Info.DefaultAbilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                cardInfo.Mods.Add(new CardModificationInfo(item) { nonCopyable = true });
            }

            yield return new WaitForSeconds(0.4f);
            if (base.Card != null)
                yield return base.Card.TransformIntoCard(cardInfo, () => base.Card.Status.damageTaken = 0);
        }
    }
}
