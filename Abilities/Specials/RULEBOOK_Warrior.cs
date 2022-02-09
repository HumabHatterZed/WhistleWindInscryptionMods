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
        private NewSpecialAbility SpecialAbility_Warrior()
        {
            const string rulebookName = "Warrior";
            const string rulebookDescription = "Kills the cowardly.";
            return WstlUtils.CreateSpecialAbility<CrumblingArmour>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class CrumblingArmour : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Warrior");
            }
        }
        /*
        private readonly string cowardKill = "A coward on the battlefield does not deserve to see its end.";

        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            CardSlot thisSlot = null;
            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card == base.Card))
            {
                if (slot != null)
                {
                    thisSlot = slot;
                }
            }
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot))
            {
                if (slot.Card != null)
                {
                    if (slot.Card.HasAbility(Ability.Submerge) || slot.Card.HasAbility(Ability.TailOnHit))
                    {
                        yield return new WaitForSeconds(0.25f);
                        base.Card.Anim.StrongNegationEffect();
                        yield return new WaitForSeconds(0.25f);
                        yield return slot.Card.Die(false, thisSlot.Card);
                        if (!PersistentValues.HasSeenCrumblingArmourKill)
                        {
                            PersistentValues.HasSeenCrumblingArmourKill = true;
                            yield return new WaitForSeconds(0.5f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(cowardKill, -0.65f, 0.4f);
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                }
            }
            yield break;
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            CardSlot thisSlot = null;
            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card == base.Card))
            {
                if (slot != null)
                {
                    thisSlot = slot;
                }
            }
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot))
            {
                if (slot.Card != null)
                {
                    if (slot.Card.HasAbility(Ability.Submerge) || slot.Card.HasAbility(Ability.TailOnHit))
                    {
                        yield return new WaitForSeconds(0.25f);
                        base.Card.Anim.StrongNegationEffect();
                        yield return slot.Card.Die(false, thisSlot.Card);
                        if (!PersistentValues.HasSeenCrumblingArmourKill)
                        {
                            PersistentValues.HasSeenCrumblingArmourKill = true;
                            yield return new WaitForSeconds(0.25f);
                            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(cowardKill, -0.65f, 0.4f);
                        }
                    }
                    yield return new WaitForSeconds(0.25f);
                }
            }
            yield break;
        }
        public bool ActivateOnPlay()
        {
            int num = 0;
            CardSlot thisSlot = null;
            foreach (var slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot && slot.Card == base.Card))
            {
                if (slot != null)
                {
                    thisSlot = slot;
                }
            }
            if (thisSlot != null)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(thisSlot).Where(slot => slot.Card != null))
                {
                    num++;
                }
            }
            return num > 0;
        }*/
    }
}
