using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod.Opponents
{
    public class SaviourBossUtils
    {
        public static bool PlayerHasHeretic
        {
            get
            {
                if (PlayerHand.m_Instance == null || BoardManager.m_Instance == null)
                    return false;

                return
                    PlayerHand.Instance.CardsInHand.Count(c => c.Info.name == "wstl_apostleHeretic") > 0 ||
                    BoardManager.Instance.AllSlotsCopy.Count(s => s.Card != null && s.Card.Info.name == "wstl_apostleHeretic") > 0;
            }
            set { }
        }

        public static int Blessings(Card card)
        {
            return OpponentPlagueDoctor(card) ? LobotomySaveManager.OpponentBlessings : LobotomyConfigManager.Instance.NumOfBlessings;
        }

        private static bool OpponentPlagueDoctor(Card card)
        {
            if (card is PlayableCard && (card as PlayableCard).OpponentCard)
                return true;

            return card.Info.name == "wstl_plagueDoctorOpponent";
        }
        public static void UpdateBlessings(Card card, int num)
        {
            if (OpponentPlagueDoctor(card))
                LobotomySaveManager.OpponentBlessings += num;
            else
                LobotomyConfigManager.Instance.UpdateBlessings(num);
        }

        public static IEnumerator ConvertCardToApostle(PlayableCard cardToConvert, int randomSeed)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);

            if (cardToConvert.HasSpecialAbility(SpecialTriggeredAbility.PackMule))
            {
                Tween.LocalPosition(cardToConvert.transform, Vector3.up * (Singleton<BoardManager>.Instance.SlotHeightOffset), 0.1f, 0.05f, Tween.EaseOut, Tween.LoopType.None);
                Tween.Rotation(cardToConvert.transform, cardToConvert.Slot.transform.GetChild(0).rotation, 0.1f, 0f, Tween.EaseOut);

                if (cardToConvert.TriggerHandler.RespondsToTrigger(Trigger.Die, false, null))
                    yield return cardToConvert.TriggerHandler.OnTrigger(Trigger.Die, false, null);

                Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;
            }

            else if (cardToConvert.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable))
                yield break;

            if (cardToConvert.HasAnyOfTraits(Trait.Pelt, Trait.Terrain))
            {
                yield return cardToConvert.DieTriggerless();
                yield break;
            }

            CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
            {
                0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                _ => CardLoader.GetCardByName("wstl_apostleStaff")
            };
            if (!PlayerHasHeretic && SeededRandom.Range(0, 12, randomSeed++) == 0)
            {
                cardToConvert.RemoveFromBoard();
                yield return new WaitForSeconds(0.5f);
                HelperMethods.ChangeCurrentView(View.Hand, 0f);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"), null, 0.25f, null);
                yield return new WaitForSeconds(0.45f);
            }

            if (cardToConvert != null)
                yield return cardToConvert.TransformIntoCard(randApostle);

            if (PlayerHasHeretic)
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleHeretic");

            if (Singleton<ViewManager>.Instance.CurrentView == View.Hand)
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);

            yield return new WaitForSeconds(0.2f);
        }

        public static void ChangeTableColours()
        {
            Color slotColour = GameColors.Instance.nearWhite;
            slotColour.a = 0.5f;

            Singleton<TableVisualEffectsManager>.Instance.ChangeTableColors(
                GameColors.Instance.nearWhite,
                GameColors.Instance.lightGray,
                GameColors.Instance.brightNearWhite,
                slotColour,
                GameColors.Instance.nearWhite,
                GameColors.Instance.brightNearWhite,
                GameColors.Instance.gold,
                GameColors.Instance.gold,
                GameColors.Instance.brightGold);
        }

        internal static EncounterBlueprintData CreateStartingBlueprint()
        {
            string minion = (TurnManager.Instance.SpecialSequencer as ApocalypseBattleSequencer).ActiveEggMinion;

            EncounterBlueprintData encounter = EncounterManager.New("SaviourBossPlan", false)
                .AddDominantTribes(AbnormalPlugin.TribeDivine)
                .AddTurns(EncounterManager.CreateTurn(minion, minion));

            return encounter;
        }
        internal static RegionData CreateRegion()
        {
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;

            RegionData saviourRegion = ScriptableObject.CreateInstance<RegionData>();
            saviourRegion.boardLightColor = new(0f, 0.3f, 0f, 1f);
            saviourRegion.cardsLightColor = new(0.2f, 0.33f, 0f, 1f);
            saviourRegion.dominantTribes = new() { Tribe.Bird };
            saviourRegion.bosses = new() { ApocalypseBossOpponent.ID };
            saviourRegion.fogAlpha = 1f;
            saviourRegion.fogEnabled = true;
            saviourRegion.fogProfile = ScriptableObject.CreateInstance<VolumetricFogAndMist.VolumetricFogProfile>();
            saviourRegion.fogProfile.color = new(1f, 1f, 1f, 0.8f);
            saviourRegion.fogProfile.lightColor = new(1f, 1f, 1f, 1f);
            saviourRegion.fogProfile.specularColor = new(1f, 1f, 1f, 0.6f);
            saviourRegion.mapAlbedo = leshy.mapAlbedo;
            saviourRegion.predefinedNodes = ScriptableObject.CreateInstance<PredefinedNodes>();
            saviourRegion.predefinedNodes.nodeRows = new()
            {
                new() {
                    new NodeData { position = new(0.5f, 0.42f) }
                },
                new() {
                    new CardBattleNodeData { position = new(0.45f, 0.65f) }
                },
                new() {
                    new CardBattleNodeData { position = new(0.48f, 0.89f) }
                },
                new() {
                    new CardBattleNodeData { position = new(0.53f, 1.11f) }
                },
                new()
                {
                    new CardMergeNodeData { position = new(0.315f, 1.27f) },
                    new GainConsumablesNodeData { position = new(0.435f, 1.28f) },
                    new TradePeltsNodeData { position = new(0.565f, 1.26f) },
                    new BuildTotemNodeData { position = new(0.685f, 1.27f) }
                },
                new()
                {
                    new BossBattleNodeData
                    {
                        bossType = ApocalypseBossOpponent.ID,
                        specialBattleId = ApocalypseBattleSequencer.ID,
                        difficulty = 20,
                        position = new(0.5f, 1.66f)
                    }
                }
            };
            return saviourRegion;
        }
    }
}
