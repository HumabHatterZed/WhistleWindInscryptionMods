using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public class TrapperTraderAbnormalBossOpponent : TrapperTraderBossOpponent
    {
		public override IEnumerator StartNewPhaseSequence()
		{
			if (base.HasGrizzlyGlitchPhase(1))
			{
				yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
				yield break;
			}
			base.sceneryObject.GetComponent<Animation>().Play("knives_table_exit");
			yield return new WaitForSeconds(0.25f);
			base.TurnPlan.Clear();
			yield return this.ClearBoardAndReturnPlayedPelts();
			yield return new WaitForSeconds(0.5f);
			yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("TrapperTraderPrePhase2", TextDisplayer.MessageAdvanceMode.Input);
			LeshyAnimationController.Instance.FlipMask(LeshyAnimationController.Mask.Trader);
			yield return new WaitForSeconds(2.75f);
			Singleton<ViewManager>.Instance.SwitchToView(View.BossCloseup);
			yield return new WaitForSeconds(0.1f);
			yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("TrapperTraderPhase2", TextDisplayer.MessageAdvanceMode.Input);
			Singleton<ViewManager>.Instance.SwitchToView(View.Default);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
			base.DestroyScenery();
			base.SpawnScenery("CratesTableEffects");
			this.tradeForPelts = base.InstantiateBossBehaviour<TradeAbnormalCardsForPelts>();
			yield return new WaitForSeconds(1.5f);
			yield return this.tradeForPelts.TradePhase(4, 4, RunState.Run.regionTier + 1, RunState.Run.regionTier);
		}
	}
}
