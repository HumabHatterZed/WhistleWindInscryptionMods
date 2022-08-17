using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_FrozenHeart()
        {
            const string rulebookName = "Frozen Heart";
            const string rulebookDescription = "When this card dies, the killer gains 1 Health.";
            const string dialogue = "Spring arrives with blossoming roses.";
            FrozenHeart.ability = AbilityHelper.CreateAbility<FrozenHeart>(
                Resources.sigilFrozenHeart, Resources.sigilFrozenHeart_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -1,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class FrozenHeart : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public CardModificationInfo mod = new(0, 1);
        public CardModificationInfo mod2 = new(0, 2);

        private string altDialogue = "The Woodcutter stuffs the melted heart into his chest.";

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
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
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
