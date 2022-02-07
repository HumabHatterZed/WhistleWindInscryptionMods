using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_BloodBath1()
        {
            const string rulebookName = "Hands 1";
            const string rulebookDescription = "Reacts to cards being sacrificed.";
            return WstlUtils.CreateSpecialAbility<BloodBath1>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class BloodBath1 : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Hands 1");
            }

        }

        private readonly string dialogue = "Another pale hand emerges.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (base.PlayableCard.OnBoard)
            {
                return !fromCombat && card != base.PlayableCard;
            }
            return false;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_bloodBath2");

            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenBloodbathHand1)
            {
                PersistentValues.HasSeenBloodbathHand1 = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
