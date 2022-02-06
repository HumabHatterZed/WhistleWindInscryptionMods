using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_TodaysShyLook()
        {
            const string rulebookName = "Shy";
            const string rulebookDescription = "Switches forms when drawn.";
            return WstlUtils.CreateSpecialAbility<TodaysShyLook>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class TodaysShyLook : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        CardInfo cardByName = null;

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Shy");
            }

        }

        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(base.PlayableCard);
            int rand = new System.Random().Next(0, 3);
            switch (rand)
            {
                case 0:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookAngry");
                    base.Card.SetInfo(cardByName);
                    if (!PersistentValues.HasSeenShyLookAngry)
                    {
                        PersistentValues.HasSeenShyLookAngry = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Weary of always forcing a smile, she gave herself a brief reprieve.", -0.65f, 0.4f);
                    }
                    break;
                case 1:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookHappy");
                    base.Card.SetInfo(cardByName);
                    if (!PersistentValues.HasSeenShyLookHappy)
                    {
                        PersistentValues.HasSeenShyLookHappy = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Conforming to other's expectation is not true happiness.", -0.65f, 0.4f);
                    }
                    break;
                case 2:
                    cardByName = CardLoader.GetCardByName("wstl_todaysShyLookNeutral");
                    base.Card.SetInfo(cardByName);
                    if (!PersistentValues.HasSeenShyLookNeutral)
                    {
                        PersistentValues.HasSeenShyLookNeutral = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput("Unable to decide what face to wear, she became shy again.", -0.65f, 0.4f);
                    }
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
