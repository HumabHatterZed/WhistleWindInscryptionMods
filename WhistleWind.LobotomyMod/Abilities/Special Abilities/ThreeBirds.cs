using DiskCardGame;
using System.Collections;
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

        public static readonly string rName = "Three Birds";
        public static readonly string rDesc = "Gain a special card when Punishing Bird, Judgement Bird, and Big Bird are on the same side of the board.";
        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForOtherCards();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForOtherCards();

        private IEnumerator CheckForOtherCards()
        {
            // Break if already have Apocalypse Bird
            if (WstlSaveManager.OwnsApocalypseBird)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Apocalypse Bird.");
                yield break;
            }

            CardSlot punishSlot = null;
            CardSlot judgeSlot = null;

            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    if (slot.Card.Info.name == "wstl_punishingBird")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Punishing Bird.");
                        punishSlot = slot;
                        continue;
                    }
                    if (slot.Card.Info.name == "wstl_judgementBird")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Judgement Bird.");
                        judgeSlot = slot;
                        continue;
                    }
                }
            }

            if (punishSlot == null || judgeSlot == null)
                yield break;

            yield return Apocalypse(punishSlot, judgeSlot);
        }

        private IEnumerator Apocalypse(CardSlot smallSlot, CardSlot longSlot)
        {
            yield return new WaitForSeconds(0.5f);

            // Exposit story of the Black Forest
            yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdIntro");

            AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
            AudioController.Instance.SetLoopAndPlay("red_noise", 1);
            AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

            if (!DialogueEventsData.EventIsPlayed("ApocalypseBirdOutro"))
            {
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                yield return new WaitForSeconds(0.5f);

                yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStory1");

                // Look down at the board
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.25f);

                smallSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStorySmall");
                longSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStoryLong");
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStoryBig");

                yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStory2");
            }

            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            // Remove cards
            smallSlot.Card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.2f);
            longSlot.Card.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.2f);
            base.PlayableCard.RemoveFromBoard(true);
            yield return new WaitForSeconds(0.5f);

            yield return BoardEffects.ApocalypseTableEffects();

            // More text
            yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStory3");

            // Give player Apocalypse in their deck and their hand
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);

            CardInfo info = CardLoader.GetCardByName("wstl_apocalypseBird");
            RunState.Run.playerDeck.AddCard(info);

            // set cost to 0 for this fight (can play immediately that way)
            info.cost = 0;
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);
            WstlSaveManager.OwnsApocalypseBird = true;
            yield return new WaitForSeconds(0.2f);

            // Li'l text blurb
            yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdStory4");

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);
            yield return DialogueEventsManager.PlayDialogueEvent("ApocalypseBirdOutro");

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
        {
            RulebookEntryThreeBirds.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryThreeBirds>(ThreeBirds.rName, ThreeBirds.rDesc).Id;
        }
        private void SpecialAbility_ThreeBirds()
        {
            ThreeBirds.specialAbility = AbilityHelper.CreateSpecialAbility<ThreeBirds>(pluginGuid, ThreeBirds.rName).Id;
        }
    }
}
