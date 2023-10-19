using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class StarSound : SpecialCardBehaviour, ISetupAttackSequence
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Sound of a Star";
        public const string rDesc = "If there are no cards on the opposing side that can be attacked, Blue Star strikes all slots directly.";

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.PlayableCard && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            return BoardManager.Instance.GetSlotsCopy(base.PlayableCard.OpponentCard);
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return 0;
        }
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
