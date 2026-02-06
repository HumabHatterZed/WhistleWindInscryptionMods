using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_UnkillableWeak() {
            AbilityInfo info = AbilitiesUtil.GetInfo(Ability.DrawCopyOnDeath);
            const string rulebookName = "Broken Samsara";
            const string rulebookDescription = "When [creature] perishes, a copy of it may be created in your hand.";
            UnkillableWeak.ability = AbnormalAbilityHelper.CreateAbility<UnkillableWeak>(
                "sigilUnkillableWeak",
                rulebookName, rulebookDescription, info.abilityLearnedDialogue.lines[0].text, info.triggerText, powerLevel: info.powerLevel,
                modular: true, opponent: info.opponentUsable, canStack: info.canStack)
                .Id;
        }
    }
    /// <summary>
    /// When [creature] attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.
    /// </summary>
    public class UnkillableWeak : OpponentDrawCreatedCard {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool finalSamsara = false;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            if (base.Card.Info.Mods.Find(x => x.singletonId == "wstl:FirstSamsara") == null) {
                base.Card.Info.Mods.Add(new() { singletonId = "wstl:FirstSamsara" });
            }
            else {
                finalSamsara = SeededRandom.Range(0, 5, base.GetRandomSeed()) == 0;
            }

            yield return base.PreSuccessfulTriggerSequence();
            yield return base.QueueOrCreateDrawnCard();
            
            if (finalSamsara) {
                yield return DialogueHelper.PlayDialogueEvent("FinalSamsara");
            }
            else {
                yield return base.LearnAbility(0.5f);
            }
        }
        public override CardInfo CardToDraw => GetCardToDraw();
        public override List<CardModificationInfo> CardToDrawTempMods => GetTempMods();

        private CardInfo GetCardToDraw() {
            if (finalSamsara && base.Card.Info.HasFinalSamsara()) {
                return CardLoader.GetCardByName(base.Card.Info.GetFinalSamsara());
            }
            return base.Card.Info;
        }

        private List<CardModificationInfo> GetTempMods() {
            // always guaranteed to draw a new card once
            if (finalSamsara) {
                return new() { new() { negateAbilities = new() { UnkillableWeak.ability, Ability.DrawCopyOnDeath } } };
            }

            return null;
        }

        public const string UNKILLABLE_WEAK_FINAL_DRAW = "UNKILLABLE_WEAK_FINAL_DRAW";
    }
}