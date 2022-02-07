using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_JudgementBird()
        {
            const string rulebookName = "Judge";
            const string rulebookDescription = "Hangs those deemed sinners.";
            return WstlUtils.CreateSpecialAbility<Judgement>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class Judgement : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Judge");
            }

        }

        private readonly string dialogue = "A hand rises from the overflowing pool.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (base.PlayableCard.InHand)
            {
                return !fromCombat && Singleton<BoardManager>.Instance.currentSacrificeDemandingCard == base.PlayableCard;
            }
            return !fromCombat;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_bloodBath1");

            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenBloodbathHand)
            {
                PersistentValues.HasSeenBloodbathHand = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
