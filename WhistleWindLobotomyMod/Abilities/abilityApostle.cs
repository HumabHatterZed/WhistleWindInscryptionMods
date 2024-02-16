using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class ApostleSigil : AbilityBehaviour, IModifyDamageTaken
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int downCount = 0;

        private bool Saviour => BoardManager.Instance.GetSlotsCopy(!base.Card.OpponentCard).Exists(s => s.Card != null && s.Card.HasAbility(TrueSaviour.ability));
        private bool Downed => base.Card.Info.name.Contains("Down");

        public override bool RespondsToUpkeep(bool playerUpkeep) => Downed && base.Card.OpponentCard != playerUpkeep;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            downCount++;
            if (downCount < 2)
                yield break;

            downCount = 0;
            yield return base.PreSuccessfulTriggerSequence();
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            base.Card.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.2f);
            if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.5f);
                yield return DialogueHelper.PlayAlternateDialogue(delay: 0f, dialogue: "[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:]");
                base.SetLearned();
            }
            yield return ReviveApostle();
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            // if killed by WhiteNight or One Sin, die normally
            if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                yield break;

            if (Downed)
            {
                // play dialogue if WhiteNight is present (cannot be killed)
                if (Saviour)
                    yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleKilledByNull");

                yield break;
            }

            yield return DownApostle();
            if (Saviour)
                yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleDowned");
        }

        private IEnumerator DownApostle()
        {
            CardInfo downedInfo = base.Card.Info.name switch
            {
                "wstl_apostleGuardian" => CardLoader.GetCardByName("wstl_apostleGuardianDown"),
                "wstl_apostleMoleman" => CardLoader.GetCardByName("wstl_apostleMolemanDown"),
                "wstl_apostleSpear" => CardLoader.GetCardByName("wstl_apostleSpearDown"),
                "wstl_apostleStaff" => CardLoader.GetCardByName("wstl_apostleStaffDown"),
                "wstl_apostleScythe" => CardLoader.GetCardByName("wstl_apostleScytheDown"),
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
        private IEnumerator ReviveApostle()
        {
            CardInfo risenInfo = base.Card.Info.name switch
            {
                "wstl_apostleGuardianDown" => CardLoader.GetCardByName("wstl_apostleGuardian"),
                "wstl_apostleMolemanDown" => CardLoader.GetCardByName("wstl_apostleMoleman"),
                "wstl_apostleSpearDown" => CardLoader.GetCardByName("wstl_apostleSpear"),
                "wstl_apostleStaffDown" => CardLoader.GetCardByName("wstl_apostleStaff"),
                _ => CardLoader.GetCardByName("wstl_apostleScythe")
            };
            yield return new WaitForSeconds(0.2f);
            yield return base.Card.TransformIntoCard(risenInfo, ResetDamage);
            yield return new WaitForSeconds(0.5f);
        }

        private void ResetDamage() => base.Card.Status.damageTaken = 0;

        public bool RespondsToModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            return base.Card == target && Downed && Saviour;
        }

        public int OnModifyDamageTaken(PlayableCard target, int damage, PlayableCard attacker, int originalDamage)
        {
            target.Anim.StrongNegationEffect();
            return 0;
        }

        public int TriggerPriority(PlayableCard target, int damage, PlayableCard attacker) => 0;
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            const string dialogue = "";

            ApostleSigil.ability = LobotomyAbilityHelper.CreateAbility<ApostleSigil>(
                "sigilApostle",
                rulebookName, "'Thou wilt abandon flesh and be born again.'", dialogue, powerLevel: -3,
                canStack: false).Id;
        }
    }
}
