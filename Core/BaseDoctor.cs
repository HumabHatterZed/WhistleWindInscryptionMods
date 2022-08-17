﻿using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public abstract class BaseDoctor : AbilityBehaviour
    {
        public bool HasHeretic = new List<PlayableCard>(Singleton<PlayerHand>.Instance.CardsInHand).FindAll((PlayableCard card) => card.Info.name == "wstl_apostleHeretic").Count != 0
            || new List<CardSlot>(Singleton<BoardManager>.Instance.AllSlotsCopy
                .Where(slot => slot.Card != null && slot.Card.Info.name == "wstl_apostleHeretic")).Count != 0;

        private readonly CardInfo ScytheApostle = CardLoader.GetCardByName("wstl_apostleScythe");
        private readonly CardInfo SpearApostle = CardLoader.GetCardByName("wstl_apostleSpear");
        private readonly CardInfo StaffApostle = CardLoader.GetCardByName("wstl_apostleStaff");
        private readonly CardInfo Heretic = CardLoader.GetCardByName("wstl_apostleHeretic");

        private readonly string hereticDialogue = "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]";

        public IEnumerator ConvertToApostle(PlayableCard otherCard, bool HasOneSin = false)
        {
            // null check should be done elsewhere
            if (otherCard.Info.HasTrait(Trait.Pelt) || otherCard.Info.HasTrait(Trait.Terrain) || otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
            {
                yield return otherCard.DieTriggerless();
            }
            else
            {
                int randomSeed = base.GetRandomSeed();
                CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
                {
                    0 => ScytheApostle,
                    1 => SpearApostle,
                    _ => StaffApostle
                };
                if (!HasHeretic && !HasOneSin)
                {
                    if (new System.Random().Next(0, 12) == 0)
                    {
                        HasHeretic = true;
                        randApostle = Heretic;
                    }
                }
                yield return otherCard.TransformIntoCard(randApostle);
                if (HasHeretic && !WstlSaveManager.ApostleHeretic)
                {
                    WstlSaveManager.ApostleHeretic = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hereticDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }
}