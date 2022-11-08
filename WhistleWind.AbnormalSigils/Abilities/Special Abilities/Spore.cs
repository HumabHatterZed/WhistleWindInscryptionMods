using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class SporeDamage : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Spore";
        public static readonly string rDesc = "At the end of the owner's turn, this card takes damage equal to the amount of Spore they have.";

        private int spores;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard != null)
            {
                return base.PlayableCard.OpponentCard != playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            spores += Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .Where((CardSlot s) => s != null && s.Card != null && s.Card.HasAbility(Spores.ability)).Count();

            base.PlayableCard.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);
            yield return base.PlayableCard.TakeDamage(spores, null);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            AbnormalPlugin.Log.LogDebug($"Card {base.PlayableCard.Info.name} has {spores} Spore.");

            if (spores == 0 || base.PlayableCard.Slot == null)
            {
                yield break;
            }

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");

            minion.Mods.Add(new(spores, spores));
            minion.cost = base.PlayableCard.Info.BloodCost;
            minion.bonesCost = base.PlayableCard.Info.BonesCost;
            minion.energyCost = base.PlayableCard.Info.EnergyCost;
            minion.gemsCost = base.PlayableCard.Info.GemsCost;

            foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => x.fromCardMerge))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                if (cardModificationInfo.healthAdjustment > 0)
                    cardModificationInfo.healthAdjustment = 0;

                if (cardModificationInfo.attackAdjustment > 0)
                    cardModificationInfo.attackAdjustment = 0;

                // cardModificationInfo.fromCardMerge = true;
                minion.Mods.Add(cardModificationInfo);
            }
            foreach (Ability item in base.PlayableCard.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                // Adds base sigils
                minion.Mods.Add(new CardModificationInfo(item));
            }

            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, base.PlayableCard.Slot, 0.15f, false);
        }
    }
    public class SporeDamageAbility : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class AbnormalPlugin
    {
        private void Ability_SporeDamage()
        {
            SporeDamageAbility.ability = AbnormalAbilityHelper.CreateRulebookAbility<SporeDamageAbility>(SporeDamage.rName, SporeDamage.rDesc).Id;
        }
        private void SpecialAbility_SporeDamage()
        {
            SporeDamage.specialAbility = AbnormalAbilityHelper.CreateSpecialAbility<SporeDamage>(SporeDamage.rName).Id;
        }
    }
}
