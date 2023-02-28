using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class YellowBrick : SpecialCardBehaviour, IOnOtherCardResolveInHand
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Yellow Brick Road";
        public static readonly string rDesc = "Gain a special card when Ozma, The Road Home, Warm-Hearted Woodsman, and Scarecrow Searching for Wisdom are all on the same side of the board.";

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public bool RespondsToOtherCardResolveInHand(PlayableCard card) => !LobotomyConfigManager.Instance.NoEvents && card.OpponentCard == base.PlayableCard.OpponentCard;

        public override IEnumerator OnResolveOnBoard() => CheckForOtherCards();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForOtherCards();
        public IEnumerator OnOtherCardResolveInHand(PlayableCard card) => CheckForOtherCards();
        private IEnumerator CheckForOtherCards()
        {
            // Break if already have Adult
            if (LobotomySaveManager.OwnsLyingAdult)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Adult Who Tells Lies.");
                yield break;
            }

            List<CardSlot> cardSlots = new()
            {
                null,
                null,
                null,
                null,
                null
            };

            // check cards on the board first
            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard)
                .Where((CardSlot s) => s.Card != null))
            {
                string cardName = slot.Card.Info.name;
                if (slot == base.PlayableCard?.Slot)
                {
                    LobotomyPlugin.Log.LogDebug("Player has [The Road Home] on the board.");
                    cardSlots[0] ??= slot;
                }
                else if (cardName == "wstl_scaredyCat" || cardName == "wstl_scaredyCatStrong")
                {
                    LobotomyPlugin.Log.LogDebug("Player has [Scaredy Cat] on the board.");
                    cardSlots[1] ??= slot;
                }
                else if (cardName == "wstl_wisdomScarecrow")
                {
                    LobotomyPlugin.Log.LogDebug("Player has [Scarecrow Searching for Wisdom] on the board.");
                    cardSlots[2] ??= slot;
                }
                else if (cardName == "wstl_warmHeartedWoodsman")
                {
                    LobotomyPlugin.Log.LogDebug("Player has [Warm-Hearted Woodsman] on the board.");
                    cardSlots[3] ??= slot;
                }
                else if (cardName == "wstl_ozma")
                {
                    LobotomyPlugin.Log.LogDebug("Player has [Ozma] on the board.");
                    cardSlots[4] ??= slot;
                }
            }

            // if 4/5 of the requisite cards are on the board
            if (cardSlots.FindAll(x => x == null).Count == 1)
            {
                // check our hand for the missing member of the quintuplet
                foreach (PlayableCard card in PlayerHand.Instance.CardsInHand)
                {
                    string cardName = card.Info.name;
                    if (cardName == "wstl_theRoadHome" || cardName == "wstl_scaredyCat" || cardName == "wstl_scaredyCatStrong" ||
                        cardName == "wstl_wisdomScarecrow" || cardName == "wstl_warmHeartedWoodsman" ||
                        cardName == "wstl_ozma")
                    {
                        LobotomyPlugin.Log.LogDebug($"Player has [{card.Info.displayedName}] in their hand.");
                        yield return Emerald(cardSlots, card);
                        break;
                    }
                }
            }

            yield break;
        }

        private IEnumerator Emerald(List<CardSlot> boardSlots, PlayableCard handCard)
        {
            yield break;
            /*            yield return new WaitForSeconds(1f);

                        // Exposit story of the Black Forest
                        if (!WstlSaveManager.HasSeenAdult)
                        {
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Let me tell you a story. The story of the [c:bR]Black Forest[c:].");
                        }

                        AudioController.Instance.SetLoopVolume(0.5f * (Singleton<GameFlowManager>.Instance as Part1GameFlowManager).GameTableLoopVolume, 0.5f);
                        AudioController.Instance.SetLoopAndPlay("red_noise", 1);
                        AudioController.Instance.SetLoopVolumeImmediate(0.3f, 1);

                        if (!WstlSaveManager.HasSeenAdult)
                        {
                            yield return new WaitForSeconds(0.4f);
                            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                            yield return new WaitForSeconds(0.5f);

                            yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                                "Once upon a time, [c:bR]three birds[c:] lived happily in the lush Forest with their fellow animals.",
                                "One day a stranger arrived at the Forest.He proclaimed that the Forest would soon be ensared in a bitter conflict.",
                                "One that would only end when everything was devoured by a[c: bR]terrible Beast[c:].",
                                "The birds, frightened by this doomsay, sought to prevent conflict from ever breaking out.");

                            // Look down at the board
                            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                            yield return new WaitForSeconds(0.25f);

                            //smallSlot.Card.Anim.StrongNegationEffect();
                            yield return new WaitForSeconds(0.4f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bR]Small Bird[c:] punished wrongdoers with his beak.");
                            //longSlot.Card.Anim.StrongNegationEffect();
                            yield return new WaitForSeconds(0.4f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("[c:bB]Long Bird[c:] weighed the sins of all creatures in the forest with his scales.");
                            base.PlayableCard.Anim.StrongNegationEffect();
                            yield return new WaitForSeconds(0.4f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("With his many eyes, [c:bG]Big Bird[c:] kept constant watch over the entire Forest.");

                            yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                                "Fights began to break out. More and more creatures left the Forest, no matter how hard the birds worked.",
                                "They decided to combine their powers. This way, they could better protect their home.",
                                "This way they could better return the peace.");
                        }

                        Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
                        yield return new WaitForSeconds(0.2f);

                        // Remove cards
                        //smallSlot.Card.RemoveFromBoard(true);
                        yield return new WaitForSeconds(0.2f);
                        //longSlot.Card.RemoveFromBoard(true);
                        yield return new WaitForSeconds(0.2f);
                        base.PlayableCard.RemoveFromBoard(true);
                        yield return new WaitForSeconds(0.5f);

                        yield return BoardEffects.EmeraldTableEffects();

                        // More text
                        if (!WstlSaveManager.HasSeenAdult)
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Darkness fell upon the forest. Mayhem ran amok as creatures screamed in terror at the towering bird.");

                        // Give player Apocalypse in their deck and their hand
                        Singleton<ViewManager>.Instance.SwitchToView(View.Hand);

                        CardInfo info = CardLoader.GetCardByName("wstl_lyingAdult");
                        RunState.Run.playerDeck.AddCard(info);

                        // set cost to 0 for this fight (can play immediately that way)
                        info.cost = 0;
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0.25f, null);
                        WstlSaveManager.OwnsLyingAdult = true;
                        yield return new WaitForSeconds(0.2f);

                        // Li'l text blurb
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Someone yelled out 'It's [c:bR]the Beast[c:]! A big, scary monster lives in the Black Forest!'");

                        yield return new WaitForSeconds(0.2f);
                        Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                        yield return new WaitForSeconds(0.15f);
                        if (!WstlSaveManager.HasSeenAdult)
                        {
                            WstlSaveManager.HasSeenAdult = true;
                            yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Leshy, 0.2f,
                                "The three birds, [c:bR]now one[c:] looked around for [c:bR]the Beast[c:]. But there was nothing.",
                                "No creatures. No beast. No sun or moon or stars. Only a single bird, alone in an empty forest.");
                        }
                        Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;*/
        }
    }
    public class RulebookEntryYellowBrick : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_YellowBrick()
            => RulebookEntryYellowBrick.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryYellowBrick>(YellowBrick.rName, YellowBrick.rDesc).Id;
        private void SpecialAbility_YellowBrick()
            => YellowBrick.specialAbility = AbilityHelper.CreateSpecialAbility<YellowBrick>(pluginGuid, YellowBrick.rName).Id;
    }
}
