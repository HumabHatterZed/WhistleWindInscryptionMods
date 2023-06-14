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
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class SerpentsNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private CardModificationInfo GetWormStatusMod()
        {
            CardModificationInfo result = StatusEffectManager.StatusMod("worm", false);
            result.AddAbilities(StatusEffectWorms.ability);

            return result;
        }
        private CardModificationInfo GetWormDecalMod()
        {
            CardModificationInfo decal = StatusEffectManager.StatusMod("worm_decal", false);
            decal.DecalIds.Add($"decalWorms_0");
            return decal;
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
                return source.LacksTrait(AbnormalPlugin.NakedSerpent) && source.GetComponent<Worms>() == null;

            return false;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();

            source.AddPermanentBehaviour<Worms>();
            source.AddTemporaryMods(GetWormStatusMod(), GetWormDecalMod());
            
            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
    }
}
