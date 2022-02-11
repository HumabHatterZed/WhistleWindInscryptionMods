using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Apostle()
        {
            const string rulebookName = "Apostle";
            string rulebookDescription = " Thou wilt abandon flesh and be born again.";
            const string dialogue = "Ye who are full of blessings, rejoice. For I am with ye.";

            if (ConfigHelper.Instance.RevealWhiteNight)
            {
                rulebookDescription = "While WhiteNight is on the board, this card will enter a downed state instead of dying.";
            }

            return WstlUtils.CreateAbility<Apostle>(
                Resources.sigilApostle,
                rulebookName, rulebookDescription, dialogue, -3);
        }
    }
    public class Apostle : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string downedDialogue = "I shall be with ye as I relieve you of the fear of horrifying death.";
        private readonly string hammeredDialogue = "Be at ease. No calamity shall be able to trouble you.";

        private int downCount = 0;

        private bool IsSpear => base.Card.Info.name.ToLowerInvariant().Contains("apostlespear");
        private bool IsStaff => base.Card.Info.name.ToLowerInvariant().Contains("apostlestaff");
        private bool IsDowned => base.Card.Info.name.ToLowerInvariant().Contains("apostle") && base.Card.Info.name.ToLowerInvariant().Contains("down");

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            CardInfo downedInfo = CardLoader.GetCardByName("wstl_apostleScytheDown");

            if (IsSpear) { downedInfo = CardLoader.GetCardByName("wstl_apostleSpearDown"); }
            if (IsStaff) { downedInfo = CardLoader.GetCardByName("wstl_apostleStaffDown"); }

            if (killer != null)
            {
                if (!killer.Info.name.ToLowerInvariant().Equals("wstl_hundredsgooddeeds"))
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(downedInfo, base.Card.Slot, 0.15f);
                    if (!PersistentValues.ApostleKilled)
                    {
                        yield return new WaitForSeconds(0.2f);
                        PersistentValues.ApostleKilled = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(downedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                    }
                }
            }
            else
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                if (!PersistentValues.ApostleKilled)
                {
                    yield return new WaitForSeconds(0.2f);
                    PersistentValues.ApostleKilled = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammeredDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                }
            }
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep && IsDowned;
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

                if (IsSpear) { risenInfo = CardLoader.GetCardByName("wstl_apostleSpear"); }
                if (IsStaff) { risenInfo = CardLoader.GetCardByName("wstl_apostleStaff"); }

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
