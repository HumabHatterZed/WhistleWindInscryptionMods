using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_CatLover()
        {
            const string rulebookName = "Cat Lover";
            const string rulebookDescription = "When [creature] is played, add a random cat-related card to your hand with the Cowardly sigil.";
            const string dialogue = "Pretty kitty.";

            CatLover.ability = AbnormalAbilityHelper.CreateAbility<CatLover>(
                Artwork.sigilCatLover, Artwork.sigilCatLover_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class CatLover : OpponentDrawCreatedCard
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsTheRoadHome => base.Card.Info.name.ToLowerInvariant().Contains("theroadhome");
        public override CardInfo CardToDraw
        {
            get
            {
                if (this.IsTheRoadHome)
                {
                    CardInfo cardByName = CardLoader.GetCardByName("wstl_scaredyCat");
                    cardByName.Mods.AddRange(base.GetNonDefaultModsFromSelf(this.Ability));
                    return cardByName;
                }
                List<CardInfo> list = ScriptableObjectLoader<CardInfo>.AllData.FindAll((CardInfo x) => x.name.ToLowerInvariant().Contains("cat"));
                list.RemoveAll((CardInfo y) => y.name.StartsWith("wstl"));
                CardInfo drawnCard = list[SeededRandom.Range(0, list.Count, base.GetRandomSeed())];
                drawnCard.Mods.Add(new(Cowardly.ability) { fromCardMerge = SaveManager.SaveFile.IsPart1 });
                return drawnCard;
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
