using DiskCardGame;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhistleWindLobotomyMod.Opponents
{
    public abstract class LobotomyBattleSequencer : BossBattleSequencer, IOnPreScalesChangedRef
    {
        public int currentExcessBones = 0;
        public bool drewInitialHand = false;

        public GameObject targetIconPrefab = ResourceBank.Get<GameObject>("Prefabs/Cards/SpecificCardModels/CannonTargetIcon");
        public readonly List<GameObject> targetIcons = new();

        public virtual bool PreventScaleDamage { get; set; } = true;
        public virtual bool DirectDamageGivesBones { get; set; } = true;
        public virtual int MaxExcessBones { get; } = 8;

        public virtual IEnumerator PreDrawOpeningHand()
        {
            yield break;
        }
        public virtual IEnumerator PostDrawOpeningHand()
        {
            yield break;
        }

        public virtual bool RespondsToPreScalesChangedRef(int damage, int numWeights, bool toPlayer)
        {
            return !toPlayer && PreventScaleDamage;
        }
        public virtual int CollectPreScalesChangedRef(int damage, ref int numWeights, ref bool toPlayer)
        {
            if (LifeManager.Instance.DamageUntilPlayerWin == 1)
                return numWeights = 0;

            if (damage >= LifeManager.Instance.DamageUntilPlayerWin)
            {
                numWeights = Mathf.Min(LifeManager.Instance.DamageUntilPlayerWin - 1, numWeights);
                return LifeManager.Instance.DamageUntilPlayerWin - 1;
            }

            return damage;
        }

        public void CreateTargetIcon(CardSlot targetSlot, Color materialColour = default)
        {
            GameObject gameObject = Instantiate(targetIconPrefab, targetSlot.transform);
            gameObject.transform.localPosition = new Vector3(0f, 0.25f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;

            if (materialColour != default)
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = materialColour;

            targetIcons.Add(gameObject);
        }
        public void CleanupTargetIcons()
        {
            targetIcons.ForEach(delegate (GameObject x)
            {
                if (x != null) CleanUpTargetIcon(x);
            });
            targetIcons.Clear();
        }
        public void CleanUpTargetIcon(GameObject icon)
        {
            Tween.LocalScale(icon.transform, Vector3.zero, 0.1f, 0f, Tween.EaseIn, Tween.LoopType.None, null, delegate
            {
                Destroy(icon);
            });
        }

        public override IEnumerator PlayerCombatEnd()
        {
            yield return base.PlayerCombatEnd();
            currentExcessBones = 0;
        }
        public override IEnumerator OpponentCombatEnd()
        {
            yield return base.OpponentCombatEnd();
            currentExcessBones = 0;
        }
    }
}
