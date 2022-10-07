using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Core.Opponents;

namespace WhistleWindLobotomyMod
{
    public class ProspectorAbnormalBossOpponent : ProspectorBossOpponent
    {
		public override IEnumerator StartNewPhaseSequence()
		{
			// Update blueprint for phase 2
			if (base.HasGrizzlyGlitchPhase(int.MinValue))
			{
				yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
				yield break;
			}
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
			yield return this.StrikeGoldSequence();
			yield return base.ClearQueue();
			yield return this.ReplaceWithCustomBlueprint(AbnormalEncounterData.ProspectorAbnormalBossP2, removeLockedCards: true);
			Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
		}
		public override void ModifyQueuedCard(PlayableCard card)
		{
			// modify Bad Wolf the same way Bloodhound is modified in vanilla
			base.ModifyQueuedCard(card);
			if (card.Info.name == "wstl_willBeBadWolf" && Singleton<TurnManager>.Instance.BattleNodeData.difficulty >= 10)
			{
				Ability[] collection = ((Singleton<TurnManager>.Instance.BattleNodeData.difficulty < 15) ? this.T2_HOUND_ABILITIES : this.T3_HOUND_ABILITIES);
				List<Ability> list = new(collection);
				list.RemoveAll((Ability x) => card.HasAbility(x));
				CardModificationInfo cardModificationInfo = new();
				cardModificationInfo.abilities.Add(list[SeededRandom.Range(0, list.Count, SaveManager.SaveFile.GetCurrentRandomSeed())]);
				cardModificationInfo.fromCardMerge = true;
				card.RenderInfo.forceEmissivePortrait = true;
				card.StatsLayer.SetEmissionColor(this.InteractablesGlowColor);
				card.AddTemporaryMod(cardModificationInfo);
			}
		}
	}
}
