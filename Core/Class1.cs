using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using System;
using System.Collections;

namespace BellBoyOhJoy
{
    [HarmonyPatch(typeof(BoardManager))]
    public static class BellistPatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(BoardManager.ResolveCardOnBoard))]
        public static IEnumerator BellistResolve(IEnumerator enumerator, BoardManager __instance, PlayableCard card, CardSlot slot, float tweenLength, Action landOnBoardCallback, bool resolveTriggers)
        {
            // first check if the resolving card has CreateBells
            // if it does, run custom code, otherwise return vanilla behaviour

            if (card.HasAbility(Ability.CreateBells))
            {
                // this is exactly the same as the vanilla code, but now with a check for random
                yield return __instance.AssignCardToSlot(card, slot, tweenLength, delegate
                {
                    if (card.IsPlayerCard())
                        card.Anim.PlayLandOnBoardEffects();

                    landOnBoardCallback?.Invoke();
                    card.OnPlayed();
                }, resolveTriggers);

                // here we trigger ResolveOnBoard
                // before that though we add the Daus special
                if (resolveTriggers)
                {
                    if (card.LacksSpecialAbility(SpecialTriggeredAbility.Daus))
                        card.AddPermanentBehaviour<Daus>();

                    // Trigger ResolveOnBoard for this card so other possessed abilities don't break
                    if (card.TriggerHandler.RespondsToTrigger(Trigger.ResolveOnBoard))
                        yield return card.TriggerHandler.OnTrigger(Trigger.ResolveOnBoard);

                    // trigger OnOtherResolve like normal
                    yield return Singleton<GlobalTriggerHandler>.Instance.TriggerCardsOnBoard(Trigger.OtherCardResolve, false, card);
                }
                if (Singleton<TurnManager>.Instance.IsPlayerTurn)
                    __instance.playerCardsPlayedThisRound.Add(card.Info);

                yield break;
            }
            yield return enumerator;
        }
    }
    [HarmonyPatch(typeof(Daus))]
    public static class DausPatch
    {
        [HarmonyPostfix, HarmonyPatch(nameof(Daus.RespondsToOtherCardDealtDamage))]
        public static void BellistRetaliate(PlayableCard attacker, PlayableCard target, ref bool __result, ref Daus __instance)
        {
            // repeat the vanilla code but add a check for the base card
            if (!attacker.Dead)
            {
                if (__instance.PlayableCard.Attack == 0)
                    __result = false;
                else if (target != __instance.PlayableCard && (bool)target?.HasAbility(Ability.CreateBells))
                    __result = true;
            }
        }
    }
}