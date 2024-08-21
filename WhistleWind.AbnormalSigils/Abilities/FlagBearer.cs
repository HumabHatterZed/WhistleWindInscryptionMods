using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FlagBearer()
        {
            const string rulebookName = "Flag Bearer";
            const string rulebookDescription = "While this card is on the board, adjacent creatures gain 2 Health.";
            const string dialogue = "Morale runs high.";

            FlagBearer.ability = AbnormalAbilityHelper.CreateAbility<FlagBearer>(
                "sigilFlagBearer",
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class FlagBearer : AbilityBehaviour, IPassiveHealthBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override bool RespondsToResolveOnBoard() => base.Card.Slot.GetAdjacentCards().Count > 0;
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard.Slot.GetAdjacentCards().Contains(base.Card);

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            foreach (PlayableCard card in base.Card.Slot.GetAdjacentCards())
            {
                if (card.Health <= 2)
                    card.HealDamage(2);
            }
            yield break;
        }
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);

        public int GetPassiveHealthBuff(PlayableCard target)
        {
            if (this.Card.OnBoard)
                return target.Slot.GetAdjacentCards().Exists(x => x.HasAbility(ability)) ? 2 : 0;

            return 0;
        }
    }
}
