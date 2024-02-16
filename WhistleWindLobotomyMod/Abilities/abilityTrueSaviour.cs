using DiskCardGame;
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
using WhistleWindLobotomyMod.Opponents.Apocalypse;

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

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return SaviourBossUtils.ConvertCardsOnBoard(base.Card.IsPlayerCard(), base.Card, base.GetRandomSeed());
        }

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null && otherCard != base.Card)
            {
                if (otherCard.Info.name != SaviourBossUtils.ONESIN_NAME && otherCard.LacksAllAbilities(ApostleSigil.ability, Confession.ability))
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
                foreach (PlayableCard card in Singleton<BoardManager>.Instance.CardsOnBoard.Where(x => x.HasAbility(ApostleSigil.ability)))
                {
                    yield return card.Die(false, base.Card);
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
            else if (CustomBossUtils.FightingCustomBoss())
            {
                if (CustomBossUtils.IsCustomBoss<ApocalypseBossOpponent>())
                {
                    yield return BoardManager.Instance.CardsOnBoard.Find(x => x.HasAbility(ApocalypseAbility.ability)).TakeDamage(20, base.Card);
                }
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
            if (Singleton<BoardManager>.Instance.GetCards(!base.Card.OpponentCard).Count < 4)
                yield break;

            // If all slots on the owner's side are full
            yield return new WaitForSeconds(0.2f);

            List<PlayableCard> cardsToKill = BoardManager.Instance.GetCards(!base.Card.OpponentCard, (PlayableCard c) => c != base.Card);
            PlayableCard cardToKill = cardsToKill[SeededRandom.Range(0, cardsToKill.Count, base.GetRandomSeed() + 1)];
            
            ViewManager.Instance.SwitchToView(View.Hand);
            foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.HasAbility(Confession.ability)))
                card.Anim.StrongNegationEffect();

            yield return new WaitForSeconds(0.4f);

            ViewManager.Instance.SwitchToView(View.Board);
            cardToKill.Anim.SetShaking(true);
            yield return new WaitForSeconds(0.25f);
            yield return cardToKill.Die(false, base.Card);
            yield return new WaitForSeconds(0.45f);
            yield return DialogueHelper.PlayDialogueEvent("WhiteNightMakeRoom");
        }
    }
}
