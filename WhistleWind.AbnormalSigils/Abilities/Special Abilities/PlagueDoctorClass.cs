using DiskCardGame;
using System.Collections;

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
