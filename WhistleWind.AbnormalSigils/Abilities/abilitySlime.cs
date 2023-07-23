using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Slime()
        {
            const string rulebookName = "Made of Slime";
            const string rulebookDescription = "At the start of the owner's turn, creatures adjacent to this card with at least 2 Health are transformed into Slimes. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.";
            const string dialogue = "Its army grows everyday.";
            const string triggerText = "Creatures adjacent to [creature] melt into slime!";
            Slime.ability = AbnormalAbilityHelper.CreateAbility<Slime>(
                "sigilSlime",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 5,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Slime : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // Transform cards
            CardSlot leftSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, true);
            CardSlot rightSlot = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, false);

            // check if left card is valid target
            bool leftValid = leftSlot != null && leftSlot.Card != null && leftSlot.Card.Info.Health > 1 &&
                leftSlot.Card.LacksAbility(ability) && !leftSlot.Card.Info.HasAnyOfTraits(Trait.Terrain, Trait.Pelt);

            // check if right card is valid target
            bool rightValid = rightSlot != null && rightSlot.Card != null && rightSlot.Card.Info.Health > 1 &&
                rightSlot.Card.LacksAbility(ability) && !rightSlot.Card.Info.HasAnyOfTraits(Trait.Terrain, Trait.Pelt);

            if (!leftValid && !rightValid)
                yield break;

            // transform valid cards
            yield return base.PreSuccessfulTriggerSequence();
            if (leftValid)
            {
                CardInfo leftInfo = SlimeInfo(leftSlot.Card.Info);
                yield return leftSlot.Card.TransformIntoCard(leftInfo);
                yield return new WaitForSeconds(0.2f);
            }
            if (rightValid)
            {
                CardInfo rightInfo = SlimeInfo(rightSlot.Card.Info);
                yield return rightSlot.Card.TransformIntoCard(rightInfo);
                yield return new WaitForSeconds(0.2f);
            }
            // learn ability or do a shimmy
            if (leftValid || rightValid)
                yield return base.LearnAbility(0.3f);
        }
        private CardInfo SlimeInfo(CardInfo otherInfo)
        {
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_meltingLoveMinion");

            cardInfo.appearanceBehaviour = otherInfo.appearanceBehaviour;
            cardInfo.cost = otherInfo.BloodCost;
            cardInfo.bonesCost = otherInfo.BonesCost;
            cardInfo.energyCost = otherInfo.EnergyCost;
            cardInfo.gemsCost = otherInfo.GemsCost;

            // Health - 1, Power 1
            int newHealth = otherInfo.baseHealth - 1;

            cardInfo.Mods.Add(new(1, newHealth));

            foreach (CardModificationInfo item in otherInfo.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Copy merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                if (cardModificationInfo.attackAdjustment > 0)
                    cardModificationInfo.attackAdjustment = 0;

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
