using DiskCardGame;
using InscryptionAPI.Triggers;
using WhistleWind.Core.Helpers;
using System.Collections;
using Resources = ModDebuggingMod.Properties.Resources;
using InscryptionAPI.Card;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void Ability_Debug()
        {
            const string rulebookName = "Debug";
            const string rulebookDescription = "Buggery";
            const string dialogue = "Amogus.";
            Debug.ability = AbilityHelper.CreateAbility<Debug>(
                pluginGuid, Resources.sigilDebug, Resources.sigilDebugP,
                rulebookName, rulebookDescription, dialogue, powerLevel: 0,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Debug : AbilityBehaviour, IOnBellRung
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn()
        {
            yield return base.PreSuccessfulTriggerSequence();

            //base.Card.AddTemporaryMod(new(1, 1));
            /*            CardInfo info = CardLoader.GetCardByName("Squirrel");
                        info.Mods.Add(new(Ability.TripleBlood));
                        info.Mods.Add(new(Ability.DrawCopy));
                        info.Mods.Add(new() { nameReplacement = "SuperSquirrel" });
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(info);*/
        }
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            return attacker && amount >= target.Health && target.OpponentCard == attacker.OpponentCard;
        }
        public override bool RespondsToOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && deathSlot.IsPlayerSlot == base.Card.IsPlayerCard();
        }

        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            Plugin.Log.LogDebug($"OtherDealt");
            return base.OnOtherCardDealtDamage(attacker, amount, target);
        }
        public bool RespondsToBellRung(bool playerCombatPhase)
        {
            return base.Card.OnBoard && base.Card.OpponentCard != playerCombatPhase;
        }

        public IEnumerator OnBellRung(bool playerCombatPhase)
        {
            Plugin.Log.LogDebug($"OnBellRung");

            yield break;
        }
    }
}
