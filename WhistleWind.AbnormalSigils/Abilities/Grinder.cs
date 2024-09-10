using DiskCardGame;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Grinder()
        {
            const string rulebookName = "Grinder";
            const string rulebookDescription = "[creature] gains the stats of the creatures sacrificed to play it.";
            const string dialogue = "Now everything will be just fine.";
            Grinder.ability = AbnormalAbilityHelper.CreateAbility<Grinder>(
                "sigilGrinder",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Grinder : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return !fromCombat && Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.AddTemporaryMod(new(card.Attack, card.Health));
            yield return base.LearnAbility();
        }
    }
}
