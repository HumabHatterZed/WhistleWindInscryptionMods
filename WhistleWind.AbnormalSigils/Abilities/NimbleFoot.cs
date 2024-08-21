using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Patches;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_NimbleFoot()
        {
            const string rulebookName = "Nimble-Footed";
            string rulebookDescription = "At the start of the owner's turn, this <color=#00a602>card</color> <color=#008b02>gains</color> <color=\"green\">2</color> Haste.";
            rulebookDescription = "At the start of the owner's turn, this card gains 1 Haste. Whenever this card moves to a new space, gain 1 additional Haste.";
            NimbleFoot.ability = AbnormalAbilityHelper.CreateAbility<NimbleFoot>(
                "sigilNimbleFoot",
                rulebookName, rulebookDescription, powerLevel: 1,
                modular: true, opponent: true, canStack: false).Id;//.SetTextRedirect("Haste", Haste.iconId).Id;
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