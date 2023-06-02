using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Worms : SpecialCardBehaviour, IGetOpposingSlots
    {
        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Worms";
        public static readonly string rDesc = "At the start of the owner's turn, this card gains 1 Worms. At 4+ Worms, attack allied spaces instead with a chance to give 1 Worms to struck cards.";

        public int wormSeverity = 1;
        private bool accountForInitialHit = true;
        private bool hasEvolved = false;
        private bool Infested => wormSeverity >= 4;
        private CardModificationInfo GetWormDecalMod()
        {
            return new()
            {
                singletonId = "worms_decal",
                DecalIds = { $"wstl_worms_{Mathf.Min(2, wormSeverity - 1)}" },
                nonCopyable = true,
            };
        }
        private CardModificationInfo GetWormStatusMod()
        {
            CardModificationInfo result = new()
            {
                singletonId = "worms_status",
                nonCopyable = true
            };
            for (int i = 0; i < wormSeverity; i++)
                result.AddAbilities(StatusEffectWorms.ability);

            return result;
        }
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        private void UpdateWorms()
        {
            wormSeverity++;
            base.PlayableCard.Info.Mods.RemoveAll(x => x.singletonId == "worms_status");
            base.PlayableCard.Info.Mods.Add(GetWormStatusMod());
            if (wormSeverity <= 3) // update the decal if the image has changed
            {
                base.PlayableCard.Info.Mods.RemoveAll(x => x.singletonId == "worms_decal");
                base.PlayableCard.Info.Mods.Add(GetWormDecalMod());
            }
            base.PlayableCard.RenderCard();
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
                        CardInfo targetCopy = target.Info.Clone() as CardInfo;
                        CardModificationInfo newWormStatusMod = new(StatusEffectWorms.ability)
                        {
                            singletonId = "worms_status",
                            nonCopyable = true,
                        };
                        CardModificationInfo newWormDecalMod = new()
                        {
                            singletonId = "worms_decal",
                            DecalIds = { "wstl_worms_0" },
                            nonCopyable = true,
                        };
                        targetCopy.Mods = new(target.Info.Mods) { newWormStatusMod, newWormDecalMod };
                        target.AddPermanentBehaviour<Worms>();
                        target.Status.hiddenAbilities.Add(StatusEffectWorms.ability);
                        target.SetInfo(targetCopy);
                    }
                    else
                        target.GetComponent<Worms>().wormSeverity++;
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
            StatusEffectWorms.ability = AbnormalAbilityHelper.CreateAbility<StatusEffectWorms>(
                Artwork.sigilWorms, Artwork.sigilWorms_pixel,
                Worms.rName, Worms.rDesc, "",
                canStack: true, statusEffect: "brown").Id;
        }
        private void SpecialAbility_Worms() => Worms.specialAbility = AbilityHelper.CreateSpecialAbility<Worms>(pluginGuid, Worms.rName).Id;
    }
}
