using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_MountainOfBodies()
        {
            const string rulebookName = "Bodies 1";
            const string rulebookDescription = "Grows when it kills a card.";
            return WstlUtils.CreateSpecialAbility<MountainOfBodies>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class MountainOfBodies : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Bodies 1");
            }

        }
        /*
        private readonly string growDialogue = "With each body its appetite grows.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.Card;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_mountainOfBodies2");

            CardInfo previous = CardLoader.GetCardByName("wstl_mountainOfBodies");

            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            if (!PersistentValues.HasSeenMountainGrow)
            {
                PersistentValues.HasSeenMountainGrow = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(growDialogue, -0.65f, 0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }*/
    }
}
