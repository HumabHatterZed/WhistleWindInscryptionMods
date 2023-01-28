using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    // for triggering special card behaviour in Healer
    public class PlagueDoctorClass : SpecialCardBehaviour
    {
        public virtual IEnumerator TriggerBlessing()
        {
            yield break;
        }
        public virtual IEnumerator TriggerClock()
        {
            yield break;
        }
    }
}
