﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_DerFreischutz()
        {
            const string rulebookName = "Devil";
            const string rulebookDescription = "The seventh bullet will pierce loved ones' hearts.";
            return WstlUtils.CreateSpecialAbility<DerFreischutz>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class DerFreischutz : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "The Devil proposed a childist contract.";
        private readonly string dialogue2 = "The seventh bullet would pierce the heart of his most beloved.";
        private readonly string dialogue3 = "On hearing this, the hunter sought and shot everyone he loved.";
        private int shotCount;

        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Devil");
            }
        }

        public override bool RespondsToDealDamage(int amount, PlayableCard target)
        {
            return true;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target)
        {
            shotCount++;
            if (shotCount >= 6)
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.5f);

                if (!PersistentValues.HasSeenDerFreischutzSeventh)
                {
                    PersistentValues.HasSeenDerFreischutzSeventh = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue2, -0.65f, 0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue3, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.5f);
                }

                CardSlot thisSlot = null;

                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card == base.Card))
                {
                    if (slot != null)
                    {
                        thisSlot = slot;
                    }
                }
                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(thisSlot.Card.Attack, thisSlot.Card);
                    }
                }
                shotCount = 0;
            }
            // Plugin.Log.LogInfo($"{shotCount} bullets fired.");
            yield break;
        }

        public override bool RespondsToDealDamageDirectly(int amount)
        {
            return true;
        }
        public override IEnumerator OnDealDamageDirectly(int amount)
        {
            shotCount++;
            if (shotCount >= 6)
            {
                yield return new WaitForSeconds(0.25f);
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.5f);

                if (!PersistentValues.HasSeenDerFreischutzSeventh)
                {
                    PersistentValues.HasSeenDerFreischutzSeventh = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue2, -0.65f, 0.4f);
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue3, -0.65f, 0.4f);
                    yield return new WaitForSeconds(0.5f);
                }

                CardSlot thisSlot = null;

                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card == base.Card))
                {
                    if (slot != null)
                    {
                        thisSlot = slot;
                    }
                }
                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(true).Where(slot => slot && slot.Card != base.Card))
                {
                    if (slot.Card != null)
                    {
                        yield return slot.Card.TakeDamage(thisSlot.Card.Attack, thisSlot.Card);
                    }
                }
                shotCount = 0;
            }
            // Plugin.Log.LogInfo($"{shotCount} bulletss fired.");
            yield break;
        }
    }
}
