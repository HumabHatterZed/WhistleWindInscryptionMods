using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WhistleWindLobotomyMod.Opponents.IPreventInstantWin;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Base class for custom opponents used by the mod.
    /// </summary>
    public abstract class LobotomyOpponent : Part1BossOpponent, IKillPlayerSequence, ICustomExhaustSequence, IPreventInstantWin
    {
        public abstract Opponent.Type ID { get; }
        public List<Ability> TotemAbilitiesWhitelist => new();
        public List<Ability> TotemAbilitiesBlacklist => new();
        public override bool GiveCurrencyOnDefeat => false;
        public Tribe DominantTribe { get; private set; }

        protected const float BG_VOLUME = 0.3f;

        public virtual bool PreventInstantWin(CardSlot triggeringSlot, InstantWinType instantWinType)
        {
            return false;
        }
        public virtual IEnumerator OnInstantWinPrevented(CardSlot triggeringSlot, InstantWinType instantWinType)
        {
            yield break;
        }
        public virtual IEnumerator OnInstantWinTriggered(CardSlot triggeringSlot, InstantWinType instantWinType)
        {
            yield break;
        }

        public virtual bool RespondsToCustomExhaustSequence(CardDrawPiles drawPiles)
        {
            return false;
        }
        public virtual IEnumerator DoCustomExhaustSequence(CardDrawPiles drawPiles)
        {
            yield break;
        }

        public virtual bool RespondsToKillPlayerSequence()
        {
            return false;
        }
        public virtual IEnumerator KillPlayerSequence()
        {
            yield break;
        }

        /// <summary>
        /// Creates and returns a new TotemBottomData instance, using the TotemAbilitiesWhitelist/Blacklist as required.
        /// </summary>
        protected TotemBottomData CreateTotemBottomData(Tribe tribe, int difficulty)
        {
            int randSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + GlobalTriggerHandler.Instance.NumTriggersThisBattle;
            TotemBottomData retval = ScriptableObject.CreateInstance<TotemBottomData>();
            retval.effect = TotemEffect.CardGainAbility;
            retval.effectParams = new();

            // whitelist overrides the blacklist
            if (TotemAbilitiesWhitelist.Count > 0)
            {
                int index = SeededRandom.Range(0, TotemAbilitiesWhitelist.Count, randSeed);
                retval.effectParams.ability = TotemAbilitiesWhitelist[index];
                TotemAbilitiesWhitelist.Remove(TotemAbilitiesWhitelist[index]);
            }
            else
            {
                int maxSigilPower = (int)Mathf.Ceil(difficulty / 5f);
                TotemsUtil.AssignAbilityToBottom(retval, tribe, randSeed, maxSigilPower - 1, maxSigilPower, true, TotemAbilitiesBlacklist);
            }

            return retval;
        }

        /// <summary>
        /// Sequence to update the totem with a different sigil.
        /// </summary>
        public IEnumerator ReplaceTotemBottom()
        {
            // "disassemble" the totem
            totem.Anim.Play("slow_disassemble", 0, 0f);
            yield return new WaitForSeconds(0.333f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            totem.ShowHighlighted(highlighted: false, immediate: true);
            totem.SetEffectsActive(particlesActive: false, lightActive: false);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);

            yield return new WaitForSeconds(0.25f);

            totem.TotemItemData.bottom = CreateTotemBottomData(DominantTribe, Difficulty);
            totem.bottomPieceParent.GetComponentInChildren<CompositeTotemPiece>().SetData(totem.TotemItemData.bottom);

            totem.Anim.Play("slow_assemble", 0, 0f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(0.166f);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.1f);
            yield return new WaitForSeconds(1.418f);
            totem.ShowHighlighted(highlighted: true, immediate: true);
            Singleton<TableVisualEffectsManager>.Instance.ThumpTable(0.2f);
            totem.SetEffectsActive(false, lightActive: true);
            AudioController.Instance.PlaySound2D("metal_object_up#2", MixerGroup.TableObjectsSFX, 1f, 0.25f);
        }

        /// <summary>
        /// Sets up the opponent's totem if it has one.
        /// </summary>
        public IEnumerator SetUpTotem() {
            TotemItemData totemData = new();
            totemData.top = new(DominantTribe);
            totemData.bottom = CreateTotemBottomData(DominantTribe, Difficulty);

            yield return base.AssembleTotem(totemData, new(0.5f, 0f, -0.5f), new(0f, 10f, 0f), this.InteractablesGlowColor, false);
            yield return new WaitForSeconds(0.5f);
        }

        /// <summary>
        /// Method for initialising important variables used by the opponent.
        /// </summary>
        /// <param name="encounter"></param>
        public virtual void InitialiseOpponent(EncounterData encounter) {
            DominantTribe = encounter.Blueprint.dominantTribes[0];
            if (encounter.Blueprint.redundantAbilities != null)
                TotemAbilitiesBlacklist.AddRange(encounter.Blueprint.redundantAbilities);
        }

        public override IEnumerator IntroSequence(EncounterData encounter) {
            InitialiseOpponent(encounter);
            yield break;
        }

        public override IEnumerator LifeLostSequence() {
            yield break; // don't run the base sequence
        }
    }
}
