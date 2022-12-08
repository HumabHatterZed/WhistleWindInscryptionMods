using WhistleWind.Core.Helpers;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class PinkTears : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Pink Tears";
        public static readonly string rDesc = "Army in Pink, Magical Girl S, and Magical Girl C will transform when an adjacent card dies.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // return true if the target was in an adjacent slot
            if (fromCombat && base.PlayableCard.OnBoard)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot).Where(slot => slot.Card != null))
                {
                    if (slot.Card == card)
                        return true;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            CardInfo cardByName;
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_magicalGirlSpade":
                    cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_knightOfDespair");
                    yield return base.PlayableCard.TransformIntoCard(cardByName);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueEventsManager.PlayDialogueEvent("KnightOfDespairTransform");
                    break;
                case "wstl_magicalGirlClover":
                    cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_servantOfWrath");
                    yield return base.PlayableCard.TransformIntoCard(cardByName);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueEventsManager.PlayDialogueEvent("ServantOfWrathTransform");
                    break;
                case "wstl_armyInPink":
                    cardByName = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_armyInBlack");
                    // reset damage taken since the evolution has less base Health
                    yield return base.PlayableCard.TransformIntoCard(cardByName, preTransformCallback: ResetDamage);
                    yield return new WaitForSeconds(0.5f);
                    yield return CreateArmyInHand();
                    yield return DialogueEventsManager.PlayDialogueEvent("ArmyInBlackTransform");
                    break;
            }
        }
        private IEnumerator CreateArmyInHand()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_armyInBlack");

            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }

            for (int i = 0; i < 2; i++)
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardByName, null, 0.25f, null);

            yield return new WaitForSeconds(0.45f);
        }
        private void ResetDamage() => base.PlayableCard.Status.damageTaken = 0;
    }
    public class RulebookEntryPinkTears : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_PinkTears()
        {
            RulebookEntryPinkTears.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryPinkTears>(PinkTears.rName, PinkTears.rDesc).Id;
        }
        private void SpecialAbility_PinkTears()
        {
            PinkTears.specialAbility = AbilityHelper.CreateSpecialAbility<PinkTears>(pluginGuid, PinkTears.rName).Id;
        }
    }
}
