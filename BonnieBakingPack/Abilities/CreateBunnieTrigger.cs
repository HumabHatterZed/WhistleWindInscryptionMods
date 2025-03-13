using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BonniesBakingPack
{
    public class CreateBunnieTrigger : NonCardTriggerReceiver
    {
        private bool opponent;
        private int turnCreated;
        private List<CardModificationInfo> cardmods;

        public override bool TriggerBeforeCards => true;
        public void Initialise(PlayableCard parent)
        {
            opponent = parent.OpponentCard;
            cardmods = new(parent.Info.Mods);
            turnCreated = TurnManager.Instance.TurnNumber;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep != opponent && TurnManager.Instance.TurnNumber > turnCreated;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            string infoName;
            if (SaveManager.SaveFile.IsPart3)
            {
                infoName = "bbp_act3_bunnie";
            }
            else if (SaveManager.SaveFile.IsGrimora)
            {
                infoName = "bbp_grimora_bunnie";
            }
            else
            {
                infoName = "bbp_act1_bunnie";
            }

            CardInfo cardInfo = CardLoader.GetCardByName(infoName);
            cardInfo.Mods = cardmods;
            if (opponent)
            {
                CardSlot queue = BoardManager.Instance.GetOpenSlots(false).FirstOrDefault();
                if (queue == null)
                {
                    queue = BoardManager.Instance.OpponentSlotsCopy[SeededRandom.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count, base.GetRandomSeed())];
                    PlayableCard card = TurnManager.Instance.Opponent.Queue.Find(x => x.QueuedSlot == queue);
                    card.ExitBoard(0.2f, new Vector3(-1f, -2f, 5f));
                    TurnManager.Instance.Opponent.Queue.Remove(card);
                }
                yield return TurnManager.Instance.Opponent.QueueCard(cardInfo, queue);
            }
            else
            {
                ViewManager.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.2f);
                yield return CardSpawner.Instance.SpawnCardToHand(cardInfo);
            }

            if (!ProgressionData.IntroducedCard(cardInfo))
            {
                yield return new WaitForSeconds(0.2f);
                if (infoName.Equals("bbp_grimora_bunnie"))
                    yield return TextDisplayer.Instance.ShowThenClear(cardInfo.description, 3f);
                else
                    yield return TextDisplayer.Instance.ShowUntilInput(cardInfo.description);

                ProgressionData.SetCardIntroduced(cardInfo);

            }
            Destroy();
        }
    }

}