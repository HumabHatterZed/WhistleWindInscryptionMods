using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Persistent()
        {
            const string rulebookName = "Persistent";
            const string rulebookDescription = "Attacks by this card cannot be avoided or redirected by sigils like Loose Tail or Waterborne.";
            const string dialogue = "Prey cannot hide so easily.";
            const string triggerText = "[creature] chases its prey down.";
            Persistent.ability = AbnormalAbilityHelper.CreateAbility<Persistent>(
                "sigilPersistent",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Persistent : AbilityBehaviour, IOnPreSlotAttackSequence, IOnPostSlotAttackSequence
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public List<PlayableCard> previousTargets = new();
        public List<PlayableCard> currentTargets = new();
        public bool RespondsToPreSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.Card.Slot;
        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot == base.Card.Slot;

        public IEnumerator OnPreSlotAttackSequence(CardSlot attackingSlot)
        {
            currentTargets = base.Card.GetOpposingSlots().FindAll(x => x.Card).Select(x => x.Card).ToList();
            yield break;
        }

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            previousTargets = new(currentTargets);
            currentTargets.Clear();
            yield break;
        }
    }
}
