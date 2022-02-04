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
        private NewSpecialAbility SpecialAbility_PlagueDoctor()
        {
            const string rulebookName = "Miracle Worker";
            const string rulebookDescription = "A worker of miracles.";
            return WstlUtils.CreateSpecialAbility<PlagueDoctor>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class PlagueDoctor : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string healDialogue = "No allies to receive a blessing. An enemy will suffice instead.";
        private readonly string failDialogue = "No enemies either. It seems no blessings will be given this turn.";
        private readonly string transformDialogue = "The clock resets.";
        private readonly string convertDialogue = "Rise, my servants. Rise and serve me.";
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Miracle Worker");
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            var thisCardSlot = Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card == base.Card);
            foreach(var slot in thisCardSlot)
            {
                if (slot.Card != null)
                {
                    if (!slot.IsPlayerSlot)
                    {
                        return !playerTurnEnd;
                    }
                    return playerTurnEnd;
                }
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (!ValidAllies())
            {
                yield return new WaitForSeconds(0.5f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(healDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.5f);

                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;
                List<CardSlot> slotsWithCards = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll((CardSlot x) => x.Card != null);
                CardSlot randSlot;
                if (slotsWithCards.Count > 0)
                {
                    PersistentValues.NumberOfBlessings++;
                    randSlot = slotsWithCards[SeededRandom.Range(0, slotsWithCards.Count, randomSeed)];
                    randSlot.Card.HealDamage(2);
                    randSlot.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.5f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(failDialogue, -0.65f, 0.4f, Emotion.Anger);
                    yield return new WaitForSeconds(0.3f);
                    yield break;
                }
                Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            }

            //PersistentValues.NumberOfBlessings+=12;

            Plugin.Log.LogInfo($"The clock now strikes: [{PersistentValues.NumberOfBlessings}]");

            if (PersistentValues.NumberOfBlessings >= 12)
            {
                PersistentValues.NumberOfBlessings = 0;

                CardInfo cardByName = CardLoader.GetCardByName("wstl_whiteNight");
                yield return base.PlayableCard.TransformIntoCard(cardByName);
                yield return new WaitForSeconds(0.2f);

                if (!PersistentValues.ClockThisRun)
                {
                    PersistentValues.ClockThisRun = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(transformDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }

                bool player = true;
                var thisCardSlot = Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card == base.Card);
                foreach (var slot in thisCardSlot)
                {
                    player = slot.IsPlayerSlot;
                }

                var slotsWithCards = Singleton<BoardManager>.Instance.GetSlots(player).Where(slot => slot.Card != base.Card);
                foreach (var slot in slotsWithCards)
                {
                    if (slot.Card != null && !slot.Card.Info.HasTrait(Trait.Pelt) && !slot.Card.Info.HasTrait(Trait.Terrain))
                    {
                        CardInfo randApostle = CardLoader.GetCardByName("wstl_apostleScythe");

                        // 1/12 chance of being Heretic
                        if (new System.Random().Next(0, 12) != 0)
                        {
                            switch (new System.Random().Next(0, 2))
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
                        else
                        {
                            Plugin.Log.LogInfo($"Heretic");
                            //randApostle = CardLoader.GetCardByName("wstl_heretic");
                        }
                        yield return slot.Card.TransformIntoCard(randApostle);
                    }
                }
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(convertDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.2f);
            }
        }

        private bool ValidAllies()
        {
            int count = 0;
            bool player = true;
            var thisCardSlot = Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card == base.Card);
            foreach (var slot in thisCardSlot)
            {
                player = slot.IsPlayerSlot;
            }
            var slotsWithCards = Singleton<BoardManager>.Instance.GetSlots(player).Where(slot => slot.Card != base.Card);
            foreach (var slot in slotsWithCards)
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
