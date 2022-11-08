using DiskCardGame;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public class ImmuneToInstaDeath : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "ImmuneToInstaDeath";
    }
    public partial class AbnormalPlugin
    {
        private void SpecialAbility_ImmuneToInstaDeath()
        {
            ImmuneToInstaDeath.specialAbility = AbnormalAbilityHelper.CreateSpecialAbility<ImmuneToInstaDeath>(ImmuneToInstaDeath.rName).Id;
        }
    }
}
