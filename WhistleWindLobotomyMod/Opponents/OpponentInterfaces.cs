using DiskCardGame;
using InscryptionAPI.Encounters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Tango;
using WhistleWind.AbnormalSigils;
using EncounterBuilder = DiskCardGame.EncounterBuilder;

namespace WhistleWindLobotomyMod.Opponents
{
    public interface IKillPlayerSequence
    {
        public bool RespondsToKillPlayerSequence();
        public IEnumerator KillPlayerSequence();
    }
    public interface IExhaustSequence
    {
        public bool RespondsToExhaustSequence(CardDrawPiles drawPiles, PlayableCard giantOpponentCard);
        public IEnumerator ExhaustSequence(CardDrawPiles drawPiles, PlayableCard giantOpponentCard);
    }
    public interface IPreventInstantWin
    {
        public bool PreventInstantWin(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinTriggered(bool timeMachine, CardSlot triggeringSlot);
        public IEnumerator OnInstantWinPrevented(bool timeMachine, CardSlot triggeringSlot);
    }
}
