using DiskCardGame;
using WhistleWind.Core.Helpers;

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
            ImmuneToInstaDeath.specialAbility = AbilityBuilder.CreateSpecialAbility<ImmuneToInstaDeath>(pluginGuid, ImmuneToInstaDeath.rName).Id;
        }
    }
}
