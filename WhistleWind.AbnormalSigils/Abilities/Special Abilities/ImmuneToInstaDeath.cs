using WhistleWind.Core.Helpers;
using DiskCardGame;

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
            ImmuneToInstaDeath.specialAbility = AbilityHelper.CreateSpecialAbility<ImmuneToInstaDeath>(pluginGuid, ImmuneToInstaDeath.rName).Id;
        }
    }
}
