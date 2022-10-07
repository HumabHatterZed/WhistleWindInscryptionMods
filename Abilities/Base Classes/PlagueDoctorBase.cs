using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public abstract class PlagueDoctorBase : SniperSelectSlot
    {
        public bool HasHeretic =
            new List<PlayableCard>(Singleton<PlayerHand>.Instance.CardsInHand).FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count != 0
            || new List<CardSlot>(Singleton<BoardManager>.Instance.AllSlotsCopy.Where((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_apostleHeretic")).Count != 0;

        private readonly string hereticDialogue = "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]";

        public IEnumerator ConvertToApostle(PlayableCard otherCard, bool HasOneSin = false)
        {
            bool isOpponent = otherCard.OpponentCard;
            // null check should be done elsewhere
            if (otherCard.Info.HasAnyOfTraits(Trait.Pelt, Trait.Terrain) || otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
            {
                yield return otherCard.DieTriggerless();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            }
            else
            {
                int randomSeed = base.GetRandomSeed();
                CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
                {
                    0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                    1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                    _ => CardLoader.GetCardByName("wstl_apostleStaff")
                };
                if (!HasHeretic && !HasOneSin)
                {
                    if (new System.Random().Next(0, 12) == 0)
                    {
                        HasHeretic = true;
                        if (!isOpponent)
                        {
                            randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
                        }
                        else
                        {
                            otherCard.RemoveFromBoard();
                            yield return new WaitForSeconds(0.5f);
                            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                            {
                                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                                yield return new WaitForSeconds(0.2f);
                            }
                            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"), null, 0.25f, null);
                            yield return new WaitForSeconds(0.45f);
                        }
                    }
                }
                if (otherCard != null)
                {
                    yield return otherCard.TransformIntoCard(randApostle);
                }
                if (HasHeretic && !WstlSaveManager.ApostleHeretic)
                {
                    WstlSaveManager.ApostleHeretic = true;
                    yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Bonelord, dialogue: hereticDialogue);
                }
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
