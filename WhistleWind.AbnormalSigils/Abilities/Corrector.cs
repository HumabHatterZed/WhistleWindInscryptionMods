using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Corrector() {
            const string rulebookName = "Corrector";
            const string rulebookDescription = "When [creature] is drawn, its stats are randomly changed according to its total play cost.";
            const string dialogue = "How balanced.";
            const string triggerText = "[creature] stats are forcefully 'corrected'.";
            Corrector.ability = AbnormalAbilityHelper.CreateAbility<Corrector>(
                "sigilCorrector",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] is drawn, its stats are randomly changed according to its total play cost.
    /// </summary>
    public class Corrector : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => base.Card.OpponentCard;
        public override bool RespondsToDrawn() => true;

        public override IEnumerator OnResolveOnBoard() {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.PlayTransformAnimation();
            yield return new WaitForSeconds(0.15f);
            GetNewStats();
            yield return new WaitForSeconds(0.55f);
            yield return base.LearnAbility();
        }
        public override IEnumerator OnDrawn() {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.Card);
            yield return new WaitForSeconds(0.15f);
            yield return base.Card.Anim.FlipInAir();
            yield return new WaitForSeconds(0.1f);
            GetNewStats();
            yield return new WaitForSeconds(0.1f);
            yield return base.LearnAbility();
        }

        private int GetCostPowerLevel() {
            int powerLevel = Mathf.CeilToInt(base.Card.BonesCost() * 1.2f);
            powerLevel += base.Card.BloodCost() switch {
                0 => 0,
                1 => 4,
                2 => 8,
                3 => 14,
                _ => 21 + (base.Card.BloodCost() - 4) * 7
            };
            if (!SaveManager.SaveFile.IsPart1 || base.Card.EnergyCost < 3) {
                powerLevel += base.Card.EnergyCost;
            }
            else {
                powerLevel += base.Card.EnergyCost switch {
                    3 => 4,
                    4 => 6,
                    5 => 9,
                    _ => 13 + (base.Card.EnergyCost - 6) * 4
                };
            }

            powerLevel += base.Card.GemsCost().Count * 3;

            // Life Cost, Forbidden Mox compatibility
            powerLevel += (base.Card.Info.GetExtendedPropertyAsInt("LifeCost") ?? 0) * 2;
            powerLevel += base.Card.Info.GetExtendedPropertyAsInt("MoneyCost") ?? 0;
            powerLevel += (base.Card.Info.GetExtendedPropertyAsInt("LifeMoneyCost") ?? 0) * 3;
            powerLevel += base.Card.Info.GetExtendedProperty("ForbiddenMoxCost") != null ? 3 : 0;

            return powerLevel;
        }
        private void GetNewStats() {
            int[] stats = new[] { 0, 0 };
            int powerLevel = GetCostPowerLevel();
            int randomSeed = base.GetRandomSeed();

            while (powerLevel > 0) {
                // 40% chance of giving Power
                if (powerLevel > 1 && SeededRandom.Value(randomSeed *= 2) <= 0.4f) {
                    stats[0]++;
                    powerLevel -= 2;
                }
                else {
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