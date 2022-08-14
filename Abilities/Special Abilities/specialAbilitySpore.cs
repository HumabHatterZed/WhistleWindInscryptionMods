using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Spore()
        {
            const string rulebookName = "Spore";
            const string rulebookDescription = "At the end of the owner's turn take damage equal to this.";
            SporeDamage.specialAbility = AbilityHelper.CreateSpecialAbility<SporeDamage>(rulebookName, rulebookDescription).Id;
        }
    }
    public class SporeDamage : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

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
            int spores = base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore") != null ?
                (int)base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore") : 0;

            // WstlPlugin.Log.LogDebug($"Card {base.PlayableCard.Info.name} has {spores} Spore.");

            int adjacentSpores = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .Where((CardSlot s) => s != null && s.Card != null && s.Card.HasAbility(Spores.ability)).Count();

            // WstlPlugin.Log.LogDebug($"There are {adjacentSpores} adjacent Spores ability cards.");

            yield return base.PlayableCard.Info.SetExtendedProperty("wstl:Spore", spores + adjacentSpores);

            // WstlPlugin.Log.LogDebug($"Card {base.PlayableCard.Info.name} has {base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore")} Spore.");

            yield return base.PlayableCard.TakeDamage((int)base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore"), null);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            int spores = base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore") != null ?
                (int)base.PlayableCard.Info.GetExtendedPropertyAsInt("wstl:Spore") : 0;

            if (spores == 0)
            {
                yield break;
            }

            CardSlot thisSlot = base.PlayableCard.Slot;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");

            minion.Mods.Add(new(spores, spores));
            minion.cost = base.PlayableCard.Info.BloodCost;
            minion.bonesCost = base.PlayableCard.Info.BonesCost;
            minion.energyCost = base.PlayableCard.Info.EnergyCost;
            minion.gemsCost = base.PlayableCard.Info.GemsCost;

            foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
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
