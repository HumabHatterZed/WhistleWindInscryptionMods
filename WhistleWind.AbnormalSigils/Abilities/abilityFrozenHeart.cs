using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FrozenHeart()
        {
            const string rulebookName = "Frozen Heart";
            const string rulebookDescription = "When this card dies the killer gains 1 Health.";
            const string dialogue = "Spring arrives with blossoming roses.";
            FrozenHeart.ability = AbnormalAbilityHelper.CreateAbility<FrozenHeart>(
                Artwork.sigilFrozenHeart, Artwork.sigilFrozenHeart_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -1,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class FrozenHeart : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public CardModificationInfo mod = new(0, 1);
        public CardModificationInfo mod2 = new(0, 2);

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
                killer.AddTemporaryMod(mod2);
                yield return new WaitForSeconds(0.2f);
                if (!base.HasLearned)
                {
                    yield return AbnormalMethods.PlayAlternateDialogue(dialogue: altDialogue);
                }
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                killer.AddTemporaryMod(mod);
                yield return base.LearnAbility(0.4f);
            }
        }
    }
}
