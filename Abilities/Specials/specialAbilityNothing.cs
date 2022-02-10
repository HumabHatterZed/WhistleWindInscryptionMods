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
        private NewSpecialAbility SpecialAbility_Nothing()
        {
            const string rulebookName = "Nothing";
            const string rulebookDescription = "Reveals itself on death. Changes formes on upkeep.";
            return WstlUtils.CreateSpecialAbility<NothingThere>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class NothingThere : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Nothing");
            }
        }

        private readonly string dialogue = "What is that thing?";

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
        }
    
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName("wstl_nothingThereTrue");
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return new WaitForSeconds(0.25f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(evolution, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            if (!PersistentValues.HasSeenNothingTransformation)
            {
                PersistentValues.HasSeenNothingTransformation = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Surprise);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
