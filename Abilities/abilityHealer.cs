using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Healer()
        {
            const string rulebookName = "Healer";
            const string rulebookDescription = "This card will heal a selected ally for 2 Health.";
            const string dialogue = "Never underestimate the importance of a healer.";
            return WstlUtils.CreateAbility<Healer>(
                Resources.sigilHealer,
                rulebookName, rulebookDescription, dialogue, 3);
        }
    }
    public class Healer : AbilityBehaviour // stolen from Zerg mod
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardSlot targetedSlot = null;

        private bool heretic = false;
        private bool IsDoctor => base.Card.Info.name.ToLowerInvariant().Contains("plaguedoctor");
        private string invalidDialogue;

        private readonly string healDialogue = "No allies to receive a blessing. An enemy will suffice instead.";
        private readonly string failDialogue = "No enemies either. It seems no blessings will be given this turn.";

        //WhiteNight
        private readonly string transformDialogue = "The time has come. A new world will come.";
        private readonly string convertDialogue = "Rise, my servants. Rise and serve me.";
        private readonly string declareDialogue = "I am death and life. Darkness and light.";
        private readonly string hereticDialogue = "Have I not chosen you, the Twelve? Yet one of you is a devil.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.Slot.IsPlayerSlot ? playerTurnEnd : !playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode, false);
            if (!ValidAllies())
            {
                Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.5f);
                if (IsDoctor)
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(healDialogue, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.25f);

                    int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;
                    List<CardSlot> slotsWithCards = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => x.Card != null);
                    CardSlot randSlot;
                    if (slotsWithCards.Count > 0)
                    {
                        PersistentValues.NumberOfBlessings++;
                        Plugin.Log.LogInfo($"The clock now strikes: [{PersistentValues.NumberOfBlessings}]");

                        randSlot = slotsWithCards[SeededRandom.Range(0, slotsWithCards.Count, randomSeed)];
                        randSlot.Card.HealDamage(2);
                        randSlot.Card.Anim.LightNegationEffect();
                        yield return new WaitForSeconds(0.25f);
                    }
                    else
                    {
                        base.Card.Anim.StrongNegationEffect();
                        yield return new WaitForSeconds(0.3f);
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f, Emotion.Anger);
                        yield return new WaitForSeconds(0.3f);
                        yield break;
                    }
                    Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
                    Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);

                    yield return ClockTwelve();
                }
                yield break;
            }

            IEnumerator selectTarget = ChooseTarget();
            yield return selectTarget;

            if (targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index))
            {
                if (IsDoctor)
                {
                    PersistentValues.NumberOfBlessings++;
                    Plugin.Log.LogInfo($"The clock now strikes: [{PersistentValues.NumberOfBlessings}]");
                }
                targetedSlot.Card.HealDamage(2);
                targetedSlot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                while (!(targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index)))
                {
                    base.Card.Anim.StrongNegationEffect();
                    if (targetedSlot == base.Card.Slot)
                    {
                        invalidDialogue = "You must choose one of your other cards to heal.";
                    }
                    else
                    {
                        int rand = new System.Random().Next(0, 3);
                        switch (rand)
                        {
                            case 0:
                                invalidDialogue = "Your creature demands a proper target.";
                                break;
                            case 1:
                                invalidDialogue = "You can't heal the air.";
                                break;
                            case 2:
                                invalidDialogue = "There's nothing there.";
                                break;
                        }
                    }
                    yield return new WaitForSeconds(0.3f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(invalidDialogue, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.3f);
                    IEnumerator reSelectTarget = ChooseTarget();
                    yield return reSelectTarget;

                    if (targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index))
                    {
                        if (IsDoctor)
                        {
                            PersistentValues.NumberOfBlessings++;
                            Plugin.Log.LogInfo($"The clock now strikes: [{PersistentValues.NumberOfBlessings}]");
                        }
                        targetedSlot.Card.HealDamage(2);
                        targetedSlot.Card.Anim.LightNegationEffect();
                        yield return new WaitForSeconds(0.25f);
                    }
                }
            }
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<CombatPhaseManager>.Instance.VisualizeClearSniperAbility();

            if (IsDoctor)
            {
                yield return ClockTwelve();
            }
        }

        private IEnumerator ChooseTarget()
        {
            //Plugin.Log.LogInfo("ChooseTarget A");
            CombatPhaseManager combatPhaseManager = Singleton<CombatPhaseManager>.Instance;
            BoardManager boardManager = Singleton<BoardManager>.Instance;
            List<CardSlot> allSlots = new List<CardSlot>(boardManager.AllSlots);
            List<CardSlot> playerSlots = new List<CardSlot>(boardManager.GetSlots(true));

            Action<CardSlot> callback1 = null;
            Action<CardSlot> callback2 = null;

            //Plugin.Log.LogInfo("ChooseTarget B");
            combatPhaseManager.VisualizeStartSniperAbility(Card.slot);

            //Plugin.Log.LogInfo("ChooseTarget C");
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;
            if (cardSlot != null && allSlots.Contains(cardSlot))
            {
                combatPhaseManager.VisualizeAimSniperAbility(Card.slot, cardSlot);
            }

            //Plugin.Log.LogInfo("ChooseTarget D");
            List<CardSlot> allTargetSlots = allSlots;
            List<CardSlot> validTargetSlots = playerSlots;

            //Plugin.Log.LogInfo("ChooseTarget E");
            targetedSlot = null;
            Action<CardSlot> targetSelectedCallback;
            if ((targetSelectedCallback = callback1) == null)
            {
                targetSelectedCallback = (callback1 = delegate (CardSlot s)
                {
                    targetedSlot = s;
                    combatPhaseManager.VisualizeConfirmSniperAbility(s);
                });
            }

            //Plugin.Log.LogInfo("ChooseTarget F");
            Action<CardSlot> invalidTargetCallback = null;
            Action<CardSlot> slotCursorEnterCallback;
            if ((slotCursorEnterCallback = callback2) == null)
            {
                slotCursorEnterCallback = (callback2 = delegate (CardSlot s)
                {
                    combatPhaseManager.VisualizeAimSniperAbility(Card.slot, s);
                });
            }

            //Plugin.Log.LogInfo("ChooseTarget G");
            yield return boardManager.ChooseTarget(allTargetSlots, validTargetSlots, targetSelectedCallback, invalidTargetCallback, slotCursorEnterCallback, () => false, CursorType.Target);
        }
        private IEnumerator ClockTwelve()
        {
            if (PersistentValues.NumberOfBlessings >= 12)
            {
                PersistentValues.NumberOfBlessings = 0;

                CardInfo cardByName = CardLoader.GetCardByName("wstl_whiteNight");
                yield return base.Card.TransformIntoCard(cardByName);
                yield return base.Card.RenderInfo.forceEmissivePortrait = true;
                base.Card.Status.hiddenAbilities.Add(Ability.Flying);
                base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Flying));
                base.Card.RenderCard();

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
                            yield return slot.Card.Die(false, base.Card);
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
            int count = 0;
            bool playerSlot = base.Card.Slot.IsPlayerSlot;
            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(playerSlot).Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null)
                {
                    count++;
                }
            }
            return count > 0;
        }
    }
}
