using WhistleWind.Core.Helpers;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Adoration : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Adoration";
        public static readonly string rDesc = "If Melting Love's Health is 1 on upkeep, absorb the Health of adjacent Slimes.";

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.PlayableCard.Health > 1)
                yield break;

            if (Singleton<ViewManager>.Instance.CurrentView != View.Board)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.15f);
            }
            CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, true);
            CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, false);

            // check if left card is valid target
            bool leftValid = leftSlot != null && leftSlot.Card != null && leftSlot.Card.Info.name == "wstl_meltingLoveMinion";

            // check if right card is valid target
            bool rightValid = rightSlot != null && rightSlot.Card != null && rightSlot.Card.Info.name == "wstl_meltingLoveMinion";

            // break if none are valid
            if (!leftValid && !rightValid)
                yield break;

            if (leftValid)
            {
                leftSlot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
            }
            if (rightValid)
            {
                rightSlot.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
            }

            yield return DialogueEventsManager.PlayDialogueEvent("MeltingLoveAbsorb");

            if (leftValid)
            {
                int leftHealth = leftSlot.Card.Health;
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return leftSlot.Card.Die(false, base.PlayableCard);
                base.PlayableCard.HealDamage(leftHealth);
                yield return new WaitForSeconds(0.4f);
            }
            if (rightValid && base.PlayableCard.Health < base.PlayableCard.MaxHealth)
            {
                int rightHealth = rightSlot.Card.Health;
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return rightSlot.Card.Die(false, base.PlayableCard);
                base.PlayableCard.HealDamage(rightHealth);
                yield return new WaitForSeconds(0.4f);
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            yield return new WaitForSeconds(0.15f);
        }
    }
    public class RulebookEntryAdoration : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Adoration()
        {
            RulebookEntryAdoration.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryAdoration>(Adoration.rName, Adoration.rDesc).Id;
        }
        private void SpecialAbility_Adoration()
        {
            Adoration.specialAbility = AbilityHelper.CreateSpecialAbility<Adoration>(pluginGuid, Adoration.rName).Id;
        }
    }
}
