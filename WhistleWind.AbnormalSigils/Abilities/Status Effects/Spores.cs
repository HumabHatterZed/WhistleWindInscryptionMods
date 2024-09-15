using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Spores : StatusEffectBehaviour
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override List<string> EffectDecalIds()
        {
            return new()
            {
                "decalSpore_" + Mathf.Min(2, EffectPotency - 1)
            };
        }

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard && base.PlayableCard.OpponentCard != playerUpkeep;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard && base.PlayableCard.OpponentCard != playerTurnEnd;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && EffectPotency > 0;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.StrongNegationEffect();
            base.PlayableCard.HealDamage(-EffectPotency);
            if (base.PlayableCard.Health <= 0)
                yield return base.PlayableCard.Die(false, null);
            yield return new WaitForSeconds(0.4f);
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (TurnGained == Singleton<TurnManager>.Instance.TurnNumber)
                yield break;

            int newSpore = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.PlayableCard.Slot)
                .FindAll(s => s.Card != null && s.Card.HasAbility(Sporogenic.ability)).Count;

            if (newSpore == 0)
                yield break;

            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.PlayableCard.Anim.LightNegationEffect();

            ModifyPotency(newSpore, false);
            if (EffectPotency <= 3)
                base.PlayableCard.AddTemporaryMod(GetStatusDecalsMod(true));

            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (base.PlayableCard.Slot == null)
                yield break;

            CardInfo minion = CardLoader.GetCardByName("wstl_theLittlePrinceMinion");
            CardModificationInfo stats = new(EffectPotency, EffectPotency)
            {
                nameReplacement = base.PlayableCard.Info.DisplayedNameLocalized,
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

            foreach (Ability item in base.PlayableCard.Info.DefaultAbilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                minion.Mods.Add(new CardModificationInfo(item)); // Add base sigils

            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(minion, base.PlayableCard.Slot, 0.15f);
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Spores()
        {
            const string rName = "Spores";
            const string rDesc = "At the end of the owner's turn, a card bearing this effect takes damage equal to its Spores. When this card perishes, create a Spore Mold Beast in its place with stats equal to its Spores.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Spores>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.brightBlue,
                TextureLoader.LoadTextureFromFile("sigilSpores.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilSpores_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Spores.specialAbility = data.Id;
            Spores.iconId = data.IconInfo.ability;
        }
    }
}
