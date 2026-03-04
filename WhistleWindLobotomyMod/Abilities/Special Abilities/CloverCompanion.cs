using DiskCardGame;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public class CloverCompanion : TransformOnAdjacentDeath {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Clover Companion";
        public const string rDesc = "Servant of Wrath will transform when an adjacent card dies.";

        public override string CardToTransformInto => SaveManager.SaveFile.IsPart1 ? Cards.servantOfWrath : Cards.servantOfWrathPixel;
        public override string PostEvolveDialogueId => "ServantOfWrathTransform";
        public override int NumDeathsTillEvolve => 2;
    }
    public class RulebookEntryCloverCompanion : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_CloverCompanion()
            => RulebookEntryCloverCompanion.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCloverCompanion>(CloverCompanion.rName, CloverCompanion.rDesc).Id;
        private static void AddSpecial_CloverCompanion()
            => CloverCompanion.specialAbility = AbilityHelper.CreateSpecialAbility<CloverCompanion>(LobotomyPlugin.pluginGuid, CloverCompanion.rName).Id;
    }
}
