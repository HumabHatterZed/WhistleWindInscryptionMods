﻿using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_GiftGiver()
        {
            const string rulebookName = "Gift Giver";
            const string rulebookDescription = "When this card is played, create a random card in your hand.";
            const string dialogue = "A gift for you.";

            GiftGiver.ability = AbilityHelper.CreateAbility<GiftGiver>(
                Resources.sigilGiftGiver, Resources.sigilGiftGiver_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
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

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return QueueOrCreateDrawnCard();
            yield return base.LearnAbility();
        }
    }
}
