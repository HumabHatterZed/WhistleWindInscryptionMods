using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddApostle() {
            const string rulebookName = "Apostle";
            ApostleSigil.ability = AbilityHelper.New<ApostleSigil>(LobotomyPlugin.pluginGuid,
                "sigilApostle", rulebookName, "This card enters a downed state instead of perishing unless already downed. Downed cards cannot be killed while an ally card is the True Saviour.", -3, true).Id;
        }
    }

    /// <summary>
    /// This card enters a downed state instead of perishing unless already downed. Downed cards cannot be killed while an ally card is the True Saviour.
    /// </summary>
    public class ApostleSigil : AbilityBehaviour, IModifyDamageTaken {
        public static Ability ability;
        public override Ability Ability => ability;

        private int downCount = 0;

        private bool Saviour => BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard).Exists(s => s.Card != null && s.Card.HasAbility(TrueSaviour.ability));
        private bool Downed => base.Card.Info.name.EndsWith("Down");

        public override bool RespondsToUpkeep(bool playerUpkeep) => Downed && base.Card.OpponentCard != playerUpkeep;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;

        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            downCount++;
            if (downCount < 2)
                yield break;

            downCount = 0;
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);
            if (!base.HasLearned) {
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayAlternateDialogue(delay: 0f, dialogue: "[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:]");
                base.SetLearned();
            }
            yield return ReviveApostle();
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            // if killed by WhiteNight or One Sin, die normally
            if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                yield break;

            if (Downed) {
                // play dialogue if WhiteNight is present (cannot be killed)
                if (Saviour)
                    yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleKilledByNull");

                yield break;
            }

            yield return DownApostle();
            if (Saviour)
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleDowned");
        }

        private IEnumerator DownApostle() {
            CardInfo downedInfo = base.Card.Info.name switch {
                Cards.apostleGuardian => CardLoader.GetCardByName(Cards.apostleGuardianDown),
                Cards.apostleMoleman => CardLoader.GetCardByName(Cards.apostleMolemanDown),
                Cards.apostleSpear => CardLoader.GetCardByName(Cards.apostleSpearDown),
                Cards.apostleStaff => CardLoader.GetCardByName(Cards.apostleStaffDown),
                Cards.apostleScythe => CardLoader.GetCardByName(Cards.apostleScytheDown),
                _ => CardLoader.GetCardByName(base.Card.Info.name)
            };

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            if (base.Card.Slot.Card != null)
                yield return base.Card.Slot.Card.TransformIntoCard(downedInfo, ResetDamage);
            else
                yield return BoardManager.Instance.CreateCardInSlot(downedInfo, base.Card.Slot);

            yield return new WaitForSeconds(0.5f);

        }
        private IEnumerator ReviveApostle() {
            CardInfo risenInfo = base.Card.Info.name switch {
                Cards.apostleGuardianDown => CardLoader.GetCardByName(Cards.apostleGuardian),
                Cards.apostleMolemanDown => CardLoader.GetCardByName(Cards.apostleMoleman),
                Cards.apostleSpearDown => CardLoader.GetCardByName(Cards.apostleSpear),
                Cards.apostleStaffDown => CardLoader.GetCardByName(Cards.apostleStaff),
                _ => CardLoader.GetCardByName(Cards.apostleScythe)
            };
            yield return new WaitForSeconds(0.2f);
            yield return base.Card.TransformIntoCard(risenInfo, ResetDamage);
            yield return new WaitForSeconds(0.5f);
        }

        private void ResetDamage() => base.Card.Status.damageTaken = 0;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            return base.Card == target && Downed && Saviour;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage) {
            target.Anim.StrongNegationEffect();
            return 0;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => -6000;
    }
}
