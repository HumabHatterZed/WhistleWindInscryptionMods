using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Spore()
        {
            const string rulebookName = "Spore";
            const string rulebookDescription = "At the end of the owner's turn take damage equal to the amount of Spore they have.";
            SporeDamage.specialAbility = AbilityHelper.CreateSpecialAbility<SporeDamage>(rulebookName, rulebookDescription).Id;
        }
    }
    public class SporeDamage : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private int spores = 0;

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
            WstlPlugin.Log.LogDebug($"Card {base.PlayableCard.Info.name} has {spores} Spore.");

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
                {
                    cardModificationInfo.healthAdjustment = 0;
                }
                if (cardModificationInfo.attackAdjustment > 0)
                {
                    cardModificationInfo.attackAdjustment = 0;
                }
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
}
