using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_FrozenHeart()
        {
            const string rulebookName = "Frozen Heart";
            const string rulebookDescription = "When this card dies, the killer gains 3 Health.";
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

        public CardModificationInfo mod = new CardModificationInfo(0, 3);
        public CardModificationInfo mod2 = new CardModificationInfo(0, 4);

        private string altDialogue = "The Woodcutter stuffs the melted heart into his chest.";

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.25f);
            if (killer != null)
            {
                //Debug.Log($"[{killer.name}]");
                killer.Anim.LightNegationEffect();
                if (killer.name == "Card (Warm-Hearted Woodsman)")
                {
                    killer.AddTemporaryMod(mod2);
                    if (!base.HasLearned)
                    {
                        yield return new WaitForSeconds(0.25f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                    }
                }
                else
                {
                    killer.AddTemporaryMod(mod);
                    yield return new WaitForSeconds(0.25f);
                    yield return base.LearnAbility(0.25f);
                }
            }
            yield return new WaitForSeconds(0.25f);
            yield break;
        }
    }
}
