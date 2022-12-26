using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Corrector()
        {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "A card bearing this sigil has its stats randomly changed according to its cost.";
            const string dialogue = "How balanced.";
            Corrector.ability = AbilityHelper.CreateAbility<Corrector>(
                Resources.sigilCorrector, Resources.sigilCorrector_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                addModular: true, opponent: true, canStack: false, isPassive: false).Id;
        }
    }
    public class Corrector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return base.Card.OpponentCard;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            ChangeStats();
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility();
        }

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
                1 => 4,
                2 => 7,
                3 => 13,
                4 => 20,
                _ => 27
            };
            powerLevel += base.Card.Info.BonesCost > 0 ? base.Card.Info.BonesCost : 0;
            powerLevel += base.Card.Info.EnergyCost switch
            {
                0 => 0,
                1 => 0,
                2 => 2,
                3 => 4,
                4 => 6,
                5 => 8,
                6 => 12,
                _ => 16
            };
            powerLevel += base.Card.Info.GemsCost.Count switch
            {
                1 => 3,
                2 => 6,
                3 => 9,
                _ => 0
            };
            // LifeCost API compatibility
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeCost") != null ? (int)base.Card.Info.GetExtendedPropertyAsInt("LifeCost") : 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") != null ? (int)base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") : 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") != null ? ((int)base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") > 0 ? (int)base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") : 0) : 0;

            int newPower = 0;
            int newHealth = 1;
            int randomSeed = base.GetRandomSeed();
            while (powerLevel > 0)
            {
                // If can afford 1 Power
                if (powerLevel - 2 >= 0)
                {
                    // Roll for 1 Power
                    if (SeededRandom.Range(0, 3, randomSeed) == 0)
                    {
                        newPower += 1;
                        powerLevel -= 2;
                        randomSeed++;
                        continue;
                    }
                }
                newHealth += 1;
                powerLevel -= 1;
                randomSeed++;
            }
            base.Card.AddTemporaryMod(new(newPower - base.Card.Attack, newHealth - base.Card.Health));
        }
    }
}