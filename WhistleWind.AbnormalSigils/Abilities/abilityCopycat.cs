using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
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
            const string rulebookDescription = "[creature] will transform into a copy of the first creature that opposes it.";
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
        public override int Priority => -1;

        bool copiedCard = false;
        private CardInfo baseInfo = null;

        private void Start()
        {
            if (base.Card == null)
                return;

            baseInfo ??= base.Card.Info.Clone() as CardInfo;
        }
        public override bool RespondsToResolveOnBoard() => !copiedCard && base.Card.OpposingCard() != null;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (!copiedCard && otherCard != null && otherCard == base.Card.OpposingCard())
                return true;

            return false;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => copiedCard && !wasSacrifice;
        public override IEnumerator OnResolveOnBoard()
        {
            if (!CanCopyCard(base.Card.OpposingCard()))
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("CopycatFail");
                yield break;
            }

            PlayableCard card = base.Card; // store the playable card here to prevent null errors
            yield return TransformIntoCopy(base.Card.OpposingCard());
            if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard))
            {
                yield return new WaitForSeconds(0.4f);
                foreach (var trigger in card.TriggerHandler.GetAllReceivers().Where(x => x != null && x.RespondsToResolveOnBoard()))
                    yield return trigger.OnResolveOnBoard();

            }
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (CanCopyCard(base.Card.OpposingCard()))
            {
                yield return TransformIntoCopy(otherCard);
                yield break;
            }
            // if cannot copy card
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return DialogueHelper.PlayDialogueEvent("CopycatFail");
        }
        private IEnumerator TransformIntoCopy(PlayableCard otherCard)
        {
            // copy temporary mods
            foreach (CardModificationInfo mod in otherCard.TemporaryMods)
            {
                tempMods.Add(mod.FullClone());
            }
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.Card.TransformIntoCard(CopyInfo(otherCard.Info), NegateCopycat);
            yield return base.LearnAbility(0.5f);
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            base.Card.Anim.StrongNegationEffect();
            base.Card.SetInfo(baseInfo);
            yield return new WaitForSeconds(0.55f);

            yield return DialogueHelper.PlayDialogueEvent("CopycatDead");
        }
        private CardInfo CopyInfo(CardInfo cloneCardInfo)
        {
            CardInfo newInfo = cloneCardInfo.Clone() as CardInfo;
            CardModificationInfo mod = new()
            {
                nameReplacement = "False " + cloneCardInfo.DisplayedNameLocalized,
                abilities = new(baseInfo.DefaultAbilities)
            };

            newInfo.Mods.Add(mod);
            return newInfo;
        }
        private readonly List<CardModificationInfo> tempMods = new();
        private void NegateCopycat()
        {
            foreach (var mod in tempMods)
                base.Card.AddTemporaryMod(mod);

            base.Card.AddTemporaryMod(new() { negateAbilities = new() { ability } });
            copiedCard = true;
        }
        private bool CanCopyCard(PlayableCard card)
        {
            return card.LacksAllTraits(Trait.Giant, Trait.Uncuttable);
        }
    }
}
