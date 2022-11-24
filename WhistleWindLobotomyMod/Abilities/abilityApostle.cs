using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        public const string ApostleHiddenDescription = "Thou wilt abandon flesh and be born again.";
        public const string ApostleRevealedDescription = "[creature] cannot die through regular means, and will instead enter a downed state upon reaching 0 Health.";
        private void Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            const string dialogue = "[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:bR]";

            Apostle.ability = AbilityHelper.CreateAbility<Apostle>(
                Artwork.sigilApostle, Artwork.sigilApostle_pixel,
                rulebookName, ApostleHiddenDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: false, isPassive: false,
                unobtainable: true).Id;
        }
    }
    public class Apostle : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool SpecialApostle => base.Card.Info.name.Contains("apostleGuardian") || base.Card.Info.name.Contains("Moleman"); // just check for Moleman as a secret l'il secret
        private int downCount = 0;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
                return killer.Info.name != "wstl_apostleHeretic" && killer.Info.name != "wstl_whiteNight";

            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // clear the slot if it's filled
            if (base.Card.Slot.Card != null)
                yield return base.Card.Slot.Card.DieTriggerless();

            if (killer != null || SpecialApostle)
            {
                // Create the downed forme of the Apostle in its slot
                CardInfo downedInfo = base.Card.Info.name switch
                {
                    "wstl_apostleGuardian" => CardLoader.GetCardByName("wstl_apostleGuardianDown"),
                    "wstl_apostleMoleman" => CardLoader.GetCardByName("wstl_apostleMolemanDown"),
                    "wstl_apostleSpear" => CardLoader.GetCardByName("wstl_apostleSpearDown"),
                    "wstl_apostleStaff" => CardLoader.GetCardByName("wstl_apostleStaffDown"),
                    _ => CardLoader.GetCardByName("wstl_apostleScytheDown")
                };
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(downedInfo, base.Card.Slot, 0.15f, false);
                yield return new WaitForSeconds(0.2f);
                if (!SpecialApostle)
                    yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleDowned");
                yield break;
            }

            // if killer == null, create a new copy of this card in the same slot
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f, false);
            yield return new WaitForSeconds(0.2f);
            yield return DialogueEventsManager.PlayDialogueEvent("WhiteNightApostleKilledByNull");
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // don't trigger on upkeep if a special Apostle or isn't a downed Apostle
            if (SpecialApostle || !base.Card.Info.name.Contains("Down"))
                return false;

            return base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            downCount++;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.LightNegationEffect();
            if (downCount >= 2)
            {
                downCount = 0;
                CardInfo risenInfo = base.Card.Info.name switch
                {
                    "wstl_apostleSpearDown" => CardLoader.GetCardByName("wstl_apostleSpear"),
                    "wstl_apostleStaffDown" => CardLoader.GetCardByName("wstl_apostleStaff"),
                    _ => CardLoader.GetCardByName("wstl_apostleScythe")
                };
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.2f);
                yield return base.LearnAbility(0.5f);
                yield return new WaitForSeconds(0.2f);
                yield return base.Card.TransformIntoCard(risenInfo);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
