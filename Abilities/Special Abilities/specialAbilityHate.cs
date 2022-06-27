using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Hate()
        {
            const string rulebookName = "Hate";
            const string rulebookDescription = "Transforms when the balance has shifted too far. Enters a weakened forme every other turn.";
            MagicalGirlHeart.specialAbility = WstlUtils.CreateSpecialAbility<MagicalGirlHeart>(rulebookName, rulebookDescription).Id;
        }
    }
    public class MagicalGirlHeart : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "The balance must be maintained. Good cannot exist without evil.";
        private readonly string altDialogue = "Good cannot exist without evil.";

        private int allyDeaths;
        private int opponentDeaths;
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return fromCombat && killer != null;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // does not increment if Magical Girl H is the killer

            if (killer != base.PlayableCard)
            {
                if (card.Slot.IsPlayerSlot)
                {
                    allyDeaths++;
                }
                else
                {
                    opponentDeaths++;
                }
            }
            yield break;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (!base.PlayableCard.Slot.IsPlayerSlot)
            {
                return !playerUpkeep;
            }
            return playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (opponentDeaths + 1 < allyDeaths)
            {
                // 2 more player card deaths than Leshy card deaths

                yield return new WaitForSeconds(0.15f);
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);

                CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    evolution.Mods.Add(cardModificationInfo);
                }
                yield return base.PlayableCard.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);

                if (!base.PlayableCard.Slot.IsPlayerSlot)
                {
                    // If this card is on Leshy's side of the board, move it to your side
                    // unless there's no available space, in which case create a copy in your hand

                    yield return new WaitForSeconds(0.25f);

                    if (base.PlayableCard.Slot.opposingSlot.Card != null)
                    {
                        yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(evolution, null, 0.25f, null);
                        yield return base.PlayableCard.Die(false);
                    }
                    else
                    {
                        base.PlayableCard.SetIsOpponentCard(opponentCard: false);
                        base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
                        yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, base.PlayableCard.Slot.opposingSlot, 0.25f);
                    }
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }
            if (allyDeaths + 1 < opponentDeaths)
            {
                // 2 more Leshy card deaths than player card deaths

                yield return new WaitForSeconds(0.15f);
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.15f);

                CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    evolution.Mods.Add(cardModificationInfo);
                }
                yield return base.PlayableCard.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);

                if (base.PlayableCard.Slot.IsPlayerSlot)
                {
                    // If this card in on your side of the board, move to Leshy's side
                    // Acts like the Angler's hook, minus the animations

                    yield return new WaitForSeconds(0.25f);
                    if (base.PlayableCard.Slot.opposingSlot.Card != null)
                    {
                        yield return Singleton<TurnManager>.Instance.Opponent.ReturnCardToQueue(base.PlayableCard.Slot.opposingSlot.Card, 0.25f);
                    }
                    base.PlayableCard.SetIsOpponentCard();
                    base.PlayableCard.transform.eulerAngles += new Vector3(0f, 0f, -180f);
                    yield return Singleton<BoardManager>.Instance.AssignCardToSlot(base.PlayableCard, base.PlayableCard.Slot.opposingSlot, 0.25f);
                    yield return new WaitForSeconds(0.25f);
                }
                yield return PlayDialogue();
            }
        }
        private IEnumerator PlayDialogue()
        {
            if (!PersistentValues.HasSeenHatredTransformation)
            {
                PersistentValues.HasSeenHatredTransformation = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(altDialogue, -0.65f, 0.4f);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
