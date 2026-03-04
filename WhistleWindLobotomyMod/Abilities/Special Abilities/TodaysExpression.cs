using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class TodaysExpression : SpecialCardBehaviour {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Today's Expression";
        public const string rDesc = "Today's Shy Look changes forme when drawn.";
        public override bool RespondsToDrawn() => true;
        public override bool RespondsToResolveOnBoard() => base.PlayableCard.OpponentCard;
        public override void OnShownInDeckReview() => this.ChangeFormeDeck();
        public override IEnumerator OnSelectedForDeckTrial() {
            this.ChangeFormeDeck();
            yield break;
        }

        public override IEnumerator OnDrawn() {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.PlayableCard);
            yield return base.PlayableCard.FlipInHand(ChangeForme);
            yield return new WaitForSeconds(0.1f);

            yield return DialogueHelper.PlayDialogueEvent(base.Card.Info.name switch {
                Cards.todaysShyLookAngry => "TodaysShyLookAngry",
                Cards.todaysShyLookHappy => "TodaysShyLookHappy",
                _ => "TodaysShyLookNeutral"
            });
        }
        public override IEnumerator OnResolveOnBoard() {
            GlobalTriggerHandler.Instance.NumTriggersThisBattle++;
            int rand = SeededRandom.Range(0, 3, base.GetRandomSeed());
            CardInfo cardByName = GetRandomForme(rand);
            cardByName.abilities.Clear();

            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.5f);

            yield return DialogueHelper.PlayDialogueEvent(base.Card.Info.name switch {
                Cards.todaysShyLookAngry => "TodaysShyLookAngry",
                Cards.todaysShyLookHappy => "TodaysShyLookHappy",
                _ => "TodaysShyLookNeutral"
            });
        }

        private void ChangeForme() {
            GlobalTriggerHandler.Instance.NumTriggersThisBattle++;
            int rand = SeededRandom.Range(0, 3, base.GetRandomSeed());
            CardInfo cardByName = GetRandomForme(rand);

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable)) {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }

        private void ChangeFormeDeck() {
            int rand = UnityEngine.Random.Range(0, 3);
            CardInfo cardByName = GetRandomForme(rand);

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable)) {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }

        private CardInfo GetRandomForme(int index) {
            return index switch {
                0 => CardLoader.GetCardByName(Cards.todaysShyLookAngry),
                1 => CardLoader.GetCardByName(Cards.todaysShyLookHappy),
                _ => CardLoader.GetCardByName(Cards.todaysShyLookNeutral),
            };
        }
    }
    public class RulebookEntryTodaysExpression : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_TodaysExpression()
            => RulebookEntryTodaysExpression.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTodaysExpression>(TodaysExpression.rName, TodaysExpression.rDesc).Id;
        private static void AddSpecial_TodaysExpression()
            => TodaysExpression.specialAbility = AbilityHelper.CreateSpecialAbility<TodaysExpression>(LobotomyPlugin.pluginGuid, TodaysExpression.rName).Id;
    }
}
