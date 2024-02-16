using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Encounters;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Regions;
using Pixelplacement;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents.Saviour;

namespace WhistleWindLobotomyMod.Opponents
{
    /// <summary>
    /// Methods and fields related to the Saviour boss and WhiteNight event
    /// </summary>
    public class SaviourBossUtils
    {
        public const string ONESIN_NAME = "wstl_oneSin";
        #region WhiteNight event
        public static IEnumerator ConvertCardsOnBoard(bool getPlayerCards, PlayableCard thisCard, int randomSeed)
        {
            foreach (PlayableCard card in Singleton<BoardManager>.Instance.GetCards(getPlayerCards, x => x != thisCard))
            {
                if (card.Info.name != ONESIN_NAME && card.LacksAllAbilities(ApostleSigil.ability, Confession.ability))
                    yield return ConvertCardToApostle(card, randomSeed++);
            }
        }
        public static IEnumerator ConvertCardToApostle(PlayableCard cardToConvert, int randomSeed)
        {
            if (cardToConvert.HasTrait(Trait.Giant)) // do not convert Giants
                yield break;

            ViewManager.Instance.SwitchToView(View.Board);

            // convert Mules, but not other Uncuttable cards
            if (cardToConvert.HasSpecialAbility(SpecialTriggeredAbility.PackMule))
            {
                Tween.LocalPosition(cardToConvert.transform, Vector3.up * Singleton<BoardManager>.Instance.SlotHeightOffset, 0.1f, 0.05f, Tween.EaseOut, Tween.LoopType.None);
                Tween.Rotation(cardToConvert.transform, cardToConvert.Slot.transform.GetChild(0).rotation, 0.1f, 0f, Tween.EaseOut);

                if (cardToConvert.TriggerHandler.RespondsToTrigger(Trigger.Die, false, null))
                    yield return cardToConvert.TriggerHandler.OnTrigger(Trigger.Die, false, null);

                ViewManager.Instance.Controller.LockState = ViewLockState.Unlocked;
            }

            else if (cardToConvert.HasTrait(Trait.Uncuttable))
                yield break;

            if (cardToConvert.HasAnyOfTraits(Trait.Pelt, Trait.Terrain)) // remove Pelts and Terrain
            {
                yield return cardToConvert.DieTriggerless();
                yield break;
            }

            if (!PlayerHasHeretic && SeededRandom.Range(0, 12, randomSeed++) == 0)
            {
                cardToConvert.RemoveFromBoard();
                yield return new WaitForSeconds(0.5f);
                yield return HelperMethods.ChangeCurrentView(View.Hand, 0f);
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"), null, 0.25f, null);
                yield return new WaitForSeconds(0.45f);
            }

            if (cardToConvert != null)
            {
                CardInfo randApostle = SeededRandom.Range(0, 3, randomSeed++) switch
                {
                    0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                    1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                    _ => CardLoader.GetCardByName("wstl_apostleStaff")
                };

                yield return cardToConvert.TransformIntoCard(randApostle);
            }

            if (PlayerHasHeretic)
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleHeretic");

            ViewManager.Instance.SwitchToView(View.Board);
            yield return new WaitForSeconds(0.2f);
        }

        public static bool PlayerHasHeretic
        {
            get
            {
                if (PlayerHand.m_Instance == null || BoardManager.m_Instance == null)
                    return false;

                return PlayerHand.Instance.CardsInHand.Exists(c => c.HasAbility(Confession.ability)) || BoardManager.Instance.CardsOnBoard.Exists(c => c.HasAbility(Confession.ability));
            }
        }

        private static bool OpponentPlagueDoctor(Card card)
        {
            if (card is PlayableCard playable && (playable.OpponentCard || playable.QueuedSlot != null))
                return true;

            return false;
        }
        public static int Blessings(Card card)
        {
            return OpponentPlagueDoctor(card) ? LobotomySaveManager.OpponentBlessings : LobotomyConfigManager.Instance.NumOfBlessings;
        }

        public static void UpdateBlessings(Card card, int num)
        {
            if (OpponentPlagueDoctor(card))
                LobotomySaveManager.OpponentBlessings += num;
            else
                LobotomyConfigManager.Instance.UpdateBlessings(num);
        }
        #endregion

        #region Saviour
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
            EncounterBlueprintData encounter = EncounterManager.New("SaviourBossPlan", false)
                .AddDominantTribes(AbnormalPlugin.TribeDivine);

            return encounter;
        }
        internal static RegionData CreateRegion()
        {
            RegionData leshy = RegionProgression.Instance.ascensionFinalRegion;

            RegionData saviourRegion = ScriptableObject.CreateInstance<RegionData>();
            saviourRegion.boardLightColor = new(5f, 0.6f, 0.3f, 1f);
            saviourRegion.cardsLightColor = new(0.4f, 0.66f, 0.2f, 1f);
            saviourRegion.dominantTribes = new() { AbnormalPlugin.TribeDivine };
            saviourRegion.bosses = new() { SaviourBossOpponent.ID };
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
                new()
                {
                    new CardMergeNodeData { position = new(0.315f, 0.65f) },
                    new GainConsumablesNodeData { position = new(0.435f, 0.64f) },
                    new TradePeltsNodeData { position = new(0.565f, 0.66f) },
                    new BuildTotemNodeData { position = new(0.685f, 0.64f) }
                },
                new() {
                    new CardBattleNodeData {
                        difficulty = 20,
                        position = new(0.45f, 0.89f)
                    }
                },
                new() {
                    new CardBattleNodeData {
                        difficulty = 20,
                        position = new(0.48f, 1.11f)
                    }
                },
                new() {
                    new CardBattleNodeData {
                        difficulty = 20,
                        position = new(0.53f, 1.34f)
                    }
                },
                new()
                {
                    new BossBattleNodeData
                    {
                        bossType = SaviourBossOpponent.ID,
                        specialBattleId = SaviourBattleSequencer.ID,
                        difficulty = 20,
                        position = new(0.5f, 1.66f)
                    }
                }
            };
            return saviourRegion;
        }
        #endregion
    }
}
