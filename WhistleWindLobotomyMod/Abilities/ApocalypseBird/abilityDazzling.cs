using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Dazzling : AbilityBehaviour, IModifyDamageTaken, IPreTakeDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RespondsToPreTakeDamage(PlayableCard source, int damage)
        {
            return source != null && source.HasStatusEffect<Enchanted>(true);
        }

        public IEnumerator OnPreTakeDamage(PlayableCard source, int damage)
        {
            base.Card.Anim.StrongNegationEffect();
            yield return source.Die(false, null);
            if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
            else
                yield return new WaitForSeconds(0.25f);
        }

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return base.Card == target && attacker != null && attacker.HasStatusEffect<Enchanted>(true);
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => 0;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => int.MinValue;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Dazzling()
        {
            const string rulebookName = "Dazzling";
            Dazzling.ability = LobotomyAbilityHelper.CreateAbility<Dazzling>(
                "sigilDazzling", rulebookName,
                "On activation, inflict up to 3 other cards on the board with Enchanted. This card takes no damage from Enchanted cards, killing them instead.",
                "Like moths to a flame.", powerLevel: 0,
                canStack: false).Id;
        }
    }
}
