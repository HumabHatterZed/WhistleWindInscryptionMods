using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
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

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForOtherCards();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForOtherCards();

        private IEnumerator CheckForOtherCards()
        {
            if (LobotomySaveManager.OwnsApocalypseBird)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Apocalypse Bird.");
                yield break;
            }

            if (BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard).Count < 3)
                yield break;

            bool isOpponent = base.PlayableCard.OpponentCard;
            PlayableCard bigBird = base.PlayableCard;
            PlayableCard punishingBird = BoardManager.Instance.GetCards(!isOpponent).Find(x => x.Info.name == Cards.punishingBird);
            PlayableCard judgementBird = BoardManager.Instance.GetCards(!isOpponent).Find(x => x.Info.name == Cards.judgementBird);

            if (punishingBird == null || judgementBird == null)
                yield break;

            yield return BeginApocalypse(bigBird, punishingBird, judgementBird, isOpponent);
        }

        private IEnumerator BeginApocalypse(PlayableCard big, PlayableCard small, PlayableCard tall, bool opponentCard)
        {
            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);
            CardInfo info = CardLoader.GetCardByName(Cards.apocalypseBird);

            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdIntro");

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager)?.GameTableLoopVolume ?? 1f, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            if (!DialogueEventsData.EventIsPlayed("ApocalypseBirdOutro"))
            {
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                yield return new WaitForSeconds(0.5f);

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory1");

                // Look down at the board
                yield return HelperMethods.ChangeCurrentView(View.Board);

                small.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStorySmall");

                tall.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStoryLong");

                big.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStoryBig");

                yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory2");
            }

            yield return HelperMethods.ChangeCurrentView(View.Board, lockAfter: true);
            
            // Remove cards
            small.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.2f);
            tall.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.2f);
            big.RemoveFromBoard(!opponentCard);
            yield return new WaitForSeconds(0.5f);

            if (!SaveManager.SaveFile.IsPart2)
                yield return BoardEffects.ApocalypseTableEffects();

            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory3");
            if (opponentCard)
            {
                List<CardSlot> validSlots = BoardManager.Instance.GetSlotsCopy(!opponentCard).FindAll(x => x.Card == null);
                yield return CombatHelpers.CreateCardInRandomSlot(info, validSlots);
            }
            else
            {
                RunState.Run.playerDeck.AddCard(info);
                info.Mods.Add(new() { bloodCostAdjustment = -info.cost });
                LobotomySaveManager.OwnsApocalypseBird = true;
                LobotomySaveManager.UnlockedApocalypseBird = true;
                yield return HelperMethods.ChangeCurrentView(View.Hand, 0.4f);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);
            }

            yield return new WaitForSeconds(0.2f);

            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdStory4");
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return DialogueHelper.PlayDialogueEvent("ApocalypseBirdOutro");

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }
    }
    public class RulebookEntryThreeBirds : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_ThreeBirds()
            => RulebookEntryThreeBirds.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryThreeBirds>(ThreeBirds.rName, ThreeBirds.rDesc).Id;
        private static void AddSpecial_ThreeBirds()
            => ThreeBirds.specialAbility = AbilityHelper.CreateSpecialAbility<ThreeBirds>(LobotomyPlugin.pluginGuid, ThreeBirds.rName).Id;
    }
}
