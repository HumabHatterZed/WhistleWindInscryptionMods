using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
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

        private int downCount = 0;

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null)
            {
                if (killer.Info.name == "wstl_apostleHeretic" || killer.Info.name == "wstl_whiteNight")
                {
                    return false;
                }
            }
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (killer != null)
            {
                bool guardian = false;
                CardInfo downedInfo;
                switch (base.Card.Info.name)
                {
                    case "wstl_apostleGuardian":
                        guardian = true;
                        downedInfo = CardLoader.GetCardByName("wstl_apostleGuardianDown");
                        break;
                    case "wstl_apostleSpear":
                        downedInfo = CardLoader.GetCardByName("wstl_apostleSpearDown");
                        break;
                    case "wstl_apostleStaff":
                        downedInfo = CardLoader.GetCardByName("wstl_apostleStaffDown");
                        break;
                    default:
                        downedInfo = CardLoader.GetCardByName("wstl_apostleScytheDown");
                        break;
                }

                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(downedInfo, base.Card.Slot, 0.15f);
                yield return new WaitForSeconds(0.2f);
                if (!WstlSaveManager.ApostleDowned && !guardian)
                {
                    WstlSaveManager.ApostleDowned = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(downedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                }
            }
            else
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                yield return new WaitForSeconds(0.2f);
                if (!WstlSaveManager.ApostleKilled)
                {
                    WstlSaveManager.ApostleKilled = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammeredDialogue, -0.65f, 0.4f, Emotion.Laughter, speaker: DialogueEvent.Speaker.Bonelord);
                }
            }
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (base.Card.Info.name == "wstl_apostleScytheDown" || base.Card.Info.name == "wstl_apostleSpearDown" || base.Card.Info.name == "wstl_apostleStaffDown")
            {
                return playerUpkeep;
            }
            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();

            downCount++;
            base.Card.Anim.LightNegationEffect();
            if (downCount >= 2)
            {
                downCount = 0;
                CardInfo risenInfo = CardLoader.GetCardByName("wstl_apostleScythe");
                switch (base.Card.Info.name)
                {
                    case "wstl_apostleSpear":
                        risenInfo = CardLoader.GetCardByName("wstl_apostleSpear");
                        break;
                    case "wstl_apostleStaff":
                        risenInfo = CardLoader.GetCardByName("wstl_apostleStaff");
                        break;
                }

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
