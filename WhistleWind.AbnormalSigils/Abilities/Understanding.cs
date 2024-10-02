using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Understanding()
        {
            const string rulebookName = "Understanding";
            const string rulebookDescription = "If [creature] perishes from the effect of Decay, deal 4 damage to each opposing creature and 4 direct damage to their owner.";
            const string dialogue = "Too slow.";
            Understanding.ability = AbnormalAbilityHelper.CreateAbility<Understanding>(
                "sigilUnderstanding",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: false, canStack: false)
                .SetAbilityRedirect("Decay", Decay.iconId, GameColors.Instance.darkPurple)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Understanding : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => killer == base.Card;

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            foreach (PlayableCard card in BoardManager.Instance.GetCards(base.Card.OpponentCard))
            {
                yield return card.TakeDamage(4, null);
            }
            yield return LifeManager.Instance.ShowDamageSequence(4, 4, base.Card.OpponentCard);
            if (!base.HasLearned)
            {
                base.SetLearned();
                if (TextDisplayer.m_Instance == null)
                    yield break;

                yield return new WaitForSeconds(0.4f);
                DialogueEvent.LineSet abilityLearnedDialogue = AbilitiesUtil.GetInfo(this.Ability).abilityLearnedDialogue;
                foreach (DialogueEvent.Line line in abilityLearnedDialogue.lines)
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(line.text, -0.65f, 0.4f, line.emotion);
                }
            }
        }
    }
}
