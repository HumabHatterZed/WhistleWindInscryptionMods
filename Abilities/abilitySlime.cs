using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Slime()
        {
            const string rulebookName = "Made of Slime";
            const string rulebookDescription = "Adjacent cards are turned into Slimes at the start of the owner's turn. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.";
            const string dialogue = "Its army grows everyday.";

            Slime.ability = AbilityHelper.CreateAbility<Slime>(
                Resources.sigilSlime, Resources.sigilSlime_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Slime : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string absorbDialogue = "They give themselves lovingly.";

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (base.Card != null)
            {
                return base.Card.OpponentCard != playerUpkeep;
            }
            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // If Melting Love and has low Health
            if (base.Card.Info.name.Equals("wstl_meltingLove") && base.Card.Health == 1)
            {
                if (Singleton<ViewManager>.Instance.CurrentView != View.Board)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                    yield return new WaitForSeconds(0.15f);
                }
                CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true);
                CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false);

                // check if left card is valid target
                bool leftValid = leftSlot != null && leftSlot.Card != null && leftSlot.Card.HasAbility(Slime.ability);

                // check if right card is valid target
                bool rightValid = rightSlot != null && rightSlot.Card != null && rightSlot.Card.HasAbility(Slime.ability);

                // break if none are valid
                if (!leftValid || !rightValid)
                {
                    base.Card.Anim.LightNegationEffect();
                    yield return new WaitForSeconds(0.15f);
                    yield break;
                }

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
                yield return new WaitForSeconds(0.2f);
                if (!WstlSaveManager.HasSeenMeltingHeal)
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(absorbDialogue);
                    yield return new WaitForSeconds(0.1f);
                }
                if (leftValid)
                {
                    int leftHealth = leftSlot.Card.Health;
                    base.Card.Anim.StrongNegationEffect();
                    yield return leftSlot.Card.Die(false, base.Card);
                    base.Card.HealDamage(leftHealth);
                    yield return new WaitForSeconds(0.4f);
                }
                if (rightValid && base.Card.Health < base.Card.Info.baseHealth)
                {
                    int rightHealth = rightSlot.Card.Health;
                    base.Card.Anim.StrongNegationEffect();
                    yield return rightSlot.Card.Die(false, base.Card);
                    base.Card.HealDamage(rightHealth);
                    yield return new WaitForSeconds(0.4f);
                }
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.15f);
                // don't transform cards
                yield break;
            }

            // Transform cards
            CardSlot leftSlot2 = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true);
            CardSlot rightSlot2 = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false);

            // check if left card is valid target
            bool leftValid2 = leftSlot2 != null && leftSlot2.Card != null && !leftSlot2.Card.HasAbility(Slime.ability) &&
                !leftSlot2.Card.Info.HasTrait(Trait.Terrain) && !leftSlot2.Card.Info.HasTrait(Trait.Pelt); ;

            // check if right card is valid target
            bool rightValid2 = rightSlot2 != null && rightSlot2.Card != null && !rightSlot2.Card.HasAbility(Slime.ability) &&
                !rightSlot2.Card.Info.HasTrait(Trait.Terrain) && !rightSlot2.Card.Info.HasTrait(Trait.Pelt);

            yield return base.PreSuccessfulTriggerSequence();

            // transform valid cards
            if (leftValid2)
            {
                CardInfo leftInfo = SlimeInfo(leftSlot2.Card.Info);
                yield return leftSlot2.Card.TransformIntoCard(leftInfo);
                yield return new WaitForSeconds(0.2f);
            }
            if (rightValid2)
            {
                CardInfo rightInfo = SlimeInfo(rightSlot2.Card.Info);
                yield return rightSlot2.Card.TransformIntoCard(rightInfo);
                yield return new WaitForSeconds(0.2f);
            }
            // learn ability or do a shimmy
            if (leftValid2 || rightValid2)
            {
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
        }
        private CardInfo SlimeInfo(CardInfo otherInfo)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

            // copy name, costs
            cardInfo.displayedName = otherInfo.displayedName + " Slime";
            cardInfo.appearanceBehaviour = otherInfo.appearanceBehaviour;
            cardInfo.cost = otherInfo.BloodCost;
            cardInfo.bonesCost = otherInfo.BonesCost;
            cardInfo.energyCost = otherInfo.EnergyCost;
            cardInfo.gemsCost = otherInfo.GemsCost;

            // Health - 1, Power 1 if killed card had Power > 0
            int newPower = otherInfo.baseAttack > 0 ? 1 : 0;
            int newHealth = otherInfo.baseHealth - 1 < 1 ? 1 : otherInfo.baseHealth - 1;

            cardInfo.Mods.Add(new(newPower, newHealth));

            foreach (CardModificationInfo item in otherInfo.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Copy merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardInfo.Mods.Add(cardModificationInfo);
            }
            foreach (Ability item in otherInfo.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                // Copy base sigils
                cardInfo.Mods.Add(new CardModificationInfo(item));
            }

            return cardInfo;
        }
    }
}
