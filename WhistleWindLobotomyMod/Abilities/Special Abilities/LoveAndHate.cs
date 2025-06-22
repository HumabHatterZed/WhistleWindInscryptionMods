using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class LoveAndHate : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "In the Name of Love and Hate";
        public const string rDesc = "When 2 more ally cards have died than opposing cards or vice versa, Magical Girl will transform then move to the side of the board that lost more cards.";

        private int cardDeathBalance; // positive == more ally deaths, negative == more opponent deaths

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
            => fromCombat && killer != null && killer != base.PlayableCard;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep && Mathf.Abs(cardDeathBalance) > 2;

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            cardDeathBalance += card.OpponentCard == base.PlayableCard.OpponentCard ? -1 : 1;
            yield break;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            CardInfo evolution = GetEvolve(base.PlayableCard);
            CardSlot opposingSlot = base.PlayableCard.Slot.opposingSlot;
            yield return PerformTransformation(evolution);

            // positive death balance means Queen of Hatred will go to the opposing side
            if (cardDeathBalance > 0) {
                if (opposingSlot.Card == null) {
                    LobotomyPlugin.Log.LogDebug("Moving Queen of Hatred to opposing slot.");
                    yield return MoveToSlot(!base.PlayableCard.OpponentCard, opposingSlot);
                }
                else {
                    LobotomyPlugin.Log.LogDebug("Adding Queen of Hatred to queue.");
                    base.PlayableCard.RemoveFromBoard();
                    yield return new WaitForSeconds(0.5f);
                    if (base.PlayableCard.OpponentCard) {
                        ViewManager.Instance.SwitchToView(View.Default);
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(evolution);
                        yield return new WaitForSeconds(0.45f);
                    }
                    else if (BoardManager.Instance.GetOpponentCards(x => x.HasTrait(Trait.Giant)).Count < BoardManager.Instance.OpponentSlotsCopy.Count) {
                        ViewManager.Instance.SwitchToView(View.Board);
                        yield return CombatHelpers.QueueCreatedCard(evolution);
                        yield return new WaitForSeconds(0.25f);
                    }
                }
            }
            yield return PlayDialogue();
        }

        private CardInfo GetEvolve(PlayableCard card) {
            CardInfo evolution = CardLoader.GetCardByName(SaveManager.SaveFile.IsPart1 ? Cards.queenOfHatred : Cards.queenOfHatredPixel);
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable)) {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            return evolution;
        }
        private IEnumerator PlayDialogue() {
            if (!DialogueEventsData.EventIsPlayed("MagicalGirlHeartTransform"))
                yield return DialogueHelper.PlayDialogueEvent("MagicalGirlHeartTransform");
            else {
                yield return DialogueHelper.ShowUntilInput("Good cannot exist without evil.");
                yield return new WaitForSeconds(0.2f);
            }
        }
        private IEnumerator PerformTransformation(CardInfo evolution) {
            yield return new WaitForSeconds(0.15f);
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
        }
        private IEnumerator MoveToSlot(bool setOpponent, CardSlot slot) {
            base.PlayableCard.SetIsOpponentCard(setOpponent);
            base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
            yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, slot, 0.25f);
        }
    }
    public class RulebookEntryLoveAndHate : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities {
        private static void Rulebook_LoveAndHate()
            => RulebookEntryLoveAndHate.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryLoveAndHate>(LoveAndHate.rName, LoveAndHate.rDesc).Id;
        private static void AddSpecial_LoveAndHate()
            => LoveAndHate.specialAbility = AbilityHelper.CreateSpecialAbility<LoveAndHate>(LobotomyPlugin.pluginGuid, LoveAndHate.rName).Id;
    }
}
