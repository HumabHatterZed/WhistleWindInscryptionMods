using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_Aggravating() {
            const string rulebookName = "Aggravating";
            const string rulebookDescription = "All creatures opposing this card gain 1 Power.";
            const string dialogue = "My beasts are incensed by your creature's presence.";

            AbilityManager.FullAbility ab = AbnormalAbilityHelper.CreateAbility<Aggravating>(
                "sigilAggravating",
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: true, canStack: true);

            ab.SetFlipYIfOpponent();
            ab.SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile("sigilAggravating_flipped.png", Assembly));
            Aggravating.ID = ab.Id;
        }
    }
    /// <summary>
    /// All creatures opposing this card gain 1 Power.
    /// </summary>
    public class Aggravating : AbilityBehaviour, IPassiveAttackBuff {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => ActivateOnPlay();
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);
        public int GetPassiveAttackBuff(PlayableCard target) {
            if (!this.Card.OnBoard || target.OpponentCard == this.Card.OpponentCard || target == base.Card)
                return 0;

            return base.Card.GetAbilityStacks(Ability);
        }
        public bool ActivateOnPlay() {
            if (base.Card.Slot != null)
                return Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard).Where(s => s.Card != null).Count() > 0;

            return false;
        }
    }
}
