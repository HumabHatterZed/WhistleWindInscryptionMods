using DiskCardGame;
using System.Collections;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Custom trigger interface for when an opponent kills the player.
    /// </summary>
    public interface IKillPlayerSequence
    {
        public bool RespondsToKillPlayerSequence();
        public IEnumerator KillPlayerSequence();
    }

    /// <summary>
    /// Custom trigger interface for when certain effects trigger during a fight.
    /// </summary>
    public interface IPreventInstantWin
    {
        public bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot);
    }
}
