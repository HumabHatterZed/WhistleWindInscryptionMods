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

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.PlayableCard;
        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_queenOfHatred":
                    CardInfo evolutionTired = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatredTired");
                    yield return base.PlayableCard.TransformIntoCard(evolutionTired);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("QueenOfHatredExhaust");
                    break;
                case "wstl_queenOfHatredTired":
                    CardInfo evolutionRecovered = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatred");
                    yield return base.PlayableCard.TransformIntoCard(evolutionRecovered);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("QueenOfHatredRecover");
                    break;
            }
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_CustomFledgling()
            => QueenOfHateExhaustion.specialAbility = AbilityHelper.CreateSpecialAbility<QueenOfHateExhaustion>(pluginGuid, "QueenOfHateExhaustion").Id;
    }
}
