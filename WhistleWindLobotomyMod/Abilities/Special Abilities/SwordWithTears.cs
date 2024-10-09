using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class SwordWithTears : TransformOnAdjacentDeath
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "The Sword Sharpened with Tears";
        public const string rDesc = "Knight of Despair and Servant of Wrath will transform when an adjacent card dies.";

        public override string CardToTransformInto => "wstl_knightOfDespair";
        public override string PostEvolveDialogueId => "KnightOfDespairTransform";
    }
    public class RulebookEntrySwordWithTears : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_SwordWithTears()
            => RulebookEntrySwordWithTears.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySwordWithTears>(SwordWithTears.rName, SwordWithTears.rDesc).Id;
        private void SpecialAbility_SwordWithTears()
            => SwordWithTears.specialAbility = AbilityHelper.CreateSpecialAbility<SwordWithTears>(pluginGuid, SwordWithTears.rName).Id;
    }
}
