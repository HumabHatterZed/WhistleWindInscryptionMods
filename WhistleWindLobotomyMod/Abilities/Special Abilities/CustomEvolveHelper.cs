using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class CustomEvolveHelper : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard.OpponentCard != playerTurnEnd;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // Run code if evolving from : Magical Girl D, Nothing There True, Nothing There Egg
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_magicalGirlDiamond":
                    yield return DialogueHelper.PlayDialogueEvent("KingOfGreedTransform");
                    break;
                case "wstl_nothingThereTrue":
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformTrue");
                    break;
                case "wstl_nothingThereEgg":
                    CardInfo evolution = HelperMethods.GetInfoWithMods(base.PlayableCard, "wstl_nothingThereFinal");
                    yield return base.PlayableCard.TransformIntoCard(evolution);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueHelper.PlayDialogueEvent("NothingThereTransformEgg");
                    break;
                default:
                    yield break;
            }
        }

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // Run code if evolving from : Queen of Hatred, Queen of Hatred (Tired)
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
                default:
                    yield break;
            }
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_CustomFledgling()
            => CustomEvolveHelper.specialAbility = AbilityHelper.CreateSpecialAbility<CustomEvolveHelper>(pluginGuid, "CustomEvolveHelper").Id;
    }
}
