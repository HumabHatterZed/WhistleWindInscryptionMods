using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WhistleWind.AbnormalSigils.Core
{
    public interface IOnRoundEnd
    {
        public bool RespondsToRoundEnd(bool opponentTurnSkipped);
        public IEnumerator OnRoundEnd(bool opponentTurnSkipped);
        public int RoundEndPriority(bool opponentTurnSkipped);
    }
}
