using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWind.AbnormalSigils
{
    public class Pebble : StatusEffectBehaviour, IModifyDamageTaken
    {
        public static SpecialTriggeredAbility specialAbility;
        public static Ability iconId;
        public override string CardModSingletonName => "pebble";

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard.Health < base.PlayableCard.MaxHealth)
            {
                base.PlayableCard.Anim.LightNegationEffect();
                base.PlayableCard.HealDamage(1);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
            }
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => target == base.PlayableCard;
        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage - 1;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
    public partial class AbnormalPlugin
    {
        private void StatusEffect_Pebble()
        {
            const string rName = "Pebble";
            const string rDesc = "This card regains 1 Health at the end of the owner's turn. When this card is struck, reduce the received damage by 1.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.NewStatusEffect<Pebble>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilPebble", pixelIconTexture: "sigilPebble_pixel",
                powerLevel: 2, iconColour: GameColors.Instance.nearWhite,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect });

            Pebble.specialAbility = data.BehaviourId;
            Pebble.iconId = data.IconId;
        }
    }
}
