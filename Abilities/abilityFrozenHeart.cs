using APIPlugin;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_FrozenHeart()
        {
            const string rulebookName = "Frozen Heart";
            const string rulebookDescription = "When this card dies, the killer gains 1 Health.";
            const string dialogue = "Spring arrives with blossoming roses.";
            return WstlUtils.CreateAbility<FrozenHeart>(
                Resources.sigilFrozenHeart,
                rulebookName, rulebookDescription, dialogue, 0);
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
                if (!base.HasLearned)
                {
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                killer.AddTemporaryMod(mod);
                yield return new WaitForSeconds(0.4f);
                yield return base.LearnAbility(0.4f);
            }
        }
    }
}
