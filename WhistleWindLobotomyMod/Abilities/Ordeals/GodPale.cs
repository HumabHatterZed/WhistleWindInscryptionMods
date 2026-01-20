using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodPale() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God Pale";
            info.rulebookDescription = "When this card is played, the Pale Eye appears in the opposing space. Activate: Move the Eye to a new opposing space.";
            info.powerLevel = 5;

            GodPale.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodPale), TextureLoader.LoadTextureFromFile("sigilGodPale.png"))
                .SetUniqueRedirect("Pale Eye", "wstl:Ordeals_Pale Eye", GameColors.Instance.fuschia)
                .Id;
        }
    }

    public class GodPale : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardSlot eyeSlot = null;
        private Animator eyeAnim;
        private bool active = false;
        private bool preActivation = false;
        private CardSlot DetermineNewSlot() {
            List<CardSlot> slots = BoardManager.Instance.PlayerSlotsCopy;
            if (eyeSlot != null) {
                slots.Remove(eyeSlot);
            }
            return slots.GetRandom();
        }

        private IEnumerator MoveEyeToSlot() {
            eyeSlot = DetermineNewSlot();
            Tween.Position(activateVisualGameObject.transform, eyeSlot.transform.position + new Vector3(0, 0.2f, 0), 1f, 0.5f, startCallback: () => ShowEye(true));
            yield return new WaitForSeconds(0.5f);
            AudioController.Instance.PlaySound2D("Violet_pale_move", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(1.5f);
        }

        private void ShowEye(bool show) {
            AudioController.Instance.PlaySound2D("Violet_pale_start", MixerGroup.TableObjectsSFX);
            if (show) {
                eyeAnim.SetTrigger("show");
            }
            else {
                eyeAnim.SetTrigger("hide");
            }
            active = show;
        }

        protected override IEnumerator PreActivate() {
            preActivation = true;
            if (active) {
                ShowEye(false); // guarantee reprieve
            }
            yield return new WaitForSeconds(0.5f);
        }
        protected override IEnumerator Activate() {
            yield return MoveEyeToSlot(); // move eye to new slot
            preActivation = true;
        }

        public override IEnumerator OnResolveOnBoard() {
            yield return base.OnResolveOnBoard();
            eyeSlot = base.Card.OpposingSlot();
            activateVisualGameObject.transform.position = eyeSlot.transform.position + new Vector3(0, 0.2f, 0);
            ViewManager.Instance.SwitchToView(View.Board);
            activateVisualGameObject.transform.GetChild(0).gameObject.SetActive(true);
            ShowEye(true);
            yield return new WaitForSeconds(0.5f);
        }

        public override void SetUpVisualGameObject() {
            activateVisualGameObject = GameObject.Instantiate(LobOpponentUtils.ShrineBossPalePrefab);
            eyeAnim = activateVisualGameObject.GetComponent<Animator>();
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !preActivation;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            int numShrines = 1 + BoardManager.Instance.GetOpponentCards(x => x.HasTrait(LobotomyCardManager.Ordeal)).Count;
            if (Random.Range(0, 7 + RunState.CurrentRegionTier + RunState.Run.DifficultyModifier  - numShrines) == 0) {
                ShowEye(!active);
                yield return new WaitForSeconds(1f);
            }

            if (active && eyeSlot.Card != null) {
                AudioController.Instance.PlaySound2D("Violet_pale_start", MixerGroup.TableObjectsSFX);
                eyeSlot.Card.Status.damageTaken += Mathf.CeilToInt(eyeSlot.Card.Health / 4f);
                if (eyeSlot.Card.Health <= 0) {
                    yield return eyeSlot.Card.Die(false, null, false);
                }
            }
        }
    }
}
