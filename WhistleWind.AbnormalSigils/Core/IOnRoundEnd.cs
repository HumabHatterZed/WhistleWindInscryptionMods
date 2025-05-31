using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WhistleWind.AbnormalSigils.Core
{
    /// <summary>
    /// Custom trigger interface that triggers at the end of a round (in practice, at the end of the opponent's turn).
    /// Serves as a way to trigger effects even if the opponent's turn was skipped.
    /// </summary>
    public interface IOnRoundEnd
    {
        public bool RespondsToRoundEnd(bool opponentTurnSkipped);
        public IEnumerator OnRoundEnd(bool opponentTurnSkipped);
        public int RoundEndPriority(bool opponentTurnSkipped);
    }
}
