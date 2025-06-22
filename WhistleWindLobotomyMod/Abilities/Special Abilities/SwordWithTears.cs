using DiskCardGame;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class SwordWithTears : TransformOnAdjacentDeath {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "The Sword Sharpened with Tears";
        public const string rDesc = "Knight of Despair and Servant of Wrath will transform when an adjacent card dies.";

        public override string CardToTransformInto => SaveManager.SaveFile.IsPart1 ? Cards.knightOfDespair : Cards.knightOfDespairPixel;
        public override string PostEvolveDialogueId => "KnightOfDespairTransform";
    }
    public class RulebookEntrySwordWithTears : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities {
        private static void Rulebook_SwordWithTears()
            => RulebookEntrySwordWithTears.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySwordWithTears>(SwordWithTears.rName, SwordWithTears.rDesc).Id;
        private static void AddSpecial_SwordWithTears()
            => SwordWithTears.specialAbility = AbilityHelper.CreateSpecialAbility<SwordWithTears>(LobotomyPlugin.pluginGuid, SwordWithTears.rName).Id;
    }
}
