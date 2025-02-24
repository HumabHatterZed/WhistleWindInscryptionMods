using DiskCardGame;
using InscryptionAPI.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhistleWind.Core.Helpers
{
    public static class HelperMethods
    {
        public static bool CompareSingleton(string id, string comparer)
        {
            return !String.IsNullOrEmpty(id) && id == comparer;
        }
        public static bool StartsWithSingleton(string id, string comparer)
        {
            return !String.IsNullOrEmpty(id) && id.StartsWith(comparer);
        }
        public static bool EndsWithSingleton(string id, string comparer)
        {
            return !String.IsNullOrEmpty(id) && id.EndsWith(comparer);
        }

        public static bool CardEquals(CardInfo baseInfo, CardInfo comparer)
        {
            if (baseInfo.name == comparer.name)
            {
                if (baseInfo.Attack == comparer.Attack && baseInfo.Health == comparer.Health)
                {
                    return baseInfo.Abilities.SequenceEqual(comparer.Abilities);
                }
            }
            return false;
        }

        public static bool CardModEquals(CardModificationInfo baseMod, CardModificationInfo comparer)
        {
            if (baseMod.singletonId.Equals(comparer.singletonId))
            {
                return baseMod.attackAdjustment == comparer.attackAdjustment &&
                    baseMod.healthAdjustment == comparer.healthAdjustment &&
                    baseMod.bloodCostAdjustment == comparer.bloodCostAdjustment &&
                    baseMod.bonesCostAdjustment == comparer.bonesCostAdjustment &&
                    baseMod.energyCostAdjustment == comparer.energyCostAdjustment &&
                    baseMod.fromCardMerge == comparer.fromCardMerge &&
                    baseMod.fromTotem == comparer.fromTotem &&
                    baseMod.fromOverclock == comparer.fromOverclock &&
                    baseMod.fromDuplicateMerge == comparer.fromDuplicateMerge &&
                    baseMod.fromLatch == comparer.fromLatch &&
                    (baseMod.abilities?.SequenceEqual(comparer.abilities) ?? baseMod.abilities == comparer.abilities) &&
                    baseMod.statIcon == comparer.statIcon &&
                    (baseMod.addGemCost?.SequenceEqual(comparer.addGemCost) ?? baseMod.addGemCost == comparer.addGemCost) &&
                    (baseMod.specialAbilities?.SequenceEqual(comparer.specialAbilities) ?? baseMod.specialAbilities == comparer.specialAbilities) &&
                    baseMod.nameReplacement.Equals(comparer.nameReplacement) &&
                    (baseMod.DecalIds?.SequenceEqual(comparer.DecalIds) ?? baseMod.DecalIds == comparer.DecalIds) &&
                    baseMod.buildACardPortraitInfo?.spriteIndices == comparer.buildACardPortraitInfo?.spriteIndices &&
                    baseMod.bountyHunterInfo?.faceIndex == comparer.bountyHunterInfo?.faceIndex &&
                    baseMod.bountyHunterInfo?.dialogueIndex == comparer.bountyHunterInfo?.dialogueIndex &&
                    baseMod.bountyHunterInfo?.hatIndex == comparer.bountyHunterInfo?.hatIndex &&
                    baseMod.bountyHunterInfo?.eyesIndex == comparer.bountyHunterInfo?.eyesIndex;
            }
            return false;
        }

        public static IEnumerator PlayTruncated3DSound(string soundId, float skipToTime, PlayableCard card)
        {
            AudioSource ocean = AudioController.Instance.PlaySound3D(soundId, MixerGroup.TableObjectsSFX, card.Slot.transform.position, skipToTime: skipToTime);
            yield return new WaitUntil(() => ocean.time >= (ocean.clip.length * 0.15f));
            ocean.Stop();
        }

        public static EncounterBlueprintData.CardBlueprint NewDifficultyCard(string card, string replacement, int difficultyReq)
        {
            return EncounterManager.NewCardBlueprint(card, difficultyReplace: true, difficultyReplaceReq: difficultyReq, replacement: replacement);
        }

        public static bool IsCardInfoOrCopy(CardInfo parentInfo, CardInfo compareInfo)
        {
            if (parentInfo != null && compareInfo != null && parentInfo.name == compareInfo.name)
            {
                return (parentInfo.Mods.Count == 0 && compareInfo.Mods.Count == 0) || parentInfo.Mods.Intersect(compareInfo.Mods).Any();
            }
            return false;
        }
        // plays hit anim then triggers Die, doesn't destroy the card object
        public static IEnumerator DieDontDestroy(PlayableCard card, bool wasSacrifice, PlayableCard killer)
        {
            card.Anim.PlayHitAnimation();
            card.Anim.SetShielded(shielded: false);
            yield return card.Anim.ClearLatchAbility();
            if (card.TriggerHandler.RespondsToTrigger(Trigger.Die, wasSacrifice, killer))
                yield return card.TriggerHandler.OnTrigger(Trigger.Die, wasSacrifice, killer);
        }

        public static T CopyAbilityBehaviour<T>(T original, GameObject gameObject) where T : AbilityBehaviour
        {
            System.Type type = original.GetType();
            Component component = gameObject.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (!field.IsLiteral)// && !field.IsInitOnly) // don't mess with constants
                    field.SetValue(component, field.GetValue(original));
            }
            return component as T;
        }
        public static T CopySpecialCardBehaviour<T>(T original, GameObject gameObject) where T : SpecialCardBehaviour
        {
            System.Type type = original.GetType();
            Component component = gameObject.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (!field.IsLiteral)// && !field.IsInitOnly) // don't mess with constants
                    field.SetValue(component, field.GetValue(original));
            }
            return component as T;
        }
        public static IEnumerator HealCard(int amount, PlayableCard card, float postWait = 0.1f, Action<PlayableCard> onHealCallback = null)
        {
            bool faceDown = card.FaceDown;
            yield return card.FlipFaceUp(faceDown);
            card.Anim.LightNegationEffect();
            card.HealDamage(amount);
            onHealCallback?.Invoke(card);
            yield return new WaitForSeconds(postWait);
            yield return card.FlipFaceDown(faceDown);
            if (faceDown)
                yield return new WaitForSeconds(0.4f);
        }
        public static void RemoveCardFromDeck(CardInfo info)
        {
            if (SaveManager.SaveFile.IsPart2)
            {
                SaveManager.SaveFile.CurrentDeck.RemoveCard(info);
                SaveManager.SaveFile.gbcData.collection.RemoveCardByName(info.name);
            }
            else
            {
                if (SaveManager.SaveFile.CurrentDeck.Cards.Contains(info))
                    SaveManager.SaveFile.CurrentDeck.RemoveCard(info);
                else
                    SaveManager.SaveFile.CurrentDeck.RemoveCardByName(info.name);
            }
        }
        public static IEnumerator FlipFaceUp(this PlayableCard card, bool alreadyFaceDown, float wait = 0.3f)
        {
            if (!alreadyFaceDown)
                yield break;

            card.SetFaceDown(false);
            card.UpdateFaceUpOnBoardEffects();
            yield return new WaitForSeconds(wait);
        }
        public static IEnumerator FlipFaceDown(this PlayableCard card, bool setFaceDown, float wait = 0.3f)
        {
            // if set down and we're down OR set up and we're up
            if ((setFaceDown && card.FaceDown) || (!setFaceDown && !card.FaceDown))
                yield break;

            if (setFaceDown)
                card.SetCardbackSubmerged();

            card.SetFaceDown(setFaceDown);

            if (!setFaceDown)
                card.UpdateFaceUpOnBoardEffects();

            yield return new WaitForSeconds(wait);
        }

        public static CardInfo GetInfoWithMods(PlayableCard card, string name)
        {
            CardInfo cardByName = CardLoader.GetCardByName(name);
            foreach (CardModificationInfo item in card.Info.Mods.FindAll((x) => !x.nonCopyable))
            {
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                cardByName.Mods.Add(cardModificationInfo);
            }
            return cardByName;
        }
        public static IEnumerator ChangeCurrentView(View view, float startDelay = 0.2f, float endDelay = 0.2f, bool immediate = false, bool lockAfter = false)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != view)
            {
                yield return new WaitForSeconds(startDelay);
                Singleton<ViewManager>.Instance.SwitchToView(view, immediate, lockAfter);
                yield return new WaitForSeconds(endDelay);
            }
        }
    }
}
