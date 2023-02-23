using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Corrector()
        {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "[creature] has its stats randomly changed according to its cost. Higher costs yield higher stats totals.";
            const string dialogue = "How balanced.";
            Corrector.ability = AbnormalAbilityHelper.CreateAbility<Corrector>(
                Artwork.sigilCorrector, Artwork.sigilCorrector_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class Corrector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => base.Card.OpponentCard;
        public override bool RespondsToDrawn() => true;

        public override IEnumerator OnResolveOnBoard()
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            ChangeStats();
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility();
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
            int powerLevel = base.Card.Info.BonesCost - 1; // to account for initial 1 Health
            powerLevel += base.Card.Info.BloodCost switch
            {
                0 => 0,
                1 => 4,
                2 => 7,
                3 => 11,
                4 => 18,
                _ => base.Card.Info.BloodCost * 7
            };
            powerLevel += base.Card.Info.EnergyCost switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 6,
                5 => 8,
                6 => 12,
                _ => 12 + (base.Card.Info.EnergyCost - 6) * 4
            };
            powerLevel += base.Card.Info.GemsCost.Count * 3;

            // LifeCost, Forbidden Mox compatibility
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeCost") ?? 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") ?? 0;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") ?? 0;
            powerLevel += base.Card.Info.GetExtendedProperty("ForbiddenMoxCost") != null ? 3 : 0;

            int[] stats = new[] { 0, 1 }; 
            int randomSeed = base.GetRandomSeed();
            while (powerLevel > 0)
            {
                // If can afford 1 Power
                if (powerLevel - 2 >= 0)
                {
                    // Roll for 1 Power
                    if (SeededRandom.Range(0, 3, randomSeed++) == 0)
                    {
                        stats[0]++;
                        powerLevel -= 2;
                        continue;
                    }
                }
                stats[1]++;
                powerLevel--;
            }
            base.Card.AddTemporaryMod(new(stats[0] - base.Card.Attack, stats[1] - base.Card.Health));
        }
    }
}