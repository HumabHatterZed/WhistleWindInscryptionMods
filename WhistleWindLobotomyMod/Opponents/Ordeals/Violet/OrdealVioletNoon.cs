using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Appears in R1
    /// Difficulty range: (5 - 9) +[0,2]
    /// 
    /// Begin with short version of Dawn encounter then do Noon proper
    /// </summary>
    public class OrdealVioletNoon : OrdealVioletDawn
    {
        private CardSlot[] loveSlots = null;

        public override IEnumerator OpponentUpkeep()
        {
            if (loveSlots == null || Opponent.NumTurnsTaken < Opponent.TurnPlan.Count + 1)
                yield break;

            CleanupTargetIcons();

            loveSlots[0].Card?.Anim.PlayDeathAnimation();
            loveSlots[1].Card?.Anim.PlayDeathAnimation();

            if (loveSlots[0].Card != null) yield return loveSlots[0].Card.Die(false);

            if (loveSlots[1].Card != null) yield return loveSlots[1].Card.Die(false);

            CameraEffects.Instance.Shake(1f, 0.75f);
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName(Cards.grantUsLove), loveSlots[0]);
            yield return new WaitForSeconds(0.2f);
            AudioController.Instance.PlaySound3D("map_slam", MixerGroup.TableObjectsSFX, Singleton<BoardManager>.Instance.transform.position);
            yield return new WaitForSeconds(1f);
            loveSlots = null;
        }
        public override bool PlayerHasDefeatedOrdeal() => loveSlots == null && base.PlayerHasDefeatedOrdeal();

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // if the next turn is the final turn in the turn plan, set up Grant Us Love
            if (loveSlots == null && Opponent.NumTurnsTaken == Opponent.TurnPlan.Count)
            {
                int slotIndex = UnityEngine.Random.Range(0, BoardManager.Instance.OpponentSlotsCopy.Count - 1);
                loveSlots = new CardSlot[] { BoardManager.Instance.OpponentSlotsCopy[slotIndex], BoardManager.Instance.OpponentSlotsCopy[slotIndex + 1] };
                CreateTargetIcon(loveSlots[0], GameColors.Instance.darkPurple);
                CreateTargetIcon(loveSlots[1], GameColors.Instance.darkPurple);
            }
            else
            {
                yield return base.OnTurnEnd(playerTurnEnd);
            }
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // noon of violet only ends when Grant Us Love dies
            if (card.Info.name != Cards.grantUsLove)
                yield break;

            yield return base.OnOtherCardDie(card, deathSlot, fromCombat, killer);
        }

        public override int ConstructOrdealBlueprint(EncounterData encounterData, int baseDifficulty)
        {
            targetIconPrefab = AssetManager.warningTargetPrefab;
            return 1 + base.ConstructOrdealBlueprint(encounterData, baseDifficulty);
        }
    }
}