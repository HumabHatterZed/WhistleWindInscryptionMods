using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_WristCutter()
        {
            const string rulebookName = "Wrist Cutter";
            const string rulebookDescription = "Transforms whenever another card is sacrificed, up to 3 times.";
            WristCutter.specialAbility = AbilityHelper.CreateSpecialAbility<WristCutter>(rulebookName, rulebookDescription).Id;
        }
    }

    public class WristCutter : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card != base.Card && !fromCombat;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_bloodBath");
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_bloodBath":
                    evolution = CardLoader.GetCardByName("wstl_bloodBath1");
                    break;
                case "wstl_bloodBath1":
                    evolution = CardLoader.GetCardByName("wstl_bloodBath2");
                    break;
                case "wstl_bloodBath2":
                    evolution = CardLoader.GetCardByName("wstl_bloodBath3");
                    break;
            }
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            View view = Singleton<ViewManager>.Instance.CurrentView;
            if (base.PlayableCard.InHand && Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard != base.PlayableCard)
            {
                base.PlayableCard.ClearAppearanceBehaviours();
                base.PlayableCard.SetInfo(evolution);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                yield return new WaitForSeconds(0.2f);
                base.PlayableCard.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return base.PlayableCard.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.4f);
            }

            switch (base.PlayableCard.Info.name)
            {
                case "wstl_bloodBath1":
                    if (!WstlSaveManager.HasSeenBloodbathHand)
                    {
                        WstlSaveManager.HasSeenBloodbathHand = true;
                        yield return CustomMethods.PlayAlternateDialogue(dialogue: "A hand rises from the scarlet pool.");
                    }
                    break;
                case "wstl_bloodBath2":
                    if (!WstlSaveManager.HasSeenBloodbathHand1)
                    {
                        WstlSaveManager.HasSeenBloodbathHand1 = true;
                        yield return CustomMethods.PlayAlternateDialogue(dialogue: "Another pale hand emerges.");
                    }
                    break;
                case "wstl_bloodBath3":
                    if (!WstlSaveManager.HasSeenBloodbathHand2)
                    {
                        WstlSaveManager.HasSeenBloodbathHand2 = true;
                        yield return CustomMethods.PlayAlternateDialogue(dialogue: "A third hand reaches out, as if asking for help.");
                    }
                    break;
            }
            yield return new WaitForSeconds(base.PlayableCard.InHand ? 0.5f : 0.25f);
            if (Singleton<ViewManager>.Instance.CurrentView != view)
            {
                Singleton<ViewManager>.Instance.SwitchToView(view);
            }
        }
    }
}
