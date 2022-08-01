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
            SpecialAbilityFledgling.specialAbility = AbilityHelper.CreateSpecialAbility<SpecialAbilityFledgling>(rulebookName, rulebookDescription).Id;
        }
    }
    public class SpecialAbilityFledgling : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private bool IsHateA => base.PlayableCard.Info.name.Equals("wstl_queenOfHatred");
        private bool IsHateB => base.PlayableCard.Info.name.Equals("wstl_queenofHatredTired");
        private bool IsGreed => base.PlayableCard.Info.name.Equals("wstl_magicalgirldiamond");
        private bool IsNothingTrue => base.PlayableCard.Info.name.Equals("wstl_nothingThereTrue");
        private bool IsNothingEgg => base.PlayableCard.Info.name.Equals("wstl_nothingThereEgg");

        private bool IsDragonHead => base.PlayableCard.Info.name.Equals("wstl_yinYangHead");

        private readonly string hateADialogue = "A formidable attack. Shame it has left her too tired to defend herself.";
        private readonly string hateBDialogue = "The monster returns to full strength.";
        private readonly string greedDialogue = "Desire unfulfilled, the koi continues for Eden.";
        private readonly string nothingTrueDialogue = "What is it doing?";
        private readonly string nothingEggDialogue = "It seems to be trying to mimic you. 'Trying' is the key word.";

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (IsGreed || IsNothingTrue || IsNothingEgg)
            {
                if (!base.PlayableCard.Slot.IsPlayerSlot)
                {
                    return !playerUpkeep;
                }
                return playerUpkeep;
            }
            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (IsGreed) // Magical Girl D --> King of Greed
            {
                if (!WstlSaveManager.HasSeenGreedTransformation)
                {
                    WstlSaveManager.HasSeenGreedTransformation = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(greedDialogue, -0.65f, 0.4f);
                }
                yield break;
            }
            if (IsNothingTrue) // Nothing There True --> Nothing There Egg
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.4f);
                if (!WstlSaveManager.HasSeenNothingTransformationTrue)
                {
                    WstlSaveManager.HasSeenNothingTransformationTrue = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(nothingTrueDialogue, -0.65f, 0.4f);
                }
                yield break;
            }
            if (IsNothingEgg) // Nothing There Egg --> Nothing There Final
            {
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
                yield break;
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (IsHateA || IsHateB)
            {
                if (!base.PlayableCard.Slot.IsPlayerSlot)
                {
                    return !playerTurnEnd;
                }
                return playerTurnEnd;
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (IsHateA) // Queen of Hatred --> Queen of Hatred Exhausted
            {
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
                yield break;
            }
            if (IsHateB) // Queen of Hatred Exhausted --> Queen of Hatred
            {
                CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatred");

                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    evolution.Mods.Add(cardModificationInfo);
                }
                yield return base.PlayableCard.TransformIntoCard(evolution);
                yield return new WaitForSeconds(0.5f);
                if (!WstlSaveManager.HasSeenHatredRecover)
                {
                    WstlSaveManager.HasSeenHatredRecover = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hateBDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (IsDragonHead && base.Card != null)
            {
                return otherCard != base.Card && otherCard.Info.name == "wstl_yinYangHead";
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            base.PlayableCard.SetInfo(CardLoader.GetCardByName("wstl_yinYangBody"));
            yield break;
        }
    }
}
