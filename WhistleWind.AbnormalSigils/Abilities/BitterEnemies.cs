using Core.AbilityClasses;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Ability_BitterEnemies() {
            const string rulebookName = "Vendetta";
            const string rulebookDescription = "[creature] remembers the first card that kills it. In all future battles, this card will carry a vendetta against similar cards.";
            const string dialogue = "A bitter grudge laid bare.";

            BitterEnemies.ability = AbnormalAbilityHelper.CreateAbility<BitterEnemies>(
                "sigilBitterEnemies",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false)
                .SetMechanicRedirect("vendetta", "Bitter Vendetta", GameColors.Instance.glowRed)
                .Id;
        }
    }
    /// <summary>
    /// [creature] remembers the first card that kills it. In all future battles, this card will carry a vendetta against similar cards.
    /// </summary>
    public class BitterEnemies : ModifyDamageDealtAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public const string VENDETTA_TARGET = "VendettaTarget";
        public const char GROUP_DELIMITER = ':';

        public string TargetName = null;
        public string TargetDisplayedName = null;
        public List<Tribe> TargetTribes = null;
        private List<string> TargetDisplayedNameSplit = null;

        public void Start() => ValidateVendettaMod();

        private void ValidateVendettaMod() {
            CardModificationInfo mod = base.Card.Info.Mods.Find(x => HelperMethods.CompareSingleton(x.singletonId, VENDETTA_TARGET));
            if (mod == null) {
                if (base.Card.OpponentCard) {
                    AssignRandomVendetta();
                    return;
                }
                else {
                    mod = GetModFromPlayerDeck(base.Card);
                    if (mod == null) // assume null is due to a vendetta not being set yet
                        return;
                }
            }

            AssignVendettaFromMod(mod);
        }

        public CardModificationInfo CreateVendettaMod(string name, List<Tribe> tribes) {
            CardModificationInfo retval = new() { singletonId = VENDETTA_TARGET };
            retval.singletonId += GROUP_DELIMITER + name;
            if (tribes != null && tribes.Count > 0) {
                retval.singletonId += GROUP_DELIMITER + tribes[0].ToString();
                for (int i = 1; i < tribes.Count; i++) {
                    retval.singletonId += tribes[i].ToString();
                }
            }
            return retval;
        }

        public void AssignVendettaFromMod(CardModificationInfo mod) {
            // key[0] == VENDETTA_TARGEt so we ignore
            string[] keys = mod.singletonId.Split(GROUP_DELIMITER);

            TargetName = keys[1];
            CardInfo info = CardManager.AllCardsCopy.Find(x => x.name == keys[1]);
            if (info != null && !string.IsNullOrEmpty(info.displayedName)) {
                TargetDisplayedName = info.DisplayedNameLocalized;
                TargetDisplayedNameSplit = info.displayedName.Split().ToList();
            }

            if (keys.Length > 2) {
                TargetTribes = new();
                string[] tribeSplit = keys[2].Split(';');
                for (int i = 0; i < tribeSplit.Length; i++) {
                    AbnormalPlugin.Log.LogInfo($"[Vendetta] [{tribeSplit[i]}]");
                    if (int.TryParse(tribeSplit[i], out int result))
                        TargetTribes.Add((Tribe)result);
                }
            }
        }
        public void AssignRandomVendetta() {
            CardInfo info = RunState.Run.playerDeck.Cards.GetSeededRandom(base.GetRandomSeed());
            TargetName = info.name;
            if (!string.IsNullOrEmpty(info.displayedName)) {
                TargetDisplayedName = info.DisplayedNameLocalized;
                TargetDisplayedNameSplit = info.displayedName.Split().ToList();
            }

            TargetTribes = new();
            int randSeed = base.GetRandomSeed();
            List<Tribe> allTribes = GuidManager.GetValues<Tribe>();
            foreach (Tribe tr in allTribes) {
                int maxVal = allTribes.Count - (TargetTribes.Count * TargetTribes.Count);
                if (maxVal < 1 || SeededRandom.Range(0, maxVal, randSeed++) == 0)
                    break;

                TargetTribes.Add(tr);
            }
        }

        public CardModificationInfo GetModFromPlayerDeck(PlayableCard card) {
            CardInfo fromDeck = RunState.Run.playerDeck.Cards.Find(x => HelperMethods.CardEquals(base.Card.Info, x));
            if (fromDeck == null) {
                return null;
            }
            CardModificationInfo deckMod = fromDeck.Mods.Find(x => HelperMethods.StartsWithSingleton(x.singletonId, VENDETTA_TARGET));
            return deckMod;
        }

        public void AddVendettaMod(CardModificationInfo mod) {
            List<CardInfo> matches = RunState.Run.playerDeck.Cards.FindAll(x => HelperMethods.CardEquals(base.Card.Info, x));
            AbnormalPlugin.Log.LogInfo($"[AddVendetta] {matches.Count} matches");
            if (matches.Count == 0 || matches[0].Mods.Exists(x => HelperMethods.StartsWithSingleton(x.singletonId, VENDETTA_TARGET)))
                return;

            RunState.Run.playerDeck.ModifyCard(matches[0], mod);
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => !wasSacrifice && killer != null;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            yield return base.PreSuccessfulTriggerSequence();

            if (base.Card.OpponentCard || base.Card.OriginatedFromQueue)
                yield break;

            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            if (!base.HasLearned) {
                yield return DialogueHelper.ShowUntilInput(base.Card.Info.DisplayedNameLocalized + " will remember this.");
            }
            AddVendettaMod(CreateVendettaMod(killer.Info.name, killer.Info.tribes));
        }
        public override bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return attacker == base.Card && TargetMeetsVendetta(target);
        }

        public override int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) => damage + 1;

        public override int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) => TargetMeetsVendetta(target);
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            yield return base.PreSuccessfulTriggerSequence();
            base.LearnAbility(0.4f);
        }

        public bool TargetMeetsVendetta(PlayableCard target) {
            // guaranteed activation for some duo interactions
            if ((base.Card.Info.name == "wstl_redHoodedMercenary" && target.Info.name == "wstl_willBeBadWolf") ||
                (target.Info.name == "wstl_redHoodedMercenary" && base.Card.Info.name == "wstl_willBeBadWolf")) {
                return true;
            }

            if (TargetName != null) {
                if (TargetName == target.Info.name)
                    return true;

                if (!string.IsNullOrEmpty(target.Info.displayedName) && TargetDisplayedNameSplit.Count > 0) {
                    string[] vendettaDisplayName = target.Info.displayedName.Split();
                    return TargetDisplayedNameSplit.Intersect(vendettaDisplayName).Count() >= (TargetDisplayedNameSplit.Count / 2);
                }

            }

            if (TargetTribes != null && TargetTribes.Count > 0) {
                foreach (Tribe tribe in TargetTribes) {
                    if (target.IsOfTribe(tribe))
                        return true;
                }
            }

            return false;
        }
    }
}
