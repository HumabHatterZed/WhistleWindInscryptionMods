using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    /// <summary>
    /// At the end of the owner's turn, this card regains 1 Health. While there are other allies with this effect, this card gains 1 Power.
    /// </summary>
    public class Pebble : StatusEffectBehaviour, IPassiveAttackBuff {
        public static Ability iconId;
        public static SpecialTriggeredAbility specialAbility;
        public override Ability IconAbility => iconId;
        public override SpecialTriggeredAbility StatusEffect => specialAbility;
        
        internal static StatusEffectManager.FullStatusEffect data;

        private bool lonelyCardExists = true;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            RecalculateLoneliness();

            if (base.PlayableCard.Health < base.PlayableCard.MaxHealth) {
                base.PlayableCard.Anim.LightNegationEffect();
                yield return base.PlayableCard.Heal(1, 0.2f);
            }
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            RecalculateLoneliness();
            yield return DialogueHelper.PlayDialogueEvent("LonelyDie", card: base.PlayableCard);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) => true;
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer) {
            RecalculateLoneliness();
            yield break;
        }

        public override bool RespondsToOtherCardAssignedToSlot(PlayableCard otherCard) => true;
        public override IEnumerator OnOtherCardAssignedToSlot(PlayableCard otherCard) {
            RecalculateLoneliness();
            yield break;
        }

        public override bool RespondsToStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus) {
            return target.OpponentCard == base.PlayableCard.OpponentCard && statusEffect.IconAbility == this.IconAbility;
        }
        public override IEnumerator OnStatusEffectAdded(PlayableCard target, int amount, StatusEffectBehaviour statusEffect, bool alreadyHasStatus) {
            RecalculateLoneliness();
            yield break;
        }
        public override bool RespondsToStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            return target.OpponentCard == base.PlayableCard.OpponentCard && statusEffect.IconAbility == this.IconAbility;
        }
        public override IEnumerator OnStatusEffectRemoved(PlayableCard target, StatusEffectBehaviour statusEffect) {
            RecalculateLoneliness();
            yield break;
        }
        private void RecalculateLoneliness() {
            lonelyCardExists = BoardManager.Instance.GetCards(!base.PlayableCard.OpponentCard, x => x.HasStatusEffect<Pebble>()).Count > 1;
        }

        public int GetPassiveAttackBuff(PlayableCard target) {
            if (target == base.PlayableCard && lonelyCardExists) {
                return 1;
            }
            return 0;
        }
    }

    public partial class AbnormalPlugin {
        private void StatusEffect_Pebble() {
            const string rName = "Pebble";
            const string rDesc = "At the end of the owner's turn, this card regains 1 Health. While there are other allies with this effect, this card gains 1 Power.";
            Pebble.data = StatusEffectManager.New<Pebble>(
                pluginGuid, rName, rDesc, 3, GameColors.Instance.nearWhite,
                TextureLoader.LoadTextureFromFile("sigilPebble.png", Assembly),
                TextureLoader.LoadTextureFromFile("sigilPebble_pixel.png", Assembly))
                .AddMetaCategories(StatusMetaCategory.Part1StatusEffect, StatusMetaCategory.Part3StatusEffect, StatusMetaCategory.GrimoraStatusEffect, StatusMetaCategory.MagnificusStatusEffect);

            Pebble.data.IconInfo.SetAbilityRedirect("Lonely", Lonely.ID, GameColors.Instance.limeGreen);
            Pebble.specialAbility = Pebble.data.Id;
            Pebble.iconId = Pebble.data.IconInfo.ability;
        }
    }
}
