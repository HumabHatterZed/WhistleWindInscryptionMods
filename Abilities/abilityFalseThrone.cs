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
        private void Ability_FalseThrone()
        {
            const string rulebookName = "False Throne";
            const string rulebookDescription = "Once per turn, choose a creature to gain the Neutered sigil. Create a copy of the selected card in your hand with its cost reduced to 0.";
            const string dialogue = "The will to fight has been lost.";
            FalseThrone.ability = AbilityHelper.CreateActivatedAbility<FalseThrone>(
                Artwork.sigilFalseThrone, Artwork.sigilFalseThrone_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                overrideModular: true).Id;
        }
    }
    public class FalseThrone : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            if (slot != null && slot.Card != null)
            {
                CardInfo cardInfo = slot.Card.Info;
                cardInfo.cost = 0;
                cardInfo.bonesCost = 0;
                cardInfo.energyCost = 0;
                cardInfo.gemsCost = new();

                CardModificationInfo cardModificationInfo = new(this.LatchAbility) { fromTotem = true, singletonId = "wstl:ActivatedLatch" };

                slot.Card.Anim.LightNegationEffect();
                slot.Card.AddTemporaryMod(cardModificationInfo);
                yield return new WaitForSeconds(0.75f);
                if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
                {
                    yield return new WaitForSeconds(0.2f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null);
                yield return new WaitForSeconds(0.45f);
                yield return base.LearnAbility(0.1f);
            }
        }
    }
}
