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
        private NewSpecialAbility SpecialAbility_MagicalGirlDiamond()
        {
            const string rulebookName = "Diamond";
            const string rulebookDescription = "Transforms into a stronger form on turn's end.";
            return WstlUtils.CreateSpecialAbility<MagicalGirlDiamond>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MagicalGirlDiamond : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "Desire unfulfilled, the koi continues for Eden.";

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Diamond");
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
            CardInfo cardByName = CardLoader.GetCardByName("wstl_kingOfGreed");
            yield return base.PlayableCard.TransformIntoCard(cardByName);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenGreedTransformation)
            {
                PersistentValues.HasSeenGreedTransformation = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
