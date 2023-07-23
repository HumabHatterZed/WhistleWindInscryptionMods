using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FrozenHeart()
        {
            const string rulebookName = "Frozen Heart";
            const string rulebookDescription = "When [creature] dies, the killer gains 2 Health.";
            const string dialogue = "Spring arrives with blossoming roses.";
            const string triggerText = "[creature] releases warm life.";
            FrozenHeart.ability = AbnormalAbilityHelper.CreateAbility<FrozenHeart>(
                "sigilFrozenHeart",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: -1,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class FrozenHeart : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string altDialogue = "The Woodcutter stuffs the melted heart into his chest.";
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice && killer != null;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            killer.Anim.LightNegationEffect();
            if (killer.Info.name.ToLowerInvariant().Contains("warmheartedwoodsman"))
            {
                killer.HealDamage(4);
                if (!base.HasLearned)
                {
                    base.SetLearned();
                    yield return DialogueHelper.PlayAlternateDialogue(dialogue: altDialogue);
                }
            }
            else
            {
                killer.HealDamage(2);
                yield return base.LearnAbility(0.4f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
