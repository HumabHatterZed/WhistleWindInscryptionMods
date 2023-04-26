using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class ThreeBirds : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Three Birds";
        public const string rDesc = "Gain a special card when Punishing Bird, Judgement Bird, and Big Bird are on the same side of the board.";

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForOtherCards();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForOtherCards();

        private IEnumerator CheckForOtherCards()
        {
            if (LobotomySaveManager.OwnsApocalypseBird)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Apocalypse Bird.");
                yield break;
            }

            CardSlot punishingBird = null;
            CardSlot judgementBird = null;

            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot.Card.Info.name == "wstl_punishingBird")
                    punishingBird = slot;
                else if (slot.Card.Info.name == "wstl_judgementBird")
                    judgementBird = slot;
            }

            if (punishingBird == null || judgementBird == null)
                yield break;

            yield return Apocalypse(punishingBird, judgementBird);
        }

        private IEnumerator Apocalypse(CardSlot smallSlot, CardSlot longSlot)
        {
            bool opponentCard = base.PlayableCard.OpponentCard;

            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdIntro");

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            if (!DialogueEventsData.EventIsPlayed("ApocalypseBirdOutro"))
            {
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                yield return new WaitForSeconds(0.5f);

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory1");

                // Look down at the board
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.25f);

                smallSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStorySmall");
                longSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStoryLong");
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStoryBig");

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory2");
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default, lockAfter: true);
            yield return new WaitForSeconds(0.2f);

            // Remove cards
            smallSlot.Card.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.2f);
            longSlot.Card.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.2f);
            base.PlayableCard.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.5f);

            yield return BoardEffects.ApocalypseTableEffects();
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory3");

            CardInfo info = CardLoader.GetCardByName("wstl_apocalypseBird");
            if (opponentCard)
            {
                List<CardSlot> validSlots = HelperMethods.GetSlotsCopy(opponentCard).FindAll(x => x.Card == null);
                if (validSlots.Count > 0)
                {
                    HelperMethods.ChangeCurrentView(View.Board, 0.4f);
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(info, validSlots[SeededRandom.Range(0, validSlots.Count - 1, RunState.RandomSeed)], resolveTriggers: false);
                }
                else
                {
                    HelperMethods.ChangeCurrentView(View.OpponentQueue, 0.4f);
                    yield return HelperMethods.QueueCreatedCard(info);
                }
            }
            else
            {
                HelperMethods.ChangeCurrentView(View.Hand, 0.4f);

                RunState.Run.playerDeck.AddCard(info);
                info.cost = 0;
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);

                LobotomySaveManager.OwnsApocalypseBird = true;
                LobotomySaveManager.UnlockedApocalypseBird = true;
            }

            yield return new WaitForSeconds(0.2f);

            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory4");
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdOutro");

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }
    }
    public class RulebookEntryThreeBirds : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_ThreeBirds()
            => RulebookEntryThreeBirds.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryThreeBirds>(ThreeBirds.rName, ThreeBirds.rDesc).Id;
        private void SpecialAbility_ThreeBirds()
            => ThreeBirds.specialAbility = AbilityHelper.CreateSpecialAbility<ThreeBirds>(pluginGuid, ThreeBirds.rName).Id;
    }
}
