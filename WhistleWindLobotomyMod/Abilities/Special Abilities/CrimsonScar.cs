using DiskCardGame;
using InscryptionAPI.Dialogue;
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
        public const string rDesc = "Big and Will Be Bad Wolf and Red Hooded Mercenary will gain 1 Power when their counterpart is played on the board. While they're on the board, target them exclusively.";

        private const string WolfName = "wstl_willBeBadWolf";
        private const string HoodName = "wstl_redHoodedMercenary";

        public bool Enraged = false;
        private bool GrudgeExists(PlayableCard thisCard, PlayableCard otherCard)
        {
            if (otherCard == null)
                return false;

            if (thisCard.Info.name == HoodName && otherCard.Info.name == WolfName)
                return true;

            if (thisCard.Info.name == WolfName && otherCard.Info.name == HoodName)
                return true;

            return false;
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => !Enraged && GrudgeExists(base.PlayableCard, otherCard);
        public override bool RespondsToResolveOnBoard()
        {
            return !Enraged && GrudgeExists(base.PlayableCard, BoardManager.Instance.AllSlotsCopy.Find(
                x => x.Card?.Info.name == (base.PlayableCard.Info.name == HoodName ? WolfName : HoodName))?
                .Card);
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            Enraged = true;
            base.PlayableCard.Anim.StrongNegationEffect();
            base.PlayableCard.AddTemporaryMod(new(1, 0) { fromTotem = true });
            if (!SaveManager.SaveFile.IsPart2)
                base.PlayableCard.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
            yield return new WaitForSeconds(0.4f);

            if (base.PlayableCard.Info.name == HoodName)
                yield return DialogueManager.PlayDialogueEventSafe("CrimsonScarHood", TextDisplayer.MessageAdvanceMode.Input);
            else
                yield return DialogueManager.PlayDialogueEventSafe("CrimsonScarWolf", TextDisplayer.MessageAdvanceMode.Input);
        }
        public override IEnumerator OnResolveOnBoard()
        {
            Enraged = true;
            base.PlayableCard.Anim.StrongNegationEffect();
            base.PlayableCard.AddTemporaryMod(new(1, 0) { fromTotem = true });
            if (!SaveManager.SaveFile.IsPart2)
                base.PlayableCard.StatsLayer.SetEmissionColor(GameColors.Instance.glowRed);
            yield return new WaitForSeconds(0.4f);

            if (base.PlayableCard.Info.name == HoodName)
                yield return DialogueManager.PlayDialogueEventSafe("CrimsonScarHood", TextDisplayer.MessageAdvanceMode.Input);
            else
                yield return DialogueManager.PlayDialogueEventSafe("CrimsonScarWolf", TextDisplayer.MessageAdvanceMode.Input);
        }

        public bool RespondsToModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return card == base.PlayableCard && modType == OpposingSlotTriggerPriority.PostAdditionModification;
        }

        public List<CardSlot> CollectModifyAttackSlots(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, ref int attackCount, ref bool didRemoveDefaultSlot)
        {
            if (Enraged)
            {
                CardSlot slot = BoardManager.Instance.AllSlotsCopy.Find(
                    x => x.Card?.Info.name == (base.PlayableCard.Info.name == HoodName ? WolfName : HoodName));

                if (slot != null)
                {
                    for (int i = 0; i < currentSlots.Count; i++)
                        currentSlots[i] = slot;
                }
            }
            return currentSlots;
        }

        public int GetTriggerPriority(PlayableCard card, OpposingSlotTriggerPriority modType, List<CardSlot> originalSlots, List<CardSlot> currentSlots, int attackCount, bool didRemoveDefaultSlot)
        {
            return 0;
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
