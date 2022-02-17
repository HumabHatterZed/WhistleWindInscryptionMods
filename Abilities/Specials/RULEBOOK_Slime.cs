using APIPlugin;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_Slime()
        {
            const string rulebookName = "Slime";
            const string rulebookDescription = "Absorbs adjacent Slimes when Health is low.";
            return WstlUtils.CreateSpecialAbility<MeltedLove>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MeltedLove : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Slime");
            }

        }
    }
}
