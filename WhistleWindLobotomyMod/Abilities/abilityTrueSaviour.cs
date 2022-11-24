﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        public const string TrueSaviourHiddenDescription = "My story is nowhere, unknown to all.";
        public const string TrueSaviourRevealedDescription = "While [creature] is on the board, transform any non-Terrain and non-Pelt cards into a random Apostle.";

        private void Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ability = AbilityHelper.CreateAbility<TrueSaviour>(
                Artwork.sigilTrueSaviour, Artwork.sigilTrueSaviour_pixel,
                rulebookName, TrueSaviourHiddenDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: false, isPassive: false,
                unobtainable: true).Id;
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool HasHeretic =
            new List<PlayableCard>(Singleton<PlayerHand>.Instance.CardsInHand).FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count != 0
            || new List<CardSlot>(Singleton<BoardManager>.Instance.AllSlotsCopy.Where((CardSlot s) => s.Card != null && s.Card.Info.name == "wstl_apostleHeretic")).Count != 0;

        private readonly string sternDialogue = "[c:bR]Do not deny me.[c:]";

        // True if on the player's side and they have Heretic in hand
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.IsPlayerCard()
            && playerUpkeep && Singleton<PlayerHand>.Instance.CardsInHand.FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count() != 0;
        public override IEnumerator OnUpkeep(bool playerUpkeep) => MakeRoomForOneSin();

        public override bool RespondsToResolveOnBoard() => base.Card.OpponentCard;
        public override IEnumerator OnResolveOnBoard()
        {
            // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
            foreach (var slot in Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                    yield return ConvertToApostle(slot.Card);
            }
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null && otherCard != base.Card)
            {
                if (!otherCard.Info.name.StartsWith("wstl_apostle") && otherCard.Info.name != "wstl_oneSin" && otherCard.Info.name != "wstl_hundredsGoodDeeds")
                    return base.Card.OnBoard && base.Card.OpponentCard == otherCard.OpponentCard;
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return ConvertToApostle(otherCard);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
                yield return KilledByNonNull(killer);
            else
                yield return KilledByNull();
        }

        private IEnumerator KilledByNonNull(PlayableCard killer)
        {
            AudioController.Instance.PlaySound2D("mycologist_scream");
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);

            // if not killed by Hundreds of Good Deeds
            if (killer.Info.name != "wstl_hundredsGoodDeeds")
            {
                // kill all Apostles
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(x => x.Card != null && x.Card.Info.name.StartsWith("wstl_apostle")))
                {
                    yield return slot.Card.DieTriggerless();
                }

                // Deal damage but more, because you somehow killed WhiteNight without the Heretic
                SpecialBattleSequencer specialSequence = null;
                var combatManager = Singleton<CombatPhaseManager>.Instance;

                yield return combatManager.DamageDealtThisPhase += 66;

                int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
                int damage = combatManager.DamageDealtThisPhase - excessDamage;

                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);

                yield return combatManager.VisualizeExcessLethalDamage(excessDamage, specialSequence);
                RunState.Run.currency += excessDamage;

                // Resets Blessings
                LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");
                ConfigManager.Instance.SetBlessings(0);
            }
            yield break;
        }
        private IEnumerator KilledByNull()
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f, false);
            yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightKilledByNull");

            yield return new WaitForSeconds(0.2f);
            yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase += 1;
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));

            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(sternDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
        }
        private IEnumerator MakeRoomForOneSin()
        {
            // If all slots on the owner's side are full
            yield return new WaitForSeconds(0.2f);
            if (Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(s => s.Card != null).Count() == 4)
            {
                int randomSeed = base.GetRandomSeed();
                List<CardSlot> cardsToKill = Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).FindAll((CardSlot s) => s.Card != null && s.Card != base.Card);
                PlayableCard cardToKill = cardsToKill[SeededRandom.Range(0, cardsToKill.Count, randomSeed++)].Card;
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_apostleHeretic"))
                    card.Anim.StrongNegationEffect();

                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered);
                cardToKill.Anim.SetShaking(true);
                yield return cardToKill.Die(false, base.Card);
                yield return new WaitForSeconds(0.4f);
                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightMakeRoom");
            }
        }
        private IEnumerator ConvertToApostle(PlayableCard otherCard)
        {
            bool isOpponent = otherCard.OpponentCard;
            // null check should be done elsewhere
            if (otherCard.HasAnyOfTraits(Trait.Pelt, Trait.Terrain) || otherCard.HasSpecialAbility(SpecialTriggeredAbility.PackMule))
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
                if (!HasHeretic)
                {
                    if (new System.Random().Next(0, 12) == 0)
                    {
                        HasHeretic = true;
                        if (!isOpponent)
                            randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
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
                    yield return otherCard.TransformIntoCard(randApostle);

                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleHeretic");

                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}