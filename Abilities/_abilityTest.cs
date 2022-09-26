using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;
using static WhistleWindLobotomyMod.WstlPlugin;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Test()
        {
            const string rulebookName = "Debug";
            const string rulebookDescription = "test";
            const string dialogue = "femboy";
            Test.ability = AbilityHelper.CreateActivatedAbility<Test>(
                Resources.sigilAbnormality, Resources.sigilAbnormality_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0).Id;
        }
    }
    public class Test : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool flag = true;
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            CardModificationInfo mod = new(Singleton<LifeManager>.Instance.DamageUntilPlayerWin, 0);
            if (flag)
            {
                base.Card.AddTemporaryMod(mod);
                flag = false;
            }
            else
            {
                base.Card.RemoveTemporaryMod(mod);
                flag = true;
            }
        }
        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.AddTemporaryMod(new(Ability.TripleBlood));
            base.Card.AddTemporaryMod(new(Ability.Flying));
        }
    }
}
