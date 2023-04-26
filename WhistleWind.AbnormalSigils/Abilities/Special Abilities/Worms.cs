﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class Worms : SpecialCardBehaviour, IGetOpposingSlots, IOnBellRung
    {
        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Worms";
        public static readonly string rDesc = "At the start of combat, this card gains 1 Worms. At 3 or more Worms, this card will attack allied spaces with a chance to give 1 Worms to struck cards.";

        public int wormSeverity = 0;
        private bool accountForInitialHit = true;
        private bool hasEvolved = false;
        public override bool RespondsToUpkeep(bool playerUpkeep) => playerUpkeep != base.PlayableCard.OpponentCard;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            wormSeverity++;

            if (wormSeverity <= 3)
            {
                base.PlayableCard.Anim.LightNegationEffect();

                CardModificationInfo cardModificationInfo = new()
                {
                    singletonId = "worms_status",
                    DecalIds = { "wstl_worms_" + (wormSeverity - 1).ToString() },
                    nonCopyable = true,
                };
                base.PlayableCard.Info.Mods.RemoveAll(x => x.singletonId == "worms_status");
                base.PlayableCard.Info.Mods.Add(cardModificationInfo);
                base.PlayableCard.UpdateStatsText();
                yield return new WaitForSeconds(0.2f);
            }

            if (wormSeverity >= 3)
            {
                if (!hasEvolved)
                {
                    hasEvolved = true;
                    CardInfo copyOfInfo = base.PlayableCard.Info.Clone() as CardInfo;
                    copyOfInfo.displayedName = "Nested " + base.PlayableCard.Info.displayedName;
                    copyOfInfo.Mods = new(base.PlayableCard.Info.Mods);
                    yield return base.PlayableCard.TransformIntoCard(copyOfInfo);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("SerpentsNestInfection");
                }
            }
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            if (target.OpponentCard == base.PlayableCard.OpponentCard)
            {
                if (target.LacksTrait(AbnormalPlugin.NakedSerpent))
                {
                    if (SeededRandom.Value(base.GetRandomSeed()) <= (wormSeverity - 2) * .1f)
                    {
                        if (!target.Info.Mods.Exists(x => x.singletonId == "wstl:serpentDummy"))
                            target.AddPermanentBehaviour<Worms>();

                        else
                            target.GetComponent<Worms>().wormSeverity++;
                    }
                }
            }
            else if (target.HasAbility(SerpentsNest.ability))
            {
                if (accountForInitialHit)
                    accountForInitialHit = false;
                else
                    wormSeverity++;
            }
            yield break;
        }

        public bool RemoveDefaultAttackSlot() => wormSeverity >= 3;
        public bool RespondsToGetOpposingSlots() => wormSeverity >= 3;

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            List<CardSlot> allySlots = HelperMethods.GetSlotsCopy(base.PlayableCard.OpponentCard);
            allySlots.Remove(base.PlayableCard.Slot);
            if (allySlots.Exists(x => x.Card != null))
            {
                allySlots = allySlots.FindAll(x => x.Card != null);
            }
            return new() { allySlots[SeededRandom.Range(0, allySlots.Count - 1, base.GetRandomSeed())] };
        }

        public bool RespondsToBellRung(bool playerCombatPhase)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator OnBellRung(bool playerCombatPhase)
        {
            throw new System.NotImplementedException();
        }
    }
    public class RulebookWorms : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class AbnormalPlugin
    {
        private void Rulebook_Worms()
        {
            RulebookWorms.ability = AbnormalAbilityHelper.CreateAbility<RulebookWorms>(Artwork.sigilWorms, Artwork.sigilWorms_pixel, Worms.rName, Worms.rDesc, "", unobtainable: true).Id;
        }
        private void SpecialAbility_Worms()
        {
            Worms.specialAbility = AbilityHelper.CreateSpecialAbility<Worms>(pluginGuid, Worms.rName).Id;
        }
    }
}
