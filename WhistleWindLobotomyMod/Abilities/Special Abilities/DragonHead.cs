using DiskCardGame;
using System.Collections;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class DragonHead : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => otherCard != base.PlayableCard;
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard.Info.name == Cards.yinYangHead)
            {
                if (base.PlayableCard.Info.name == Cards.yinYangHead)
                    base.PlayableCard.SetInfo(CardLoader.GetCardByName(Cards.yinYangHorns));
                else
                    base.PlayableCard.SetInfo(CardLoader.GetCardByName(Cards.yinYangBody));
                base.PlayableCard.UpdateStatsText();
            }
            yield break;
        }
    }
    public partial class Abilities
    {
        private static void AddSpecial_DragonHead()
            => DragonHead.specialAbility = AbilityHelper.CreateSpecialAbility<DragonHead>(LobotomyPlugin.pluginGuid, "DragonHead").Id;
    }
}
