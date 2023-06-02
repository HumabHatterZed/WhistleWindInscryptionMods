using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

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
                Artwork.sigilSerpentsNest, Artwork.sigilSerpentsNest_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class SerpentsNest : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private readonly CardModificationInfo wormStatusMod = new(StatusEffectWorms.ability)
        {
            singletonId = "worms_status",
            nonCopyable = true,
        };
        private readonly CardModificationInfo wormDecalMod = new()
        {
            singletonId = "worms_decal",
            DecalIds = { "wstl_worms_0" },
            nonCopyable = true,
        };
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

            CardInfo copyOfSource = source.Info.Clone() as CardInfo;
            copyOfSource.Mods = new(source.Info.Mods) { wormStatusMod, wormDecalMod };
            source.AddPermanentBehaviour<Worms>();
            source.SetInfo(copyOfSource);
            
            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
    }
}
