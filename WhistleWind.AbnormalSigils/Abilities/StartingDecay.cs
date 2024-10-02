using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_StartingDecay()
        {
            const string rulebookName = "Imminent Decay";
            const string rulebookDescription = "When [creature] is played, it gains 1 Decay for each stack of this sigil.";
            StartingDecay.ability = AbnormalAbilityHelper.CreateAbility<StartingDecay>(
                "sigilDecay",
                rulebookName, rulebookDescription, powerLevel: -3,
                modular: false, opponent: false, canStack: true)
                .SetAbilityRedirect("Decay", Decay.iconId, GameColors.Instance.darkPurple)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class StartingDecay : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => base.Card.LacksTrait(AbnormalPlugin.ImmuneToAilments);
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.Card.AddStatusEffect<Decay>(1);
        }
    }
}
