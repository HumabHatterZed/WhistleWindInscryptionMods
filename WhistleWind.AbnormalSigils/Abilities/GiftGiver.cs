using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Saves;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_GiftGiver() {
            const string rulebookName = "Gift-Laden";
            const string rulebookDescription = "When [creature] is first played, create a random card in your hand.";
            const string dialogue = "A gift for you.";
            const string triggerText = "[creature] has a gift for you!";
            GiftGiver.ability = AbnormalAbilityHelper.CreateAbility<GiftGiver>(
                "sigilGiftGiver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    /// <summary>
    /// When [creature] is first played, create a random card in your hand.
    /// </summary>
    public class GiftGiver : OpponentDrawCreatedCard {
        public static Ability ability;
        public override Ability Ability => ability;
        public const string CUSTOM_CARD_PROPERTY = "wstl:GiftGiver";
        private string CustomCardToDraw => base.Card.Info.GetExtendedProperty(CUSTOM_CARD_PROPERTY);
        public override CardInfo CardToDraw {
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

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            yield return base.PreSuccessfulTriggerSequence();
            yield return QueueOrCreateDrawnCard();
            base.Card.AddTemporaryMod(new() { negateAbilities = new() { this.Ability }, singletonId = "GiftGiverDisabled", nonCopyable = true });
            yield return base.LearnAbility();
        }
    }
}
