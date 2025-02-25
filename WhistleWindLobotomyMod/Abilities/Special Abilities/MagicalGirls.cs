using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
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
    public class MagicalGirls : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Magical Girls";
        public const string rDesc = "Gain a special card when all 4 Magical Girls or their evolutions are on the same side of the board.";

        private static List<string>[] ValidCardNames => new List<string>[3]
        {
            new() { Cards.magicalGirlHeart, Cards.magicalGirlHeartPixel, Cards.queenOfHatred, Cards.queenOfHatredPixel, Cards.queenOfHatredTired, Cards.queenOfHatredTiredPixel  },
            new() { Cards.magicalGirlSpade, Cards.magicalGirlSpadePixel, Cards.knightOfDespair, Cards.knightOfDespairPixel },
            new() { Cards.magicalGirlClover, Cards.magicalGirlCloverPixel, Cards.servantOfWrath, Cards.servantOfWrathPixel }
            
        };

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForMagicGirls();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForMagicGirls();

        private IEnumerator CheckForMagicGirls()
        {
            if (LobotomySaveManager.OwnsJesterOfNihil)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Jester of Nihil.");
                yield break;
            }

            if (BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard).Count < 4)
                yield break;

            CardSlot[] magicGirlSlots = new CardSlot[4] { base.PlayableCard.Slot, null, null, null };

            foreach (PlayableCard card in BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard, x => x.HasTrait(LobotomyCardManager.MagicalGirl)))
            {
                if (card == base.PlayableCard || LobotomyHelpers.CardIsMimicking(card))
                    continue;

                string cardName = card.Info.name;
                if (ValidCardNames[0].Contains(cardName))
                    magicGirlSlots[1] = card.Slot;

                else if (ValidCardNames[1].Contains(cardName))
                    magicGirlSlots[2] = card.Slot;

                if (ValidCardNames[2].Contains(cardName))
                    magicGirlSlots[3] = card.Slot;
            }
            if (magicGirlSlots.Count(x => x != null) < 4)
            {
                if (LobotomyConfigManager.NoRuina && magicGirlSlots[1] != null && magicGirlSlots[2] != null)
                    yield return RuinaIsDisabled(magicGirlSlots[1], magicGirlSlots[2]);

                yield break;
            }

            yield return BeginEntropy(magicGirlSlots[0], magicGirlSlots[1], magicGirlSlots[2], magicGirlSlots[3]);
        }

        private IEnumerator BeginEntropy(CardSlot greed, CardSlot hate, CardSlot despair, CardSlot wrath)
        {
            bool opponentCard = !greed.IsPlayerSlot;
            bool canInitiateCombat = LobotomyHelpers.AllowInitiateCombat(false);

            yield return HelperMethods.ChangeCurrentView(View.Board, lockAfter: true);

            hate.Card.Anim.StrongNegationEffect();
            greed.Card.Anim.StrongNegationEffect();
            despair.Card.Anim.StrongNegationEffect();
            wrath.Card.Anim.StrongNegationEffect();            
            yield return new WaitForSeconds(0.4f);

            yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilIntro", 0f);

            // turn out the lights, activate table effects, remove magic girls
            if (!SaveManager.SaveFile.IsPart2)
            {
                Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: false);
                Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: false);
                yield return BoardEffects.EntropyTableEffects();
            }

            RemoveMagicGirls(greed, hate, despair, wrath, opponentCard);
            
            yield return HelperMethods.ChangeCurrentView(View.Default, 0.4f, 0.4f, immediate: true);
            
            if (!DialogueEventsData.EventIsPlayed("JesterOfNihilStory"))
            {
                yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilStory");
                yield return new WaitForSeconds(0.4f);
            }

            Singleton<VideoCameraRig>.Instance?.PlayCameraAnim("refocus_quick");

            if (Singleton<ExplorableAreaManager>.Instance != null)
            {
                Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: true);
                Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: true);
            }

            CardInfo info = CardLoader.GetCardByName("wstl_jesterOfNihil");
            if (opponentCard)
            {
                List<CardSlot> validSlots = BoardManager.Instance.GetSlotsCopy(!opponentCard).FindAll(x => x.Card == null);
                yield return CombatHelpers.CreateCardInRandomSlot(info, validSlots);
            }
            else
            {
                // add the card to the player's deck (this adds a clone so we can modify info after-the-fact for this battle only)
                RunState.Run.playerDeck.AddCard(info);
                info.Mods.Add(new() { bonesCostAdjustment = -info.bonesCost });
                LobotomySaveManager.OwnsJesterOfNihil = true;
                LobotomySaveManager.UnlockedJesterOfNihil = true;
                yield return HelperMethods.ChangeCurrentView(View.Hand, 0.4f);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0f, null);
            }

            yield return new WaitForSeconds(0.2f);
            yield return DialogueHelper.PlayDialogueEvent("JesterOfNihilOutro");
            yield return HelperMethods.ChangeCurrentView(View.Default);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            LobotomyHelpers.AllowInitiateCombat(canInitiateCombat);
        }

        private void RemoveMagicGirls(CardSlot greed, CardSlot hate, CardSlot despair, CardSlot wrath, bool opponentSlot)
        {
            // remove all cards in the player's deck that are magical girls
            // since the evolutions can also trigger the sequence,
            // we remove them this way instead
            if (!opponentSlot)
            {
                if (SaveManager.SaveFile.IsPart2)
                {
                    SaveManager.SaveFile.gbcData.deck.RemoveCardByName(Cards.magicalGirlHeartPixel);
                    SaveManager.SaveFile.gbcData.deck.RemoveCardByName(Cards.magicalGirlDiamondPixel);
                    SaveManager.SaveFile.gbcData.deck.RemoveCardByName(Cards.magicalGirlSpadePixel);
                    SaveManager.SaveFile.gbcData.deck.RemoveCardByName(Cards.magicalGirlCloverPixel);
                }
                else if (SaveManager.SaveFile.IsPart1)
                {
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlHeart);
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlDiamond);
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlSpade);
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlClover);
                }
                else
                {
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlHeartPixel);   
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlDiamondPixel);
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlSpadePixel);
                    RunState.Run.playerDeck.RemoveCardByName(Cards.magicalGirlCloverPixel);
                }
            }

            hate.Card.RemoveFromBoard(false, 0f);
            greed.Card.RemoveFromBoard(false, 0f);
            despair.Card.RemoveFromBoard(false, 0f);
            wrath.Card.RemoveFromBoard(false, 0f);
        }

        private IEnumerator RuinaIsDisabled(CardSlot queenOfHatred, CardSlot knightOfDespair)
        {
            queenOfHatred.Card.Anim.StrongNegationEffect();
            knightOfDespair.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return DialogueHelper.PlayAlternateDialogue(dialogue: "Without the [c:g1]fourth[c:], their purpose is rendered null.");
        }
    }
    public class RulebookEntryMagicalGirls : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_MagicalGirls()
            => RulebookEntryMagicalGirls.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMagicalGirls>(MagicalGirls.rName, MagicalGirls.rDesc).Id;
        private static void AddSpecial_MagicalGirls()
            => MagicalGirls.specialAbility = AbilityHelper.CreateSpecialAbility<MagicalGirls>(LobotomyPlugin.pluginGuid, MagicalGirls.rName).Id;
    }
}
