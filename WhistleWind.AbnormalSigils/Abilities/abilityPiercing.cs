using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Piercing()
        {
            const string rulebookName = "Piercing";
            const string rulebookDescription = "Damage dealt by this card cannot be negated or reduced. If there is another card behind the struck card, deal 1 damage to it.";
            const string dialogue = "Your beast runs mine through.";

            Piercing.ability = AbnormalAbilityHelper.CreateAbility<Piercing>(
                Artwork.sigilPiercing, Artwork.sigilPiercing_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Piercing : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => true;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (base.Card.IsPlayerCard())
            {
                PlayableCard queuedCard = Singleton<BoardManager>.Instance.GetCardQueuedForSlot(target.Slot);
                if (queuedCard == null || queuedCard.Dead)
                    yield break;

                yield return Singleton<CombatPhaseManager>.Instance.DealOverkillDamage(base.Card.Info.name == "wstl_ApostleSpear" ? base.Card.Attack : 1, base.Card.Slot, target.Slot);
                yield return LearnAbility(0.25f);
            }
            if (target.HasAnyOfAbilities(Ability.DeathShield, ThickSkin.ability) ||
                target.Slot.GetAdjacentCards().Exists(x => x.HasAbility(Protector.ability)))
                yield return LearnAbility(0.25f);
        }
    }
}
