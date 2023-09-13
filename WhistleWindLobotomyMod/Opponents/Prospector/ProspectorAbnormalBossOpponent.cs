using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Core.SpecialSequencers;

namespace WhistleWindLobotomyMod.Opponents.Prospector
{
    public class ProspectorAbnormalBossOpponent : ProspectorBossOpponent
    {
        public override IEnumerator StartNewPhaseSequence()
        {
            // Update blueprint for phase 2
            if (HasGrizzlyGlitchPhase(int.MinValue))
            {
                // do Apostles only if it's ascension and the Grizzly challenge is active
                if (SaveFile.IsAscension)
                    yield return AbnormalGrizzlySequence.ApostleGlitchSequence(this);
                else
                    yield return GrizzlyGlitchSequence();

                yield break;
            }
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return StrikeGoldSequence();
            yield return ClearQueue();
            yield return this.ReplaceWithCustomBlueprint(LobotomyEncounterManager.ProspectorAbnormalBossP2, removeLockedCards: true);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
        public override void ModifyQueuedCard(PlayableCard card)
        {
            // modify Bad Wolf the same way Bloodhound is modified in vanilla
            base.ModifyQueuedCard(card);
            if (card.Info.name == "wstl_willBeBadWolf" && Singleton<TurnManager>.Instance.BattleNodeData.difficulty >= 10)
            {
                Ability[] collection = Singleton<TurnManager>.Instance.BattleNodeData.difficulty < 15 ? T2_HOUND_ABILITIES : T3_HOUND_ABILITIES;
                List<Ability> list = new(collection);
                list.RemoveAll((x) => card.HasAbility(x));
                CardModificationInfo cardModificationInfo = new();
                cardModificationInfo.abilities.Add(list[SeededRandom.Range(0, list.Count, SaveManager.SaveFile.GetCurrentRandomSeed())]);
                cardModificationInfo.fromCardMerge = true;
                card.RenderInfo.forceEmissivePortrait = true;
                card.StatsLayer.SetEmissionColor(InteractablesGlowColor);
                card.AddTemporaryMod(cardModificationInfo);
            }
        }
    }
}
