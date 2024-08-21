using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Wedge()
        {
            const string rulebookName = "Drive It In";
            const string rulebookDescription = "[creature] deals 1 additional damage when striking uninjured creatures.";
            const string dialogue = "A hard beginning blow.";
            const string triggerText = "[creature] drives into its prey.";
            Wedge.ability = AbnormalAbilityHelper.CreateAbility<Wedge>(
                "sigilWedge",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 2,
                modular: false, opponent: true, canStack: true).Id;
        }
    }
    public class Wedge : AbilityBehaviour, IModifyTakenDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target.Status.damageTaken < 1;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return base.LearnAbility(0.3f);
        }
        
        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return target.Status.damageTaken < 1 && attacker == base.Card;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage + 1;
        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }
}
