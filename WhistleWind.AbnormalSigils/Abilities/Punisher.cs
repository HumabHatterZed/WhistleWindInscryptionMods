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
        private void Ability_Punisher()
        {
            const string rulebookName = "Punisher";
            const string rulebookDescription = "When [creature] is struck and killed, the attacker also perishes.";
            const string dialogue = "Retaliation is switft and brutal.";
            const string triggerText = "[creature] swiftly retaliates!";
            Punisher.ability = AbnormalAbilityHelper.CreateAbility<Punisher>(
                "sigilPunisher",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Punisher : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) {
            if (!wasSacrifice && killer != null) {
                return !killer.HasAbility(Ability.MadeOfStone) && !killer.HasTrait(AbnormalPlugin.ImmuneToInstaDeath);
            }
            return false;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            yield return base.PreSuccessfulTriggerSequence();
            yield return killer.Die(false, base.Card);
            yield return base.LearnAbility(0.4f);
        }
    }
}
