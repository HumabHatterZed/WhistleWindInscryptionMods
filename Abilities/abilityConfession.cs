using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Confession()
        {
            const string rulebookName = "Confession and Pentinence";
            string rulebookDescription = ConfigUtils.Instance.RevealWhiteNight ? "Activate: Kills the Heretic and creates a special card. If used on the special card, kill WhiteNight, his Apostles, and deal 33 direct damage." : "Activate: Keep faith with unwavering resolve.";
            const string dialogue = "[c:bG]Keep faith with unwavering resolve.[c:]";

            Confession.ability = WstlUtils.CreateActivatedAbility<Confession>(
                Resources.sigilConfession,
                rulebookName, rulebookDescription, dialogue, -3).Id;
        }
    }
    public class Confession : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool CanActivate()
        {
            return base.Card.Info.name != "wstl_hundredsGoodDeeds";
        }
        public override IEnumerator Activate()
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            CardSlot thisSlot = base.Card.Slot;
            yield return base.Card.Die(false, base.Card);
            yield return new WaitForSeconds(0.5f);
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_hundredsGoodDeeds");
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardInfo, thisSlot, 0.15f);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                // kill WhiteNight first
                if (slot.Card.Info.name.ToLowerInvariant().Contains("whitenight"))
                {
                    while (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(66, base.Card);
                        yield return new WaitForSeconds(0.4f);
                    }
                }
            }
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card.Info.name.ToLowerInvariant().Contains("apostle"))
                {
                    while (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(66, base.Card);
                        yield return new WaitForSeconds(0.4f);
                    }
                }
            }
            SpecialBattleSequencer specialSequence = null;
            var combatManager = Singleton<CombatPhaseManager>.Instance;

            yield return combatManager.DamageDealtThisPhase += 33;

            yield return new WaitForSeconds(0.4f);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            yield return combatManager.VisualizeDamageMovingToScales(true);
            int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
            int damage = combatManager.DamageDealtThisPhase - excessDamage;

            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);

            RunState.Run.currency += excessDamage;
            yield return combatManager.VisualizeExcessLethalDamage(excessDamage, specialSequence);

            if (Singleton<TurnManager>.Instance.Opponent.NumLives > 1)
            {
                yield return thisSlot.Card.Die(false, thisSlot.Card);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (killer != base.Card)
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
            }
        }
    }
}
