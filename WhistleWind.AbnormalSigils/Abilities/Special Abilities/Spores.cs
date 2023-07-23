using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using Steamworks;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Spores";
        public static readonly string rDesc = "At the start of its owner's turn, this card takes damage equal to its Spores. Upon dying, create a Spore Mold Creature with stats equal to its Spores.";

        public int spore = 1;
        public int turnPlayed = -1;

        public CardModificationInfo GetSporeStatusMod()
        {
            CardModificationInfo sporeStatusMod = StatusEffectManager.StatusMod("spore", false);
            for (int i = 0; i < spore; i++)
                sporeStatusMod.AddAbilities(StatusEffectSpores.ability);

            return sporeStatusMod;
        }
        public CardModificationInfo GetSporeDecalMod()
        {
            CardModificationInfo sporeDecalMod = StatusEffectManager.StatusMod("spore_decal", false);
            sporeDecalMod.DecalIds.Add($"decalSpore_{Mathf.Min(2, spore - 1)}");
            return sporeDecalMod;
        }

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard && base.PlayableCard.OpponentCard != playerUpkeep;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard && base.PlayableCard.OpponentCard != playerTurnEnd;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && spore > 0;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return base.PlayableCard.TakeDamageTriggerless(spore, null);
            yield return new WaitForSeconds(0.4f);
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (turnPlayed == Singleton<TurnManager>.Instance.TurnNumber)
                yield break;
            
            int newSpore = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .FindAll(s => s.Card != null && s.Card.HasAbility(Sporogenic.ability)).Count;

            if (newSpore == 0)
                yield break;

            spore += newSpore;
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.LightNegationEffect();
            base.PlayableCard.AddTemporaryMod(GetSporeStatusMod());
            if (spore <= 3)
                base.PlayableCard.AddTemporaryMod(GetSporeDecalMod());

            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (base.PlayableCard.Slot == null)
                yield break;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");
            CardModificationInfo stats = new(spore, spore)
            {
                bloodCostAdjustment = base.PlayableCard.Info.BloodCost,
                bonesCostAdjustment = base.PlayableCard.Info.BonesCost,
                energyCostAdjustment = base.PlayableCard.Info.EnergyCost,
                addGemCost = base.PlayableCard.Info.GemsCost
            };

            minion.Mods.Add(stats);

            foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                if (item.abilities.Count == 0) // Add merged sigils
                    continue;

                CardModificationInfo cardModificationInfo = new()
                {
                    abilities = item.abilities,
                    fromCardMerge = item.fromCardMerge,
                    fromDuplicateMerge = item.fromDuplicateMerge
                };

                minion.Mods.Add(cardModificationInfo);
            }

            foreach (Ability item in base.PlayableCard.Info.abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                minion.Mods.Add(new CardModificationInfo(item)); // Add base sigils

            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, base.PlayableCard.Slot, 0.15f);
        }
    }
    public class StatusEffectSpores : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Spores()
        {
            StatusEffectManager.StatusEffect<StatusEffectSpores, Spores>(
                ref StatusEffectSpores.ability, ref Spores.specialAbility,
                pluginGuid, "sigilSpores", Spores.rName, Spores.rDesc,
                false, StatusEffectManager.IconColour.Green, StatusEffectManager.Part1StatusEffect);
        }
    }
}
