using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Martyr()
        {
            const string rulebookName = "Martyr";
            const string rulebookDescription = "When a card bearing this sigil dies, all allied creatures gain 2 Health.";
            const string dialogue = "A selfless death to cleanse your beasts of evil.";

            return WstlUtils.CreateAbility<Martyr>(
                Resources.sigilMartyr,
                rulebookName, rulebookDescription, dialogue, 3, addModular: true);
        }
    }
    public class Martyr : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // SigilADay julianperge

            var slotsWithCards = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot.Card != base.Card);

            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            foreach (var slot in slotsWithCards)
            {
                slot.Card.HealDamage(2);
                slot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.15f);
            }

            if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
            {
                yield return new WaitForSeconds(0.15f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.15f);
            }

            yield return base.LearnAbility(0.25f);
            yield break;
        }
    }
}
