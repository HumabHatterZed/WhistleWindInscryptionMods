using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class WristCutter : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Wrist Cutter";
        public const string rDesc = "Bloodbath transforms whenever a card is sacrificed.";
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card != base.Card && !fromCombat;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            View view = Singleton<ViewManager>.Instance.CurrentView;
            string nameOfEvo = Cards.bloodBath;
            switch (base.PlayableCard.Info.name)
            {
                case Cards.bloodBath:
                    nameOfEvo = Cards.bloodBath1;
                    break;
                case Cards.bloodBath1:
                    nameOfEvo = Cards.bloodBath2;
                    break;
                case Cards.bloodBath2:
                    nameOfEvo = Cards.bloodBath3;
                    break;
            }
            CardInfo evolution = HelperMethods.GetInfoWithMods(base.PlayableCard, nameOfEvo);

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
                case Cards.bloodBath1:
                    yield return DialogueHelper.PlayDialogueEvent("Bloodbath1");
                    break;
                case Cards.bloodBath2:
                    yield return DialogueHelper.PlayDialogueEvent("Bloodbath2");
                    break;
                case Cards.bloodBath3:
                    yield return DialogueHelper.PlayDialogueEvent("Bloodbath3");
                    break;
            }
            if (base.PlayableCard.InHand && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard != base.PlayableCard)
                yield return new WaitForSeconds(0.3f);

            yield return HelperMethods.ChangeCurrentView(View.Default);
        }
    }
    public class RulebookEntryWristCutter : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_WristCutter()
            => RulebookEntryWristCutter.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryWristCutter>(WristCutter.rName, WristCutter.rDesc).Id;
        private static void AddSpecial_WristCutter()
            => WristCutter.specialAbility = AbilityHelper.CreateSpecialAbility<WristCutter>(LobotomyPlugin.pluginGuid, WristCutter.rName).Id;
    }
}
