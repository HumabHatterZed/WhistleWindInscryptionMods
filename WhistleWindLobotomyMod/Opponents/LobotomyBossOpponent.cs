using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWind.AbnormalSigils;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBossOpponent : Part1BossOpponent
    {
        public abstract Opponent.Type ID { get; }
        public override string DefeatedPlayerDialogue => "";
        public override bool GiveCurrencyOnDefeat => false;

        public List<Ability> bossTotemAbilities = new();

        public GameObject bossObjectAnimation;
        public Animator MasterAnimator;

        public override IEnumerator IntroSequence(EncounterData encounter)
        {
            RunState.CurrentMapRegion.FadeOutAmbientAudio();
            yield return ReducePlayerLivesSequence();
            yield return new WaitForSeconds(0.4f);

            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                yield return SetUpBossTotem(Tribe.Bird, new(0.5f, 0f, -0.5f), new(0f, 10f, 0f));
                yield return new WaitForSeconds(0.25f);
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
        }
        public override IEnumerator OutroSequence(bool wasDefeated)
        {
            if (AscensionSaveData.Data.ChallengeIsActive(AscensionChallenge.BossTotems))
            {
                yield return base.DisassembleTotem();
                Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
            }
            if (!wasDefeated)
            {
                bossObjectAnimation.transform.SetParent(null);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private TotemBottomData CreateTotemBottomData()
        {
            int index = SeededRandom.Range(0, bossTotemAbilities.Count, SaveManager.SaveFile.GetCurrentRandomSeed() + GlobalTriggerHandler.Instance.NumTriggersThisBattle);
            Ability ability = bossTotemAbilities[index];
            bossTotemAbilities.Remove(ability);

            TotemBottomData retval = ScriptableObject.CreateInstance<TotemBottomData>();
            retval.effect = TotemEffect.CardGainAbility;
            retval.effectParams = new() { ability = ability };
            return retval;
        }
        public IEnumerator SetUpBossTotem(Tribe bossTribe, Vector3 positionOffset, Vector3 rotationOffset)
        {
            yield return new WaitForSeconds(0.25f);
            ChallengeActivationUI.TryShowActivation(AscensionChallenge.BossTotems);

            TotemItemData totemData = ScriptableObject.CreateInstance<TotemItemData>();
            totemData.top = ScriptableObject.CreateInstance<TotemTopData>();
            totemData.top.prerequisites = new() { tribe = bossTribe };
            totemData.bottom = CreateTotemBottomData();
            yield return base.AssembleTotem(totemData, positionOffset, rotationOffset, this.InteractablesGlowColor, false);
            yield return new WaitForSeconds(0.5f);
            if (!DialogueEventsData.EventIsPlayed("ChallengeBossTotems"))
            {
                yield return Singleton<TextDisplayer>.Instance.PlayDialogueEvent("ChallengeBossTotems", TextDisplayer.MessageAdvanceMode.Input);
            }
            Singleton<OpponentAnimationController>.Instance.ClearLookTarget();
        }
        public IEnumerator ReplaceTotemBottom()
        {
            // "disassemble" the totem
            this.totem.Anim.Play("slow_disassemble", 0, 0f);
            yield return new WaitForSeconds(0.333f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            this.totem.ShowHighlighted(highlighted: false, immediate: true);
            this.totem.SetEffectsActive(particlesActive: false, lightActive: false);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);

            yield return new WaitForSeconds(0.25f);

            totem.TotemItemData.bottom = CreateTotemBottomData();
            totem.bottomPieceParent.GetComponentInChildren<CompositeTotemPiece>().SetData(totem.TotemItemData.bottom);

            this.totem.Anim.Play("slow_assemble", 0, 0f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
            this.totem.ShowHighlighted(highlighted: true, immediate: true);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
            this.totem.SetEffectsActive(false, lightActive: true);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);
        }

        public virtual bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot)
        {
            return true;
        }
        public virtual IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot)
        {
            yield break;
        }
        public virtual IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot)
        {
            yield break;
        }
    }
}
