using DiskCardGame;
using InscryptionAPI;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class CrimsonScar : SpecialCardBehaviour, ISetupAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Crimson Scar";
        public const string rDesc = "When Big and Will Be Bad Wolf is on the board, Little Red Riding Hooded Mercenary gains 2 Power and targets them.";
        private string CardName => base.PlayableCard.Info.name;
        private const string WolfName = "wstl_willBeBadWolf";
        private const string HoodName = "wstl_redHoodedMercenary";
        private bool GrudgeExists(PlayableCard otherCard)
        {
            if (otherCard == null)
                return false;

            if (base.PlayableCard.name == HoodName && otherCard.name == WolfName)
                return true;

            if (base.PlayableCard.name == WolfName && otherCard.name == HoodName)
                return true;

            return false;
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => GrudgeExists(otherCard);
        public override bool RespondsToResolveOnBoard()
        {
            if (base.PlayableCard.name == HoodName)
                return GrudgeExists(BoardManager.Instance.AllSlotsCopy.Find(x => x.Card?.name == WolfName).Card);
            else
                return GrudgeExists(BoardManager.Instance.AllSlotsCopy.Find(x => x.Card?.name == HoodName).Card);

            return false;
        }

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.Card && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            string nameToFind;
            if (base.PlayableCard.name == HoodName)
                nameToFind = WolfName;
            else
                nameToFind = HoodName;

            CardSlot slot = BoardManager.Instance.AllSlotsCopy.Find(x => x.Card?.name == nameToFind);
            for (int i = 0; i < currentSlots.Count; i++)
                currentSlots[i] = slot;

            return currentSlots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return int.MinValue;
        }
    }
    public class RulebookEntryCrimsonScar : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_CrimsonScar()
            => RulebookEntryCrimsonScar.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCrimsonScar>(CrimsonScar.rName, CrimsonScar.rDesc).Id;
        private void SpecialAbility_CrimsonScar()
            => CrimsonScar.specialAbility = AbilityHelper.CreateSpecialAbility<CrimsonScar>(pluginGuid, CrimsonScar.rName).Id;
    }
}
