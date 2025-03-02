using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.AbilityClasses;

namespace WhistleWindLobotomyMod
{
    public class OceanSodaItem : NoFirstPersonTargetSlotItem
    {
        public override View SelectionView => View.Board;
        public override bool ExtraActivationPrerequisitesMet() => GetValidTargets().Count > 0;
        public override IEnumerator OnValidTargetSelected(CardSlot target, GameObject firstPersonItem)
        {
            PlayableCard targetCard = target.Card;
            AudioController.Instance.PlaySound3D("angler_use_hook", MixerGroup.TableObjectsSFX, target.transform.position, 1f, 0.1f);
            yield return SodaAbilityBehaviour.Sequence(targetCard, OceanSodaEffect.specialAbility, Ability.Submerge, 3, OceanSoda.id);
            yield return new WaitForSeconds(0.5f);
        }

        public override void OnInvalidTargetSelected(CardSlot targetSlot)
        {
            if (targetSlot.Card != null)
            {
                if (targetSlot.Card.Info.HasTrait(Trait.Uncuttable))
                {
                    CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("You can't hook that one.", 2.5f, 0f, Emotion.Anger));
                }
                else if (targetSlot.opposingSlot.Card != null)
                {
                    CustomCoroutine.Instance.StartCoroutine(Singleton<TextDisplayer>.Instance.ShowThenClear("There's no space to pull that one into.", 3f));
                }
            }
        }

        public override List<CardSlot> GetAllTargets()
        {
            return Singleton<BoardManager>.Instance.AllSlotsCopy;
        }

        public override List<CardSlot> GetValidTargets()
        {
            List<CardSlot> allSlotsCopy = Singleton<BoardManager>.Instance.AllSlotsCopy;
            allSlotsCopy.RemoveAll(x => x.Card == null || x.Card.Info.HasAnyOfTraits(Trait.Giant, AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments));
            return allSlotsCopy;
        }
    }
}
