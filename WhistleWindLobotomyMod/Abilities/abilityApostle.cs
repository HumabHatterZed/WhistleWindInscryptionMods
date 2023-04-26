﻿using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public class Apostle : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int downCount = 0;

        private bool Saviour => HelperMethods.GetSlotsCopy(base.Card.OpponentCard).Exists(s => s.Card != null && s.Card.HasAbility(TrueSaviour.ability));
        private bool Downed => base.Card.Info.name.Contains("Down");

        public override bool RespondsToUpkeep(bool playerUpkeep) => Downed && base.Card.OpponentCard != playerUpkeep;
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            downCount++;
            if (downCount >= 2)
            {
                downCount = 0;
                yield return base.PreSuccessfulTriggerSequence();
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                base.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
                if (!base.HasLearned)
                {
                    yield return new WaitForSeconds(0.5f);
                    yield return HelperMethods.PlayAlternateDialogue(delay: 0f, dialogue: "[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:]");
                    base.SetLearned();
                }
                yield return ReviveApostle();
            }
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            // if killed by WhiteNight or One Sin, do nothing
            if (killer != null && killer.HasAnyOfAbilities(Confession.ability, TrueSaviour.ability))
                yield break;

            if (Downed)
            {
                if (Saviour) // Downed Apostles without WhiteNight just die
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, resolveTriggers: false);
                    yield return DialogueHelper.PlayDialogueEvent("WhiteNightApostleKilledByNull");
                }
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
                _ => CardLoader.GetCardByName(base.Card.Info.name)
            };

            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            yield return base.Card.TransformIntoCard(downedInfo, ResetDamage);
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
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            const string dialogue = "";

            Apostle.ability = LobotomyAbilityHelper.CreateAbility<Apostle>(
                Artwork.sigilApostle, Artwork.sigilApostle_pixel,
                rulebookName, "Thou wilt abandon flesh and be born again.", dialogue, powerLevel: -3,
                canStack: false).Id;
        }
    }
}
