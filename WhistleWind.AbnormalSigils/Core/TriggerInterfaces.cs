using System.Collections;

namespace WhistleWind.AbnormalSigils.Core {
    /// <summary>
    /// Trigger that's called before TurnEnd.
    /// </summary>
    public interface IOnSnapshotTakenStoreInteger {
        public int RetrieveIntegerToStore();
        public IEnumerator OnReceiveInteger(int value);
    }
    /// <summary>
    /// Trigger that's called before TurnEnd.
    /// </summary>
    public interface IPreTurnEnd {
        public bool RespondsToPreTurnEnd(bool playerTurnEnd);
        public IEnumerator OnPreTurnEnd(bool playerTurnEnd);
    }

    /// <summary>
    /// Trigger intended for non-card receivers, or cards with effects that should trigger even if the opponent's turn is skipped.
    /// </summary>
    public interface IOpponentTurnEnd {
        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped);
        public IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped);
        public int OpponentTurnEndPriority(bool opponentTurnSkipped);
    }

    /// <summary>
    /// Trigger intended for non-card receivers, triggers at the very end of the player's turn, after all card triggers.
    /// </summary>
    public interface IPlayerTurnEnd {
        public bool RespondsToPlayerTurnEnd();
        public IEnumerator OnPlayerTurnEnd();
        public int PlayerTurnEndPriority();
    }
}
