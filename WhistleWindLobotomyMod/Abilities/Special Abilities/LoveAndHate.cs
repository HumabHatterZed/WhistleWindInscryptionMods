using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class LoveAndHate : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Love and Hate";
        public static readonly string rDesc = "Magical Girl H transforms on upkeep when 2 more ally cards than opponent cards have died or vice versa. Gain a special card when all 4 Magical Girls or their evolutions are on the same side of the board.";

        private int allyDeaths;
        private int opponentDeaths;
        private bool IsMagical => base.PlayableCard.Info.name == "wstl_magicalGirlHeart";

        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && IsMagical && base.PlayableCard.OnBoard && killer != null;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return IsMagical && base.PlayableCard.OnBoard && base.PlayableCard.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnResolveOnBoard() => CheckForMagicGirls();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForMagicGirls();
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // does not increment if Magical Girl H is the killer
            if (killer != base.PlayableCard)
            {
                if (card.OpponentCard)
                    opponentDeaths++;
                else
                    allyDeaths++;
            }
            yield break;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // 2 more player cards have died than opponent cards
            if (allyDeaths > opponentDeaths + 1)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                // If on opponent's side, move to player's if there's room, otherwise create in hand
                if (base.PlayableCard.OpponentCard)
                {
                    if (base.PlayableCard.Slot.opposingSlot.Card != null)
                    {
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);

                        if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                        {
                            yield return new WaitForSeconds(0.2f);
                            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                            yield return new WaitForSeconds(0.2f);
                        }
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(evolution);
                        yield return new WaitForSeconds(0.45f);
                    }
                    else
                    {
                        yield return MoveToSlot(false, base.PlayableCard.Slot.opposingSlot);
                        yield return new WaitForSeconds(0.25f);
                    }
                }
                yield return PlayDialogue();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            }

            // 2 more Leshy card deaths than player card deaths
            if (allyDeaths + 1 < opponentDeaths)
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                List<CardSlot> giantCheck = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => s.Card != null && s.Card.Info.HasTrait(Trait.Giant)).ToList();
                // if owned by the player and there are no giant cards, go to the opponent's side
                if (!base.PlayableCard.OpponentCard && giantCheck.Count() == 0)
                {
                    CardSlot opposingSlot = base.PlayableCard.Slot.opposingSlot;
                    List<CardSlot> queuedSlots = Singleton<TurnManager>.Instance.Opponent.QueuedSlots;

                    // if the opposing slot is empty, move over to it
                    if (opposingSlot.Card == null)
                    {
                        LobotomyPlugin.Log.LogDebug("Moving Queen of Hatred to opposing slot.");
                        yield return MoveToSlot(true, opposingSlot);
                    }
                    // if the opposing slot is occupied add to queue
                    else
                    {
                        LobotomyPlugin.Log.LogDebug("Adding Queen of Hatred to queue.");
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);
                        AbnormalMethods.QueueCreatedCard(evolution);
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }
        }

        private CardInfo GetEvolve(PlayableCard card)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            return evolution;
        }
        private IEnumerator PlayDialogue()
        {
            if (!DialogueEventsData.EventIsPlayed("MagicalGirlHeartTransform"))
                yield return DialogueEventsManager.PlayDialogueEvent("MagicalGirlHeartTransform");
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Good cannot exist without evil.");
                yield return new WaitForSeconds(0.2f);
            }
        }
        private IEnumerator PerformTransformation(CardInfo evolution)
        {
            yield return new WaitForSeconds(0.15f);
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator MoveToSlot(bool opponent, CardSlot slot)
        {
            base.PlayableCard.SetIsOpponentCard(opponent);
            base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, slot, 0.25f);
        }

        private IEnumerator CheckForMagicGirls()
        {
            // Break if already have Jester
            if (WstlSaveManager.OwnsJesterOfNihil)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Jester of Nihil.");
                yield break;
            }

            CardSlot greedSlot = null;
            CardSlot despairSlot = null;
            CardSlot wrathSlot = null;
            foreach (CardSlot slot in AbnormalMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    string slotName = slot.Card.Info.name;
                    if (slotName == "wstl_magicalGirlDiamond" || slotName == "wstl_kingOfGreed")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Diamond.");
                        greedSlot = slot;
                        continue;
                    }
                    if (slotName == "wstl_magicalGirlSpade" || slotName == "wstl_knightOfDespair")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Spade.");
                        despairSlot = slot;
                        continue;
                    }
                    if (slotName == "wstl_magicalGirlClover" || slotName == "wstl_servantOfWrath")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Clover.");
                        wrathSlot = slot;
                        continue;
                    }
                }
            }
            if (greedSlot != null && despairSlot != null && wrathSlot != null)
                yield return Entropy(greedSlot, despairSlot, wrathSlot);

            yield break;
        }

        private IEnumerator Entropy(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            greed.Card.Anim.StrongNegationEffect();
            despair.Card.Anim.StrongNegationEffect();
            wrath.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilIntro", 0f);

            // turn out the lights, activate table effects, remove magic girls
            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: false);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: false);
            yield return BoardEffects.EntropyTableEffects();
            RemoveMagic(greed, despair, wrath);

            yield return new WaitForSeconds(0.4f);

            // switch to default view while the lights are off
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, true);

            yield return new WaitForSeconds(0.4f);

            if (!DialogueEventsData.EventIsPlayed("JesterOfNihilStory"))
                yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilStory");
            else
                yield return new WaitForSeconds(0.4f);

            if (Singleton<VideoCameraRig>.Instance != null)
                Singleton<VideoCameraRig>.Instance.PlayCameraAnim("refocus_quick");

            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: true);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: true);

            // Show hand so player can see the jester
            yield return new WaitForSeconds(0.4f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);

            // add jester to deck and hand
            CardInfo info = CardLoader.GetCardByName("wstl_jesterOfNihil");
            RunState.Run.playerDeck.AddCard(info);

            // set cost to 0 for this fight (can play immediately that way)
            info.cost = 0;
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0f, null);
            WstlSaveManager.OwnsJesterOfNihil = true;
            yield return new WaitForSeconds(0.2f);

            yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilOutro");

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private void RemoveMagic(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            // Remove cards
            greed.Card.RemoveFromBoard(true, 0f);
            despair.Card.RemoveFromBoard(true, 0f);
            wrath.Card.RemoveFromBoard(true, 0f);
            base.PlayableCard.RemoveFromBoard(true, 0f);
        }
    }
    public class RulebookEntryLoveAndHate : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_LoveAndHate()
        {
            RulebookEntryLoveAndHate.ability = AbilityHelper.CreateRulebookAbility<RulebookEntryLoveAndHate>(LoveAndHate.rName, LoveAndHate.rDesc).Id;
        }
        private void SpecialAbility_LoveAndHate()
        {
            LoveAndHate.specialAbility = AbilityHelper.CreateSpecialAbility<LoveAndHate>(LoveAndHate.rName).Id;
        }
    }
}
