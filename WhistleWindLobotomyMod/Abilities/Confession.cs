using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
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
            Singleton<ViewManager>.Instance.SwitchToView(BoardManager.Instance.CombatView);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Locked;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.55f);
            CardSlot thisSlot = base.Card.Slot;
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_hundredsGoodDeeds");
            yield return base.Card.Die(false, base.Card);
            yield return new WaitForSeconds(0.5f);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardInfo, thisSlot, 0.15f);

            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Where(x => x.HasAnyOfAbilities(ApostleSigil.ability, TrueSaviour.ability)))
            {
                card.Anim.SetShaking(true);
            }

            yield return new WaitForSeconds(0.8f);
            yield return base.LearnAbility();
            yield return new WaitForSeconds(0.4f);

            PlayableCard whiteNight = BoardManager.Instance.CardsOnBoard.Find(x => x.HasAbility(TrueSaviour.ability));
            if (whiteNight != null)
            {
                int dmgToDeal = whiteNight.Health / 6; // kill WhiteNight in 6 hits
                while (whiteNight != null && whiteNight.Health > 0)
                {
                    base.StartCoroutine(DamageApostles());
                    yield return whiteNight.TakeDamage(dmgToDeal, base.Card);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Where(x => x.HasAbility(ApostleSigil.ability)))
                    {
                        int damageToDeal = card.MaxHealth / 3;
                        yield return card.TakeDamage(damageToDeal, base.Card);
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }

            PlayableCard oneSinCard = BoardManager.Instance.CardsOnBoard.Find(x => x.Slot == thisSlot);
            if (oneSinCard != null)
            {
                ViewManager.Instance.SwitchToView(BoardManager.Instance.CombatView);
                yield return new WaitForSeconds(0.4f);
                yield return oneSinCard.Die(false, oneSinCard);
                yield return new WaitForSeconds(0.5f);
            }
            ViewManager.Instance.SwitchToView(BoardManager.Instance.DefaultView);
        }
        private IEnumerator DamageApostles()
        {
            foreach (PlayableCard card in BoardManager.Instance.CardsOnBoard.Where(x => x.HasAbility(ApostleSigil.ability)))
            {
                int damageToDeal = card.MaxHealth / 3; // kill each Apostle in 3 hits max
                yield return card.TakeDamage(damageToDeal, base.Card);
            }
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (killer != base.Card || killer.LacksAbility(this.Ability)) // when killed, reset Health
            {
                yield return new WaitForSeconds(0.15f);
                base.Card.Anim.PlayTransformAnimation();
                yield return new WaitForSeconds(0.15f);
                base.Card.Status.damageTaken = 0;
                yield return new WaitForSeconds(0.4f);
            }

            yield break;
        }
    }
}
