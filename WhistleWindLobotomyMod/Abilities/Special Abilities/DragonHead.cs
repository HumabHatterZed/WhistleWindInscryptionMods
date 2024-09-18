using DiskCardGame;
using System.Collections;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class DragonHead : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) =>
            otherCard != base.PlayableCard;// && base.PlayableCard.Info.name != "wstl_yinYangBody";

        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard.Info.name == "wstl_yinYangHead")
            {
                if (base.PlayableCard.Info.name == "wstl_yinYangHead")
                    base.PlayableCard.SetInfo(CardLoader.GetCardByName("wstl_yinYangHorns"));
                else
                    base.PlayableCard.SetInfo(CardLoader.GetCardByName("wstl_yinYangBody"));
                base.PlayableCard.UpdateStatsText();
            }
            yield break;
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_DragonHead()
            => DragonHead.specialAbility = AbilityHelper.CreateSpecialAbility<DragonHead>(pluginGuid, "DragonHead").Id;
    }
}
