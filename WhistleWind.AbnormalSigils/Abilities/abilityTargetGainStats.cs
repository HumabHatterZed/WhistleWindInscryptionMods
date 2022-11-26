using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_TargetGainStats()
        {
            string rulebookDescription = "When [creature] is sacrificed, give its stats to the sacrificing card.";
            if (SpellAPI.Enabled)
                rulebookDescription = "For spells: Activate upon selecting a target.\n" + rulebookDescription;

            const string rulebookName = "Strengthen Target";
            const string dialogue = "Your beast's strength grows.";
            TargetGainStats.ability = AbnormalAbilityHelper.CreateAbility<TargetGainStats>(
                Artwork.sigilTargetGainStats, Artwork.sigilTargetGainStats_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class TargetGainStats : TargetedSpell
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool TargetAlly => true;

        public override IEnumerator OnSacrifice()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.currentSacrificeDemandingCard;
            CardModificationInfo mod = new(base.Card.Attack, base.Card.Health);

            yield return base.PreSuccessfulTriggerSequence();
            card.Anim.LightNegationEffect();
            card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility();
        }
        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            CardModificationInfo mod = new(base.Card.Attack, base.Card.Health);

            slot.Card.Anim.PlayTransformAnimation();
            slot.Card.AddTemporaryMod(mod);
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();
        }
    }
}
