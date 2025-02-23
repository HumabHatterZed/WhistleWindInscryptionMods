using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Smile : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Smile";
        public const string rDesc = "Mountain of Smiling Bodies grows into a stronger forme whenever killing a card. Upon dying, revert to a previous forme if possible.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            return killer == base.PlayableCard && !base.PlayableCard.Dead && base.PlayableCard.Info.name != Cards.mountainOfBodies3;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            CardInfo evolution = CardLoader.GetCardByName(base.PlayableCard.Info.HasCardMetaCategory(CardMetaCategory.Rare) ? Cards.mountainOfBodies2 : Cards.mountainOfBodies3);
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                evolution.Mods.Add(cardModificationInfo);
            }
            yield return base.PlayableCard.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("MountainOfBodiesGrow");
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice && base.PlayableCard.Info.LacksCardMetaCategory(CardMetaCategory.Rare);
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo previous = CardLoader.GetCardByName(base.PlayableCard.Info.name == Cards.mountainOfBodies2 ? (SaveManager.SaveFile.IsPart1 ? Cards.mountainOfBodies : Cards.mountainOfBodiesPixel) : Cards.mountainOfBodies2);
            yield return new WaitForSeconds(0.25f);
            foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                previous.Mods.Add(cardModificationInfo);
            }
            previous.Mods.Add(new(-1, -1));
            if (base.PlayableCard.Slot.Card != null)
                yield return base.PlayableCard.TransformIntoCard(previous, () => base.PlayableCard.Status.damageTaken = 0);
            else
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(previous, base.PlayableCard.Slot, 0.15f);

            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("MountainOfBodiesShrink");
        }
    }
    public class RulebookEntrySmile : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_Smile()
            => RulebookEntrySmile.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySmile>(Smile.rName, Smile.rDesc).Id;
        private static void AddSpecial_Smile()
            => Smile.specialAbility = AbilityHelper.CreateSpecialAbility<Smile>(LobotomyPlugin.pluginGuid, Smile.rName).Id;
    }
}
