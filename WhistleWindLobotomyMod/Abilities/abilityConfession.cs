using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Ability_Confession()
        {
            const string rulebookName = "Confession and Pentinence";
            const string dialogue = "[c:bG]Keep faith with unwavering resolve.[c:]";

            Confession.ability = LobotomyAbilityHelper.CreateActivatedAbility<Confession>(
                "sigilConfession",
                rulebookName, "Keep faith with unwavering resolve.", dialogue, powerLevel: -3).Id;
        }
    }
    public class Confession : ActivatedAbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool CanActivate() => base.Card.Info.name != "wstl_hundredsGoodDeeds";
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator Activate()
        {
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            CardSlot thisSlot = base.Card.Slot;
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_hundredsGoodDeeds");
            yield return base.Card.Die(false, base.Card);
            yield return new WaitForSeconds(0.5f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardInfo, thisSlot, 0.15f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                // kill WhiteNight first
                if (slot.Card.Info.name == "wstl_whiteNight" || slot.Card.Info.name.Contains("wstl_apostle"))
                {
                    if (slot.Card != base.Card)
                        slot.Card.Anim.SetShaking(true);
                }
            }
            yield return new WaitForSeconds(0.8f);
            yield return base.LearnAbility();
            yield return new WaitForSeconds(0.4f);
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                // kill WhiteNight first
                if (slot.Card.Info.name == "wstl_whiteNight")
                {
                    while (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(11, base.Card);
                        yield return new WaitForSeconds(0.25f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card.Info.name.ToLowerInvariant().Contains("apostle"))
                {
                    while (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(3, base.Card);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);

            if (Singleton<TurnManager>.Instance.Opponent.NumLives > 1)
                yield return thisSlot.Card.DieTriggerless();
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != null && killer.HasTrait(LobotomyCardManager.TraitApostle))
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);

            yield break;
        }
    }
}
