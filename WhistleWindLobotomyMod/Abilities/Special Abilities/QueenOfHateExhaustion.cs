using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class QueenOfHateExhaustion : SpecialCardBehaviour, IOnPostSlotAttackSequence
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.PlayableCard && base.PlayableCard.Info.name == "wstl_queenOfHatred";
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.PlayableCard.OpponentCard;

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            exhaustedThisTurn = true;
            CardInfo evolutionTired = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatredTired");
            yield return base.PlayableCard.TransformIntoCard(evolutionTired);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("QueenOfHatredExhaust");
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (exhaustedThisTurn)
            {
                exhaustedThisTurn = false;
                yield break;
            }
            CardInfo evolutionRecovered = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatred");
            yield return base.PlayableCard.TransformIntoCard(evolutionRecovered);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("QueenOfHatredRecover");
        }
        private bool exhaustedThisTurn = false;
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_QueenOfHatredExhaustion()
            => QueenOfHateExhaustion.specialAbility = AbilityHelper.CreateSpecialAbility<QueenOfHateExhaustion>(pluginGuid, "QueenOfHateExhaustion").Id;
    }
}
