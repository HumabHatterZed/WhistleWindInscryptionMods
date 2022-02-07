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
        private NewSpecialAbility SpecialAbility_MountainOfBodies2()
        {
            const string rulebookName = "Bodies 2";
            const string rulebookDescription = "Grows when it kills a card. Returns to a weaker form when killed.";
            return WstlUtils.CreateSpecialAbility<MountainOfBodies2>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MountainOfBodies2 : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Bodies 2");
            }

        }
        /*
        private readonly string dieDialogue = "Though it has become smaller, its hunger continues to grow.";
        private readonly string growDialogue = "The smiling faces are unfamiliar, yet sorrowful.";

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo previous = CardLoader.GetCardByName("wstl_mountainOfBodies");

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                previous.Mods.Add(cardModificationInfo);
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenMountainShrink2)
            {
                PersistentValues.HasSeenMountainShrink2 = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dieDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_mountainOfBodies3");

            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenMountainGrow2)
            {
                PersistentValues.HasSeenMountainGrow2 = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(growDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }*/
    }
}
