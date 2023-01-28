using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            string rulebookDescription = ConfigManager.Instance.RevealWhiteNight ? "This card will enter a downed state instead of dying, recovering at the start of the owner's turn." : "Thou wilt abandon flesh and be born again.";
            const string dialogue = "[c:bR]Ye who are full of blessings, rejoice. For I am with ye.[c:bR]";

            Apostle.ability = AbilityHelper.CreateAbility<Apostle>(
                Resources.sigilApostle, Resources.sigilApostle_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                addModular: false, opponent: false, canStack: false, isPassive: false,
                overrideModular: true).Id;
        }
    }
    public class Apostle : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string downedDialogue = "[c:bR]None of you can leave my side until I permit you.[c:]";
        private readonly string hammeredDialogue = "[c:bR]Be at ease. No calamity shall be able to trouble you.[c:]";

        private bool SpecialApostle => base.Card.Info.name.Contains("apostleGuardian") || base.Card.Info.name.Contains("Moleman");
        private int downCount = 0;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
            {
                return killer.Info.name != "wstl_apostleHeretic" && killer.Info.name != "wstl_whiteNight";
            }
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            // clear the slot if it's filled
            if (base.Card.Slot.Card != null)
                yield return base.Card.Slot.Card.DieTriggerless();

            // Special Apostles always go down when killed
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
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(downedInfo, base.Card.Slot, 0.15f);
                yield return new WaitForSeconds(0.2f);
                if (!WstlSaveManager.ApostleDowned && !SpecialApostle)
                {
                    WstlSaveManager.ApostleDowned = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(downedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                }
                yield break;
            }
            // Recreate this card in the same slot
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
            yield return new WaitForSeconds(0.2f);
            if (!WstlSaveManager.ApostleKilled)
            {
                WstlSaveManager.ApostleKilled = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammeredDialogue, -0.65f, 0.4f, Emotion.Laughter, speaker: DialogueEvent.Speaker.Bonelord);
            }
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
