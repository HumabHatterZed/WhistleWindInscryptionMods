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
        private NewSpecialAbility SpecialAbility_Despair()
        {
            const string rulebookName = "Despair";
            const string rulebookDescription = "Adjacent cards take 1 less damage. Transforms when they die.";
            return WstlUtils.CreateSpecialAbility<MagicalGirlSpade>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MagicalGirlSpade : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Despair");
            }
        }
    }
}
