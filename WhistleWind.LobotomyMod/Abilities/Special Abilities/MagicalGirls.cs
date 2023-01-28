using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core;
using WhistleWind.LobotomyMod.Core.Helpers;

namespace WhistleWind.LobotomyMod
{
    public class MagicalGirls : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Magical Girls";
        public static readonly string rDesc = "Gain a special card when all 4 Magical Girls or their evolutions are on the same side of the board.";

        public override bool RespondsToResolveOnBoard() => !LobotomyConfigManager.Instance.NoEvents;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !LobotomyConfigManager.Instance.NoEvents && otherCard.OpponentCard == base.PlayableCard.OpponentCard;
        public override IEnumerator OnResolveOnBoard() => CheckForMagicGirls();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => CheckForMagicGirls();

        private IEnumerator CheckForMagicGirls()
        {
            // Break if already have Jester
            if (LobotomySaveManager.OwnsJesterOfNihil)
            {
                LobotomyPlugin.Log.LogDebug("Player already has Jester of Nihil.");
                yield break;
            }

            CardSlot greedSlot = null;
            CardSlot despairSlot = null;
            CardSlot wrathSlot = null;
            foreach (CardSlot slot in HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard).Where((CardSlot s) => s.Card != null))
            {
                if (slot != base.PlayableCard.Slot)
                {
                    string slotName = slot.Card.Info.name;
                    if (slotName == "wstl_magicalGirlDiamond" || slotName == "wstl_kingOfGreed")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Diamond.");
                        greedSlot = slot;
                        continue;
                    }
                    if (slotName == "wstl_magicalGirlSpade" || slotName == "wstl_knightOfDespair")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Spade.");
                        despairSlot = slot;
                        continue;
                    }
                    if (slotName == "wstl_magicalGirlClover" || slotName == "wstl_servantOfWrath")
                    {
                        LobotomyPlugin.Log.LogDebug("Player has Clover.");
                        wrathSlot = slot;
                        continue;
                    }
                }
            }
            if (greedSlot != null && despairSlot != null)
            {
                if (LobotomyPlugin.RuinaCardsDisabled)
                    yield return NoRuina(greedSlot, despairSlot);
                else if (wrathSlot != null)
                    yield return Entropy(greedSlot, despairSlot, wrathSlot);
            }

            yield break;
        }

        private IEnumerator Entropy(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return new WaitForSeconds(0.2f);

            greed.Card.Anim.StrongNegationEffect();
            despair.Card.Anim.StrongNegationEffect();
            wrath.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.5f);

            yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilIntro", 0f);

            // turn out the lights, activate table effects, remove magic girls
            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: false);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: false);
            yield return BoardEffects.EntropyTableEffects();
            RemoveMagic(greed, despair, wrath);

            yield return new WaitForSeconds(0.4f);

            // switch to default view while the lights are off
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, true);

            yield return new WaitForSeconds(0.4f);

            if (!DialogueEventsData.EventIsPlayed("JesterOfNihilStory"))
                yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilStory");
            else
                yield return new WaitForSeconds(0.4f);

            if (Singleton<VideoCameraRig>.Instance != null)
                Singleton<VideoCameraRig>.Instance.PlayCameraAnim("refocus_quick");

            Singleton<ExplorableAreaManager>.Instance.HangingLight.gameObject.SetActive(value: true);
            Singleton<ExplorableAreaManager>.Instance.HandLight.gameObject.SetActive(value: true);

            // Show hand so player can see the jester
            yield return new WaitForSeconds(0.4f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
            yield return new WaitForSeconds(0.2f);

            // add jester to deck and hand
            CardInfo info = CardLoader.GetCardByName("wstl_jesterOfNihil");
            RunState.Run.playerDeck.AddCard(info);

            // set cost to 0 for this fight (can play immediately that way)
            info.cost = 0;
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info, null, 0f, null);
            LobotomySaveManager.OwnsJesterOfNihil = true;
            yield return new WaitForSeconds(0.2f);

            yield return DialogueEventsManager.PlayDialogueEvent("JesterOfNihilOutro");

            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
        }

        private IEnumerator NoRuina(CardSlot greed, CardSlot despair)
        {
            greed.Card.Anim.StrongNegationEffect();
            despair.Card.Anim.StrongNegationEffect();
            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.PlayAlternateDialogue(dialogue: "Without the [c:g1]fourth[c:], their purpose is rendered null.");
        }
        private void RemoveMagic(CardSlot greed, CardSlot despair, CardSlot wrath)
        {
            // Remove cards
            greed.Card.RemoveFromBoard(true, 0f);
            despair.Card.RemoveFromBoard(true, 0f);
            wrath.Card.RemoveFromBoard(true, 0f);
            base.PlayableCard.RemoveFromBoard(true, 0f);
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
        {
            RulebookEntryMagicalGirls.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMagicalGirls>(MagicalGirls.rName, MagicalGirls.rDesc).Id;
        }
        private void SpecialAbility_MagicalGirls()
        {
            MagicalGirls.specialAbility = AbilityHelper.CreateSpecialAbility<MagicalGirls>(pluginGuid, MagicalGirls.rName).Id;
        }
    }
}
