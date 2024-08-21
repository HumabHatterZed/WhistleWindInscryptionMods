using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Nettles()
        {
            const string rulebookName = "Nettle Clothes";
            const string rulebookDescription = "When this card is played, Brothers are created on adjacent empty spaces. This card gains the first sigil of each adjacent Brother while they are on the board.";
            const string dialogue = "These clothes will restore our happy days.";
            const string triggerText = "[creature] brings out its family!";
            Nettles.ability = AbnormalAbilityHelper.CreateAbility<Nettles>(
                "sigilNettles",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Nettles : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private const int _priority = int.MaxValue;
        public override int Priority => _priority;

        PlayableCard leftCard = null;
        PlayableCard rightCard = null;

        PlayableCard oldLeftCard = null;
        PlayableCard oldRightCard = null;

        private readonly CardModificationInfo leftMod = new(Ability.Sharp) { singletonId = "BlackSwan_Left" };
        private readonly CardModificationInfo rightMod = new(Ability.Sharp) { singletonId = "BlackSwan_Right" };

        private int randomBrotherSeed;
        private string GetRandomBrotherCardName()
        {
            List<string> validCards = CardManager.AllCardsCopy.Where(x => x.HasTrait(AbnormalPlugin.SwanBrother)).Select(x => x.name).ToList();
            validCards.RemoveAll(x => BoardManager.Instance.GetCards(!base.Card.OpponentCard).Exists(pc => pc.name == x));
            return validCards[SeededRandom.Range(0, validCards.Count, randomBrotherSeed++)];
        }
        public override IEnumerator OnResolveOnBoard()
        {
            randomBrotherSeed = base.GetRandomSeed() * 2;
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;
            yield return base.PreSuccessfulTriggerSequence();
            if (toLeftValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft);
            }
            if (toRightValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight);
            }
            if (toLeftValid || toRightValid)
            {
                yield return base.LearnAbility();
            }
            else if (!base.HasLearned)
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Once again, she is left alone.", -0.65f, 0.4f);
            }
        }
        private IEnumerator SpawnCardOnSlot(CardSlot slot)
        {
            CardInfo cardByName = CardLoader.GetCardByName(GetRandomBrotherCardName());
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }

        public override void ManagedUpdate()
        {
            base.ManagedUpdate();
            if (base.Card.OnBoard && GlobalTriggerHandler.Instance.StackSize == 0)
            {
                oldLeftCard = leftCard;
                leftCard = BoardManager.Instance.GetAdjacent(base.Card.Slot, true)?.Card;
                if (leftCard != oldLeftCard)
                {
                    base.Card.RemoveTemporaryMod(leftMod, false);
                    if (leftCard != null)
                    {
                        leftMod.abilities.Clear();
                        leftMod.abilities.Add(leftCard.Info.Abilities.Count > 0 ? leftCard.Info.Abilities[0] : Ability.Sharp);
                        base.Card.AddTemporaryMod(leftMod);
                    }
                    else
                    {
                        base.Card.OnStatsChanged();
                    }
                }

                oldRightCard = rightCard;
                rightCard = BoardManager.Instance.GetAdjacent(base.Card.Slot, false)?.Card;
                if (rightCard != oldRightCard)
                {
                    base.Card.RemoveTemporaryMod(rightMod, false);
                    if (rightCard != null)
                    {
                        rightMod.abilities.Clear();
                        rightMod.abilities.Add(rightCard.Info.Abilities.Count > 0 ? rightCard.Info.Abilities[0] : Ability.Sharp);
                        base.Card.AddTemporaryMod(rightMod);
                    }
                    else
                    {
                        base.Card.OnStatsChanged();
                    }
                }
            }
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return card == leftCard || card == rightCard;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return DialogueHelper.PlayDialogueEvent("NettlesDie");
        }
    }
}
