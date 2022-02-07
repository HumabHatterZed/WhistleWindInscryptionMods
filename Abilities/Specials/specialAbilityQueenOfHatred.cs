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
        private NewSpecialAbility SpecialAbility_QueenOfHatred()
        {
            const string rulebookName = "Hate A";
            const string rulebookDescription = "Transforms into a weakened form on turn's end.";
            return WstlUtils.CreateSpecialAbility<QueenOfHatred>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class QueenOfHatred : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Hate A");
            }
        }

        private readonly string dialogue = "A formidable attack. Shame it has left her too tired to defend herself.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (!base.PlayableCard.Slot.IsPlayerSlot)
            {
                return !playerTurnEnd;
            }
            return playerTurnEnd;
        }

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_queenOfHatredExhausted");

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenHatredTireOut)
            {
                PersistentValues.HasSeenHatredTireOut = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
