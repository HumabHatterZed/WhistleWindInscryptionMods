using InscryptionAPI;
using InscryptionAPI.Card;
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
                Resources.sigilHealer, Resources.sigilHealer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Healer : BaseDoctor
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDoctor => base.Card.Info.name.ToLowerInvariant().Contains("plaguedoctor");

        private CardSlot targetedSlot = null;

        private string invalidDialogue;
        private readonly string failDialogue = "No one to heal.";
        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";
        private readonly string eventDialogue = "[c:bR]The time has come. A new world will come.[c:]";
        private readonly string eventDialogue2 = "[c:bR]I am death and life. Darkness and light.[c:]";
        private readonly string eventDialogue3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private readonly string eventDialogueA = "[c:bR]The time has come again. I will be thy guide.[c:]";

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
                    base.Card.Anim.LightNegationEffect();
                    ConfigUtils.Instance.UpdateBlessings(1);
                    UpdatePortrait();
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
                    base.Card.Anim.LightNegationEffect();
                    ConfigUtils.Instance.UpdateBlessings(1);
                    UpdatePortrait();
                    yield return new WaitForSeconds(0.15f);
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
                base.Card.Anim.LightNegationEffect();
                ConfigUtils.Instance.UpdateBlessings(1);
                UpdatePortrait();
                yield return new WaitForSeconds(0.15f);
                yield return ClockTwelve();
            }
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
            // Change Leshy's eyes to red
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));
            // Transform the Doctor into Him
            yield return base.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            base.Card.Status.hiddenAbilities.Add(Ability.Flying);
            base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Flying));
            yield return new WaitForSeconds(0.2f);
            // Create dialogue depending on whether this is the first time this has happened this run
            if (!WstlSaveManager.ClockThisRun)
            {
                WstlSaveManager.ClockThisRun = true;
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
            bool sinful = new List<CardInfo>(RunState.DeckList).FindAll((CardInfo info) => info.name == "wstl_oneSin").Count() > 0;

            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                {
                    yield return base.ConvertToApostle(slot.Card, sinful);
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
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
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
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
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
                if (!HasHeretic)
                {
                    HasHeretic = true;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        private void UpdatePortrait()
        {
            Texture2D portrait;
            Texture2D emissive;

            switch (ConfigUtils.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11_emission);
                    break;
            }

            base.Card.Info.SetPortrait(portrait, emissive);
            base.Card.UpdateStatsText();
        }
    }
}
