using DiskCardGame;
using System.Collections;

namespace WhistleWindLobotomyMod.Opponents
{
    public interface IKillPlayerSequence
    {
        public bool RespondsToKillPlayerSequence();
        public IEnumerator KillPlayerSequence();
    }

    public interface IPreventInstantWin
    {
        public bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot);
    }
}
