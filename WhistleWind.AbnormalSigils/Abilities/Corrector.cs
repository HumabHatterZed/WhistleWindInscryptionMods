using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Corrector()
        {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "When [creature] is drawn, randomly change its stats according to its total play cost.";
            const string dialogue = "How balanced.";
            const string triggerText = "[creature] stats are forcefully 'corrected'.";
            Corrector.ability = AbnormalAbilityHelper.CreateAbility<Corrector>(
                "sigilCorrector",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
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
            GetNewStats();
            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
            yield return new WaitForSeconds(0.15f);
            yield return base.Card.Anim.FlipInAir();
            yield return new WaitForSeconds(0.1f);
            GetNewStats();
            yield return new WaitForSeconds(0.1f);
            yield return base.LearnAbility();
        }

        private int GetCostPowerLevel()
        {
            int powerLevel = base.Card.Info.BonesCost;
            powerLevel += base.Card.Info.BloodCost switch
            {
                0 => 0,
                1 => 4,
                2 => 8,
                3 => 13,
                _ => 24 + (base.Card.Info.BloodCost - 4) * 7
            };
            powerLevel += base.Card.Info.EnergyCost switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 7,
                5 => 9,
                _ => 12 + (base.Card.Info.EnergyCost - 6) * 4
            };
            powerLevel += base.Card.Info.GemsCost.Count * 3;

            // Life Cost, Forbidden Mox compatibility
            powerLevel += (base.Card.Info.GetExtendedPropertyAsInt("LifeCost") ?? 0) * 2;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") ?? 0;
            powerLevel += (base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") ?? 0) * 3;
            powerLevel += base.Card.Info.GetExtendedProperty("ForbiddenMoxCost") != null ? 3 : 0;

            return powerLevel;
        }
        private void GetNewStats()
        {
            int[] stats = new[] { 0, 0 };
            int powerLevel = GetCostPowerLevel();
            int randomSeed = base.GetRandomSeed();

            while (powerLevel > 0)
            {
                // 40% chance of giving Power
                if (powerLevel > 1 && SeededRandom.Value(randomSeed *= 2) <= 0.4f)
                {
                    stats[0]++;
                    powerLevel -= 2;
                }
                else
                {
                    stats[1]++;
                    powerLevel--;
                }
            }

            // give 1 extra Health if there is none present
            if (stats[1] == 0)
                stats[1] = 1;

            base.Card.AddTemporaryMod(new(stats[0] - base.Card.Attack, stats[1] - base.Card.Health) { nonCopyable = true });
        }
    }
}