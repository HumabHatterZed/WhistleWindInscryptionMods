using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Slots;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddScenarioOverseer() {
            ScenarioOverseer.ID = AbilityHelper.New<ScenarioOverseer>(LobotomyPlugin.pluginGuid,
                "Scenario Overseer",
                "While this card is in your hand: At the start of your turn, you may either take a snapshot of the board or close 2 Energy Cells to revert the board to a stored snapshot.",
                TextureLoader.LoadTextureFromFile("sigilOneTrueBook.png", LobotomyPlugin.ModAssembly),
                5,
                true,
                dialogue: "You would use my own tools against me?")
                .Id;
        }
    }

    public class ScenarioOverseer : AbilityBehaviour, IOnUpkeepInHand {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;

        private AngelaSnapshotManager snapshotManager;
        private AngelaSnapshotUI snapshotUI;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn() {
            if (snapshotManager == null) {
                LobotomyPlugin.Log.LogDebug("[Scenario Overseer] Create snapshot manager");
                snapshotManager = base.Card.gameObject.AddComponent<AngelaSnapshotManager>();
            }
            yield return base.PreSuccessfulTriggerSequence();
        }

        public bool RespondsToUpkeepInHand(bool playerUpkeep) => base.Card.InHand && playerUpkeep && TurnManager.Instance.TurnNumber > 1;

        public IEnumerator OnUpkeepInHand(bool playerUpkeep) {
            if (snapshotManager == null) {
                LobotomyPlugin.Log.LogDebug("[Scenario Overseer] Create snapshot manager");
                snapshotManager = base.Card.gameObject.AddComponent<AngelaSnapshotManager>();
            }

            // don't trigger if the player has no energy on upkeep for whatever reason
            yield return base.PreSuccessfulTriggerSequence();
            yield return TakeSnapshot();
            yield return base.LearnAbility(0.5f);
        }

        // modified version of vanilla method to account for missing Act 3 stuff
        private IEnumerator TakeSnapshot() {
            Singleton<InteractionCursor>.Instance.InteractionDisabled = true;
            yield return new WaitForSeconds(0.1f);

            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().SetIntensity(1f, 0.075f);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, 20f);
            yield return new WaitForSeconds(0.2f);
            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().SetIntensity(0f, 0f);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(0f, float.MaxValue);

            Singleton<CameraEffects>.Instance.SetColorCorrectionEnabled(ccEnabled: true);

            Singleton<ViewManager>.Instance.SwitchToView(View.Board, immediate: true);
            CustomCoroutine.FlickerSequence(delegate
            {
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(0f, float.MaxValue);
            }, delegate
            {
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
            }, startOn: true, endOn: true, 0.05f, 2);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(ViewController.ControlMode.PhotographerDrone, immediate: true);
            Singleton<ViewManager>.Instance.Controller.LockState = ViewLockState.Unlocked;

            // initialise the snapshot UI
            if (snapshotUI == null) {
                GameObject droneUIObj = Object.Instantiate(ResourceBank.Get<GameObject>("Prefabs/UI/PhotographerDroneUI"), Singleton<UIManager>.Instance.Canvas.transform);
                droneUIObj.transform.localPosition = Vector3.zero;
                PhotographerUI ui = droneUIObj.GetComponent<PhotographerUI>();
                // reassign everything to the fixed ui class
                snapshotUI = droneUIObj.AddComponent<AngelaSnapshotUI>();
                snapshotUI.cameraButton = ui.cameraButton;
                snapshotUI.cameraButton.onClick.AddListener(snapshotUI.OnSnapshotPressed);
                snapshotUI.cancelButton = ui.cancelButton;
                snapshotUI.cancelButton.onClick.AddListener(snapshotUI.OnCancelPressed);
                snapshotUI.screenshotButton = ui.screenshotButton;
                snapshotUI.screenshotButton.onClick.AddListener(snapshotUI.OnRestoreSnapshotPressed);
                snapshotUI.screenshotImage = ui.screenshotImage;
                snapshotUI.largeScreenshotImage = ui.largeScreenshotImage;
                snapshotUI.staticScreenshotImage = ui.staticScreenshotImage;
                snapshotUI.noSnapshotText = ui.noSnapshotText;
                snapshotUI.largeScreenshot = ui.largeScreenshot;
                snapshotUI.uiParent = ui.uiParent;
                Destroy(ui); // remove vanilla component
            }
            else {
                snapshotUI.CompletedInteraction = false;
                snapshotUI.largeScreenshot.SetActive(false);
                snapshotUI.uiParent.SetActive(true);
                snapshotUI.SetButtonsEnabled(true);
                snapshotUI.gameObject.SetActive(true);
            }

            snapshotUI.Initialize(this.snapshotManager);
            snapshotUI.screenshotButton.interactable = ResourcesManager.Instance.PlayerMaxEnergy > 1;
            AudioController.Instance.SetLoopVolume(0.05f, 0.5f, 0, cancelOtherFades: false);
            yield return new WaitUntil(() => snapshotUI.CompletedInteraction);
            snapshotUI.gameObject.SetActive(false);

            AudioController.Instance.SetLoopVolume(0.2f, 0.5f, 0, cancelOtherFades: false);

            Singleton<CameraEffects>.Instance.SetColorCorrectionEnabled(ccEnabled: false);
            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().SetIntensity(1f, 0f);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearBlack);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
            Singleton<CameraEffects>.Instance.TweenBlur(3f, 0f);
            Singleton<ViewManager>.Instance.SwitchToView(View.PhotographerDroneView, immediate: true);
            yield return new WaitForEndOfFrame();
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(0f, 15f);
            Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().SetIntensity(0f, 0.2f);
            yield return new WaitForSeconds(0.4f);
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(ViewController.ControlMode.CardGameDefault);
            Singleton<InteractionCursor>.Instance.InteractionDisabled = false;
            yield return Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().Blink(delegate
            {
                Singleton<CameraEffects>.Instance.TweenBlur(1.5f, 0f);
            });
            yield return new WaitForSeconds(0.05f);
            yield return Singleton<UIManager>.Instance.Effects.GetEffect<EyelidMaskEffect>().Blink(delegate
            {
                Singleton<CameraEffects>.Instance.TweenBlur(0f, 0f);
            }, 1.25f);

            yield return new WaitForSeconds(0.1f);
        }

        private void OnDestroy() {
            if (snapshotUI != null) {
                Object.Destroy(snapshotUI.gameObject);
            }
        }

        public override int Priority => 9005; // arbitrary
    }

    // recreate PhotographerUI
    public class AngelaSnapshotUI : ManagedBehaviour {
        public GameObject uiParent;

        public UnityEngine.UI.Button cameraButton;
        public UnityEngine.UI.Button screenshotButton;
        public UnityEngine.UI.Button cancelButton;

        public Image screenshotImage;
        public Image staticScreenshotImage;

        public GameObject largeScreenshot;
        public Image largeScreenshotImage;

        public Text noSnapshotText;

        private AngelaSnapshotManager snapshotManager;

        public bool CompletedInteraction { get; set; }

        public void Initialize(AngelaSnapshotManager snapshotManager) {
            this.snapshotManager = snapshotManager;
            this.UpdateScreenshotImage();
        }

        public void OnCancelPressed() {
            this.SetButtonsEnabled(buttonsEnabled: false);
            this.CompletedInteraction = true;
            AudioController.Instance.PlaySound2D("photodrone_restore_photo");
        }

        public void OnSnapshotPressed() {
            this.SetButtonsEnabled(buttonsEnabled: false);
            base.StartCoroutine(this.TakeSnapshotSequence());
            AudioController.Instance.PlaySound2D("photodrone_take_photo");
        }

        public void OnRestoreSnapshotPressed() {
            if (this.snapshotManager.HasSnapshot) {
                this.SetButtonsEnabled(buttonsEnabled: false);
                base.StartCoroutine(this.RestoreSnapshotSequence());
                AudioController.Instance.PlaySound2D("photodrone_restore_photo");
                return;
            }
            AudioController.Instance.PlaySound2D("glitch");
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.2f);
            CustomCoroutine.FlickerSequence(delegate
            {
                if (this.noSnapshotText != null) {
                    this.noSnapshotText.enabled = true;
                }
            }, delegate
            {
                if (this.noSnapshotText != null) {
                    this.noSnapshotText.enabled = false;
                }
            }, startOn: true, endOn: false, 0.2f, 5);
            this.screenshotButton.enabled = false;
            CustomCoroutine.WaitThenExecute(1f, delegate
            {
                if (this.screenshotButton != null) {
                    this.screenshotButton.enabled = true;
                }
            });
        }

        public void SetButtonsEnabled(bool buttonsEnabled) {
            UnityEngine.UI.Button button = this.cameraButton;
            UnityEngine.UI.Button button2 = this.screenshotButton;
            bool flag = (this.cancelButton.enabled = buttonsEnabled);
            bool flag3 = (button2.enabled = flag);
            button.enabled = flag3;
        }

        private void UpdateScreenshotImage() {
            this.screenshotImage.enabled = this.snapshotManager.HasSnapshot;
            this.staticScreenshotImage.enabled = !this.snapshotManager.HasSnapshot;
            Texture2D snapshotTexture = this.snapshotManager.SnapshotTexture;
            if (snapshotTexture != null) {
                this.screenshotImage.sprite = Sprite.Create(snapshotTexture, new Rect(0f, 0f, snapshotTexture.width, snapshotTexture.height), new Vector2(0.5f, 0.5f));
            }
        }

        private IEnumerator TakeSnapshotSequence() {
            //yield return ResourcesManager.Instance.SpendEnergy(1);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetColor(GameColors.Instance.nearWhite);
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(1f, float.MaxValue);
            this.uiParent.SetActive(value: false);
            PhotographerSnapshotManager.SnapshotSubject subject = PhotographerSnapshotManager.SnapshotSubject.Board;

            this.snapshotManager.TakeSnapshot(subject);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenColorEffect>().SetIntensity(0f, 4f);
            this.UpdateScreenshotImage();
            this.largeScreenshot.SetActive(value: true);
            this.largeScreenshotImage.sprite = this.screenshotImage.sprite;
            this.largeScreenshot.transform.localPosition = new Vector3(0f, -1000f, 0f);
            Tween.LocalPosition(this.largeScreenshot.transform, Vector3.zero, 0.25f, 0f, Tween.EaseOut);
            yield return new WaitForSeconds(1.5f);
            this.CompletedInteraction = true;
        }

        private IEnumerator RestoreSnapshotSequence() {
            yield return ResourcesManager.Instance.RemoveMaxEnergy(2);
            this.uiParent.SetActive(value: false);
            this.snapshotManager.RevertToCurrentSnapshot();
            this.snapshotManager.ClearSnapshot();
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);
            yield return new WaitForSeconds(1f);
            this.CompletedInteraction = true;
        }
    }
    // modified variant of snapshot manager to account for mod-specific interactions and missing Act 3 stuff
    public class AngelaSnapshotManager : PhotographerSnapshotManager {
        private List<SlotModificationManager.ModificationType> playerSlotMods = new();
        private List<SlotModificationManager.ModificationType> opponentSlotMods = new();
        public new void TakeSnapshot(SnapshotSubject subject) {
            playerSlotMods.Clear();
            opponentSlotMods.Clear();
            base.TakeSnapshot(subject);
            foreach (CardSlot slot in BoardManager.Instance.PlayerSlotsCopy) {
                playerSlotMods.Add(slot.GetSlotModification());
            }
            foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy) {
                opponentSlotMods.Add(slot.GetSlotModification());
            }
        }
        public new void RevertToCurrentSnapshot() {
            LobotomyPlugin.Log.LogDebug("[ScenarioOverseer] RevertToCurrentSnapshot");
            if (this.currentSnapshot != null) {
                this.RevertToBoardSnapshot(this.currentSnapshot);
            }
            playerSlotMods.Clear();
            opponentSlotMods.Clear();
        }
        private IEnumerator ApplySlotState(BoardState.SlotState slotState, CardSlot slot, SlotModificationManager.ModificationType modType) {
            Debug.Log($"[ScenarioOverseer] ApplySlotState: {slotState} {slot} {modType}");

            if (slotState.card != null && slot.Card == null) {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(slotState.card.info, slot, 0f, resolveTriggers: false);
                PlayableCard card = slot.Card;
                card.TemporaryMods = new(slotState.card.temporaryMods);
                card.Status = new PlayableCardStatus(slotState.card.status);
                card.OnStatsChanged();
                Singleton<ResourcesManager>.Instance.ForceGemsUpdate();
            }
            if (slot.GetSlotModification() != modType) {
                yield return slot.SetSlotModification(modType);
            }
        }
        private void ApplySlotStates(List<BoardState.SlotState> slotStates, List<CardSlot> actualSlots, List<SlotModificationManager.ModificationType> slotMods) {
            for (int i = 0; i < slotStates.Count; i++) {
                base.StartCoroutine(this.ApplySlotState(slotStates[i], actualSlots[i], slotMods[i]));
            }
        }

        private new void RevertToBoardSnapshot(Snapshot snapshot) {
            foreach (CardSlot allSlot in Singleton<BoardManager>.Instance.AllSlots) {
                if (allSlot.Card != null && allSlot.Card.LacksAllTraits(Trait.Giant, Trait.Uncuttable, AbnormalPlugin.ImmuneToInstaDeath)) {
                    PlayableCard card = allSlot.Card;
                    allSlot.Card.UnassignFromSlot();
                    Object.Destroy(card.gameObject);
                }
            }
            this.ApplySlotStates(snapshot.boardState.playerSlots, Singleton<BoardManager>.Instance.PlayerSlotsCopy, playerSlotMods);
            this.ApplySlotStates(snapshot.boardState.opponentSlots, Singleton<BoardManager>.Instance.OpponentSlotsCopy, opponentSlotMods);
        }
    }
}
