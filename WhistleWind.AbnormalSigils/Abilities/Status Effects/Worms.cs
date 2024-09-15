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
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;


        private bool accountForInitialHit = true;
        private bool hasEvolved = false;
        public bool Infested => EffectPotency > 4;

        public override List<string> EffectDecalIds()
        {
            return new()
            {
                "decalWorms_" + Mathf.Min(2, EffectPotency - 1)
            };
        }
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public void UpdateWorms()
        {
            ModifyPotency(1, false);
            if (EffectPotency <= 3) // update the decal if the image has changed
                base.PlayableCard.AddTemporaryMod(GetStatusDecalsMod(true));
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
                if (SeededRandom.Value(base.GetRandomSeed()) <= (EffectPotency - 2) * .1f)
                {
                    if (target.LacksSpecialAbility(specialAbility))
                        yield return target.AddStatusEffect<Worms>(0, true);

                    else
                        target.GetStatusEffect<Worms>().UpdateWorms();

                }
            }

            yield break;
        }

        public bool RemoveDefaultAttackSlot() => Infested;
        public bool RespondsToGetOpposingSlots() => Infested;

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            List<CardSlot> slots = new();
            List<PlayableCard> allyCards = Singleton<BoardManager>.Instance.GetCards(!base.PlayableCard.OpponentCard);
            allyCards.Remove(base.PlayableCard);

            // if there are other cards, target them exclusively
            if (allyCards.Count > 0)
            {
                List<PlayableCard> cards = new(allyCards);
                cards.RemoveAll(x => x.HasAnyOfTraits(Trait.Terrain, Trait.Pelt));
                if (cards.Count > 0)
                {
                    slots.Add(cards[SeededRandom.Range(0, cards.Count, base.GetRandomSeed())].Slot);
                }
                else
                {
                    slots.Add(allyCards[SeededRandom.Range(0, allyCards.Count, base.GetRandomSeed())].Slot);
                }
            }
            return slots;
        }
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Worms()
        {
            const string rName = "Worms";
            const string rDesc = "At the start of the owner's turn, a card bearing this effect gains 1 Worms. At 5+ Worms, target ally creatures with a chance to inflict 1 Worms with each strike.";
            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Worms>(
                pluginGuid, rName, rDesc, -1, GameColors.Instance.lightBrown,
                TextureLoader.LoadTextureFromFile("sigilWorms.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilWorms_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Worms.specialAbility = data.Id;
            Worms.iconId = data.IconInfo.ability;
        }
    }
}
