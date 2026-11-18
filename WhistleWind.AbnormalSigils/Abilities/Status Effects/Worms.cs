using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public class Worms : StatusEffectBehaviour, ISetupAttackSequence {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        public override int MaxPotency => 5;

        public override List<string> EffectDecalIds() {
            return new()
            {
                "decalWorms_" + (Mathf.Min(3, EffectPotency) - 1)
            };
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) {
            if (base.PlayableCard.OpponentCard != playerUpkeep) {
                return !AtMaxPotency;
            }
            return false;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            yield return base.PreSuccessfulTriggerSequence();

            base.PlayableCard.Anim.LightNegationEffect();
            yield return base.PlayableCard.AddStatusEffect<Worms>(1, true);
            yield return new WaitForSeconds(0.2f);

            if (AtMaxPotency) {
                CardModificationInfo mod = new() {
                    nameReplacement = string.Format("Nested {0}", base.PlayableCard.Info.displayedName)
                };

                base.PlayableCard.Anim.PlayTransformAnimation();
                base.PlayableCard.Info.Mods.Add(mod);
                base.PlayableCard.RenderCard();
                yield return new WaitForSeconds(0.5f);

                yield return DialogueHelper.PlayDialogueEvent("SerpentsNestInfection");
            }
        }

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => AtMaxPotency && amount > 0 && target != null;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            if (target.LacksTrait(AbnormalPlugin.NakedSerpent)) {
                yield return base.PreSuccessfulTriggerSequence();
                yield return target.AddStatusEffect<Worms>(1, true);
            }

            yield break;
        }

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return card == base.PlayableCard && AtMaxPotency && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot) {
            int seed = base.GetRandomSeed();
            List<CardSlot> slots = new();
            List<CardSlot> allSlots = BoardManager.Instance.AllSlotsCopy;
            allSlots.RemoveAll(x => x == PlayableCard.Slot);

            List<CardSlot> occupiedSlots = allSlots.FindAll(x => x.Card != null);

            for (int i = 0; i < currentSlots.Count; i++) {
                if (occupiedSlots.Count > 0) {
                    slots.Add(occupiedSlots.GetSeededRandom(seed++));
                }
                else {
                    slots.Add(allSlots.GetSeededRandom(seed++));
                }
            }

            return slots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot) {
            return 0;
        }
    }
    public partial class AbnormalPlugin {
        private void StatusEffect_Worms() {
            const string rName = "Worms";
            const string rDesc = "At the start of the owner's turn, a card bearing this effect gains 1 Worms. At 5 Worms, this card will also target ally cards and inflict 1 Worms when striking.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Worms>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.lightBrown,
                TextureLoader.LoadTextureFromFile("sigilWorms.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilWorms_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Worms.specialAbility = data.Id;
            Worms.iconId = data.IconInfo.ability;
            data.IconInfo.SetGBCTriggerText("The worms take their fill.");
        }
    }
}
