using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Adoration : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Adoration";
        public const string rDesc = "At the start of the owner's turn, if Melting Love has lost Health, absorb the Health of adjacent Slime cards until Melting Love is at max Health.";

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.PlayableCard.Health >= base.PlayableCard.MaxHealth)
                yield break;

            yield return HelperMethods.ChangeCurrentView(View.Board);

            CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, true);
            CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.PlayableCard.Slot, false);

            bool leftValid = leftSlot?.Card != null && leftSlot.Card.HasTrait(AbnormalPlugin.LovingSlime) && leftSlot.Card.Info.name != base.PlayableCard.Info.name;
            bool rightValid = rightSlot?.Card != null && rightSlot.Card.HasTrait(AbnormalPlugin.LovingSlime) && rightSlot.Card.Info.name != base.PlayableCard.Info.name;

            // break if none are valid
            if (!leftValid && !rightValid)
                yield break;

            // find out how much Health we can heal back
            int missingHealth = base.PlayableCard.MaxHealth - base.PlayableCard.Health;
            if (leftValid && missingHealth > 0)
            {
                // if we need to absorb more health than the left card has, only heal by the left card's current Health
                int healAmount = (leftSlot.Card.Health - missingHealth) > 0 ? missingHealth : leftSlot.Card.Health;
                missingHealth -= healAmount; // update our remaining health we need to heal back

                leftSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                leftSlot.Card.Status.damageTaken += healAmount;
                leftSlot.Card.UpdateStatsText();
                if (leftSlot.Card.Health > 0)
                    leftSlot.Card.Anim.PlayHitAnimation();
                else
                    yield return leftSlot.Card.Die(false);

                base.Card.Anim.LightNegationEffect();
                base.PlayableCard.HealDamage(healAmount);
                yield return new WaitForSeconds(0.2f);
                    
            }
            // if rightValid and we still have missing health, absorb from the right card as well
            if (rightValid && missingHealth > 0)
            {
                int healAmount = (rightSlot.Card.Health - missingHealth) > 0 ? missingHealth : rightSlot.Card.Health;
                
                rightSlot.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                rightSlot.Card.Status.damageTaken += healAmount;
                rightSlot.Card.UpdateStatsText();
                if (leftSlot.Card.Health > 0)
                    rightSlot.Card.Anim.PlayHitAnimation();
                else
                    yield return rightSlot.Card.Die(false);

                base.Card.Anim.LightNegationEffect();
                base.PlayableCard.HealDamage(healAmount);
                yield return new WaitForSeconds(0.2f);
            }

            yield return DialogueHelper.PlayDialogueEvent("MeltingLoveAbsorb");
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
            => RulebookEntryAdoration.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryAdoration>(Adoration.rName, Adoration.rDesc).Id;
        private void SpecialAbility_Adoration()
            => Adoration.specialAbility = AbilityHelper.CreateSpecialAbility<Adoration>(pluginGuid, Adoration.rName).Id;
    }
}
