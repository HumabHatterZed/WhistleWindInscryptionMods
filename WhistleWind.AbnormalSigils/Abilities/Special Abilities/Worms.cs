using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Worms : SpecialCardBehaviour, IGetOpposingSlots
    {
        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Worms";
        public static readonly string rDesc = "At the start of the owner's turn, this card gains 1 Worms. At 5+ Worms, attack allied creatures instead with a chance to give 1 Worms to struck cards.";

        public int wormSeverity = 1;
        private bool accountForInitialHit = true;
        private bool hasEvolved = false;
        private bool Infested => wormSeverity >= 5;
        private CardModificationInfo GetWormStatusMod(int severity)
        {
            CardModificationInfo result = StatusEffectManager.StatusMod("worm", false);
            for (int i = 0; i < severity; i++)
                result.AddAbilities(StatusEffectWorms.ability);

            return result;
        }
        private CardModificationInfo GetWormDecalMod(int severity)
        {
            CardModificationInfo decal = StatusEffectManager.StatusMod("worm_decal", false);
            decal.DecalIds.Add($"decalWorms_{Mathf.Min(2, severity - 1)}");
            return decal;
        }
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        private void UpdateWorms()
        {
            wormSeverity++;
            base.PlayableCard.AddTemporaryMod(GetWormStatusMod(wormSeverity));

            // update the decal if the image has changed
            if (wormSeverity <= 3)
                base.PlayableCard.AddTemporaryMod(GetWormDecalMod(wormSeverity));
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
                if (SeededRandom.Value(base.GetRandomSeed()) <= (wormSeverity - 2) * .1f)
                {
                    if (target.LacksSpecialAbility(Worms.specialAbility))
                    {
                        CardModificationInfo newWormStatusMod = GetWormStatusMod(1);
                        CardModificationInfo newWormDecalMod = GetWormDecalMod(1);

                        target.AddPermanentBehaviour<Worms>();
                        target.AddTemporaryMods(newWormStatusMod, newWormDecalMod);
                    }
                    else
                    {
                        var component = target.GetComponent<Worms>();
                        component.wormSeverity++;
                        target.AddTemporaryMod(GetWormStatusMod(component.wormSeverity));
                        if (component.wormSeverity <= 3)
                            target.AddTemporaryMod(GetWormDecalMod(component.wormSeverity));
                    }
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
    public class StatusEffectWorms : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Worms()
        {
            StatusEffectManager.StatusEffect<StatusEffectWorms, Worms>(
                ref StatusEffectWorms.ability, ref Worms.specialAbility,
                pluginGuid, "sigilWorms", Worms.rName, Worms.rDesc,
                false, StatusEffectManager.IconColour.Brown, StatusEffectManager.Part1StatusEffect);
        }
    }
}
