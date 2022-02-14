using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_Greed()
        {
            const string rulebookName = "Greed";
            const string rulebookDescription = "Transforms into a stronger form on turn's end.";
            return WstlUtils.CreateSpecialAbility<MagicalGirlDiamond>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, overrideDesc: true);
        }
    }
    public class MagicalGirlDiamond : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Greed");
            }
        }
    }
}
