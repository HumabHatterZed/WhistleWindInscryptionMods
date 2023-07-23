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
        private void Ability_Copycat()
        {
            const string rulebookName = "Copycat";
            const string rulebookDescription = "This gains the sigils and stats of the first card to be played in the opposing space.";
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

        bool isACopy = false;
        private CardInfo baseInfo;

        private void Start() => baseInfo = base.Card.Info.Clone() as CardInfo;

        public override bool RespondsToResolveOnBoard() => base.Card.OpposingCard() != null;
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => otherCard.Slot == base.Card.OpposingSlot() && !isACopy;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && isACopy;
        public override IEnumerator OnResolveOnBoard()
        {
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
            CardInfo newInfo = baseInfo.Clone() as CardInfo;
            newInfo.baseAttack = cardToCopy.baseAttack;
            newInfo.baseHealth = cardToCopy.baseHealth;
            newInfo.Mods = new(base.Card.Info.Mods);
            foreach (CardModificationInfo mod in cardToCopy.Mods.FindAll(x => !x.nonCopyable))
                newInfo.Mods.Add(mod.Clone() as CardModificationInfo);

            return newInfo;
        }
        private void NegateCopycat()
        {
            isACopy = true;
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { ability } });
        }
    }
}
