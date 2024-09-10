using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Woodcutter()
        {
            const string rulebookName = "Woodcutter";
            const string rulebookDescription = "When a creature moves into the space opposite this card, they take damage equal to this card's Power.";
            const string dialogue = "No matter how many trees fall, the forest remains dense.";
            const string triggerText = "[creature] takes a free swing.";
            Woodcutter.ability = AbnormalAbilityHelper.CreateAbility<Woodcutter>(
                "sigilWoodcutter",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    
    public class Woodcutter : Sentry, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => RespondsToTrigger(otherCard);
        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => RespondsToTrigger(otherCard);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => OnOtherCardAssignedToSlot(otherCard);
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            if (base.Card.Attack == 0 || (otherCard == this.lastShotCard && Singleton<TurnManager>.Instance.TurnNumber == this.lastShotTurn))
                yield break;

            modifyTarget = true;
            yield return FireAtOpposingSlot(otherCard);
        }

        private bool modifyTarget = false;
        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return attacker == base.Card && target == this.lastShotCard && modifyTarget;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            modifyTarget = false;
            return damage + (base.Card.Attack - 1); // change damage to equal this card's Attack, account for Sentry already dealing 1 damage
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
