using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infiniscryption.Spells.Sigils
{
    public class DrawTwoCards : AbilityBehaviour
    {
        public override Ability Ability => AbilityID;
        public static Ability AbilityID { get; private set; }

        public static void Register()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Draw Twice";
            info.rulebookDescription = "When [creature] dies, draw two more cards and add them to your deck.";
            info.canStack = true;
            info.passive = false;
            info.metaCategories = new List<AbilityMetaCategory>() { AbilityMetaCategory.Part1Rulebook };
            info.SetPixelAbilityIcon(AssetHelper.LoadTexture("drawtwocardsondeath_pixel"));

            DrawTwoCards.AbilityID = AbilityManager.Add(
                InfiniscryptionSpellsPlugin.OriginalPluginGuid,
                info,
                typeof(DrawTwoCards),
                AssetHelper.LoadTexture("ability_drawtwocardsondeath")
            ).Id;
        }

        public override bool RespondsToResolveOnBoard() => !base.Card.Info.IsSpell();
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => base.Card.Info.IsSpell();

        public override IEnumerator OnResolveOnBoard() => Effect();
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (base.Card.OpponentCard)
            {
                int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed();
                List<CardInfo> possibleCards = new();

                // go through the turn plan and get all cards that Leshy will/has play/ed
                foreach (List<CardInfo> turn in Singleton<TurnManager>.Instance.Opponent.TurnPlan)
                {
                    foreach (CardInfo info in turn)
                    {
                        if (base.Card.Info != info)
                            possibleCards.Add(info);
                    }
                }

                // create 2 cards in queue
                for (int i = 0; i < 2; i++)
                {
                    List<CardSlot> openSlots = Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(s => !Singleton<TurnManager>.Instance.Opponent.QueuedSlots.Contains(s)).ToList();

                    if (openSlots.Count() > 0)
                    {

                        CardSlot index = openSlots[SeededRandom.Range(0, openSlots.Count, randomSeed++)];
                        CardInfo cardToQueue = possibleCards[SeededRandom.Range(0, possibleCards.Count, randomSeed++)];
                        yield return Singleton<TurnManager>.Instance.Opponent.QueueCard(cardToQueue, index);
                        possibleCards.Remove(cardToQueue);
                    }
                }
                yield return new WaitForSeconds(0.45f);
            }
            else
            {
                yield return Effect();
            }
        }

        private IEnumerator Effect()
        {
            yield return base.PreSuccessfulTriggerSequence();
            ViewManager.Instance.SwitchToView(View.Default);

            // Now we draw the top card from each deck
            if (CardDrawPiles.Instance is CardDrawPiles3D)
            {
                CardDrawPiles3D cardPiles = CardDrawPiles.Instance as CardDrawPiles3D;
                yield return cardPiles.DrawCardFromDeck();
                yield return cardPiles.DrawFromSidePile();
            }
            else
            {
                yield return CardDrawPiles.Instance.DrawCardFromDeck();
                yield return CardDrawPiles.Instance.DrawCardFromDeck();
            }

            yield return base.LearnAbility(0.5f);
        }
    }
}
