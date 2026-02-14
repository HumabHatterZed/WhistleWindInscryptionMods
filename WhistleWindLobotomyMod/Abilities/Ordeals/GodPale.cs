using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
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
            info.rulebookDescription = "When this card is played, the Pale Eye appears in the opposing space. At half health or on activate: Move the Eye to a new opposing space.";
            info.powerLevel = 5;

            GodPale.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodPale), TextureLoader.LoadTextureFromFile("sigilGodPale.png"))
                .SetUniqueRedirect("Pale Eye", "wstl:Ordeals_Pale Eye", GameColors.Instance.glowSeafoam)
                .Id;
        }
    }

    public class GodPale : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private CardSlot eyeSlot = null;
        private Animator eyeAnim;
        private bool active = true;

        private IEnumerator MoveEyeToSlot() {
            LobotomyPlugin.Log.LogDebug("[PaleEye] Move to slot");
            ShowEye(true);
            AudioController.Instance.PlaySound2D("Violet_eye_move", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(0.2f);
            Tween.Position(activateVisualGameObject.transform, eyeSlot.transform.position + new Vector3(0, 0.2f, 0), 0.5f, 0f);
            yield return new WaitForSeconds(0.75f);
        }

        private void ShowEye(bool show) {
            AudioController.Instance.PlaySound2D("Violet_eye_start", MixerGroup.TableObjectsSFX);
            if (show) {
                LobotomyPlugin.Log.LogDebug("[PaleEye] Toggle Show");
                eyeAnim.Play("eye_show");
            }
            else {
                LobotomyPlugin.Log.LogDebug("[PaleEye] Toggle hide");
                eyeAnim.Play("eye_hide");
            }
            active = show;
        }

        private CardSlot DetermineNewSlot() {
            List<CardSlot> slots = BoardManager.Instance.PlayerSlotsCopy;
            if (eyeSlot != null) {
                slots.Remove(eyeSlot);
            }
            return slots.GetRandom();
        }

        public override IEnumerator OnResolveOnBoard() {
            yield return base.OnResolveOnBoard();
            eyeSlot = base.Card.OpposingSlot();
            activateVisualGameObject.transform.position = eyeSlot.transform.position + new Vector3(0, 0.2f, 0);
            ViewManager.Instance.SwitchToView(View.Board);
            AudioController.Instance.PlaySound2D("Violet_eye_start", MixerGroup.TableObjectsSFX);
            activateVisualGameObject.transform.GetChild(0).gameObject.SetActive(true);
            active = true;
            yield return new WaitForSeconds(0.5f);
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !preActivated;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            if (active) {
                // higher chance of eye disappearing when other shrines are active
                int numShrines = BoardManager.Instance.GetOpponentCards(x => x.HasTrait(LobotomyCardManager.Ordeal)).Count;
                if (numShrines > 1 && Random.Range(0, 7 + RunState.CurrentRegionTier + RunState.Run.DifficultyModifier - numShrines) == 0) {
                    ShowEye(false);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else if (RunState.CurrentRegionTier > 2 || RunState.Run.DifficultyModifier > 3 || Random.Range(0, 4 - RunState.Run.DifficultyModifier) == 0) {
                ShowEye(true);
                yield return new WaitForSeconds(0.5f);
            }


            if (active && eyeSlot.Card != null) {
                AudioController.Instance.PlaySound2D("Violet_eye_start", MixerGroup.TableObjectsSFX);
                eyeSlot.Card.Status.damageTaken += Mathf.CeilToInt(eyeSlot.Card.Health / 4f);
                if (eyeSlot.Card.Health <= 0) {
                    yield return eyeSlot.Card.Die(false, null, false);
                }
            }
        }

        protected override IEnumerator PreActivate(bool halfHealth) {
            if (!halfHealth) {
                if (active) {
                    ShowEye(false); // guarantee reprieve
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
        protected override IEnumerator Activate(bool halfHealth) {
            if (!halfHealth) {
                eyeSlot = DetermineNewSlot();
            }
            else if (eyeSlot != base.Card.OpposingSlot()) {
                eyeSlot = base.Card.OpposingSlot();
            }

            yield return MoveEyeToSlot(); // move eye to new slot
        }

        public override void SetUpVisualGameObject() {
            if (activateVisualGameObject == null) {
                activateVisualGameObject = GameObject.Instantiate(LobOpponentUtils.ShrineBossPalePrefab);
                eyeAnim = activateVisualGameObject.GetComponent<Animator>();
            }
        }
        protected override IEnumerator CleanUpVisuals() {
            ShowEye(false);
            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
        }
    }
}
