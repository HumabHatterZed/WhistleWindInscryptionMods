﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Alchemist()
        {
            const string rulebookName = "Alchemist";
            const string rulebookDescription = "Pay 2 Energy to discard your current hand and draw cards equal to the amount you discarded.";
            const string dialogue = "The unending faith of countless promises.";
            const string triggerText = "[creature] replaces your hand with a new one!";
            Alchemist.ability = AbnormalAbilityHelper.CreateActivatedAbility<Alchemist>(
                "sigilAlchemist",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3).Id;
        }
    }
    public class Alchemist : ExtendedActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int StartingEnergyCost => 2;

        public override bool CanActivate()
        {
            if (Singleton<PlayerHand>.Instance.CardsInHand.Count() > 0)
            {
                if (Singleton<CardDrawPiles3D>.Instance.Deck.Cards.Count == 0)
                    return Singleton<CardDrawPiles3D>.Instance.SideDeck.Cards.Count > 0;

                return true;
            }
            return false;
        }

        // Discard all hands in card and draw an equal number
        public override IEnumerator Activate()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return HelperMethods.ChangeCurrentView(View.Hand);

            List<PlayableCard> cardsInHand = new(Singleton<PlayerHand>.Instance.CardsInHand);
            int count = 0;

            Singleton<PlayerHand>.Instance.CardsInHand.Clear();
            foreach (PlayableCard item in cardsInHand)
            {
                if (item != null)
                {
                    count++;
                    yield return Singleton<PlayerHand>.Instance.CleanUpCard(item);
                }
            }
            for (int i = 0; i < count; i++)
            {
                if (Singleton<CardDrawPiles3D>.Instance.Deck.Cards.Count > 0)
                {
                    Singleton<CardDrawPiles3D>.Instance.Pile.Draw();
                    yield return Singleton<CardDrawPiles3D>.Instance.DrawCardFromDeck();
                }
                else if (Singleton<CardDrawPiles3D>.Instance.SideDeck.Cards.Count > 0)
                {
                    Singleton<CardDrawPiles3D>.Instance.SidePile.Draw();
                    yield return Singleton<CardDrawPiles3D>.Instance.DrawFromSidePile();
                }
                else
                {
                    yield return DialogueHelper.PlayAlternateDialogue(dialogue: "You've exhausted all of your cards.");
                    break;
                }
            }
            yield return base.LearnAbility(0.2f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }
    }
}
