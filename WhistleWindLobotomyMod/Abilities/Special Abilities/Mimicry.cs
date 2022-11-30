using WhistleWind.Core.Helpers;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Mimicry : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Mimicry";
        public static readonly string rDesc = "Nothing There reveals itself on death.";

        public override bool RespondsToDrawn() => true;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice;
        public override IEnumerator OnDrawn()
        {
            this.DisguiseInBattle();
            yield break;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            base.PlayableCard.ClearAppearanceBehaviours();
            CardInfo evolution = CardLoader.GetCardByName("wstl_nothingThereTrue");
            foreach (Ability item in base.Card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                // Adds base sigils
                evolution.Mods.Add(new CardModificationInfo(item));
            }

            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(evolution, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            yield return DialogueEventsManager.PlayDialogueEvent("NothingThereReveal");
        }

        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            this.DisguiseOutOfBattle();
            yield break;
        }

        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.DisguiseOutOfBattle();
            yield break;
        }

        public override void OnShownInDeckReview() => DisguiseOutOfBattle();
        public override void OnShownForCardChoiceNode() => DisguiseAsCardChoice();

        private void DisguiseInBattle()
        {
            List<CardInfo> list = new();
            foreach (CardModificationInfo i in SaveManager.SaveFile.deathCardMods)
            {
                CardInfo info = CardLoader.CreateDeathCard(i);
                list.Add(info);
            }
            list.RemoveAll((CardInfo x) => x.name == "wstl_nothingThere" || x.name == "!STATIC!GLITCH");
            CardInfo disguise = ((list.Count <= 0) ? CardLoader.GetCardByName("wstl_nothingThere") : list[SeededRandom.Range(0, list.Count, SaveManager.SaveFile.GetCurrentRandomSeed())]);
            this.DisguiseAsCard(disguise);
            base.PlayableCard.AddPermanentBehaviour<Mimicry>();
        }

        private void DisguiseOutOfBattle()
        {
            List<CardInfo> list = new();
            foreach (CardModificationInfo i in SaveManager.SaveFile.deathCardMods)
            {
                CardInfo info = CardLoader.CreateDeathCard(i);
                list.Add(info);
            }
            list.RemoveAll((CardInfo x) => x.name == "wstl_nothingThere" || x.name == "!STATIC!GLITCH");
            CardInfo disguise = ((list.Count <= 0) ? CardLoader.GetCardByName("wstl_nothingThere") : list[UnityEngine.Random.Range(0, list.Count)]);
            this.DisguiseAsCard(disguise);
        }

        private void DisguiseAsCardChoice()
        {
            List<CardInfo> list = new();
            foreach (CardModificationInfo i in SaveManager.SaveFile.deathCardMods)
            {
                CardInfo info = CardLoader.CreateDeathCard(i);
                list.Add(info);
            }
            int randomSeed = Environment.TickCount;
            CardInfo disguise = ((list.Count <= 0) ? CardLoader.GetCardByName("wstl_nothingThere") : list[SeededRandom.Range(0, list.Count, randomSeed)]);

            CardModificationInfo cardModificationInfo = new();
            cardModificationInfo.singletonId = "wstl_nothingThere";
            cardModificationInfo.nameReplacement = string.Format(Localization.Translate("{0}?"), disguise.DisplayedNameLocalized);
            disguise.Mods.Add(cardModificationInfo);
            this.DisguiseAsCard(disguise);
        }

        private void DisguiseAsCard(CardInfo disguise)
        {
            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(disguise);
        }
    }
    public class RulebookEntryMimicry : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Mimicry()
        {
            RulebookEntryMimicry.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMimicry>(Mimicry.rName, Mimicry.rDesc).Id;
        }
        private void SpecialAbility_Mimicry()
        {
            Mimicry.specialAbility = AbilityHelper.CreateSpecialAbility<Mimicry>(pluginGuid, Mimicry.rName).Id;
        }
    }
}
