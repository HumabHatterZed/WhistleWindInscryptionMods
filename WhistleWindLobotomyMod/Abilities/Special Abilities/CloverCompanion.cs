using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class CloverCompanion : TransformOnAdjacentDeath
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Clover Companion";
        public const string rDesc = "Servant of Wrath will transform when an adjacent card dies.";

        public override string CardToTransformInto => "wstl_servantOfWrath";
        public override string PostEvolveDialogueId => "ServantOfWrathTransform";
    }
    public class RulebookEntryCloverCompanion : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_CloverCompanion()
            => RulebookEntryCloverCompanion.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCloverCompanion>(CloverCompanion.rName, CloverCompanion.rDesc).Id;
        private void SpecialAbility_CloverCompanion()
            => CloverCompanion.specialAbility = AbilityHelper.CreateSpecialAbility<CloverCompanion>(pluginGuid, CloverCompanion.rName).Id;
    }
}
