using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;


namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Martyr()
        {
            const string rulebookName = "Martyr";
            const string rulebookDescription = "When [creature] dies, all allied creatures gain 2 Health and lose all negative status effects.";
            const string dialogue = "A selfless death to cleanse your beasts of evil.";
            const string triggerText = "[creature]'s death cleanses your other creatures!";
            Martyr.ability = AbnormalAbilityHelper.CreateAbility<Martyr>(
                "sigilMartyr",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 1,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Martyr : AbilityBehaviour // original code taken from SigilADay - julianperge
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            List<CardSlot> validSlots = Singleton<BoardManager>.Instance.GetSlotsCopy(!base.Card.OpponentCard).FindAll(s => s.Card && s.Card != base.Card);

            // if no other cards on the board except this card
            if (validSlots.Count == 0)
                yield break;

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.1f);
            foreach (var slot in validSlots)
            {
                // get all temp mods relating to negative status effects
                List<CardModificationInfo> negativeAbilities = slot.Card.TemporaryMods.FindAll(x => x.IsStatusMod(false));

                // remove all special abilities relating to negative status effects
                SpecialCardBehaviour[] components = slot.Card.GetComponents<SpecialCardBehaviour>();
                foreach (SpecialCardBehaviour component in components)
                {
                    foreach (var effect in StatusEffectManager.StatusEffects[false])
                    {
                        if (component.GetType() == effect.Key.AbilityBehaviour)
                        {
                            Destroy(component);
                            break;
                        }
                    }
                }

                slot.Card.Anim.LightNegationEffect();
                slot.Card.HealDamage(2);
                if (negativeAbilities.Count > 0)
                {
                    slot.Card.RemoveTemporaryMods(negativeAbilities.ToArray());
                    yield return new WaitForSeconds(0.15f);
                }
                yield return new WaitForSeconds(0.15f);
            }
            yield return base.LearnAbility(0.25f);
        }
    }
}
