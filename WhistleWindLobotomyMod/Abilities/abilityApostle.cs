using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        public const string ApostleHiddenDescription = "Thou wilt abandon flesh and be born again.";
        public const string ApostleRevealedDescription = "[creature] cannot die through regular means, and will instead transform into a downed forme upon reaching 1 Health.";
        private void Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            const string dialogue = "";

            Apostle.ability = LobotomyAbilityHelper.CreateAbility<Apostle>(
                Artwork.sigilApostle, Artwork.sigilApostle_pixel,
                rulebookName, ApostleHiddenDescription, dialogue, powerLevel: -3,
                canStack: false).Id;
        }
    }
    public class Apostle : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool SpecialApostle => base.Card.Info.name.Contains("apostleGuardian") || base.Card.Info.name.Contains("Moleman"); // just check for Moleman as a secret l'il secret
        private int downCount = 0;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // don't trigger on upkeep if a special Apostle or isn't a downed Apostle
            if (SpecialApostle || !base.Card.Info.name.Contains("Down"))
                return false;

            return base.Card.OpponentCard != playerUpkeep;
        }
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
                return killer.Info.name != "wstl_apostleHeretic" && killer.Info.name != "wstl_whiteNight";

            return true;
        }
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source != null)
                return source.Info.name != "wstl_apostleHeretic" && source.Info.name != "wstl_whiteNight";

            return true;
        }

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
                if (base.HasLearned)
                {
                    yield return new WaitForSeconds(0.5f);
                    yield return HelperMethods.PlayAlternateDialogue(delay: 0f, dialogue: $"[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:bR]");
                    base.SetLearned();
                }
                yield return ReviveApostle();
            }
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            CardInfo downedInfo = DownApostle();
            yield return base.PreSuccessfulTriggerSequence();

            yield return new WaitForSeconds(0.2f);
            yield return base.Card.TransformIntoCard(downedInfo, ResetDamage);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(0.2f);

            if (killer != null && !SpecialApostle)
                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleDowned");
            else
                yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleKilledByNull");
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            if (base.Card.Health == 0)
            {
                yield return base.PreSuccessfulTriggerSequence();
                CardInfo downedInfo = DownApostle();
                yield return new WaitForSeconds(0.2f);
                yield return base.Card.TransformIntoCard(downedInfo, ResetDamage);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private CardInfo DownApostle()
        {
            return base.Card.Info.name switch
            {
                "wstl_apostleGuardian" => CardLoader.GetCardByName("wstl_apostleGuardianDown"),
                "wstl_apostleMoleman" => CardLoader.GetCardByName("wstl_apostleMolemanDown"),
                "wstl_apostleSpear" => CardLoader.GetCardByName("wstl_apostleSpearDown"),
                "wstl_apostleStaff" => CardLoader.GetCardByName("wstl_apostleStaffDown"),
                "wstl_apostleScythe" => CardLoader.GetCardByName("wstl_apostleScytheDown"),
                _ => CardLoader.GetCardByName(base.Card.Info.name)
            };
        }
        private IEnumerator ReviveApostle()
        {
            CardInfo risenInfo = base.Card.Info.name switch
            {
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
}
