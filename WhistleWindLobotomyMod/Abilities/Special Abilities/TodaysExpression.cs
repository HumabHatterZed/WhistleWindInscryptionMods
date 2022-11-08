using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class TodaysExpression : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Today's Expression";
        public static readonly string rDesc = "Today's Shy Look changes forme when drawn.";
        public override bool RespondsToDrawn() => true;
        public override void OnShownInDeckReview() => this.ChangeFormeDeck();
        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.ChangeFormeDeck();
            yield break;
        }

        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.PlayableCard);
            yield return base.PlayableCard.FlipInHand(ChangeForme);
            yield return new WaitForSeconds(0.1f);
            switch (base.Card.Info.name)
            {
                case "wstl_todaysShyLookAngry":
                    yield return DialogueEventsManager.PlayDialogueEvent("TodaysShyLookAngry");
                    break;
                case "wstl_todaysShyLookHappy":
                    yield return DialogueEventsManager.PlayDialogueEvent("TodaysShyLookHappy");
                    break;
                case "wstl_todaysShyLookNeutral":
                    yield return DialogueEventsManager.PlayDialogueEvent("TodaysShyLookNeutral");
                    break;
            }
        }

        private void ChangeForme()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_todaysShyLookNeutral");

            int rand = SeededRandom.Range(0, 3, base.GetRandomSeed());
            switch (rand)
            {
                case 0:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookAngry");
                    break;
                case 1:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookHappy");
                    break;
                default:
                    break;
            }
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }

        private void ChangeFormeDeck()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_todaysShyLookNeutral");

            int rand = UnityEngine.Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookAngry");
                    break;
                case 1:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookHappy");
                    break;
                case 2:
                    break;
            }
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }
    }
    public class RulebookEntryTodaysExpression : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_TodaysExpression()
        {
            RulebookEntryTodaysExpression.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTodaysExpression>(TodaysExpression.rName, TodaysExpression.rDesc).Id;
        }
        private void SpecialAbility_TodaysExpression()
        {
            TodaysExpression.specialAbility = LobotomyAbilityHelper.CreateSpecialAbility<TodaysExpression>(TodaysExpression.rName).Id;
        }
    }
}
