using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_NothingThereTrue()
        {
            const string rulebookName = "Nothing 1";
            const string rulebookDescription = "Transforms into another form on turn's end.";
            return WstlUtils.CreateSpecialAbility<NothingThereTrue>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class NothingThereTrue : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "What is it doing?";

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Nothing 1");
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            // check player slots
            var slotsWithCardsP = Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card == base.Card);
            foreach (var slot in slotsWithCardsP)
            {
                if (slot.Card != null)
                {
                    return !playerTurnEnd;
                }
            }
            // check opponent slots
            var slotsWithCardsL = Singleton<BoardManager>.Instance.GetSlots(false).Where(slot => slot && slot.Card == base.Card);
            foreach (var slot in slotsWithCardsL.Where(slot => slot && base.Card))
            {
                if (slot.Card != null)
                {
                    return playerTurnEnd;
                }
            }
            // default to this
            return !playerTurnEnd;
        }

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return new WaitForSeconds(0.25f);
            CardInfo cardByName = CardLoader.GetCardByName("wstl_nothingThereEgg");
            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenNothingTransformationTrue)
            {
                PersistentValues.HasSeenNothingTransformationTrue = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
