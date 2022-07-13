using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Corrector()
        {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "A card bearing this sigil has its stats changed based on its cost. Higher costs yield higher overall stats.";
            const string dialogue = "How balanced.";
            Corrector.ability = WstlUtils.CreateAbility<Corrector>(
                Resources.sigilCorrector,
                rulebookName, rulebookDescription, dialogue, 2).Id;
        }
    }
    public class Corrector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
            yield return base.Card.FlipInHand(ChangeStats);
            yield return new WaitForSeconds(0.1f);
            yield return base.LearnAbility();
        }

        private void ChangeStats()
        {
            int powerLevel = base.Card.Info.BloodCost switch
            {
                0 => 0,
                1 => 5,
                2 => 8,
                3 => 14,
                4 => 21,
                _ => 28
            };
            powerLevel += base.Card.Info.BonesCost > 0 ? base.Card.Info.BonesCost + 1 : 0;
            powerLevel += base.Card.Info.EnergyCost switch
            {
                0 => 0,
                1 => 1,
                2 => 3,
                3 => 5,
                4 => 7,
                5 => 9,
                6 => 13,
                _ => 17
            };
            powerLevel += base.Card.Info.GemsCost.Count switch
            {
                1 => 4,
                2 => 7,
                3 => 10,
                _ => 0
            };
            // LifeCost API compatibility (maybe)
            powerLevel += !(base.Card.Info.GetExtendedPropertyAsInt("LifeCost") != null) ? 0 : (int)base.Card.Info.GetExtendedPropertyAsInt("LifeCost");
            powerLevel += !(base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") != null) ? 0 : (int)base.Card.Info.GetExtendedPropertyAsInt("MoneyCost");
            powerLevel += !(base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") != null) ? 0 : (int)base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost");

            int newPower = 0;
            int newHealth = 0;
            if (powerLevel <= 0)
            {
                newPower = base.Card.Info.Attack > 0 ? 1 : 0;
            }
            while (powerLevel > 0)
            {
                // If can afford 1 Power
                if (powerLevel - 2 >= 0)
                {
                    // Roll for 1 Power
                    if (new System.Random().Next(0, 2) == 0)//SeededRandom.Range(0, 3, base.GetRandomSeed()) == 0;
                    {
                        newPower += 1;
                        powerLevel -= 2;
                        continue;
                    }
                }
                newHealth += 1;
                powerLevel -= 1;
                WstlPlugin.Log.LogInfo($"{powerLevel}");
            }
            base.Card.AddTemporaryMod(new(newPower - base.Card.Attack, newHealth - base.Card.Health));
        }
    }
}
