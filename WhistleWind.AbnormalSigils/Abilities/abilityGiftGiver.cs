using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_GiftGiver()
        {
            const string rulebookName = "Gift Giver";
            const string rulebookDescription = "When [creature] is played, create a random card in your hand.";
            const string dialogue = "A gift for you.";

            GiftGiver.ability = AbnormalAbilityHelper.CreateAbility<GiftGiver>(
                Artwork.sigilGiftGiver, Artwork.sigilGiftGiver_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class GiftGiver : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsLaetitia => base.Card.Info.name.ToLowerInvariant().Contains("laetitia");
        public override CardInfo CardToDraw
        {
            get
            {
                if (this.IsLaetitia)
                {
                    CardInfo cardByName = CardLoader.GetCardByName("wstl_laetitiaFriend");
                    cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                    return cardByName;
                }
                List<CardInfo> list = ScriptableObjectLoader<CardInfo>.AllData.FindAll((CardInfo x) => x.metaCategories.Contains(CardMetaCategory.ChoiceNode));
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
