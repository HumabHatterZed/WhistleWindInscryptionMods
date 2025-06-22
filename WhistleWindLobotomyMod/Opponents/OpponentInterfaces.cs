using DiskCardGame;
using System.Collections;

namespace WhistleWindLobotomyMod.Opponents {
    /// <summary>
    /// Custom trigger interface for when an opponent kills the player.
    /// </summary>
    public interface IKillPlayerSequence {
        public bool RespondsToKillPlayerSequence();
        public IEnumerator KillPlayerSequence();
    }

    /// <summary>
    /// Custom trigger interface for when certain effects trigger during a fight.
    /// </summary>
    public interface IPreventInstantWin {
        public bool PreventInstantWin(CardSlot triggeringSlot, InstantWinType instantWinType);
        /// <summary>
        /// Triggered if PreventInstantWin is true
        /// </summary>
        public IEnumerator OnInstantWinPrevented(CardSlot triggeringSlot, InstantWinType instantWinType);
        /// <summary>
        /// Triggered if an instant win condition is not prevented
        /// </summary>
        public IEnumerator OnInstantWinTriggered(CardSlot triggeringSlot, InstantWinType instantWinType);

        public enum InstantWinType {
            TimeMachine,
            Confession
        }
    }
}
