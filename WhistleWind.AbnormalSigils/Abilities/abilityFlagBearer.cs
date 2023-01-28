using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_FlagBearer()
        {
            const string rulebookName = "Flag Bearer";
            const string rulebookDescription = "While this card is on the board, adjacent cards gain 2 Health.";
            const string dialogue = "Morale runs high.";

            FlagBearer.ability = AbnormalAbilityHelper.CreateAbility<FlagBearer>(
                Artwork.sigilFlagBearer, Artwork.sigilFlagBearer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: true).Id;
        }
    }
    public class FlagBearer : AbilityBehaviour, IPassiveHealthBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override bool RespondsToResolveOnBoard() =>
            Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Exists(slot => slot.Card != null);
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) =>
            Singleton<BoardManager>.Instance.GetAdjacentSlots(otherCard.Slot).Exists(slot => slot.Card == base.Card);

        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card.Health < 3)
                    slot.Card.HealDamage(2);
            }
            yield break;
        }

        public int GetPassiveHealthBuff(PlayableCard target)
        {
            if (this.Card.OnBoard)
                return Singleton<BoardManager>.Instance.GetAdjacentSlots(target.Slot)
                    .Where(slot => slot.Card != null && slot.Card.HasAbility(ability)).Count() > 0 ? 2 : 0;

            return 0;
        }
    }
}
