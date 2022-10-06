using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Mimicry()
        {
            const string rulebookName = "Mimicry";
            const string rulebookDescription = "Changes forme when killed. Changes forme on upkeep.";
            Mimicry.specialAbility = AbilityHelper.CreateSpecialAbility<Mimicry>(rulebookName, rulebookDescription).Id;
        }
    }
    public class Mimicry : SpecialCardBehaviour
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
			if (!WstlSaveManager.HasSeenNothingTransformation)
			{
				WstlSaveManager.HasSeenNothingTransformation = true;
				yield return CustomMethods.PlayAlternateDialogue(Emotion.Surprise, dialogue: dialogue);
			}
			yield return new WaitForSeconds(0.25f);
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
}
