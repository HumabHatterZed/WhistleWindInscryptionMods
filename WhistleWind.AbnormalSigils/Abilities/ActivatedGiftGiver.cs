using Core.Helpers;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Saves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_ActivatedGiftGiver() {
            const string rulebookName = "Gift Giver";
            const string rulebookDescription = "Create a random card in your hand, then deactivate this sigil for 3 turns.";
            const string dialogue = "A gift for you.";
            const string triggerText = "[creature] has a gift for you!";
            ActivatedGiftGiver.ability = AbnormalAbilityHelper.CreateActivatedAbility<ActivatedGiftGiver>(
                "sigilGiftLatch",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3)
                .Id;
        }
    }
    /// <summary>
    /// Create a random card in your hand, then deactivate this sigil for 3 turns.
    /// </summary>
    public class ActivatedGiftGiver : DelayedActivatedAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int TurnDelay => 3;
        private string CustomCardToDraw => base.Card.Info.GetExtendedProperty(GiftGiver.CUSTOM_CARD_PROPERTY);
        private CardInfo CardToDraw {
            get {
                if (CustomCardToDraw != null) {
                    CardInfo cardByName = CardLoader.GetCardByName(CustomCardToDraw);
                    cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                    return cardByName;
                }
                List<CardInfo> list = CardManager.AllCardsCopy.FindAll(x => x.HasCardMetaCategory(CardMetaCategory.ChoiceNode) && x.temple == SaveManager.SaveFile.GetSceneAsCardTemple());
                list = CardLoader.RemoveDeckSingletonsIfInDeck(list);
                if (SaveManager.SaveFile.IsPart2) {
                    list.RemoveAll(x => x.LacksCardMetaCategory(CardMetaCategory.GBCPlayable));
                }

                if (list.Count == 0) {
                    list.Add(CardLoader.GetCardByName("Coyote"));
                }

                return list[SeededRandom.Range(0, list.Count, base.GetRandomSeed())];
            }
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            if (currentTurnDelay == 1) {
                base.Card.RenderInfo.overriddenAbilityIcons.Remove(this.Ability);
                base.Card.RenderCard();
            }
            yield return base.OnUpkeep(playerUpkeep);
        }

        public override IEnumerator Activate() {
            base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, ResourceBank.Get<Texture>("Art/Cards/AbilityIcons/sigilGiftLatch_disabled"));
            base.Card.RenderCard();
            yield return CombatHelpers.QueueOrCreateDrawnCard(CardToDraw, base.Card.OpponentCard);
            yield return base.LearnAbility(0.4f);
            yield return base.Activate();
        }
    }
}
