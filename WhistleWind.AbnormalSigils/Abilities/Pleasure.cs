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
        private void Ability_Pleasure()
        {
            const string rulebookName = "Pleasure";
            const string rulebookDescription = "At the end of the owner's turn, [creature] deals 1 direct damage to the opposing side.";
            const string dialogue = "Tick tock.";
            Pleasure.ability = AbnormalAbilityHelper.CreateAbility<Pleasure>(
                "sigilPleasure",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Pleasure : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.Card.OpponentCard;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return LifeManager.Instance.ShowDamageSequence(1, 1, base.Card.OpponentCard);
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
