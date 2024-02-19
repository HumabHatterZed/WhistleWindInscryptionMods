using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Worms : StatusEffectBehaviour, IGetOpposingSlots
    {
        public static SpecialTriggeredAbility specialAbility;
        public override string CardModSingletonName => "worm";

        private bool accountForInitialHit = true;
        private bool hasEvolved = false;
        public bool Infested => EffectSeverity >= 5;

        public override List<string> EffectDecalIds()
        {
            return new()
            {
                "decalWorms_" + Mathf.Min(2, EffectSeverity - 1)
            };
        }
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public void UpdateWorms()
        {
            AddSeverity(1, false);
            if (EffectSeverity <= 3) // update the decal if the image has changed
                base.PlayableCard.AddTemporaryMod(EffectDecalMod());
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            base.PlayableCard.Anim.LightNegationEffect();
            UpdateWorms();
            yield return new WaitForSeconds(0.2f);

            if (Infested && !hasEvolved)
            {
                hasEvolved = true;
                CardInfo copyOfInfo = base.PlayableCard.Info.Clone() as CardInfo;
                copyOfInfo.Mods = new(base.PlayableCard.Info.Mods)
                {
                    new() { nameReplacement = $"Nested {base.PlayableCard.Info.displayedName}" }
                };
                yield return base.PlayableCard.TransformIntoCard(copyOfInfo);
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayDialogueEvent("SerpentsNestInfection");
            }
        }

        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            // increase Worms 
            if (target.HasAbility(SerpentsNest.ability))
            {
                // since OnDealDamage triggers after OnTakeDamage, we need to account for when Worms is first dealt
                if (accountForInitialHit)
                    accountForInitialHit = false;
                else
                    UpdateWorms();
            }

            if (Infested && target.LacksTrait(AbnormalPlugin.NakedSerpent))
            {
                if (SeededRandom.Value(base.GetRandomSeed()) <= (EffectSeverity - 2) * .1f)
                {
                    if (target.LacksSpecialAbility(specialAbility))
                        target.AddStatusEffect<Worms>(0, true);

                    else
                        target.GetComponent<Worms>().UpdateWorms();

                }
            }

            yield break;
        }

        public bool RemoveDefaultAttackSlot() => Infested;
        public bool RespondsToGetOpposingSlots() => Infested;

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            List<CardSlot> allySlots = Singleton<BoardManager>.Instance.GetSlotsCopy(!base.PlayableCard.OpponentCard);
            allySlots.Remove(base.PlayableCard.Slot);

            // if there are other cards, target them exclusively
            if (allySlots.Exists(x => x.Card))
                allySlots.RemoveAll(x => !x.Card);

            return new() { allySlots[SeededRandom.Range(0, allySlots.Count - 1, base.GetRandomSeed())] };
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Worms()
        {
            const string rName = "Worms";
            const string rDesc = "At the start of the owner's turn, this card gains 1 Worms. At 5+ Worms, attack allied creatures instead with a chance to give 1 Worms to struck cards.";

            Worms.specialAbility = StatusEffectManager.NewStatusEffect<Worms>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilWorms", pixelIconTexture: "sigilWorms_pixel",
                powerLevel: -3, iconColour: GameColors.Instance.brown,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
