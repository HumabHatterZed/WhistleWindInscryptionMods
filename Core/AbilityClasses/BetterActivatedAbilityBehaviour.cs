using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;

namespace WhistleWind.Core.AbilityClasses
{
    // a version of ActivatedAbilityBehaviour with adjustable start costs and support for Health costs
    public abstract class BetterActivatedAbilityBehaviour : AbilityBehaviour
    {
        public int energyCostMod;
        public int bonesCostMod;
        public int healthCostMod;
        public virtual int StartingEnergyCost { get; }
        public virtual int StartingBonesCost { get; }
        public virtual int StartingHealthCost { get; }
        public int EnergyCost => StartingEnergyCost + energyCostMod;
        public int BonesCost => StartingBonesCost + bonesCostMod;
        public int HealthCost => StartingHealthCost + healthCostMod;

        public sealed override bool RespondsToResolveOnBoard() => SaveManager.SaveFile.IsPart2 && !ProgressionData.LearnedMechanic(MechanicsConcept.GBCActivatedAbilities);
        public sealed override IEnumerator OnResolveOnBoard()
        {
            // only for Part 2 tutorial dialogue
            yield return new WaitForSeconds(0.15f);
            if (StoryEventsData.EventCompleted(StoryEvent.GBCUndeadAmbition))
            {
                yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent("ActivatedAbilityTutorial", TextBox.Style.Undead, Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Grimora), null, TextBox.ScreenPosition.ForceTop);
            }
            else if (StoryEventsData.EventCompleted(StoryEvent.GBCNatureAmbition))
            {
                yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent("ActivatedAbilityTutorial", TextBox.Style.Nature, Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Leshy), null, TextBox.ScreenPosition.ForceTop);
            }
            else if (StoryEventsData.EventCompleted(StoryEvent.GBCTechAmbition))
            {
                yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent("ActivatedAbilityTutorial", TextBox.Style.Tech, Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.P03), null, TextBox.ScreenPosition.ForceTop);
            }
            else
            {
                yield return Singleton<DialogueHandler>.Instance.PlayDialogueEvent("ActivatedAbilityTutorial", TextBox.Style.Magic, Singleton<InBattleDialogueSpeakers>.Instance.GetSpeaker(DialogueSpeaker.Character.Magnificus), null, TextBox.ScreenPosition.ForceTop);
            }
        }

        public sealed override bool RespondsToActivatedAbility(Ability ability) => this.Ability == ability;
        public sealed override IEnumerator OnActivatedAbility()
        {
            if (this.CanAfford() && this.CanActivate())
            {
                if (EnergyCost > 0)
                {
                    yield return Singleton<ResourcesManager>.Instance.SpendEnergy(EnergyCost);
                    if (Singleton<ConduitCircuitManager>.Instance != null)
                    {
                        CardSlot cardSlot = Singleton<BoardManager>.Instance.GetSlots(getPlayerSlots: true).Find((CardSlot x) => x.Card != null && x.Card.HasAbility(Ability.ConduitEnergy));
                        if (cardSlot != null)
                        {
                            ConduitEnergy component = cardSlot.Card.GetComponent<ConduitEnergy>();
                            if (component != null && component.CompletesCircuit())
                            {
                                yield return Singleton<ResourcesManager>.Instance.AddEnergy(EnergyCost);
                            }
                        }
                    }
                }
                if (BonesCost > 0)
                {
                    yield return Singleton<ResourcesManager>.Instance.SpendBones(BonesCost);
                }

                if (HealthCost > 0)
                {
                    base.Card.Anim.LightNegationEffect();
                    base.Card.Status.damageTaken++;
                }
                yield return new WaitForSeconds(0.1f);
                yield return base.PreSuccessfulTriggerSequence();
                yield return this.Activate();
                ProgressionData.SetMechanicLearned(MechanicsConcept.GBCActivatedAbilities);

                if (HealthCost > 0) // card still exists and has 0 Health
                {
                    if (base.Card != null && base.Card.NotDead() && base.Card.Health == 0)
                        yield return base.Card.Die(false);
                }
            }
            else
            {
                base.Card.Anim.LightNegationEffect();
                AudioController.Instance.PlaySound2D("toneless_negate", MixerGroup.GBCSFX, 0.2f);
                yield return new WaitForSeconds(0.25f);
            }
        }

        public virtual bool CanActivate()
        {
            return true;
        }

        public abstract IEnumerator Activate();

        private bool CanAfford()
        {
            if (base.Card.Health >= HealthCost)
            {
                if (Singleton<ResourcesManager>.Instance.PlayerEnergy >= EnergyCost)
                {
                    return Singleton<ResourcesManager>.Instance.PlayerBones >= BonesCost;
                }
            }
            return false;
        }
    }
}
