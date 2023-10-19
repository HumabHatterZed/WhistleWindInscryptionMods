using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class Sin : StatusEffectBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public override string CardModSingletonName => "sin";

        public override List<string> EffectDecalIds() => new();

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (CustomBossUtils.FightingCustomBoss() && TurnManager.Instance.Opponent is ApocalypseBossOpponent opp)
                return opp.BattleSequence.DisabledEggEffects.Contains(ActiveEggEffect.LongArms) && base.PlayableCard.OpponentCard != playerUpkeep;

            return false;
        }
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            target.AddStatusEffect<Sin>(EffectSeverity);
            SetSeverity(0, false);
            yield break;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            Destroy();
            yield break;
        }
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Sin()
        {
            const string rName = "Sin";
            const string rDesc = "When this card strikes another card, transfer all Sin to that card.";

            Sin.specialAbility = StatusEffectManager.NewStatusEffect<Sin>(
                pluginGuid, rName, rDesc,
                iconTexture: "sigilUnjustScale.png",
                modAssembly: Assembly.GetCallingAssembly(),
                powerLevel: -5, iconColour: GameColors.Instance.gold,
                categories: new() { StatusEffectManager.StatusMetaCategory.Part1StatusEffect }).BehaviourId;
        }
    }
}
