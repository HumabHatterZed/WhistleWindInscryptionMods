using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WhistleWind.AbnormalSigils;
using WhistleWindLobotomyMod.Challenges;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod.Opponents {
    [HarmonyPatch]
    public static class QlippothCards {
        public static Dictionary<string, CardModificationInfo> QLIPPOTH_CARDS { get; private set; }
        public static readonly Dictionary<string, List<string>> ORDEAL_QLIPPOTH_CARDS = new();

        public const string QLIPPOTH_ID = "wst:EMPOWERED_CARD";
        internal static void InitialiseQlippothCardInfos() {
            Dictionary<string, CardModificationInfo> retval = new() {
                { Cards.allAroundHelper, new() { fromCardMerge = true, abilities = new() { Ability.StrafeSwap }, negateAbilities = new() { Ability.Strafe } } },
                { Cards.bigBird, new() { fromCardMerge = true, abilities = new() { Dazzling.ability } } },
                { Cards.burrowingHeaven, new(0, 1) },
                { Cards.canOfWellCheers, new() { fromCardMerge = true, abilities = new() { GiftGiver.ability } } },
                { Cards.cloudedMonk, new() { fromCardMerge = true, abilities = new() { UnkillableWeak.ability } } },
                { Cards.derFreischutz, new(0, 2) },
                { Cards.dimensionalRefraction, new(1, 0) },
                { Cards.dingleDangle, new() { fromCardMerge = true, abilities = new() { Ability.SplitStrike } } },
                { Cards.dontTouchMe, new() { fromCardMerge = true, abilities = new() { Ability.GuardDog } } },
                { Cards.dreamingCurrent, new(0, 1) },
                { Cards.drownedSisters, new(0, 1) { fromCardMerge = true, abilities = new() { Ability.GuardDog } } },
                { Cards.eyeballChick_mook, new() { fromCardMerge = true, abilities = new() { Piercing.ability } } },
                { Cards.fairyFestival, new(0, 2) },
                { Cards.forestKeeper_mook, new(1, 1) },
                { Cards.forsakenMurderer, new(0, 2) },
                { Cards.fragmentOfUniverse, new() { fromCardMerge = true, abilities = new() { Ability.DebuffEnemy } } },
                { Cards.funeralOfButterflies, new(0, 1) { fromCardMerge = true, abilities = new() { Ability.DoubleStrike } } },
                { Cards.happyTeddyBear, new(2, 0) },
                { Cards.heartOfAspiration, new() { fromCardMerge = true, abilities = new() { FlagBearer.ability } } },
                { Cards.honouredMonk, new() { fromCardMerge = true, abilities = new() { UnkillableWeak.ability } } },
                { Cards.judgementBird, new(1, 0) },
                { Cards.laetitia, new() { fromCardMerge = true, abilities = new() { GiftGiver.ability } }  },
                { Cards.magicalGirlHeart, new() { fromCardMerge = true, abilities = new() { Ability.Sentry } } },
                { Cards.magicalGirlSpade, new(1, 0) },
                { Cards.mhz176, new() { negateAbilities = new() { Ability.BuffEnemy } } },
                { Cards.oldLady, new(0) { fromCardMerge = true, abilities = new() { Scorching.ability } } },
                { Cards.oneSin, new() { fromCardMerge = true, abilities = new() { Idol.ability } } },
                { Cards.ozma, new(Protector.ability) { fromCardMerge = true } },
                { Cards.punishingBird, new(1, 0) },
                { Cards.redHoodedMercenary, new(1, 0) },
                { Cards.redShoes, new(Ability.WhackAMole) { fromCardMerge = true, negateAbilities = new() { Ability.GuardDog } } },
                { Cards.runawayBird_mook, new(0, 1) { fromCardMerge = true, abilities = new() { Persistent.ability } } },
                { Cards.schadenfreude, new() { fromCardMerge = true, abilities = new() { Ability.Sentry, NimbleFoot.ability } } },
                { Cards.silentGirl, new() { fromCardMerge = true, abilities = new() { OneSided.ability } } },
                { Cards.singingMachine, new() { negateAbilities = new() { Aggravating.ability } } },
                { Cards.snowQueen, new() { fromCardMerge = true, abilities = new() { Ability.BuffNeighbours } } },
                { Cards.snowWhitesApple, new(0, 1) { fromCardMerge = true, abilities = new() { Ability.Deathtouch } } },
                { Cards.theFirebird, new() { fromCardMerge = true, abilities = new() { Scorching.ability } } },
                { Cards.theNakedNest, new(0, 2) { fromCardMerge = true, abilities = new() { Ability.WhackAMole } } },
                { Cards.theresia, new(Healer.ability) { fromCardMerge = true } },
                { Cards.theRoadHome, new(0, 1) { fromCardMerge = true, abilities = new() { NimbleFoot.ability } } },
                { Cards.trainingDummy, new(1, 0) },
                { Cards.voidDream, new(1, 1) },
                { Cards.wallLady, new(0, 1) { fromCardMerge = true, abilities = new() { Ability.Sharp } } },
                { Cards.warmHeartedWoodsman, new() { fromCardMerge = true, abilities = new() { ThickSkin.ability } } },
                { Cards.weCanChangeAnything, new(1, 2) },
                { Cards.whiteLake, new(1, 1) { fromCardMerge = true, abilities = new() { Alluring.ability } } },
                { Cards.willBeBadWolf, new(0, 2) },
                { Cards.wisdomScarecrow, new(0, 2) { fromCardMerge = true, bonesCostAdjustment = -1, abilities = new() { MindStrike.ability } } },
                { Cards.yang, new(Ability.MoveBeside) { fromCardMerge = true } },
                { Cards.youMustBeHappy, new(0, 2) }
            };

            ORDEAL_QLIPPOTH_CARDS.Add("Mechanical", new() { Cards.allAroundHelper, Cards.canOfWellCheers, Cards.schadenfreude, Cards.singingMachine, Cards.youMustBeHappy, Cards.weCanChangeAnything });
            ORDEAL_QLIPPOTH_CARDS.Add("Divine", new() { Cards.burrowingHeaven, Cards.fleshIdol, Cards.fragmentOfUniverse, Cards.oneSin, Cards.yin, Cards.yang });
            ORDEAL_QLIPPOTH_CARDS.Add("Fae", new() { Cards.fairyFestival, Cards.laetitia, Cards.magicalGirlHeart, Cards.magicalGirlSpade, Cards.ozma, Cards.redShoes, Cards.snowQueen });
            ORDEAL_QLIPPOTH_CARDS.Add("Insect", new() { Cards.funeralOfButterflies, Cards.theNakedNest, Cards.dontTouchMe, Cards.forestKeeper_mook, Cards.wisdomScarecrow, Cards.youMustBeHappy });
            ORDEAL_QLIPPOTH_CARDS.Add("Anthropoid", new() { Cards.oldLady, Cards.redHoodedMercenary, Cards.honouredMonk, Cards.dingleDangle, Cards.runawayBird_mook, Cards.redShoes });

            QLIPPOTH_CARDS = retval;
        }

        public static List<List<CardInfo>> AddEmpoweredCardsToTurnPlan(List<List<CardInfo>> turnPlan, string keyOverride = null) {
            int empoweredCards = 0;
            int seed = SaveManager.SaveFile.GetCurrentRandomSeed() + 1000;
            bool addEmpoweredCard = SeededRandom.Value(seed) <= 0.21f;
            seed += 1000;
            for (int i = 0; i < turnPlan.Count; i++) {
                if (!addEmpoweredCard) {
                    addEmpoweredCard = SeededRandom.Value(seed) <= (0.21f - empoweredCards * 0.05f);
                    seed += 1000;
                    continue;
                }
                List<CardInfo> infos = turnPlan[i].Randomize().ToList();
                CardInfo infoToReplace = infos.Find(x => QLIPPOTH_CARDS.ContainsKey(x.name));
                if (infoToReplace == null) {
                    List<string> possibilities = keyOverride != null ? ORDEAL_QLIPPOTH_CARDS[keyOverride] : QLIPPOTH_CARDS.Keys.ToList();
                    string name = possibilities.GetSeededRandom(SaveManager.SaveFile.GetCurrentRandomSeed() + GlobalTriggerHandler.Instance.NumTriggersThisBattle);
                    CardInfo card = CardLoader.GetCardByName(name);
                    card.Mods.Add(QLIPPOTH_CARDS[name]);
                    card.Mods.Add(new() { singletonId = QLIPPOTH_ID });
                    if (turnPlan[i].Count < 4) {
                        turnPlan[i].Add(card);
                    }
                    else if (infos.Exists(x => x.LacksTrait(LobotomyCardManager.Ordeal))) {
                        infos.RemoveAll(x => x.HasTrait(LobotomyCardManager.Ordeal));
                        infoToReplace = infos.GetRandom();
                        turnPlan[i][turnPlan[i].IndexOf(infoToReplace)] = card;
                    }
                    else {
                        continue;
                    }
                }
                else {
                    infoToReplace.Mods.Add(QLIPPOTH_CARDS[infoToReplace.name]);
                    infoToReplace.Mods.Add(new() { singletonId = QLIPPOTH_ID });
                }
                empoweredCards++;
                addEmpoweredCard = SeededRandom.Value(seed) <= (0.21f - empoweredCards * 0.05f);
                seed += 1000;
            }
            return turnPlan;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(Part1Opponent), nameof(Part1Opponent.QueueNewCards))]
        private static IEnumerator TryActivateQlippothUI(IEnumerator result, Part1Opponent __instance, bool doTween, bool changeView) {
            List<PlayableCard> oldQueue = new(__instance.Queue);
            yield return result;
            PlayableCard playableCard = __instance.Queue.Find(c => c.Info.Mods.Exists(x => x.singletonId == QLIPPOTH_ID));
            if (playableCard != null && !oldQueue.Contains(playableCard)) {
                ChallengeActivationUI.TryShowActivation(QlippothMeltdown.Id);
            }
        }
        [HarmonyPostfix, HarmonyPatch(typeof(Opponent), nameof(Opponent.ModifyTurnPlan))]
        private static void TryAddQlippothCards(ref List<List<CardInfo>> __result, List<List<CardInfo>> turnPlan) {
            if (LobotomyConfigManager.ChallengeIsActive(QlippothMeltdown.Id)) {
                __result = AddEmpoweredCardsToTurnPlan(turnPlan);
            }
        }
    }
}
