using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Nettles()
        {
            const string rulebookName = "Clothes Made of Nettles";
            const string rulebookDescription = "When a card bearing this sigil is played, create random Brothers in empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.";
            const string dialogue = "If she gave her brothers the nettle clothing, their happy days would be restored.";
            Nettles.ability = WstlUtils.CreateAbility<Nettles>(
                Resources.sigilNettles,
                rulebookName, rulebookDescription, dialogue, 5,
                overrideModular: true).Id;
        }
    }
    public class Nettles : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string altDialogue = "She saw her dear brothers in the distance. Her family that needed the nettle clothing to be free of the curse.";
        private readonly string altDialogue2 = "She fell to the ground, vomiting ooze like the rest of [c:bR]the City[c:].";
        private readonly string altDialogue3 = "The lake ripples gently. As if a number of swans just took flight.";

        private CardModificationInfo mod1 = new(Ability.DoubleStrike);
        private CardModificationInfo mod2 = new(1,0);
        private CardModificationInfo mod3 = new(Reflector.ability);
        private CardModificationInfo mod4 = new(Ability.Deathtouch);
        private CardModificationInfo mod5 = new(Ability.Sharp);
        private CardModificationInfo mod6 = new(Ability.DebuffEnemy);
        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            List<CardSlot> validSlots = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).FindAll((CardSlot slot) => !(slot.Card != null) && slot.Card != base.Card);

            yield return base.PreSuccessfulTriggerSequence();
            // Show 
            if (validSlots.Count == 0)
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                if (!PersistentValues.HasSeenSwanFail)
                {
                    PersistentValues.HasSeenSwanFail = true;
                    yield return new WaitForSeconds(0.25f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue2, -0.65f, 0.4f);
                }
                else
                {
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue3, -0.65f, 0.4f);
                }
                yield break;
            }

            List<CardInfo> brothers = new()
            {
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother1"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother2"),
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother3"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother4"),
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother5"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother6")
            };

            foreach (CardSlot s in validSlots)
            {
                int seed = SeededRandom.Range(0, brothers.Count, base.GetRandomSeed());
                if (brothers != null)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(brothers[seed], s, 0.15f);
                    brothers.RemoveAt(seed);
                }
                else
                {
                    break;
                }
            }
            yield return base.LearnAbility();
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return otherCard.OpponentCard == base.Card.OpponentCard && otherCard.Info.name.Contains("wstl_dreamOfABlackSwanBrother");
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            CardModificationInfo cardMod = otherCard.Info.name switch
            {
                "wstl_dreamOfABlackSwanBrother1" => mod1,
                "wstl_dreamOfABlackSwanBrother2" => mod2,
                "wstl_dreamOfABlackSwanBrother3" => mod3,
                "wstl_dreamOfABlackSwanBrother4" => mod4,
                "wstl_dreamOfABlackSwanBrother5" => mod5,
                _ => mod6
            };
            if (otherCard.Info.Abilities.Count != 0)
            {
                base.Card.Status.hiddenAbilities.Add(otherCard.Info.Abilities[0]);
            }
            base.Card.AddTemporaryMod(cardMod);
            yield break;
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card.OpponentCard == base.Card.OpponentCard && card.Info.name.Contains("wstl_dreamOfABlackSwanBrother");
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardModificationInfo cardMod = card.Info.name switch
            {
                "wstl_dreamOfABlackSwanBrother1" => mod1,
                "wstl_dreamOfABlackSwanBrother2" => mod2,
                "wstl_dreamOfABlackSwanBrother3" => mod3,
                "wstl_dreamOfABlackSwanBrother4" => mod4,
                "wstl_dreamOfABlackSwanBrother5" => mod5,
                _ => mod6
            };
            if (card.Info.Abilities.Count != 0)
            {
                base.Card.Status.hiddenAbilities.Remove(card.Info.Abilities[0]);
            }
            base.Card.RemoveTemporaryMod(cardMod);
            if (!PersistentValues.HasSeenSwanFail)
            {
                PersistentValues.HasSeenSwanFail = true;
                yield return new WaitForSeconds(0.25f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue2, -0.65f, 0.4f);
            }
            base.Card.TakeDamage(1, null);
        }
    }
}
