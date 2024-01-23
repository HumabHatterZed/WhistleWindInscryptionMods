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
            rulebookDescription = "At the start of the owner's turn, this card gains 2 Haste.";
            NimbleFoot.ability = AbnormalAbilityHelper.CreateAbility<NimbleFoot>(
                "sigilNimbleFoot",
                rulebookName, rulebookDescription, powerLevel: 1,
                modular: true, opponent: true, canStack: true).Id;//.SetTextRedirect("Haste", Haste.iconId).Id;
        }
    }
    public class NimbleFoot : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // only apply Bind if target isn't null/dead
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return AddHasteToCard(base.Card);
            yield return base.LearnAbility(0.4f);
        }
        private IEnumerator AddHasteToCard(PlayableCard card)
        {
            card.Anim.LightNegationEffect();
            card.AddStatusEffect<Haste>(base.Card.GetAbilityStacks(ability) * 2, modifyTurnGained: delegate (int i)
            {
                if (card.HasStatusEffect<Haste>())
                    return i;

                return i + 1;
            });
            yield return new WaitForSeconds(0.1f);
        }
    }
}