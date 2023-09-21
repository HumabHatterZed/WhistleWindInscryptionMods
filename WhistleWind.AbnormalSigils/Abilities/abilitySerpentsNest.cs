using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_SerpentsNest()
        {
            const string rulebookName = "Serpent's Nest";
            const string rulebookDescription = "When [creature] is struck, the striker gains 1 Worms.";
            const string dialogue = "It can enter your body through any aperture.";

            SerpentsNest.ability = AbnormalAbilityHelper.CreateAbility<SerpentsNest>(
                "sigilSerpentsNest",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class SerpentsNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
                return source.LacksTrait(AbnormalPlugin.NakedSerpent);

            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            int stacks = base.Card.GetAbilityStacks(ability);

            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();

            if (source.HasStatusEffect<Worms>())
                source.UpdateStatusEffectCount<Worms>(stacks, true);
            else
                source.AddStatusEffectToCard<Worms>(stacks - 1, true);

            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
    }
}
