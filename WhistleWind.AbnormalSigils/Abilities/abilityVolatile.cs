using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Volatile()
        {
            const string rulebookName = "Volatile";
            const string rulebookDescription = "When [creature] dies, adjacent and opposing cards are dealt 10 damage.";
            const string dialogue = "An explosive finish.";

            Volatile.ability = AbnormalAbilityHelper.CreateAbility<Volatile>(
                Artwork.sigilVolatile, Artwork.sigilVolatile_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: false, opponent: true, canStack: false,
                flipTexture: Artwork.sigilVolatile_flipped).Id;
        }
    }
    public class Volatile : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            if (AbnormalPlugin.SpellAPI.Enabled && base.Card.Info.IsTargetedSpell())
                return true;

            return base.Card.Info.GetExtendedPropertyAsBool("wstl:Sap") ?? false;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.25f);
            yield return base.Card.Info.SetExtendedProperty("wstl:Sap", false);
            yield return base.Card.Die(false, null);
        }
        public override bool RespondsToPreDeathAnimation(bool wasSacrifice) => base.Card.OnBoard && !wasSacrifice;
        public override IEnumerator OnPreDeathAnimation(bool wasSacrifice)
        {
            base.Card.Anim.LightNegationEffect();
            yield return base.PreSuccessfulTriggerSequence();
            yield return this.ExplodeFromSlot(base.Card.Slot);
            yield return base.LearnAbility(0.25f);
        }

        protected IEnumerator ExplodeFromSlot(CardSlot slot)
        {
            List<CardSlot> adjacentSlots = Singleton<BoardManager>.Instance.GetAdjacentSlots(slot);
            if (adjacentSlots.Count > 0 && adjacentSlots[0].Index < slot.Index)
            {
                if (adjacentSlots[0].Card != null && !adjacentSlots[0].Card.Dead)
                    yield return this.BombCard(adjacentSlots[0].Card, slot.Card);

                adjacentSlots.RemoveAt(0);
            }
            if (slot.opposingSlot.Card != null && !slot.opposingSlot.Card.Dead)
                yield return this.BombCard(slot.opposingSlot.Card, slot.Card);

            if (adjacentSlots.Count > 0 && adjacentSlots[0].Card != null && !adjacentSlots[0].Card.Dead)
                yield return this.BombCard(adjacentSlots[0].Card, slot.Card);
        }

        private IEnumerator BombCard(PlayableCard target, PlayableCard attacker)
        {
            yield return new WaitForSeconds(0.25f);
            yield return target.TakeDamage(10, attacker);
        }
    }
}
