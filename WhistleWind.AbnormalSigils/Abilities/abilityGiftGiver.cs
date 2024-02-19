using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GiftGiver()
        {
            const string rulebookName = "Gift Giver";
            const string rulebookDescription = "When [creature] is played, create a random card in your hand.";
            const string dialogue = "A gift for you.";
            const string triggerText = "[creature] has a gift for you!";
            GiftGiver.ability = AbnormalAbilityHelper.CreateAbility<GiftGiver>(
                "sigilGiftGiver",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;
        }
    }
    public class GiftGiver : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsLaetitia => base.Card.Info.name.ToLowerInvariant().Contains("laetitia");
        private string CustomCardToDraw => base.Card.Info.GetExtendedProperty("wstl:GiftGiver");
        public override CardInfo CardToDraw
        {
            get
            {
                if (IsLaetitia || CustomCardToDraw != null)
                {
                    CardInfo cardByName = CardLoader.GetCardByName(CustomCardToDraw ?? "wstl_laetitiaFriend");
                    cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                    return cardByName;
                }
                List<CardInfo> list = CardManager.AllCardsCopy.FindAll(x => x.HasCardMetaCategory(CardMetaCategory.ChoiceNode));
                list = CardLoader.RemoveDeckSingletonsIfInDeck(list);
                if (SaveManager.SaveFile.IsPart2)
                    list.RemoveAll(x => x.LacksCardMetaCategory(CardMetaCategory.GBCPlayable));

                if (list.Count == 0)
                    list.Add(CardLoader.GetCardByName("wstl_trainingDummy"));

                return list[SeededRandom.Range(0, list.Count, base.GetRandomSeed())];
            }
        }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return QueueOrCreateDrawnCard();
            yield return base.LearnAbility();
        }
    }
}
