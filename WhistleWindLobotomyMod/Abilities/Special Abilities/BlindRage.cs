using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using InscryptionCommunityPatch.Card;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class BlindRage : SpecialCardBehaviour, ISetupAttackSequence
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Blind Rage";
        public const string rDesc = "Servant of Wrath will target a random space on the board, prioritising occupied spaces.";

        private List<CardSlot> GetRandomOccupiedSlot()
        {
            List<CardSlot> retval = new();
            List<CardSlot> allSlots = Singleton<BoardManager>.Instance.AllSlotsCopy;
            allSlots.Remove(base.PlayableCard.Slot);

            if (allSlots.Exists(x => x.Card != null))
                allSlots.RemoveAll(x => x.Card == null);

            int randomSeed = base.GetRandomSeed();
            CardSlot slotToAttack = allSlots[SeededRandom.Range(0, allSlots.Count, randomSeed++)];

            retval.Add(slotToAttack);
            if (base.PlayableCard.HasAbility(Ability.SplitStrike))
            {
                retval.AddRange(Singleton<BoardManager>.Instance.GetAdjacentSlots(slotToAttack));
                retval.Remove(slotToAttack);
            }
            if (base.PlayableCard.HasTriStrike())
            {
                retval.AddRange(Singleton<BoardManager>.Instance.GetAdjacentSlots(slotToAttack));
                if (!retval.Contains(slotToAttack))
                    retval.Add(slotToAttack);
            }
            if (base.PlayableCard.HasAbility(Ability.DoubleStrike))
                retval.Add(slotToAttack);

            return retval;
        }

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.PlayableCard && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            return GetRandomOccupiedSlot();
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return 0; // I have no clue what this does, I think it's used for sorting the order cards are attacked?
        }
    }
    public class RulebookEntryBlindRage : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_BlindRage()
            => RulebookEntryBlindRage.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryBlindRage>(BlindRage.rName, BlindRage.rDesc).Id;
        private void SpecialAbility_BlindRage()
            => BlindRage.specialAbility = AbilityHelper.CreateSpecialAbility<BlindRage>(pluginGuid, BlindRage.rName).Id;
    }

    [HarmonyPatch]
    internal class BlindRagePatch
    {
        [HarmonyPostfix, HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSlot))]
        private static IEnumerator ForceAttack(IEnumerator enumerator, CombatPhaseManager __instance, CardSlot attackingSlot, CardSlot opposingSlot)
        {
            if (attackingSlot.Card != null && attackingSlot.Card.HasSpecialAbility(BlindRage.specialAbility))
            {
                Part1SniperVisualizer component = __instance.GetComponent<Part1SniperVisualizer>() ?? __instance.gameObject.AddComponent<Part1SniperVisualizer>();

                __instance.VisualizeConfirmSniperAbility(opposingSlot);
                component?.VisualizeConfirmSniperAbility(opposingSlot);
            }

            yield return enumerator;

            if (attackingSlot.Card != null && attackingSlot.Card.HasSpecialAbility(BlindRage.specialAbility))
            {
                Part1SniperVisualizer component = __instance.GetComponent<Part1SniperVisualizer>() ?? __instance.gameObject.AddComponent<Part1SniperVisualizer>();

;                __instance.VisualizeClearSniperAbility();
                component?.VisualizeClearSniperAbility();
            }
        }
    }
}
