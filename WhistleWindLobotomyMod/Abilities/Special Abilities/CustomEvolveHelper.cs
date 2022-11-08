using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class CustomEvolveHelper : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        private static CardInfo BodyInfo => CardLoader.GetCardByName("wstl_yinYangBody");
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.PlayableCard.OpponentCard != playerUpkeep;
        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.PlayableCard.OpponentCard != playerTurnEnd;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // Run code if evolving from : Magical Girl D, Nothing There True, Nothing There Egg
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_magicalGirlDiamond":
                    yield return DialogueEventsManager.PlayDialogueEvent("KingOfGreedTransform");
                    break;
                case "wstl_nothingThereTrue":
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    yield return DialogueEventsManager.PlayDialogueEvent("NothingThereTransformTrue");
                    break;
                case "wstl_nothingThereEgg":
                    CardInfo evolution = AbnormalCustomMethods.GetInfoWithMods(base.PlayableCard, "wstl_nothingThereFinal");
                    yield return base.PlayableCard.TransformIntoCard(evolution);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueEventsManager.PlayDialogueEvent("NothingThereTransformEgg");
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
                    CardInfo evolutionTired = AbnormalCustomMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatredTired");
                    yield return base.PlayableCard.TransformIntoCard(evolutionTired);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueEventsManager.PlayDialogueEvent("QueenOfHatredExhaust");
                    break;
                case "wstl_queenOfHatredTired":
                    CardInfo evolutionRecovered = AbnormalCustomMethods.GetInfoWithMods(base.PlayableCard, "wstl_queenOfHatred");
                    yield return base.PlayableCard.TransformIntoCard(evolutionRecovered);
                    yield return new WaitForSeconds(0.5f);
                    yield return DialogueEventsManager.PlayDialogueEvent("QueenOfHatredRecover");
                    break;
                default:
                    yield break;
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (base.Card != null && base.PlayableCard.Info.name.Equals("wstl_yinYangHead"))
                return otherCard != base.Card && otherCard.Info.name == "wstl_yinYangHead";

            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            // change to Body when a Head is played
            base.PlayableCard.SetInfo(BodyInfo);
            base.PlayableCard.UpdateStatsText();
            yield break;
        }
    }
    public partial class LobotomyPlugin
    {
        private void SpecialAbility_CustomFledgling()
        {
            const string rulebookName = "CustomEvolveHelper";
            CustomEvolveHelper.specialAbility = LobotomyAbilityHelper.CreateSpecialAbility<CustomEvolveHelper>(rulebookName).Id;
        }
    }
}
