using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void AddFreshFoodMagnificus() {
            const string rulebookName = "Fresh Food Magnificus";
            const string rulebookDescription = "When this card is drawn, create a random Food in your hand. If the opponent owns gems, remove this card from the board and draw a Rabbid Rabbit next turn.";
            const string dialogue = "A freshly baked confectionary, made with love and care.";
            const string triggerText = "[creature] books it!";

            FreshFoodMagnificus.ability = AbilityManager.New(pluginGuid, rulebookName, rulebookDescription, typeof(FreshFoodMagnificus), GetTexture("sigilFreshFood_m.png"))
                .SetAbilityLearnedDialogue(dialogue)
                .SetGBCTriggerText(triggerText)
                .SetPowerlevel(3)
                .SetRulebookName("Fresh Food")
                .SetPixelAbilityIcon(GetTexture("sigilFreshFood_m_pixel.png"))
                .AddMetaCategories(AbilityMetaCategory.MagnificusRulebook)
                .ability;
        }
    }

    public class FreshFoodMagnificus : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int Priority => int.MinValue;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn() {
            base.StartCoroutine(FreshFood.SpawnFoodToHoof(this, base.Card));
            return base.OnOtherCardDrawn(base.Card);
        }

        public override bool RespondsToResolveOnBoard() {
            if (!base.Card.OpponentCard) {
                //OpponentGemsManager.Instance.ForceGemsUpdate();
                return OpponentGemsManager.Instance.opponentGems.Count > 0;
            }

            //ResourcesManager.Instance.ForceGemsUpdate();
            return ResourcesManager.Instance.gems.Count > 0;
        }
        public override IEnumerator OnResolveOnBoard() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return FreshFood.PrepareForBunnie(base.Card);
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => base.Card.OnBoard && this.RespondsToResolveOnBoard();
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) {
            yield return RemoveWizardPortrait();
            yield return this.OnResolveOnBoard();
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => this.RespondsToResolveOnBoard();
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            yield return RemoveWizardPortrait();
            yield return this.OnResolveOnBoard();
        }

        private IEnumerator RemoveWizardPortrait() {
            WizardBattlePortraitSlot slot = WizardPortraitSlotManager.Instance.GetPortraitSlotForCardSlot(base.Card.Slot);
            if (slot != null) {
                yield return slot.OnOtherCardDie(base.Card, null, true, null);
            }
        }
    }
}