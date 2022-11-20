using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Corrector()
        {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "[creature] has its stats randomly changed according to its cost.";
            const string dialogue = "How balanced.";
            Corrector.ability = AbnormalAbilityHelper.CreateAbility<Corrector>(
                Artwork.sigilCorrector, Artwork.sigilCorrector_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false, isPassive: false).Id;
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
            yield return AbnormalMethods.ChangeCurrentView(View.Board);
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
                3 => 11,
                4 => 18,
                _ => base.Card.Info.BloodCost * 7
            };
            powerLevel += base.Card.Info.BonesCost;
            powerLevel += base.Card.Info.EnergyCost;
            powerLevel += base.Card.Info.GemsCost.Count * 3;

            // LifeCost API compatibility
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeCost") ?? 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") ?? 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") ?? 0;

            int newPower = 0;
            int newHealth = 1;
            int randomSeed = base.GetRandomSeed();
            while (powerLevel > 0)
            {
                // If can afford 1 Power
                if (powerLevel - 2 >= 0)
                {
                    // Roll for 1 Power
                    if (SeededRandom.Range(0, 3, randomSeed++) == 0)
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