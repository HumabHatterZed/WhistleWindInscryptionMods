using DiskCardGame;
using Pixelplacement;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod {
    public class OrdealCounterManager : Singleton<OrdealCounterManager> {
        private const string REMAINING_TEXT = "remaining";

        public static Sprite dawnSprite;
        public static Sprite noonSprite;
        public static Sprite duskSprite;
        public static Sprite midnightSprite;

        private Animator anim;

        private SpriteRenderer leftRenderer;
        private TextMeshPro counterText;
        private TextMeshPro subtitleText;

        /// <summary>
        /// Whether or not the monitor display's subtitle displays the default 'remaining' subtitle.
        /// </summary>
        public bool Dirty { get; private set; }

        public int amountLeft;

        public void UpdateIconRenderer(Sprite sp) {
            leftRenderer.sprite = sp;
        }

        public void ResetToDisplayRemaining(int ordealTier) {
            UpdateConsole(ordealTier, amountLeft);
        }

        public void UpdateConsole(int ordealTier, int startingAmount, string text = REMAINING_TEXT) {
            Dirty = text != REMAINING_TEXT;
            if (!Dirty) {
                amountLeft = startingAmount;
                counterText.text = this.amountLeft.ToString();
            }
            else {
                counterText.text = startingAmount.ToString();
            }
            subtitleText.text = text;

            // allow for setting a custom sprite before updating the console
            if (ordealTier != -1) {
                UpdateIconRenderer(ordealTier switch {
                    0 => dawnSprite,
                    1 => noonSprite,
                    2 => duskSprite,
                    3 => midnightSprite,
                    _ => null
                });
            }
        }
        public void SetShown(bool shown) {
            if (shown) {
                this.anim.gameObject.SetActive(true);
                this.anim.Play("enter", 0, 0f);
                Tween.Position(LeshyAnimationController.Instance.transform, new Vector3(0f, 6f, 9f), 1f, 0.5f);
            }
            else {
                this.anim.Play("exit", 0, 0f);
                Tween.Position(LeshyAnimationController.Instance.transform, new Vector3(0f, 4.75f, 9f), 1f, 0.5f);
                CustomCoroutine.WaitThenExecute(0.5f, delegate {
                    UpdateConsole(4, 0);
                    SetTextColour(Color.black);
                    this.anim.gameObject.SetActive(false);
                });
            }
        }

        public void SetTextColour(Color colour) {
            counterText.color = colour;
        }
        public void EnableConsole(bool enable) {
            AudioController.Instance.PlaySound3D("holomap_power_off", MixerGroup.TableObjectsSFX, Instance.transform.position, 1f, 0f, new AudioParams.Pitch(0.9f));
            if (enable) {
                this.anim.Play("enable", 1, 0f);
            }
            else {
                this.anim.Play("disable", 1, 0f);
            }
        }

        public IEnumerator FlickerConsole(int ordealTier, int numValue, string text = REMAINING_TEXT, float preWait = 0.8f, float postWait = 0.8f) {
            OrdealCounterManager.Instance.EnableConsole(false);
            yield return new WaitForSeconds(0.5f);
            OrdealCounterManager.Instance.UpdateConsole(ordealTier, numValue, text);
            OrdealCounterManager.Instance.EnableConsole(true);
            yield return new WaitForSeconds(1f);
        }

        public IEnumerator UpdateAmountLeft(int amountKilled, float waitTime = 0.125f) {
            if (this.amountLeft == 0)
                yield break;

            for (int i = 0; i < Mathf.Abs(amountKilled); i++) {
                this.amountLeft += amountKilled < 0 ? 1 : -1;
                yield return UpdateDisplayedValue(this.amountLeft, waitTime);
            }
        }

        public IEnumerator UpdateDisplayedValue(int value, float waitTime = 0.125f) {
            if (value == 0) {
                SetTextColour(Color.red);
            }
            else {
                SetTextColour(Color.black);
            }
            counterText.text = value.ToString();

            AudioController.Instance.PlaySound3D("holomap_power_off", MixerGroup.TableObjectsSFX, Instance.transform.position, 1f, 0f, new AudioParams.Pitch(0.9f));
            yield return new WaitForSeconds(waitTime);
        }

        private void Initialise() {
            anim = Instance.transform.GetChild(0).GetComponent<Animator>();
            leftRenderer = anim.transform.GetChild(0).GetComponent<SpriteRenderer>();
            counterText = anim.transform.GetChild(1).GetComponent<TextMeshPro>();
            subtitleText = counterText.transform.GetChild(0).GetComponent<TextMeshPro>();
            anim.transform.position = new(0f, -4.5f, 5f);
        }

        public static void ValidateOrdealManagers() {
            if (OrdealCounterManager.Instance == null) {
                LobotomyPlugin.Log.LogDebug("[ValidateOrdealManagers] Setting up OrdealCounterManager");
                GameObject obj = Instantiate(AssetManager.ordealCounterPrefab, BoardManager.Instance.transform.parent);
                m_Instance = obj.AddComponent<OrdealCounterManager>();
                OrdealCounterManager.Instance.Initialise();


            }

            if (OrdealBannerManager.Instance == null) {
                LobotomyPlugin.Log.LogDebug("[ValidateOrdealManagers] Setting up OrdealBannerManager");
                GameObject obj2 = Instantiate(AssetManager.ordealBannerPrefab, TextDisplayer.Instance.transform.parent);
                OrdealBannerManager.m_Instance = obj2.AddComponent<OrdealBannerManager>();
                OrdealBannerManager.Instance.Initialise();
            }
        }
    }
}
