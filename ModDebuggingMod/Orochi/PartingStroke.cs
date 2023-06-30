/*using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.AbilityClasses;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    [HarmonyPatch]
    internal class TestPatch
    {
        [HarmonyPrefix, HarmonyPatch(typeof(AbilityBehaviour), nameof(AbilityBehaviour.Card), MethodType.Getter)]
        private static bool DummyCardWorkAround(AbilityBehaviour __instance, ref PlayableCard __result)
        {
            // null check doesn't work so we use try-catch
            try
            {
                // vanilla code; this is what's causing the error
                // essentially negateAbilities removes ability triggers by destroying the associated object
                // so the error results from trying to get components of a non-existent object (i think)
                __result = __instance.GetComponent<PlayableCard>();
            }
            catch
            {
                // if we get an error, provide a dummy PlayableCard with an empty CardInfo
                // this handles Sentry's NumShots check, which looks at the CardInfo
                __result = new() { Info = ScriptableObject.CreateInstance<CardInfo>() };
            }
            return false;
        }
    }
    public class PartingStroke : SniperSelectSlot
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int Priority => int.MinValue + 1;
        public override bool IsPositiveEffect => false;
        public override List<CardSlot> InitialTargets => BoardManager.Instance.GetSlotsCopy(base.Card.OpponentCard);

        public override bool RespondsToOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (fromCombat && killer != null)
            {
                return base.Card.Slot == deathSlot;*//* && deathSlot != base.Card.Slot*//*;
            }
            return false;
        }
        public override bool SlotIsNotValid(CardSlot slot)
        {
            return base.SlotIsNotValid(slot) || slot.Card.HasAnyOfTraits(Trait.Giant, Trait.Uncuttable);
        }
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            yield return PreSuccessfulTriggerSequence();
            //Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, true);
            yield return new WaitForSeconds(0.1f);
            Singleton<FirstPersonController>.Instance.AnimController.SpawnFirstPersonAnimation("FirstPersonBleachBrush");
            yield return new WaitForSeconds(0.4f);
            AudioController.Instance.PlaySound2D("magnificus_brush_splatter_bleach", MixerGroup.None, 0.5f);

            CardModificationInfo mod = new();
*//*            foreach (var v in slot.Card.TriggerHandler.triggeredAbilities)
            {
                if (!v.Item2.Activating)
                    mod.negateAbilities.Add(v.Item1);
                else
            }*//*
            mod.negateAbilities.AddRange(CardExtensions.AllAbilities(slot.Card).Distinct().ToList());

            slot.Card.Anim.PlayTransformAnimation();
            slot.Card.AddTemporaryMod(mod);

            yield return new WaitForSeconds(0.2f);
        }
        public override IEnumerator OnOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.SelectionSequence();
        }
        *//*
        public override IEnumerator OnOtherCardPreDeath(CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            GameObject clawPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/LatchClaw");
            List<CardSlot> validTargets = Singleton<BoardManager>.Instance.AllSlotsCopy;
            validTargets.RemoveAll((CardSlot x) => x.Card == null || x.Card.Info.Abilities == null || x.Card.Dead || x.Card == deathSlot.Card || x.Card.HasAbility(ability));
            if (validTargets.Count <= 0)
            {
                yield break;
            }
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            deathSlot.Card.Anim.PlayHitAnimation();
            yield return new WaitForSeconds(0.1f);
            DiskCardAnimationController cardAnim = deathSlot.Card.Anim as DiskCardAnimationController;
            GameObject claw = UnityEngine.Object.Instantiate(clawPrefab, cardAnim.WeaponParent.transform);
            CardSlot selectedSlot = null;
            if (deathSlot.Card.OpponentCard)
            {
                yield return new WaitForSeconds(0.3f);
                yield return AISelectTarget(validTargets, delegate (CardSlot s)
                {
                    selectedSlot = s;
                }
                );
                if (selectedSlot != null && selectedSlot.Card != null)
                {
                    cardAnim.AimWeaponAnim(selectedSlot.transform.position);
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                List<CardSlot> allSlotsCopy = Singleton<BoardManager>.Instance.AllSlotsCopy;
                allSlotsCopy.Remove(deathSlot.Card.Slot);
                yield return Singleton<BoardManager>.Instance.ChooseTarget(allSlotsCopy, validTargets, delegate (CardSlot s)
                {
                    selectedSlot = s;
                }, OnInvalidTarget, delegate (CardSlot s)
                {
                    if (s.Card != null)
                    {
                        cardAnim.AimWeaponAnim(s.transform.position);
                    }
                }, null, CursorType.Target);
            }
            CustomCoroutine.FlickerSequence(delegate
            {
                claw.SetActive(value: true);
            }, delegate
            {
                claw.SetActive(value: false);
            }, startOn: true, endOn: false, 0.05f, 2);
            if (selectedSlot != null && selectedSlot.Card != null)
            {

                CardModificationInfo SubmergeMod = new(Ability.Submerge);
                List<Ability> strokeList  = selectedSlot.Card.Info.Abilities;
                strokeList.Sort();
                CardModificationInfo strokeMod = new CardModificationInfo() 
                {
                    negateAbilities = new List<Ability>
                    {
                        selectedSlot.Card == deathSlot.Card.OpponentCard ? strokeList.Last() : strokeList.First() 
                    } 
                };
                selectedSlot.Card.Anim.ShowLatchAbility();
                selectedSlot.Card.AddTemporaryMod(strokeMod);
                OnSuccessfullyLatched(selectedSlot.Card);
                yield return new WaitForSeconds(0.75f);
                yield return LearnAbility();
            }
        }

        protected virtual void OnSuccessfullyLatched(PlayableCard target)
        {
        }

        private IEnumerator AISelectTarget(List<CardSlot> validTargets, Action<CardSlot> chosenCallback)
        {
            if (validTargets.Count > 0)
            {
                validTargets.Sort((CardSlot a, CardSlot b) => AIEvaluateTarget(b.Card, true) - AIEvaluateTarget(a.Card, true));
                chosenCallback(validTargets[0]);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                base.Card.Anim.LightNegationEffect();
                yield return new WaitForSeconds(0.2f);
            }
        }

        private int AIEvaluateTarget(PlayableCard card, bool positiveEffect)
        {
            int num = card.PowerLevel;
            if (card.Info.HasTrait(Trait.Terrain))
            {
                num = 10 * ((!positiveEffect) ? 1 : (-1));
            }
            if (card.OpponentCard == positiveEffect)
            {
                num += 1000;
            }
            return num;
        }

        private void OnInvalidTarget(CardSlot slot)
        {
            if (slot.Card != null && slot.Card.Info.Abilities == null && !Singleton<TextDisplayer>.Instance.Displaying)
            {
                StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("It lacks a sigil...", 2.5f, 0f, Emotion.Anger));
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return base.Card != base.Card.OpponentCard && base.Card != null;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
            {
                if (slot.Card.NotDead())
                {
                    yield return slot.Card.Dead;
                    yield return base.LearnAbility(0.5f);
                }
            }
        }
        *//*
    }

    public partial class Plugin
    {
        public void Add_Ability_SpilledPaint()
        {
            const string rulebookDescription = "When non-brittle allied creatures perish in combat, its owner chooses a creature to remove all of its sigils.";

            PartingStroke.ability = AbilityHelper.CreateAbility<PartingStroke>(pluginGuid, "sigilCursed", "Parting Stroke", rulebookDescription).Id;
        }
    }
}*/