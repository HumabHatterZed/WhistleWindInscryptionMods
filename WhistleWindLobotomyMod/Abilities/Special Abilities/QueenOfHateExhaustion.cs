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

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.PlayableCard && base.PlayableCard.Info.baseAttack != 0;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd != base.PlayableCard.OpponentCard;

        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            exhaustedThisTurn = true;
            CardInfo evolutionTired = HelperMethods.GetInfoWithMods(base.PlayableCard, SaveManager.SaveFile.IsPart1 ? Cards.queenOfHatredTired : Cards.queenOfHatredTiredPixel);
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
            CardInfo evolutionRecovered = HelperMethods.GetInfoWithMods(base.PlayableCard, SaveManager.SaveFile.IsPart1 ? Cards.queenOfHatred : Cards.queenOfHatredPixel);
            yield return base.PlayableCard.TransformIntoCard(evolutionRecovered);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("QueenOfHatredRecover");
        }
        private bool exhaustedThisTurn = false;
    }
    public partial class Abilities
    {
        private static void AddSpecial_QueenOfHatredExhaustion()
            => QueenOfHateExhaustion.specialAbility = AbilityHelper.CreateSpecialAbility<QueenOfHateExhaustion>(LobotomyPlugin.pluginGuid, "QueenOfHateExhaustion").Id;
    }
}
