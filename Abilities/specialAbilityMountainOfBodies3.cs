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
        private NewSpecialAbility SpecialAbility_MountainOfBodies3()
        {
            const string rulebookName = "Bodies 3";
            const string rulebookDescription = "Grows when it kills a card. Returns to a weaker form when killed.";
            return WstlUtils.CreateSpecialAbility<MountainOfBodies3>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MountainOfBodies3 : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string dieDialogue = "No worry. There are bodies to spare.";

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Bodies 3");
            }

        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo previous = CardLoader.GetCardByName("wstl_mountainOfBodies2");

            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                previous.Mods.Add(cardModificationInfo);
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenMountainShrink3)
            {
                PersistentValues.HasSeenMountainShrink3 = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dieDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
