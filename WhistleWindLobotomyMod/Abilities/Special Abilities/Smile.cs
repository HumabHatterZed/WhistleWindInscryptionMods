using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Smile : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Smile";
        public const string rDesc = "Mountain of Smiling Bodies grows into a stronger forme whenever killing a card. Upon dying, revert to a previous forme if possible.";
        private string CardName => base.PlayableCard.Info.name;

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.PlayableCard && !base.PlayableCard.Dead && CardName != "wstl_mountainOfBodies3";
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardName == "wstl_mountainOfBodies" ? CardLoader.GetCardByName("wstl_mountainOfBodies2") : CardLoader.GetCardByName("wstl_mountainOfBodies3");
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueEventsManager.PlayDialogueEvent("MountainOfBodiesGrow");
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice && CardName != "wstl_mountainOfBodies";
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo previous = CardName.Equals("wstl_mountainOfBodies2") ? CardLoader.GetCardByName("wstl_mountainOfBodies") : CardLoader.GetCardByName("wstl_mountainOfBodies2");
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                previous.Mods.Add(cardModificationInfo);
            }
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            yield return DialogueEventsManager.PlayDialogueEvent("MountainOfBodiesShrink");
        }
    }
    public class RulebookEntrySmile : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Smile()
            => RulebookEntrySmile.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySmile>(Smile.rName, Smile.rDesc).Id;
        private void SpecialAbility_Smile()
            => Smile.specialAbility = AbilityHelper.CreateSpecialAbility<Smile>(pluginGuid, Smile.rName).Id;
    }
}
