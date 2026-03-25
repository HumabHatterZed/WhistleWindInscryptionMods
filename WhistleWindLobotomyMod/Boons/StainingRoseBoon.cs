using DiskCardGame;
using InscryptionAPI.Boons;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWindLobotomyMod {
    public class StainingRoseBoon : BoonBehaviour {
        public override bool RespondsToPostBoonActivation() => true;
        public override IEnumerator OnPostBoonActivation() {
            return base.OnPostBoonActivation();
        }
        //public const string STAINING_ROSE_DRY = "Staining Rose (Dry)";
        //public const string STAINING_ROSE_WET = "Staining Rose (Wet)";
        //public const string STAINING_ROSE_DRENCHED = "Staining Rose (Drenched)";
        //public const string STAINING_ROSE_DRAINED = "Staining Rose (Drained)";
        //public override bool RespondsToTurnEnd(bool playerTurnEnd) {
        //    // trigger on the player's turn end and when the rose is dry or wet
        //    return playerTurnEnd && GetStainingRoseStage() < 2 && LifeManager.Instance.Balance < 0;
        //}
        //public override bool RespondsToUpkeep(bool playerUpkeep) {
        //    // trigger on the player's upkeep and when the rose has been drenched
        //    return playerUpkeep && GetStainingRoseStage() == 3;
        //}
        //public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
        //    List<PlayableCard> cardsToAffect = BoardManager.Instance.GetPlayerCards();
        //    // if this is the weak variant of the curse, don't give Paper Rose to cards that already have it
        //    if (GetStainingRoseStage() == 1) {
        //        cardsToAffect.RemoveAll(x => x.HasStatusEffect<PaperRose>());
        //        yield return BoonsHandler.Instance.PlayBoonAnimation(Boons.RoseCurseWeak);
        //    }
        //    else {
        //        yield return BoonsHandler.Instance.PlayBoonAnimation(Boons.RoseCurse);
        //    }

        //    foreach (PlayableCard card in cardsToAffect) {
        //        yield return card.AddStatusEffect<PaperRose>(1, true);
        //    }
        //}
        //public override IEnumerator OnUpkeep(bool playerUpkeep) {
        //    yield return BoonsHandler.Instance.PlayBoonAnimation(Boons.RoseBoon);
        //    yield return LifeManager.Instance.ShowDamageSequence(1, 1, false);
        //}

        //public int GetStainingRoseStage() {
        //    if (ItemsManager.Instance.SaveDataItemsList.Contains(STAINING_ROSE_DRY)) {
        //        return 0;
        //    }
        //    if (ItemsManager.Instance.SaveDataItemsList.Contains(STAINING_ROSE_WET)) {
        //        return 1;
        //    }
        //    if (ItemsManager.Instance.SaveDataItemsList.Contains(STAINING_ROSE_DRENCHED)) {
        //        return 2;
        //    }
        //    if (ItemsManager.Instance.SaveDataItemsList.Contains(STAINING_ROSE_DRAINED)) {
        //        return 3;
        //    }
        //    return -1;
        //}
    }
}
