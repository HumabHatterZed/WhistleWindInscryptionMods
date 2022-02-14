using APIPlugin;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_Queen()
        {
            const string rulebookName = "Queen";
            const string rulebookDescription = "Freezes the hearts of opposing adjacent cards.";
            return WstlUtils.CreateSpecialAbility<SnowQueen>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class SnowQueen : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Queen");
            }

        }
    }
}
