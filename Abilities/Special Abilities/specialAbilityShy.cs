using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Shy()
        {
            const string rulebookName = "Shy";
            const string rulebookDescription = "Switches formes when drawn.";
            TodaysShyLook.specialAbility = AbilityHelper.CreateSpecialAbility<TodaysShyLook>(rulebookName, rulebookDescription).Id;
        }
    }
    public class TodaysShyLook : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.PlayableCard);
            yield return base.PlayableCard.FlipInHand(ChangeForme);
            yield return new WaitForSeconds(0.1f);
            switch (base.Card.Info.name.ToLowerInvariant())
            {
                case "wstl_todaysshylookangry":
                    if (!WstlSaveManager.HasSeenShyLookAngry)
                    {
                        WstlSaveManager.HasSeenShyLookAngry = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Some days you don't feel like smiling.", -0.65f, 0.4f);
                    }
                    break;
                case "wstl_todaysshylookhappy":
                    if (!WstlSaveManager.HasSeenShyLookHappy)
                    {
                        WstlSaveManager.HasSeenShyLookHappy = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("There was no place for frowns in the City.", -0.65f, 0.4f);
                    }
                    break;
                case "wstl_todaysshylookneutral":
                    if (!WstlSaveManager.HasSeenShyLookNeutral)
                    {
                        WstlSaveManager.HasSeenShyLookNeutral = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Unable to decide what face to wear, she became shy again.", -0.65f, 0.4f);
                    }
                    break;
            }
        }

        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.ChangeFormeDeck();
            yield break;
        }

        public override void OnShownInDeckReview()
        {
            this.ChangeFormeDeck();
        }
        private void ChangeForme()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_todaysShyLookNeutral");

            int rand = SeededRandom.Range(0, 3, base.GetRandomSeed());
            switch (rand)
            {
                case 0:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookAngry");
                    break;
                case 1:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookHappy");
                    break;
                case 2:
                    break;
            }
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }

        private void ChangeFormeDeck()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_todaysShyLookNeutral");

            int rand = UnityEngine.Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookAngry");
                    break;
                case 1:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookHappy");
                    break;
                case 2:
                    break;
            }
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardModificationInfo.fromCardMerge = true;
                cardByName.Mods.Add(cardModificationInfo);
            }

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }
    }
}
