using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
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
                { Cards.allAroundHelper, new() { abilities = new() { Ability.StrafeSwap }, negateAbilities = new() { Ability.Strafe } } },
                { Cards.bigBird, new() { abilities = new() { Dazzling.ability } } },
                { Cards.burrowingHeaven, new(0, 1) },
                { Cards.canOfWellCheers, new() { abilities = new() { GiftGiver.ability } } },
                { Cards.derFreischutz, new(0, 2) },
                { Cards.dimensionalRefraction, new(1, 0) },
                { Cards.dingleDangle, new() { abilities = new() { Ability.SplitStrike } } },
                { Cards.dontTouchMe, new() { abilities = new() { Ability.WhackAMole } } },
                { Cards.dreamingCurrent, new(0, 1) },
                { Cards.drownedSisters, new(0, 1) { abilities = new() { Ability.GuardDog } } },
                { Cards.eyeballChick_mook, new() { abilities = new() { Piercing.ability } } },
                { Cards.fairyFestival, new(0, 2) },
                { Cards.forestKeeper_mook, new(1, 1) },
                { Cards.forsakenMurderer, new(0, 2) },
                { Cards.fragmentOfUniverse, new() { abilities = new() { Ability.DebuffEnemy } } },
                { Cards.funeralOfButterflies, new(0, 1) { abilities = new() { Ability.DoubleStrike } } },
                { Cards.happyTeddyBear, new(2, 0) },
                { Cards.heartOfAspiration, new() { abilities = new() { FlagBearer.ability } } },
                { Cards.judgementBird, new(1, 0) },
                { Cards.laetitia, new() { abilities = new() { GiftGiver.ability } }  },
                { Cards.magicalGirlHeart, new() { abilities = new() { Ability.Sentry } } },
                { Cards.magicalGirlSpade, new(1, 0) },
                { Cards.mhz176, new() { negateAbilities = new() { Ability.BuffEnemy } } },
                { Cards.oldLady, new(0) { abilities = new() { Scorching.ability } } },
                { Cards.oneSin, new() { abilities = new() { Idol.ability } } },
                { Cards.punishingBird, new(1, 0) { abilities = new() { Woodcutter.ability } } },
                { Cards.redHoodedMercenary, new(0, 2) },
                { Cards.runawayBird_mook, new(0, 1) { abilities = new() { Persistent.ability } } },
                { Cards.schadenfreude, new() { abilities = new() { Ability.Sentry, NimbleFoot.ability } } },
                { Cards.silentGirl, new() { abilities = new() { OneSided.ability } } },
                { Cards.singingMachine, new() { negateAbilities = new() { Aggravating.ability } } },
                { Cards.snowQueen, new() { abilities = new() { Ability.BuffNeighbours } } },
                { Cards.snowWhitesApple, new(0, 1) { abilities = new() { Ability.Deathtouch } } },
                { Cards.theFirebird, new() { abilities = new() { Scorching.ability, Scorching.ability } } },
                { Cards.theNakedNest, new(0, 2) { abilities = new() { Ability.WhackAMole } } },
                { Cards.theresia, new(0, 1) { abilities = new() { Healer.ability } } },
                { Cards.theRoadHome, new(0, 2) { abilities = new() { NimbleFoot.ability } } },
                { Cards.trainingDummy, new(1, 2) },
                { Cards.voidDream, new(1, 1) },
                { Cards.wallLady, new(0, 1) { abilities = new() { Ability.Sharp } } },
                { Cards.warmHeartedWoodsman, new() { abilities = new() { ThickSkin.ability } } },
                { Cards.weCanChangeAnything, new(1, 2) },
                { Cards.whiteLake, new(1, 0) { abilities = new() { Ability.BuffNeighbours } } },
                { Cards.willBeBadWolf, new(1, 0) },
                { Cards.wisdomScarecrow, new(0, 3) { abilities = new() { MindStrike.ability } } },
                { Cards.yang, new() { abilities = new() { Ability.MoveBeside } } },
                { Cards.youMustBeHappy, new(0, 2) }
            };

            ORDEAL_QLIPPOTH_CARDS.Add("Mechanical", new() { Cards.allAroundHelper, Cards.canOfWellCheers, Cards.schadenfreude, Cards.singingMachine, Cards.trainingDummy, Cards.weCanChangeAnything });
            ORDEAL_QLIPPOTH_CARDS.Add("Divine", new() { Cards.burrowingHeaven, Cards.fleshIdol, Cards.fragmentOfUniverse, Cards.oneSin, Cards.yin, Cards.yang, Cards.dontTouchMe });
            ORDEAL_QLIPPOTH_CARDS.Add("Fae", new() { Cards.fairyFestival, Cards.laetitia, Cards.magicalGirlHeart, Cards.magicalGirlSpade, Cards.snowQueen, Cards.theRoadHome });
            ORDEAL_QLIPPOTH_CARDS.Add("Insect", new() { Cards.funeralOfButterflies, Cards.theNakedNest, Cards.dontTouchMe, Cards.forestKeeper_mook, Cards.wisdomScarecrow });
            ORDEAL_QLIPPOTH_CARDS.Add("Anthropoid", new() { Cards.oldLady, Cards.redHoodedMercenary, Cards.forsakenMurderer, Cards.dingleDangle, Cards.runawayBird_mook });

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
                    string name = keyOverride != null ? ORDEAL_QLIPPOTH_CARDS[keyOverride].GetRandom() : QLIPPOTH_CARDS.Keys.ToList().GetRandom();
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
