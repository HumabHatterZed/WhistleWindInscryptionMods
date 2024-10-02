using DiskCardGame;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class Sin : StatusEffectBehaviour, IOnUpkeepInHand
    {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;

        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;

        public override List<string> EffectDecalIds() => new();

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // remove Sin when Long Arms is broken
            if (base.PlayableCard.OpponentCard != playerUpkeep && TurnManager.Instance.Opponent is ApocalypseBossOpponent opp)
                return opp.BattleSequencer.DisabledEggEffects.Contains(ActiveEggEffect.LongArms);

            return false;
        }
        public bool RespondsToUpkeepInHand(bool playerUpkeep) => this.RespondsToUpkeep(playerUpkeep);

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0 && target != null && EffectPotency > 0;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            yield return target.AddStatusEffect<Sin>(1);
            ModifyPotency(-1, false);
            yield break;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            Destroy();
            yield break;
        }
        public IEnumerator OnUpkeepInHand(bool playerUpkeep) => this.OnUpkeep(playerUpkeep);
    }
    public partial class LobotomyPlugin
    {
        private void StatusEffect_Sin()
        {
            const string rName = "Sin";
            const string rDesc = "When this card deals damage to another creature, transfer 1 Sin to that card.";

            StatusEffectManager.FullStatusEffect data = StatusEffectManager.New<Sin>(
                pluginGuid, rName, rDesc, 0, GameColors.Instance.gold,
                TextureLoader.LoadTextureFromFile("sigilUnjustScale.png", ModAssembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect);

            Sin.specialAbility = data.Id;
            Sin.iconId = data.IconInfo.ability;
        }
    }
}
