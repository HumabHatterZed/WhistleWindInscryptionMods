using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Sap()
        {
            const string rulebookName = "Sap";
            const string rulebookDescription = "When sacrificed, has a chance to cause the sacrificing card to explode.";
            Sap.specialAbility = AbilityHelper.CreateSpecialAbility<Sap>(rulebookName, rulebookDescription).Id;
        }
    }
    public class Sap : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "A strange gurgling sound comes from your beast's stomach.";
        private int sacrificeCount;

        public override bool RespondsToSacrifice()
        {
            return true;
        }
        public override IEnumerator OnSacrifice()
        {
            if (sacrificeCount >= 8 || SeededRandom.Range(0, 10 - sacrificeCount, base.GetRandomSeed()) == 0)
            {
                sacrificeCount = 0;
                PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;

                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Curious);
                yield return new WaitForSeconds(0.25f);

                if (!card.HasAbility(Volatile.ability))
                {
                    card.FlipInHand(AddVolatile);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                }
                yield return card.Info.SetExtendedProperty("wstl:Sap", true);
            }
            else
            {
                this.sacrificeCount++;
            }
        }

        private void AddVolatile()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            card.AddTemporaryMod(new CardModificationInfo(Volatile.ability));
        }
    }
}
