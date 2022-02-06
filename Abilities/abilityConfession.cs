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
        private NewAbility Ability_Confession()
        {
            const string rulebookName = "";
            const string rulebookDescription = "Keep faith with unwavering resolve.";
            const string dialogue = "Keep faith with unwavering resolve.";

            return WstlUtils.CreateAbility<Confession>(
                Resources.sigilMartyr,
                rulebookName, rulebookDescription, dialogue, -3);
        }
    }
    public class Confession : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool Pentinence => base.Card.Info.name.ToLowerInvariant().Contains("hundredsgooddeeds");
        public override bool RespondsToResolveOnBoard()
        {
            return Pentinence;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card.Info.name.ToLowerInvariant().Contains("apostle") || slot.Card.Info.name.ToLowerInvariant().Contains("whitenight"))
                {
                    while (slot.Card != null)
                    {
                        if (slot.Card.Health > 0)
                        {
                            yield return slot.Card.TakeDamage(66, base.Card);
                            yield return new WaitForSeconds(0.4f);
                        }
                    }
                }
            }
            SpecialBattleSequencer specialSequence = null;
            var combatManager = Singleton<CombatPhaseManager>.Instance;

            yield return combatManager.DamageDealtThisPhase += 33;

            yield return new WaitForSeconds(0.4f);
            yield return combatManager.VisualizeDamageMovingToScales(true);

            int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
            int damage = combatManager.DamageDealtThisPhase - excessDamage;

            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);

            RunState.Run.currency += excessDamage;
            yield return combatManager.VisualizeExcessLethalDamage(excessDamage, specialSequence);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (Pentinence)
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                yield break;
            }

            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }

            CardInfo cardInfo = CardLoader.GetCardByName("wstl_hundredsGoodDeeds");
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.5f);
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return !playerTurnEnd && !Pentinence;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            yield return base.Card.Die(false, base.Card);
        }
    }
}
