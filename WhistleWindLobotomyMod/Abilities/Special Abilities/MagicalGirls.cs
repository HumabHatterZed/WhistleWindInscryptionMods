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
    public class MagicalGirls : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Magical Girls";
        public const string rDesc = "Gain a special card when all 4 Magical Girls or their evolutions are on the same side of the board.";

        private static Dictionary<string, List<string>> ValidCardNames => new()
        {
            { "Clover", new() {"wstl_magicalGirlClover", "wstl_servantOfWrath" }},
            { "Heart", new() {"wstl_magicalGirlHeart", "wstl_queenOfHatred", "wstl_queenOfHatredTired" }},
            { "Spade", new() {"wstl_magicalGirlSpade", "wstl_knightOfDespair" }}
        };

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForMagicGirls();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForMagicGirls();

        private IEnumerator CheckForMagicGirls()
        {
            if (LobotomySaveManager.OwnsJesterOfNihil)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Jester of Nihil.");
                yield break;
            }

            List<CardSlot> otherMagicGirls = new() { null, null, null };

            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where(s => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    string slotName = slot.Card.Info.name;
                    if (ValidCardNames["Heart"].Contains(slotName))
                        otherMagicGirls[0] ??= slot;

                    else if (ValidCardNames["Spade"].Contains(slotName))
                        otherMagicGirls[1] = slot;

                    if (ValidCardNames["Clover"].Contains(slotName))
                        otherMagicGirls[2] = slot;
                }
            }
            if (otherMagicGirls.Contains(null))
            {
                if (LobotomyConfigManager.Instance.NoRuina && otherMagicGirls[2] == null && otherMagicGirls.Count(x => x == null) == 1)
                    yield return NoRuina(otherMagicGirls[0], otherMagicGirls[1]);

                yield break;
            }

            yield return Entropy(otherMagicGirls[0], otherMagicGirls[1], otherMagicGirls[2]);
        }

        private IEnumerator Entropy(CardSlot queenOfHatred, CardSlot knightOfDespair, CardSlot servantOfWrath)
        {
            bool opponentCard = base.PlayableCard.OpponentCard;

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            queenOfHatred.Card.Anim.StrongNegationEffect();
            knightOfDespair.Card.Anim.StrongNegationEffect();
            servantOfWrath.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilIntro", 0f);

            // turn out the lights, activate table effects, remove magic girls
            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: false);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: false);
            yield return BoardEffects.EntropyTableEffects();
            RemoveMagic(queenOfHatred, knightOfDespair, servantOfWrath, opponentCard);

            yield return new WaitForSeconds(0.4f);

            // switch to default view while the lights are off
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, true);

            yield return new WaitForSeconds(0.4f);

            if (!DialogueEventsData.EventIsPlayed("JesterOfNihilStory"))
                yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilStory");
            else
                yield return new WaitForSeconds(0.4f);

            if (Singleton<VideoCameraRig>.Instance != null)
                Singleton<VideoCameraRig>.Instance.PlayCameraAnim("refocus_quick");

            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: true);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: true);

            CardInfo info = CardLoader.GetCardByName("wstl_jesterOfNihil");
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

                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0f, null);
                LobotomySaveManager.OwnsJesterOfNihil = true;
                LobotomySaveManager.UnlockedJesterOfNihil = true;
            }

            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilOutro");
            HelperMethods.ChangeCurrentView(View.Default);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private void RemoveMagic(CardSlot queenOfHatred, CardSlot knightOfDespair, CardSlot servantOfWrath, bool opponentCard)
        {
            if (!opponentCard)
            {
                foreach (CardInfo card in RunState.Run.playerDeck.Cards.Where(x => x.HasTrait(LobotomyCardManager.TraitMagicalGirl)))
                    RunState.Run.playerDeck.RemoveCardByName(card.name);
            }

            queenOfHatred.Card.RemoveFromBoard(false, 0f);
            knightOfDespair.Card.RemoveFromBoard(false, 0f);
            servantOfWrath.Card.RemoveFromBoard(false, 0f);
            base.PlayableCard.RemoveFromBoard(false, 0f);
        }

        private IEnumerator NoRuina(CardSlot queenOfHatred, CardSlot knightOfDespair)
        {
            queenOfHatred.Card.Anim.StrongNegationEffect();
            knightOfDespair.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.PlayAlternateDialogue(dialogue: "Without the [c:g1]fourth[c:], their purpose is rendered null.");
        }
    }
    public class RulebookEntryMagicalGirls : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_MagicalGirls()
            => RulebookEntryMagicalGirls.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMagicalGirls>(MagicalGirls.rName, MagicalGirls.rDesc).Id;
        private void SpecialAbility_MagicalGirls()
            => MagicalGirls.specialAbility = AbilityHelper.CreateSpecialAbility<MagicalGirls>(pluginGuid, MagicalGirls.rName).Id;
    }
}
