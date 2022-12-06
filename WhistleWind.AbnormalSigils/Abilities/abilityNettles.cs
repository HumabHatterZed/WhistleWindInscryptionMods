using WhistleWind.Core.Helpers;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Nettles()
        {
            const string rulebookName = "Nettle Clothes";
            const string rulebookDescription = "When [creature] is played, create a random Brother in all empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.";
            const string dialogue = "If she gave her brothers the nettle clothing, their happy days would be restored.";
            Nettles.ability = AbnormalAbilityHelper.CreateAbility<Nettles>(
                Artwork.sigilNettles, Artwork.sigilNettles_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Nettles : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        // 1 mode for each brother
        private readonly CardModificationInfo mod1 = new(Ability.DoubleStrike);
        private readonly CardModificationInfo mod2 = new(Piercing.ability);
        private readonly CardModificationInfo mod3 = new(Reflector.ability);
        private readonly CardModificationInfo mod4 = new(Ability.Deathtouch);
        private readonly CardModificationInfo mod5 = new(Burning.ability);
        private readonly CardModificationInfo mod6 = new(ThickSkin.ability);
        public override bool RespondsToResolveOnBoard() => true;

        public override IEnumerator OnResolveOnBoard()
        {
            yield return HelperMethods.ChangeCurrentView(View.Board);
            List<CardSlot> validSlots = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).FindAll((CardSlot slot) => !(slot.Card != null) && slot.Card != base.Card);

            if (validSlots.Count == 0)
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                yield return AbnormalDialogueManager.PlayDialogueEvent("NettlesFail");
                yield break;
            }

            yield return base.PreSuccessfulTriggerSequence();

            // Create list of all Brothers
            List<CardInfo> brothers = new()
            {
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother1"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother2"),
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother3"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother4"),
                CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother5"), CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother6")
            };

            // Create a unique Brother for each valid slot
            foreach (CardSlot s in validSlots)
            {
                int seed = SeededRandom.Range(0, brothers.Count, base.GetRandomSeed());
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(brothers[seed], s, 0.15f);
                brothers.RemoveAt(seed);
            }
            yield return base.LearnAbility();
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            // respond if otherCard on same side of board and is a Brother
            return otherCard.OpponentCard == base.Card.OpponentCard && otherCard.Info.name.Contains("wstl_dreamOfABlackSwanBrother");
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            // get the right mod
            CardModificationInfo cardMod = otherCard.Info.name switch
            {
                "wstl_dreamOfABlackSwanBrother1" => mod1,
                "wstl_dreamOfABlackSwanBrother2" => mod2,
                "wstl_dreamOfABlackSwanBrother3" => mod3,
                "wstl_dreamOfABlackSwanBrother4" => mod4,
                "wstl_dreamOfABlackSwanBrother5" => mod5,
                _ => mod6
            };
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
                base.Card.Status.hiddenAbilities.Remove(card.Info.Abilities[0]);

            base.Card.RemoveTemporaryMod(cardMod);
            yield return AbnormalDialogueManager.PlayDialogueEvent("NettlesDie");
            yield return base.Card.TakeDamage(1, null);
        }
    }
}
