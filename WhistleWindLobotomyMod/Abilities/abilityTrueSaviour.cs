﻿using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ability = LobotomyAbilityHelper.CreateAbility<TrueSaviour>(
                "sigilTrueSaviour",
                rulebookName, "'My story is nowhere, unknown to all.'", dialogue, powerLevel: -3,
                canStack: false).Id;
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string sternDialogue = "[c:bR]Do not deny me.[c:]";

        // True if on the player's side and they have Heretic in hand
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.IsPlayerCard() == playerUpkeep
            && Singleton<PlayerHand>.Instance.CardsInHand.Count(c => c.Info.name == "wstl_apostleHeretic") >= 0;
        public override IEnumerator OnUpkeep(bool playerUpkeep) => MakeRoomForOneSin();

        public override bool RespondsToResolveOnBoard() => base.Card.OpponentCard;
        public override IEnumerator OnResolveOnBoard()
        {
            // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
            foreach (var slot in Singleton<BoardManager>.Instance.OpponentSlotsCopy.Where(slot => slot.Card != base.Card))
            {
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                    yield return SaviourBossUtils.ConvertCardToApostle(slot.Card, base.GetRandomSeed());
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
            yield return SaviourBossUtils.ConvertCardToApostle(otherCard, base.GetRandomSeed());
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
            Singleton<UIManager>.Instance?.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);

            // if not killed by Hundreds of Good Deeds
            if (killer.LacksAbility(Confession.ability))
            {
                // kill all Apostles
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(x => x.Card != null && x.Card.Info.name.StartsWith("wstl_apostle")))
                {
                    yield return slot.Card.Die(false, base.Card);
                }
            }

            yield return new WaitForSeconds(0.5f);

            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            if (!LobotomyPlugin.PreventOpponentDamage)
            {
                SpecialBattleSequencer specialSequence = null;
                var combatManager = Singleton<CombatPhaseManager>.Instance;

                yield return combatManager.DamageDealtThisPhase += 33;

                int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
                int damage = combatManager.DamageDealtThisPhase - excessDamage;

                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);
                yield return combatManager.VisualizeExcessLethalDamage(excessDamage, specialSequence);

                if (SaveManager.SaveFile.IsPart2)
                    SaveManager.SaveFile.gbcData.currency += excessDamage;
                else
                    RunState.Run.currency += excessDamage;
            }

            if (killer.LacksAbility(Confession.ability))
            {
                foreach (CardInfo card in RunState.Run.playerDeck.CardInfos)
                {
                    RunState.Run.playerDeck.ModifyCard(card, new(1, 2));
                }
                foreach (PlayableCard card in BoardManager.Instance.GetPlayerCards())
                    card.AddTemporaryMod(new(1, 2));
                foreach (PlayableCard card in PlayerHand.Instance.CardsInHand.Where(x => !CardDrawPiles3D.Instance.SideDeckData.Exists(y => x.Info.name == y.name)))
                    card.AddTemporaryMod(new(1, 2));

                DialogueHelper.ShowUntilInput("Divine power infuses the beasts in your caravan, empowering them.");
            }

            // Resets Blessings
            LobotomyPlugin.Log.LogDebug($"Resetting the clock to [0].");
            LobotomyConfigManager.Instance.SetBlessings(0);
        }
        private IEnumerator KilledByNull()
        {
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f, false);
            yield return DialogueHelper.PlayDialogueEvent("WhiteNightKilledByNull");

            yield return new WaitForSeconds(0.2f);
            yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));

            yield return DialogueHelper.ShowUntilInput(sternDialogue, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord, -0.65f, 0.4f);
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
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightMakeRoom");
            }
        }
    }
}
