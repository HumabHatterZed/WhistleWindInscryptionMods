using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class WristCutter : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Wrist Cutter";
        public static readonly string rDesc = "Bloodbath transforms whenever a card is sacrificed.";
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card != base.Card && !fromCombat;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            View view = Singleton<ViewManager>.Instance.CurrentView;
            string nameOfEvo = "wstl_bloodBath";
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_bloodBath":
                    nameOfEvo = "wstl_bloodBath1";
                    break;
                case "wstl_bloodBath1":
                    nameOfEvo = "wstl_bloodBath2";
                    break;
                case "wstl_bloodBath2":
                    nameOfEvo = "wstl_bloodBath3";
                    break;
            }
            CardInfo evolution = AbnormalMethods.GetInfoWithMods(base.PlayableCard, nameOfEvo);

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
                yield return new WaitForSeconds(0.5f);
            }

            switch (base.PlayableCard.Info.name)
            {
                case "wstl_bloodBath1":
                    yield return DialogueEventsManager.PlayDialogueEvent("Bloodbath1");
                    break;
                case "wstl_bloodBath2":
                    yield return DialogueEventsManager.PlayDialogueEvent("Bloodbath2");
                    break;
                case "wstl_bloodBath3":
                    yield return DialogueEventsManager.PlayDialogueEvent("Bloodbath3");
                    break;
            }
            if (base.PlayableCard.InHand && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard != base.PlayableCard)
                yield return new WaitForSeconds(0.5f);

            if (Singleton<ViewManager>.Instance.CurrentView != view)
                Singleton<ViewManager>.Instance.SwitchToView(view);
        }
    }
    public class RulebookEntryWristCutter : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_WristCutter()
        {
            RulebookEntryWristCutter.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryWristCutter>(WristCutter.rName, WristCutter.rDesc).Id;
        }
        private void SpecialAbility_WristCutter()
        {
            WristCutter.specialAbility = LobotomyAbilityHelper.CreateSpecialAbility<WristCutter>(WristCutter.rName).Id;
        }
    }
}
