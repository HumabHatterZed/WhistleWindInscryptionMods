using InscryptionAPI;
using InscryptionAPI.Nodes;
using InscryptionAPI.Encounters;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class CustomExtensions
    {
        public static IEnumerator DieTriggerless(this PlayableCard card)
        {
            if (!card.Dead)
            {
                card.Dead = true;
                CardSlot slotBeforeDeath = card.Slot;
                if (card.Info != null && card.Info.name.ToLower().Contains("squirrel"))
                {
                    AscensionStatsData.TryIncrementStat(AscensionStat.Type.SquirrelsKilled);
                }
                card.Anim.SetShielded(shielded: false);
                yield return card.Anim.ClearLatchAbility();
                if (card.HasAbility(Ability.PermaDeath))
                {
                    card.Anim.PlayPermaDeathAnimation();
                    yield return new WaitForSeconds(1.25f);
                }
                else
                {
                    card.Anim.PlayDeathAnimation();
                }
                if (!card.HasAbility(Ability.QuadrupleBones) && slotBeforeDeath.IsPlayerSlot)
                {
                    yield return Singleton<ResourcesManager>.Instance.AddBones(1, slotBeforeDeath);
                }
                card.UnassignFromSlot();
                card.StartCoroutine(card.DestroyWhenStackIsClear());
            }
        }

        public static void RemoveFromBoard(this PlayableCard item, bool removeFromDeck = false)
        {
            if (removeFromDeck)
            {
                RunState.Run.playerDeck.RemoveCard(item.Info);
            }
            item.UnassignFromSlot();
            SpecialCardBehaviour[] components = item.GetComponents<SpecialCardBehaviour>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnCleanUp();
            }
            item.ExitBoard(0.3f, Vector3.zero);
        }
    }
}
