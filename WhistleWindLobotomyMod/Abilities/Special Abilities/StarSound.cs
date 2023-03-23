using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class StarSound : SpecialCardBehaviour, IGetOpposingSlots
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Sound of a Star";
        public static readonly string rDesc = "If there are no cards on the opposing side that can be attacked, Blue Star strikes all slots directly.";

        public bool RemoveDefaultAttackSlot() => AttackAllOpposingSlots();
        public bool RespondsToGetOpposingSlots() => AttackAllOpposingSlots();

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            return HelperMethods.GetSlotsCopy(!base.PlayableCard.OpponentCard);
        }

        private bool AttackAllOpposingSlots() => base.PlayableCard.HasAbility(Ability.AllStrike);
    }
    public class RulebookEntryStarSound : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_StarSound()
            => RulebookEntryStarSound.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryStarSound>(StarSound.rName, StarSound.rDesc).Id;
        private void SpecialAbility_StarSound()
            => StarSound.specialAbility = AbilityHelper.CreateSpecialAbility<StarSound>(pluginGuid, StarSound.rName).Id;
    }
}
