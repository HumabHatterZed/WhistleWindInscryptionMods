using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_QueenOfHatredExhausted()
        {
            const string rulebookName = "Hate B";
            const string rulebookDescription = "Transforms into a stronger form on turn's end.";
            return WstlUtils.CreateSpecialAbility<QueenOfHatredExhausted>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class QueenOfHatredExhausted : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "The monster returns to full strength.";

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Hate B");
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!base.PlayableCard.Slot.IsPlayerSlot)
            {
                return !playerTurnEnd;
            }
            return playerTurnEnd;
        }

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenHatredRecover)
            {
                PersistentValues.HasSeenHatredRecover = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
