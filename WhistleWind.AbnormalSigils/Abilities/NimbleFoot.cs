using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_NimbleFoot()
        {
            const string rulebookName = "Nimble-Footed";
            const string rulebookDescription = "At the start of the owner's turn, [creature] gains Haste equal to 1 plus the number of times it has moved on the board.";
            NimbleFoot.ability = AbnormalAbilityHelper.CreateAbility<NimbleFoot>(
                "sigilNimbleFoot",
                rulebookName, rulebookDescription, powerLevel: 1,
                modular: true, opponent: true, canStack: false)
                .SetAbilityRedirect("Haste", Haste.iconId, GameColors.Instance.orange)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class NimbleFoot : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        int extraHaste = 0;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            yield return base.Card.AddStatusEffect<Haste>(extraHaste, modifyTurnGained: delegate (int i)
            {
                if (base.Card.HasStatusEffect<Haste>())
                    return i;

                return i + 1;
            });
            yield return new WaitForSeconds(0.2f);
            yield return base.LearnAbility(0.4f);
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            return otherCard == base.Card;
        }
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard)
        {
            extraHaste++;
            yield break;
        }
    }
}