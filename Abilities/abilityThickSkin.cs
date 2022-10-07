using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_ThickSkin()
        {
            const string rulebookName = "Thick Skin";
            const string rulebookDescription = "A card bearing this sigil takes 1 less damage from attacks.";
            const string dialogue = "Your creature's hide absorbs the blow.";

            ThickSkin.ability = AbilityHelper.CreateAbility<ThickSkin>(
                Artwork.sigilThickSkin, Artwork.sigilThickSkin_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                addModular: true, opponent: true, canStack: true, isPassive: false).Id;
        }
    }
    public class ThickSkin : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return source != null;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility(0.4f);
        }
    }
}
