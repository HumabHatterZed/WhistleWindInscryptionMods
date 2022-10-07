﻿using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "When this card strikes a card and there is another card behind it, deal 1 overkill damage to that card.";
            const string dialogue = "Your beast runs mine through.";

            Piercing.ability = AbilityHelper.CreateAbility<Piercing>(
                Artwork.sigilPiercing, Artwork.sigilPiercing_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true, opponent: false, canStack: true, isPassive: false).Id;
        }
    }
    public class Piercing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return !base.Card.OpponentCard;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            PlayableCard queuedCard = Singleton<BoardManager>.Instance.GetCardQueuedForSlot(target.Slot);
            if (queuedCard != null && !queuedCard.Dead)
            {
                yield return base.PreSuccessfulTriggerSequence();
                yield return Singleton<CombatPhaseManager>.Instance.DealOverkillDamage(base.Card.Info.name == "wstl_ApostleSpear" ? base.Card.Attack : 1, base.Card.Slot, target.Slot);
                yield return LearnAbility(0.25f);
            }
            else
            {
                yield break;
            }
        }
    }
}
