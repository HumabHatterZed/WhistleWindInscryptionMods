using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddDelusion() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Delusion";
            info.rulebookDescription = "At the start of the owner's turn, reduce this sigil's counter by 1. If the counter is 0, activate this card's God sigil, deal 1 damage directly to the player, then reset the counter to 2~3.";
            info.powerLevel = 0;

            Delusion.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Delusion), TextureLoader.LoadTextureFromFile("sigilDelusion.png")).Id;
        }
    }

    public class Delusion : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private int counter;
        private GodColourAbilityBehaviour behav = null;
        private bool triggerHalfHealth = true;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            counter = SeededRandom.Range(2, 5, base.GetRandomSeed() + base.Card.Slot.Index);
            behav = base.Card.TriggerHandler.GetComponent<GodColourAbilityBehaviour>();
            base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
            base.Card.RenderCard();
            yield return new WaitForSeconds(0.4f);
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            counter--;
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
            base.Card.RenderCard();
            yield return new WaitForSeconds(0.4f);
            if (counter == 1) {
                // play sound to indicate it's about to pop
                yield return behav.OnPreActivate(false);
            }
            else if (counter < 1) {
                GlobalTriggerHandler.Instance.NumTriggersThisBattle++;
                yield return behav.OnActivate(false);
                counter = SeededRandom.Range(2, 4, base.GetRandomSeed() + TurnManager.Instance.TurnNumber + base.Card.Slot.Index);
                base.Card.Anim.LightNegationEffect();
                base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
                base.Card.RenderCard();
                yield return new WaitForSeconds(0.75f);
                yield return LifeManager.Instance.ShowDamageSequence(1, 1, true);
            }
        }

        public override bool RespondsToTakeDamage(PlayableCard source) => triggerHalfHealth && (float)base.Card.Health / base.Card.MaxHealth <= 0.5f;
        public override IEnumerator OnTakeDamage(PlayableCard source) {
            yield return behav.OnPreActivate(true);
            yield return behav.OnActivate(true);
            triggerHalfHealth = false;
        }
        private Texture GetDelusionOverrideTex() {
            if (counter > 0) {
                return ResourceBank.Get<Texture>("Art/Cards/AbilityIcons/sigilDelusion_" + counter);
            }
            return AbilityManager.AllAbilities.AbilityByID(this.Ability).Texture;
        }
    }

    public abstract class GodColourAbilityBehaviour : AbilityBehaviour {
        protected GameObject activateVisualGameObject = null;

        protected abstract IEnumerator PreActivate(bool halfHealth);
        protected abstract IEnumerator Activate(bool halfHealth);
        public abstract void SetUpVisualGameObject();
        protected abstract IEnumerator CleanUpVisuals();

        public IEnumerator OnPreActivate(bool halfHealth) {
            yield return PreActivate(halfHealth);
        }
        public IEnumerator OnActivate(bool halfHealth) {
            yield return Activate(halfHealth);
        }

        public override bool RespondsToResolveOnBoard() => activateVisualGameObject == null;
        public override IEnumerator OnResolveOnBoard() {
            SetUpVisualGameObject();
            yield break;
        }

        private void OnDestroy() {
            base.StartCoroutine(CleanUpVisuals());
        }
    }
}
