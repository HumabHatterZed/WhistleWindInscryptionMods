using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWind.LobotomyMod.Core.Helpers;

namespace WhistleWind.LobotomyMod
{
    public class StarSound : SpecialCardBehaviour, IGetOpposingSlots
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Sound of a Star";
        public static readonly string rDesc = "If there are no cards on the opposing side that can be attacked, Blue Star strikes all slots directly.";

        public bool RespondsToGetOpposingSlots() => CheckOpposingSlots();

        public List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            return HelperMethods.GetSlotsCopy(!base.PlayableCard.OpponentCard);
        }

        public bool RemoveDefaultAttackSlot() => CheckOpposingSlots();

        private bool CheckOpposingSlots()
        {
            List<CardSlot> opposingSlots = HelperMethods.GetSlotsCopy(!base.PlayableCard.OpponentCard);
            opposingSlots.RemoveAll(s => s.Card != null);
            return opposingSlots.Count > 0;
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
        {
            RulebookEntryStarSound.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryStarSound>(StarSound.rName, StarSound.rDesc).Id;
        }
        private void SpecialAbility_StarSound()
        {
            StarSound.specialAbility = AbilityHelper.CreateSpecialAbility<StarSound>(pluginGuid, StarSound.rName).Id;
        }
    }
}
