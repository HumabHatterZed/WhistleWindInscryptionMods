using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Nothing()
        {
            const string rulebookName = "Nothing";
            const string rulebookDescription = "Reveals itself on death. Changes formes on upkeep.";
            NothingThere.specialAbility = WstlUtils.CreateSpecialAbility<NothingThere>(rulebookName, rulebookDescription).Id;
        }
    }
    public class NothingThere : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "What is that thing?";



		public override bool RespondsToDrawn()
		{
			return true;
		}
		public override IEnumerator OnDrawn()
		{
			this.DisguiseInBattle();
			yield break;
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

		public override void OnShownInDeckReview()
		{
			this.DisguiseOutOfBattle();
		}

		public override void OnShownForCardChoiceNode()
		{
			this.DisguiseAsCardChoice();
		}

		private void DisguiseInBattle()
		{
			List<CardInfo> list = new();
			foreach (CardModificationInfo i in SaveManager.SaveFile.deathCardMods)
            {
				CardInfo info = CardLoader.CreateDeathCard(i);
				list.Add(info);
            }
			list.RemoveAll((CardInfo x) => x.name == "!STATIC!GLITCH");
			CardInfo disguise = ((list.Count <= 0) ? CardLoader.GetCardByName("wstl_nothingThere") : list[SeededRandom.Range(0, list.Count, SaveManager.SaveFile.GetCurrentRandomSeed())]);
			this.DisguiseAsCard(disguise);
			base.PlayableCard.AddPermanentBehaviour<NothingThere>();
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
			list.RemoveAll((CardInfo x) => x.name == "wstl_nothingThere" || x.name == "!STATIC!GLITCH");
			CardInfo disguise = ((list.Count <= 0) ? CardLoader.GetCardByName("wstl_nothingThere") : list[UnityEngine.Random.Range(0, list.Count)]);

			CardModificationInfo cardModificationInfo = new CardModificationInfo();
			cardModificationInfo.singletonId = "nothingThere";
			cardModificationInfo.nameReplacement = string.Format(Localization.Translate("{0}?"), disguise.DisplayedNameLocalized);
			disguise.Mods.Add(cardModificationInfo);
			this.DisguiseAsCard(disguise);
		}

		private void DisguiseAsCard(CardInfo disguise)
		{
			base.Card.ClearAppearanceBehaviours();
			base.Card.SetInfo(disguise);
		}
		public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return !wasSacrifice;
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
