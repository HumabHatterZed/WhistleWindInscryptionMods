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
            const string rulebookDescription = "Switches forms when drawn.";
            TodaysShyLook.specialAbility = WstlUtils.CreateSpecialAbility<TodaysShyLook>(rulebookName, rulebookDescription).Id;
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
                    if (!PersistentValues.HasSeenShyLookAngry)
                    {
                        PersistentValues.HasSeenShyLookAngry = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Some days you don't feel like smiling.", -0.65f, 0.4f);
                    }
                    break;
                case "wstl_todaysshylookhappy":
                    if (!PersistentValues.HasSeenShyLookHappy)
                    {
                        PersistentValues.HasSeenShyLookHappy = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("There was no place for frowns in the City.", -0.65f, 0.4f);
                    }
                    break;
                case "wstl_todaysshylookneutral":
                    if (!PersistentValues.HasSeenShyLookNeutral)
                    {
                        PersistentValues.HasSeenShyLookNeutral = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Unable to decide what face to wear, she became shy again.", -0.65f, 0.4f);
                    }
                    break;
            }
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

            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(cardByName);
        }
    }
}
