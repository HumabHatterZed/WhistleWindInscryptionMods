using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class LoveAndHate : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Love and Hate";
        public const string rDesc = "Magical Girl H will keep track of the number of allied and opposing cards that have been killed. When the difference of the two is at least 2, transform then ally itself with whomever has lost more.";

        private int cardDeathBalance; // positive == more ally deaths, negative == more opponent deaths

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
            => fromCombat && killer != null && killer != base.PlayableCard;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep && Mathf.Abs(cardDeathBalance) >= 2;

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            cardDeathBalance += card.OpponentCard ? -1 : 1;
            yield break;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (cardDeathBalance > 0)
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

                        HelperMethods.ChangeCurrentView(View.Hand);

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
            else
            {
                CardInfo evolution = GetEvolve(base.PlayableCard);

                yield return PerformTransformation(evolution);

                bool giantCard = Singleton<BoardManager>.Instance.OpponentSlotsCopy.FindAll(s => s.Card != null && s.Card.HasTrait(Trait.Giant)).Count > 0;

                if (!base.PlayableCard.OpponentCard && !giantCard)
                {
                    CardSlot opposingSlot = base.PlayableCard.Slot.opposingSlot;

                    if (opposingSlot.Card == null) // if the opposing slot is empty, move over to it
                    {
                        LobotomyPlugin.Log.LogDebug("Moving Queen of Hatred to opposing slot.");
                        yield return MoveToSlot(true, opposingSlot);
                    }
                    else // if the opposing slot is occupied add to queue
                    {
                        LobotomyPlugin.Log.LogDebug("Adding Queen of Hatred to queue.");
                        base.PlayableCard.RemoveFromBoard();
                        yield return new WaitForSeconds(0.5f);
                        HelperMethods.QueueCreatedCard(evolution);
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
                yield return DialogueHelper.PlayDialogueEvent("MagicalGirlHeartTransform");
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
    }
    public class RulebookEntryLoveAndHate : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_LoveAndHate()
            => RulebookEntryLoveAndHate.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryLoveAndHate>(LoveAndHate.rName, LoveAndHate.rDesc).Id;
        private void SpecialAbility_LoveAndHate()
            => LoveAndHate.specialAbility = AbilityHelper.CreateSpecialAbility<LoveAndHate>(pluginGuid, LoveAndHate.rName).Id;
    }
}
