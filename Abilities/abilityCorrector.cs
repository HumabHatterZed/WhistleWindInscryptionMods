using InscryptionAPI;
using DiskCardGame;
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
            const string rulebookDescription = "A card bearing this sigil has its stats changed depending on its cost, with higher costs giving higher stat totals.";
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

        private List<int> bloodPower = new()
        {
            0,
            7,
            9,
            14,
            21,
            28
        };

        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
            yield return base.Card.FlipInHand(ChangeStats);
            yield return new WaitForSeconds(0.1f);
        }

        private void ChangeStats()
        {
            int bloodPower = base.Card.Info.BloodCost switch
            {
                0 => 0,
                1 => 7,
                2 => 9,
                3 => 14,
                4 => 21,
                _ => 28
            };
            int bonesPower = base.Card.Info.BonesCost switch
            {
                0 => 0,
                1 => 2,
                2 => 4,
                3 => 5,
                4 => 6,
                5 => 6,
                6 => 7,
                7 => 8,
                8 => 9,
                _ => 10
            };
            int energyPower = base.Card.Info.EnergyCost switch
            {
                0 => 0,
                1 => 2,
                2 => 5,
                3 => 6,
                4 => 7,
                5 => 9,
                6 => 13,
                _ => 18
            };
            int gemsPower = base.Card.Info.GemsCost.Count switch
            {
                0 => 0,
                1 => 4,
                2 => 7,
                _ => 12
            };
            int newPower = 0;
            int newHealth = 1;
            int totalPower = bloodPower + bonesPower + energyPower + gemsPower - 1;
            while (totalPower > 0)
            {
                bool givePower = SeededRandom.Range(0, 3, base.GetRandomSeed()) == 0;
                if (givePower && totalPower - 2 >= 0)
                {
                    newPower += 1;
                    totalPower -= 2;
                }
                else
                {
                    newHealth += 1;
                    totalPower -= 1;
                }
            }
            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(null);
        }
    }
}
