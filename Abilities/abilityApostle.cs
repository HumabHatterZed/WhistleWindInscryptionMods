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
            const string rulebookName = "";
            string rulebookDescription = " Thou wilt abandon flesh and be born again.";
            const string dialogue = "Do not fear, for I am with thee.";

            if (WhiteNightDescRulebook)
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

        private readonly string dialogue = "Do not fear, for I am with thee.";
        private readonly string dialogue2 = "Be not frightened. I am thy savior and I shall be with thee.";

        private bool IsSpear => base.Card.Info == base.Card.Info.name.ToLowerInvariant().Contains("apostlespear");
        private bool IsStaff => base.Card.Info == base.Card.Info.name.ToLowerInvariant().Contains("apostlestaff");

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
                if (!killer.Info.name.ToLowerInvariant().Contains("hundredsgooddeeds"))
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(downedInfo, base.Card.Slot, 0.15f);
                    if (!PersistentValues.ApostleKilled)
                    {
                        yield return new WaitForSeconds(0.2f);
                        PersistentValues.ApostleKilled = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
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
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue2, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                }
            }
        }
    }
}
