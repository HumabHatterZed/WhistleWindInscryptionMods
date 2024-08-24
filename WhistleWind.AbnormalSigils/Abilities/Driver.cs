using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Driver()
        {
            const string rulebookName = "Driven In";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking injured creatures.";
            const string dialogue = "A ferocious onslaught.";
            const string triggerText = "[creature] won't let its target go!";
            Driver.ability = AbnormalAbilityHelper.CreateAbility<Driver>(
                "sigilDriver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Driver : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target.Status.damageTaken > 0;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.LearnAbility(0.3f);
        }
        
        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return target.Status.damageTaken > 0 && attacker == base.Card;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage + 1;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
