using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;


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
                return source.LacksTrait(AbnormalPlugin.NakedSerpent) && source.GetComponent<Worms>() == null;

            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            int extraStacks = Mathf.Max(0, base.Card.GetAbilityStacks(ability) - 1);
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();

            source.AddPermanentBehaviour<Worms>();
            Worms component = source.GetComponent<Worms>();
            component.effectCount += extraStacks;
            source.AddTemporaryMods(component.GetEffectDecalMod(), component.GetEffectDecalMod());
            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
    }
}
