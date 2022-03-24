using InscryptionAPI;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Healer()
        {
            const string rulebookName = "Healer";
            const string rulebookDescription = "This card will heal a selected ally for 2 Health.";
            const string dialogue = "Never underestimate the importance of a healer.";
            Healer.ability = WstlUtils.CreateAbility<Healer>(
                Resources.sigilHealer,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    public class Healer : AbilityBehaviour // stolen from Zerg mod
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDoctor => base.Card.Info.name.ToLowerInvariant().Contains("plaguedoctor");

        private CardSlot targetedSlot = null;

        private List<GameObject> sniperIcons = new List<GameObject>();
        private GameObject sniperIconPrefab;

        private int softLock = 0;
        private bool heretic = false;
        private string invalidDialogue;
        private readonly string failDialogue = "No one to heal.";
        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. An enemy will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";
        private readonly string transformDialogue = "The time has come. A new world will come.";
        private readonly string convertDialogue = "Rise, my servants. Rise and serve me.";
        private readonly string declareDialogue = "I am death and life. Darkness and light.";
        private readonly string hereticDialogue = "Have I not chosen you, the Twelve? Yet one of you is a devil.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard ? !playerTurnEnd : playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            yield return base.PreSuccessfulTriggerSequence();

            if (!ValidAllies())
            {
                Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (!IsDoctor)
                {
                    // If no valid allies and this card is not Plague Doctor, spit out failure message then break
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f);
                    yield break;
                }

                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failAsDoctorDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.25f);

                CardSlot randSlot;
                List<CardSlot> opposingSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
                List<CardSlot> validTargets = opposingSlots.FindAll((CardSlot x) => x.Card != null);
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

                // If there are valid targets on the opposing side, heal a random one of their cards.
                // Else spit out another failure message then break
                if (validTargets.Count > 0)
                {
                    randSlot = validTargets[SeededRandom.Range(0, validTargets.Count, randomSeed)];
                    WstlCombatPhasePatcher.Instance.VisualizeConfirmSniperAbility(randSlot, false);
                    yield return new WaitForSeconds(0.25f);
                    randSlot.Card.HealDamage(2);
                    randSlot.Card.Anim.StrongNegationEffect();
                    WstlCombatPhasePatcher.Instance.VisualizeClearSniperAbility();
                    ConfigUtils.Instance.UpdateBlessings(1);
                    yield return new WaitForSeconds(0.25f);
                }
                else
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failExtraHardDialogue, -0.65f, 0.4f, Emotion.Anger);
                    yield return new WaitForSeconds(0.25f);
                    yield break;
                }

                yield return ClockTwelve();
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
                yield break;
            }

            #region OPPONENT LOGIC
            if (base.Card.OpponentCard)
            {
                // Grabs a random card from the available slots and heals it
                CardSlot randSlot;
                List<CardSlot> opponentSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card); ;
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

                randSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, randomSeed)];
                WstlCombatPhasePatcher.Instance.VisualizeConfirmSniperAbility(randSlot, false);
                yield return new WaitForSeconds(0.25f);
                randSlot.Card.HealDamage(2);
                randSlot.Card.Anim.StrongNegationEffect();
                WstlCombatPhasePatcher.Instance.VisualizeClearSniperAbility();
                if (IsDoctor)
                {
                    ConfigUtils.Instance.UpdateBlessings(1);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
            #endregion

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            yield return PlayerChooseTarget();

            bool valid = targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index);
            
            if (valid)
            {
                targetedSlot.Card.HealDamage(2);
                targetedSlot.Card.Anim.StrongNegationEffect();
                WstlCombatPhasePatcher.Instance.VisualizeClearSniperAbility();
                if (IsDoctor)
                {
                    ConfigUtils.Instance.UpdateBlessings(1);
                }
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                while (!valid)
                {
                    base.Card.Anim.StrongNegationEffect();
                    if (targetedSlot == base.Card.Slot)
                    {
                        invalidDialogue = "You must choose one of your other cards to heal.";
                    }
                    else
                    {
                        invalidDialogue = "You can't heal the air.";
                    }

                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(invalidDialogue, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.25f);

                    WstlCombatPhasePatcher.Instance.VisualizeClearSniperAbility();
                    yield return PlayerChooseTarget();

                    valid = targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index);
                    if (valid)
                    {
                        targetedSlot.Card.HealDamage(2);
                        targetedSlot.Card.Anim.StrongNegationEffect();
                        WstlCombatPhasePatcher.Instance.VisualizeClearSniperAbility();
                        if (IsDoctor)
                        {
                            ConfigUtils.Instance.UpdateBlessings(1);
                        }
                        yield return new WaitForSeconds(0.25f);
                    }
                }
            }

            if (IsDoctor)
            {
                yield return ClockTwelve();
            }
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
        }

        private IEnumerator PlayerChooseTarget()
        {
            WstlCombatPhasePatcher.Instance.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> targetSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                WstlCombatPhasePatcher.Instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }

            targetedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(targetSlots, targetSlots, delegate (CardSlot s)
            {
                targetedSlot = s;
                WstlCombatPhasePatcher.Instance.VisualizeConfirmSniperAbility(s, false);
            }, null, delegate (CardSlot s)
            {
                WstlCombatPhasePatcher.Instance.VisualizeAimSniperAbility(base.Card.Slot, s);

            }, () => false, CursorType.Target);
        }

        private IEnumerator ClockTwelve()
        {
            if (ConfigUtils.Instance.NumOfBlessings >= 12)
            {
                ConfigUtils.Instance.UpdateBlessings(-ConfigUtils.Instance.NumOfBlessings);

                CardInfo cardByName = CardLoader.GetCardByName("wstl_whiteNight");
                yield return base.Card.TransformIntoCard(cardByName);
                base.Card.Status.hiddenAbilities.Add(Ability.Flying);
                base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Flying));

                yield return new WaitForSeconds(0.2f);
                if (!PersistentValues.ClockThisRun)
                {
                    PersistentValues.ClockThisRun = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(transformDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(declareDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(convertDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }

                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(slot => slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        if (slot.Card.Info.HasTrait(Trait.Pelt) || slot.Card.Info.HasTrait(Trait.Terrain) || slot.Card.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
                        {
                            softLock++;
                            yield return slot.Card.Die(false, base.Card);
                            if (softLock >= 6)
                            {
                                softLock = 0;
                                yield break;
                            }
                        }
                        else
                        {
                            CardInfo randApostle = CardLoader.GetCardByName("wstl_apostleScythe");

                            // 1/12 chance of being Heretic, there can only be one Heretic per battle
                            if (new System.Random().Next(0, 1) == 0 && !heretic)
                            {
                                heretic = true;
                                randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
                            }
                            else
                            {
                                switch (new System.Random().Next(0, 3))
                                {
                                    case 0: // Scythe
                                        break;
                                    case 1: // Spear
                                        randApostle = CardLoader.GetCardByName("wstl_apostleSpear");
                                        break;
                                    case 2: // Staff
                                        randApostle = CardLoader.GetCardByName("wstl_apostleStaff");
                                        break;
                                }
                            }
                            yield return slot.Card.TransformIntoCard(randApostle);

                            if (heretic && !PersistentValues.ApostleHeretic)
                            {
                                PersistentValues.ApostleHeretic = true;
                                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hereticDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                                yield return new WaitForSeconds(0.2f);
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        private bool ValidAllies()
        {
            // Checks whether there are allies available to be healed.

            List<CardSlot> validSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            foreach (var slot in validSlots.Where((CardSlot slot) => slot.Card != base.Card))
            {
                if (slot.Card != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
