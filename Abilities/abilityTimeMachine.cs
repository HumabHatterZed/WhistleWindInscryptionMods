using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TimeMachine()
        {
            const string rulebookName = "Time Machine";
            const string rulebookDescription = "Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck at random based on their power level.";
            const string dialogue = "Close your eyes, and count to ten.";

            TimeMachine.ability = WstlUtils.CreateActivatedAbility<TimeMachine>(
                Resources.sigilTimeMachine,
                rulebookName, rulebookDescription, dialogue, 5).Id;
        }
    }
    public class TimeMachine : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // Failsafe that prevents ability from being used multiple times per run
        public override bool CanActivate()
        {
            return !PersistentValues.HasUsedBackwardClock;
        }

        // Ends the battle
        public override IEnumerator Activate()
        {
            PersistentValues.HasUsedBackwardClock = true;
            int damage = Singleton<LifeManager>.Instance.DamageUntilPlayerWin;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
            RunState.Run.playerDeck.RemoveCard(base.Card.Info);
        }
    }
}
