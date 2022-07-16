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
            Healer.ability = AbilityHelper.CreateAbility<Healer>(
                Resources.sigilHealer,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    public class Healer : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDoctor => base.Card.Info.name.ToLowerInvariant().Contains("plaguedoctor");

        private CardSlot targetedSlot = null;

        private int softLock = 0;
        private string invalidDialogue;
        private readonly string failDialogue = "No one to heal.";
        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";
        private readonly string eventDialogue = "[c:bR]The time has come. A new world will come.[c:]";
        private readonly string eventDialogue2 = "[c:bR]I am death and life. Darkness and light.[c:]";
        private readonly string eventDialogue3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private readonly string eventDialogueA = "[c:bR]The time has come again. I will be thy guide.[c:]";
        private readonly string hereticDialogue = "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.Card != null)
            {
                return base.Card.OpponentCard != playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;

            yield return base.PreSuccessfulTriggerSequence();
            Card.Anim.StrongNegationEffect();
            // Checks whether there are other cards on this card's side of the board that can be healed
            if (!ValidAllies())
            {
                yield return new WaitForSeconds(0.4f);
                // If this card is not the Plague Doctor, spit out a failure message then break
                // Otherwise, check for valid opponent cards to heal instead
                if (!IsDoctor)
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f);
                    yield break;
                }
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failAsDoctorDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.25f);

                CardSlot randSlot;
                List<CardSlot> opposingSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
                List<CardSlot> validTargets = opposingSlots.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card);
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

                // If there are valid targets on the opposing side, heal a random one of their cards.
                // Else spit out another failure message then break
                if (validTargets.Count > 0)
                {
                    randSlot = validTargets[SeededRandom.Range(0, validTargets.Count, randomSeed)];
                    CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(randSlot, false);
                    yield return new WaitForSeconds(0.25f);
                    randSlot.Card.HealDamage(2);
                    randSlot.Card.Anim.StrongNegationEffect();
                    CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
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
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
                // Call the Clock if an opponent is healed
                yield return ClockTwelve();
                yield break;
            }
            // Logic for opponent cards
            // Heals a randomly selected card from the available pool
            if (base.Card.OpponentCard)
            {
                CardSlot randSlot;
                List<CardSlot> opponentSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card); ;
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

                randSlot = opponentSlots[SeededRandom.Range(0, opponentSlots.Count, randomSeed)];
                CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(randSlot, false);
                yield return new WaitForSeconds(0.25f);
                randSlot.Card.HealDamage(2);
                randSlot.Card.Anim.StrongNegationEffect();
                CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
                if (IsDoctor)
                {
                    ConfigUtils.Instance.UpdateBlessings(1);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.ChoosingSlotViewMode);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            yield return PlayerChooseTarget();

            bool valid = targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index);
            
            // If the chosen target is invalid, loop until one of the valid targets is chosen
            if (!valid)
            {
                while (!valid)
                {
                    invalidDialogue = targetedSlot == base.Card.Slot ? "You must choose one of your other cards to heal." : "You can't heal the air.";
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(invalidDialogue, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.25f);

                    CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
                    yield return PlayerChooseTarget();

                    valid = targetedSlot != null && (targetedSlot.Card != null && targetedSlot.Card != base.Card && targetedSlot.Index != base.Card.Slot.Index);
                }
            }
            targetedSlot.Card.HealDamage(2);
            targetedSlot.Card.Anim.StrongNegationEffect();
            CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
            yield return new WaitForSeconds(0.25f);
            yield return base.LearnAbility();
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            if (IsDoctor)
            {
                ConfigUtils.Instance.UpdateBlessings(1);
                yield return ClockTwelve();
            }
        }

        // Call the Clock
        private IEnumerator ClockTwelve()
        {
            // If Blessings are between (0,11), break
            if (0 <= ConfigUtils.Instance.NumOfBlessings && ConfigUtils.Instance.NumOfBlessings < 12)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
            // If blessings are in the negatives (aka someone cheated), wag a finger and go 'nuh-uh-uh!'
            if (ConfigUtils.Instance.NumOfBlessings < 0)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]Thou cannot stop my ascension. Even the tutelary bows to mine authority.[c:]", -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
            }
            // Reset the number of Blessings to 0 and change Leshy's eyes to red
            //ConfigUtils.Instance.UpdateBlessings(-ConfigUtils.Instance.NumOfBlessings);
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));
            // Transform the Doctor into Him
            yield return base.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            base.Card.Status.hiddenAbilities.Add(Ability.Flying);
            base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Flying));
            yield return new WaitForSeconds(0.2f);
            // Create dialogue depending on whether this is the first time this has happened this run
            if (!PersistentValues.ClockThisRun)
            {
                PersistentValues.ClockThisRun = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventDialogue2, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventDialogue3, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
            }
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventDialogueA, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(eventDialogue3, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
            }
            yield return new WaitForSeconds(0.2f);
            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            bool heretic = false;
            bool sinful = new List<CardInfo>(RunState.DeckList).FindAll((CardInfo info) => info.name == "wstl_oneSin").Count() > 0;
            // Kill non-living/Mule cards and transform the rest (excluding One Sin) into Apostles
            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                {
                    if (slot.Card.Info.HasTrait(Trait.Pelt) || slot.Card.Info.HasTrait(Trait.Terrain) ||
                        slot.Card.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
                    {
                        if ((slot.Card.HasAbility(Ability.DrawCopy) || slot.Card.HasAbility(Ability.DrawCopyOnDeath)) && slot.Card.HasAbility(Ability.CorpseEater))
                        {
                            slot.Card.Info.RemoveBaseAbility(Ability.CorpseEater);
                        }
                        if (slot.Card.HasAbility(Ability.IceCube))
                        {
                            slot.Card.Info.RemoveBaseAbility(Ability.IceCube);
                        }
                        yield return slot.Card.Die(false, base.Card);
                        softLock++;
                        if (softLock >= 3)
                        {
                            softLock = 0;
                            WstlPlugin.Log.LogWarning("Stuck in a loop, forcing removal of card.");
                            slot.Card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = slot.Card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            slot.Card.ExitBoard(0.3f, Vector3.zero);
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    else
                    {
                        CardInfo randApostle = SeededRandom.Range(0, 3, base.GetRandomSeed()) switch
                        {
                            0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                            1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                            _ => CardLoader.GetCardByName("wstl_apostleStaff")
                        };
                        if (!heretic && !sinful)
                        {
                            if (new System.Random().Next(0, 12) == 0)
                            {
                                heretic = true;
                                randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
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
            // If the player has One Sin
            if (sinful)
            {
                yield return new WaitForSeconds(0.5f);
                // if there is a One Sin on the board
                if (Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot slot) => slot.Card != null && slot.Card.Info.name == "wstl_oneSin").Count > 0)
                {
                    foreach (CardSlot slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where(s => s.Card != null && s.Card.Info.name == "wstl_oneSin"))
                    {
                        // Transform the first One Sin into Heretic
                        // Remove the rest
                        if (!heretic)
                        {
                            heretic = true;
                            yield return slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        }
                        else
                        {
                            slot.Card.Dead = true;
                            slot.Card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = slot.Card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            slot.Card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }
                }
                else
                {
                    // Transform into Heretic
                    yield return new WaitForSeconds(0.25f);
                    foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_oneSin"))
                    {
                        if (!heretic)
                        {
                            heretic = true;
                            yield return card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                            yield return new WaitForSeconds(0.5f);
                        }
                        else
                        {
                            card.Dead = true;
                            card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }

                }
                // Spawn card to hand if One Sin is in the deck or dead
                if (!heretic)
                {
                    heretic = true;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
            }
            //Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.2f);
        }
        // Stolen from Zerg mod with love <3
        private IEnumerator PlayerChooseTarget()
        {
            CombatPhaseManagerPatch.Instance.VisualizeStartSniperAbility(base.Card.Slot);

            List<CardSlot> targetSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.OpponentSlotsCopy : Singleton<BoardManager>.Instance.PlayerSlotsCopy;
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;

            if (cardSlot != null && targetSlots.Contains(cardSlot))
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, cardSlot);
            }

            targetedSlot = null;

            yield return Singleton<BoardManager>.Instance.ChooseTarget(targetSlots, targetSlots, delegate (CardSlot s)
            {
                targetedSlot = s;
                CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(s, false);
            }, null, delegate (CardSlot s)
            {
                CombatPhaseManagerPatch.Instance.VisualizeAimSniperAbility(base.Card.Slot, s);

            }, () => false, CursorType.Target);
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
