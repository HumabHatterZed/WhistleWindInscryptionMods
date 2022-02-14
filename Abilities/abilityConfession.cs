using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Confession()
        {
            const string rulebookName = "Confession and Pentinence";
            string rulebookDescription = "Keep faith with unwavering resolve.";
            const string dialogue = "Keep faith with unwavering resolve.";

            if (ConfigHelper.Instance.RevealWhiteNight)
            {
                rulebookDescription = "If held by the Heretic, kills the Heretic at the end of the opponent's turn and adds a special card to your hand. If held by the special card, kill Apostles and WhiteNight and deal 33 direct damage.";
            }

            return WstlUtils.CreateAbility<Confession>(
                Resources.sigilConfession,
                rulebookName, rulebookDescription, dialogue, -3, overrideModular: true);
        }
    }
    public class Confession : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool deeds = false;

        public override bool RespondsToResolveOnBoard()
        {
            return base.Card.Info.name.ToLowerInvariant().Equals("wstl_hundredsgooddeeds");
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();

            Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
            yield return new WaitForSeconds(0.4f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);

            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                // kill WhiteNight first
                if (slot.Card.Info.name.ToLowerInvariant().Contains("whitenight"))
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

            if (!killer.Info.name.ToLowerInvariant().Equals("wstl_hundredsgooddeeds"))
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
            }
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (!deeds)
            {
                deeds = true;
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
        }
    }
}
