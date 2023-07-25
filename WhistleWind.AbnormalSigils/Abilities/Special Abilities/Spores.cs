using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : StatusEffectBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public override string SingletonName => "spore";

        public override List<string> EffectDecalIds()
        {
            return new()
            {
                "decalSpore_" + Mathf.Min(2, effectCount - 1)
            };
        }

        public int turnPlayed = -1;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard && base.PlayableCard.OpponentCard != playerUpkeep;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard && base.PlayableCard.OpponentCard != playerTurnEnd;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && effectCount > 0;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            yield return base.PlayableCard.TakeDamageTriggerless(effectCount, null);
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

            effectCount += newSpore;
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.LightNegationEffect();
            base.PlayableCard.AddTemporaryMod(GetEffectCountMod());
            if (effectCount <= 3)
                base.PlayableCard.AddTemporaryMod(GetEffectDecalMod());

            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (base.PlayableCard.Slot == null)
                yield break;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");
            CardModificationInfo stats = new(effectCount, effectCount)
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
            const string rName = "Spores";
            const string rDesc = "At the start of its owner's turn, this card takes damage equal to its Spores. Upon dying, create a Spore Mold Creature with stats equal to its Spores.";
            
            Spores.specialAbility = StatusEffectManager.NewStatusEffect<Spores>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilSpores", pixelIconTexture: "sigilSpores_pixel",
                powerLevel: -2, iconColour: GameColors.Instance.darkBlue,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).Item1;
        }
    }
}
