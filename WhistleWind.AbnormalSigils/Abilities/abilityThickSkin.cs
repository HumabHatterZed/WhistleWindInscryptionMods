using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_ThickSkin()
        {
            const string rulebookName = "Thick Skin";
            const string rulebookDescription = "[creature] takes 1 less damage from attacks.";
            const string dialogue = "Your creature's hide absorbs the blow.";

            ThickSkin.ability = AbnormalAbilityHelper.CreateAbility<ThickSkin>(
                Artwork.sigilThickSkin, Artwork.sigilThickSkin_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: true, opponent: true, canStack: true, isPassive: false).Id;
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
