using DiskCardGame;
using EasyFeedback.APIs;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;
using static UnityEngine.ParticleSystem.PlaybackState;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodWhite() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God White";
            info.rulebookDescription = "Activate or at half Health (once): Swipe a Tentacle across the player's side of the board, affecting three spaces on the board based on which side of the board the Tentacle is on.";
            info.powerLevel = 5;

            GodWhite.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodWhite), TextureLoader.LoadTextureFromFile("sigilGodWhite.png"))
                .SetUniqueRedirect("Tentacle", "wstl:Ordeals_White Tentacle", GameColors.Instance.purple)
                .Id;
        }
    }

    public class GodWhite : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
        private Animator anim;
        private Animator defendAnim;

        private bool attackLeft = false;
        private bool attackLeftDefend = false;

        protected override IEnumerator PreActivate(bool halfHealth) {
            Animator currentAnim;
            if (halfHealth) {
                attackLeftDefend = base.Card.Slot.Index < 2;
                currentAnim = defendAnim;

                if (attackLeftDefend) {
                    currentAnim.transform.position = BoardManager.Instance.PlayerSlotsCopy[0].transform.position + Vector3.left + Vector3.back + Vector3.up;
                    currentAnim.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
                }
                else {
                    currentAnim.transform.position = BoardManager.Instance.PlayerSlotsCopy[BoardManager.Instance.PlayerSlotsCopy.Count - 1].transform.position + Vector3.right + Vector3.back + Vector3.up;
                    currentAnim.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
                }
            }
            else {
                attackLeft = SeededRandom.Bool(base.GetRandomSeed() + TurnManager.Instance.TurnNumber);
                currentAnim = anim;

                if (attackLeft) {
                    currentAnim.transform.position = BoardManager.Instance.PlayerSlotsCopy[0].transform.position + Vector3.left + Vector3.up;
                    currentAnim.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
                }
                else {
                    currentAnim.transform.position = BoardManager.Instance.PlayerSlotsCopy[BoardManager.Instance.PlayerSlotsCopy.Count - 1].transform.position + Vector3.right + Vector3.up;
                    currentAnim.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
                }
            }



            ViewManager.Instance.SwitchToView(View.Default);
            AudioController.Instance.PlaySound2D("Violet_portal_on", MixerGroup.TableObjectsSFX);
            currentAnim.SetTrigger("Show");
            currentAnim.SetLayerWeight(1, 1f);
            yield return new WaitForSeconds(1f);
            AudioController.Instance.PlaySound2D("Violet_tentacle", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(0.30f);
        }
        protected override IEnumerator Activate(bool halfHealth) {
            int stacks = SinkingStacks();
            List<CardSlot> slotsAffected = BoardManager.Instance.PlayerSlotsCopy;
            Animator currentAnim;

            if (halfHealth) {
                currentAnim = defendAnim;
                if (attackLeftDefend) {
                    slotsAffected.Remove(slotsAffected[slotsAffected.Count - 1]);
                }
                else {
                    slotsAffected.Remove(slotsAffected[0]);
                }
            }
            else {
                currentAnim = anim;
                if (attackLeft) {
                    slotsAffected.Remove(slotsAffected[slotsAffected.Count - 1]);
                }
                else {
                    slotsAffected.Remove(slotsAffected[0]);
                }
            }

            ViewManager.Instance.SwitchToView(View.Default);

            currentAnim.SetTrigger("Extend");
            yield return new WaitForSeconds(1.5f);
            AudioController.Instance.PlaySound2D("Violet_tentacle", MixerGroup.TableObjectsSFX);

            foreach (CardSlot slot in slotsAffected.Where(x => x.Card != null)) {
                yield return slot.Card.AddStatusEffectToFaceDown<Sinking>(stacks);
            }

            yield return new WaitForSeconds(1.5f);
            AudioController.Instance.PlaySound2D("Violet_portal_off", MixerGroup.TableObjectsSFX);
            currentAnim.SetTrigger("Hide");
            currentAnim.SetLayerWeight(1, 0f);
            yield return new WaitForSeconds(0.5f);
            ViewManager.Instance.SwitchToView(View.Board);
        }

        public static int SinkingStacks() {
            return 3 + Mathf.Min(2, RunState.CurrentRegionTier + Mathf.Max(0, RunState.Run.DifficultyModifier - 1));
        }
        public override void SetUpVisualGameObject() {
            activateVisualGameObject = new("ShrineTentaclePool");
            GameObject obj = Instantiate(LobOpponentUtils.ShrineBossWhitePrefab, activateVisualGameObject.transform);
            anim = obj.GetComponent<Animator>();

            GameObject obj2 = Instantiate(LobOpponentUtils.ShrineBossWhitePrefab, activateVisualGameObject.transform);
            defendAnim = obj2.GetComponent<Animator>();
        }

        protected override IEnumerator CleanUpVisuals() {
            //ShowEye(false);
            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
        }
    }
}
