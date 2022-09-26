using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Evolve()
        {
            const string rulebookName = "SpecialAbilityFledgling";
            const string rulebookDescription = "Special ability version of Fledgling for certain cards.";
            CustomFledgling.specialAbility = AbilityHelper.CreateSpecialAbility<CustomFledgling>(rulebookName, rulebookDescription).Id;
        }
    }
    public class CustomFledgling : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string hateADialogue = "A formidable attack. Shame it has left her too tired to defend herself.";
        private readonly string hateBDialogue = "The monster returns to full strength.";
        private readonly string greedDialogue = "Desire unfulfilled, the koi continues for Eden.";
        private readonly string nothingTrueDialogue = "What is it doing?";
        private readonly string nothingEggDialogue = "It seems to be trying to mimic you. 'Trying' is the key word.";

        private readonly CardInfo bodyInfo = CardLoader.GetCardByName("wstl_yinYangBody");
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.PlayableCard.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            // Run code if evolving from : Magical Girl D, Nothing There True, Nothing There Egg
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_magicalGirlDiamond":
                    if (!WstlSaveManager.HasSeenGreedTransformation)
                    {
                        WstlSaveManager.HasSeenGreedTransformation = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(greedDialogue, -0.65f, 0.4f);
                    }
                    break;
                case "wstl_nothingThereTrue":
                    base.Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                    if (!WstlSaveManager.HasSeenNothingTransformationTrue)
                    {
                        WstlSaveManager.HasSeenNothingTransformationTrue = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(nothingTrueDialogue, -0.65f, 0.4f);
                    }
                    break;
                case "wstl_nothingThereEgg":
                    CardInfo evolution = CardLoader.GetCardByName("wstl_nothingThereFinal");

                    foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                    {
                        CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                        if (cardModificationInfo.HasAbility(Ability.Evolve))
                        {
                            cardModificationInfo.abilities.Remove(Ability.Evolve);
                        }
                        evolution.Mods.Add(cardModificationInfo);
                    }
                    yield return base.PlayableCard.TransformIntoCard(evolution);
                    yield return new WaitForSeconds(0.5f);
                    if (!WstlSaveManager.HasSeenNothingTransformationEgg)
                    {
                        WstlSaveManager.HasSeenNothingTransformationEgg = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(nothingEggDialogue, -0.65f, 0.4f, Emotion.Curious);
                    }
                    yield return new WaitForSeconds(0.25f);
                    break;
                default:
                    yield break;
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.PlayableCard.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            // Run code if evolving from : Queen of Hatred, Queen of Hatred (Tired)
            switch (base.PlayableCard.Info.name)
            {
                case "wstl_queenOfHatred":
                    CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatredTired");

                    foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                    {
                        CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                        evolution.Mods.Add(cardModificationInfo);
                    }
                    yield return base.PlayableCard.TransformIntoCard(evolution);
                    yield return new WaitForSeconds(0.5f);
                    if (!WstlSaveManager.HasSeenHatredTireOut)
                    {
                        WstlSaveManager.HasSeenHatredTireOut = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hateADialogue, -0.65f, 0.4f);
                    }
                    yield return new WaitForSeconds(0.25f);
                    break;
                case "wstl_queenOfHatredTired":
                    CardInfo evolution1 = CardLoader.GetCardByName("wstl_queenOfHatred");

                    foreach (CardModificationInfo item in base.PlayableCard.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                    {
                        // Adds merged sigils
                        CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                        cardModificationInfo.fromCardMerge = true;
                        evolution1.Mods.Add(cardModificationInfo);
                    }
                    yield return base.PlayableCard.TransformIntoCard(evolution1);
                    yield return new WaitForSeconds(0.5f);
                    if (!WstlSaveManager.HasSeenHatredRecover)
                    {
                        WstlSaveManager.HasSeenHatredRecover = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hateBDialogue, -0.65f, 0.4f);
                    }
                    yield return new WaitForSeconds(0.25f);
                    break;
                default:
                    yield break;
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (base.Card != null && base.PlayableCard.Info.name.Equals("wstl_yinYangHead"))
            {
                return otherCard != base.Card && otherCard.Info.name == "wstl_yinYangHead";
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            // change to Body when a Head is played
            base.PlayableCard.SetInfo(bodyInfo);
            base.PlayableCard.UpdateStatsText();
            yield break;
        }
    }
}
