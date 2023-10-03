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
        private void Ability_Copycat()
        {
            const string rulebookName = "Copycat";
            const string rulebookDescription = "[creature] will transform into a copy of the first opposing creature it faces, retaining its own sigils.";
            const string dialogue = "A near perfect impersonation.";
            const string triggerText = "[creature] tries to mimick the opposing creature.";
            Copycat.ability = AbnormalAbilityHelper.CreateAbility<Copycat>(
                "sigilCopycat",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class Copycat : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        bool copiedCard = false;
        private CardInfo baseInfo = null;

        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (!copiedCard && otherCard.Slot == base.Card.OpposingSlot())
                return CheckValid(base.Card.OpposingCard());

            return false;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => copiedCard && !wasSacrifice;
        public override IEnumerator OnResolveOnBoard()
        {
            baseInfo ??= base.Card.Info.Clone() as CardInfo;
            if (copiedCard || !CheckValid(base.Card.OpposingCard()))
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return base.Card.TransformIntoCard(CopyInfo(base.Card.OpposingCard().Info), NegateCopycat);
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.Card.TransformIntoCard(CopyInfo(otherCard.Info), NegateCopycat);
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            base.Card.Anim.StrongNegationEffect();
            base.Card.SetInfo(baseInfo);
            yield return new WaitForSeconds(0.55f);

            yield return DialogueHelper.PlayDialogueEvent("CopycatFail");
        }
        private CardInfo CopyInfo(CardInfo cardToCopy)
        {
            CardInfo newInfo = cardToCopy.Clone() as CardInfo;

            CardModificationInfo mod = new()
            {
                nameReplacement = "False " + cardToCopy.DisplayedNameLocalized,
                abilities = new(baseInfo.DefaultAbilities)
            };

            newInfo.Mods.Add(mod);
            return newInfo;
        }
        private void NegateCopycat()
        {
            copiedCard = true;
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { ability } });
        }
        private bool CheckValid(PlayableCard card)
        {
            if (card == null || card.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable))
                return false;

            return true;
        }
    }
}
