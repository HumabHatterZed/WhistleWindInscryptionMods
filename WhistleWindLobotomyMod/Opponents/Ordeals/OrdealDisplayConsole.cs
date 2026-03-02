using DiskCardGame;
using Pixelplacement;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod {
    public class OrdealDisplayConsole : Singleton<OrdealDisplayConsole> {
        public const string REMAINING_TEXT = "remaining";
        public const string SCALE_LOCK_TEXT = "scale lock";

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
        private int ordealTier;

        /// <summary>
        /// Updates the string displayed for the counter (large text).
        /// </summary>
        /// <param name="text"></param>
        public void SetCounterText(string text) {
            counterText.text = text;
        }
        public void SetCounterTextColour(Color colour) {
            counterText.color = colour;
        }

        public void SetSubtitleText(string text) {
            subtitleText.text = text;
        }

        public void SetIconRenderer(Sprite sp) {
            leftRenderer.sprite = sp;
        }
        public void UpdateCounterIcon() {
            if (ordealTier != -1) {
                SetIconRenderer(ordealTier switch {
                    0 => dawnSprite,
                    1 => noonSprite,
                    2 => duskSprite,
                    3 => midnightSprite,
                    _ => null
                });
            }
        }

        public void SetStartingVariables(int ordealTier, int startingAmount) {
            this.ordealTier = ordealTier;
            amountLeft = startingAmount;
            SetCounterTextColour(Color.black);
            SetCounterText(startingAmount.ToString());
            SetSubtitleText(REMAINING_TEXT);
            Dirty = false;
        }

        public void ResetConsoleDisplay() {
            UpdateConsoleDisplay(amountLeft.ToString());
        }

        public void UpdateConsoleDisplay(string counterText, string subtitleText = REMAINING_TEXT, bool updateIconRenderer = true) {
            Dirty = subtitleText != REMAINING_TEXT;
            //if (!Dirty) {
            //    amountLeft = startingAmount;
            //    SetCounterText(amountLeft.ToString());
            //}
            //else {
                
            //}
            SetCounterText(counterText);
            SetSubtitleText(subtitleText);

            // allow for setting a custom sprite before updating the console
            if (updateIconRenderer) {
                UpdateCounterIcon();
            }
        }

        public IEnumerator ResetConsoleDisplay(float waitBefore, float waitAfter) {
            EnableConsole(false);
            yield return new WaitForSeconds(waitBefore);
            ResetConsoleDisplay();
            EnableConsole(true);
            if (waitAfter > 0f) {
                yield return new WaitForSeconds(waitAfter);
            }
        }
        public IEnumerator UpdateConsoleDisplay(string counterText, string subtitleText, bool updateIconRenderer, float waitBefore, float waitAfter) {
            EnableConsole(false);
            yield return new WaitForSeconds(waitBefore);
            UpdateConsoleDisplay(counterText, subtitleText, updateIconRenderer);
            EnableConsole(true);
            if (waitAfter > 0f) {
                yield return new WaitForSeconds(waitAfter);
            }
        }

        //public IEnumerator FlickerConsole(int ordealTier, int numValue, string text = REMAINING_TEXT, float preWait = 0.8f, float postWait = 0.8f) {
        //    Instance.EnableConsole(false);
        //    yield return new WaitForSeconds(0.5f);
        //    Instance.UpdateConsole(ordealTier, numValue, text);
        //    Instance.EnableConsole(true);
        //    yield return new WaitForSeconds(1f);
        //}

        public IEnumerator UpdateCounterDisplayValue(string counterText, bool counterTextRed, float waitTime = 0.125f) {
            SetCounterTextColour(counterTextRed ? Color.red : Color.black);
            
            PlayDisplayToggleSound();
            SetCounterText(counterText);
            yield return new WaitForSeconds(waitTime);
        }

        public IEnumerator UpdateAmountLeft(int amountKilled, float waitTime = 0.125f) {
            if (Dirty) {
                yield return ResetConsoleDisplay(0.3f, 0.5f);
            }
            if (this.amountLeft != 0) {
                for (int i = 0; i < Mathf.Abs(amountKilled); i++) {
                    this.amountLeft += amountKilled < 0 ? 1 : -1;
                    yield return UpdateCounterDisplayValue(this.amountLeft.ToString(), this.amountLeft == 0);
                }
            }
        }

        public IEnumerator DisplayScaleLock(int highestScaleBalance, float waitBefore, float waitAfter) {
            EnableConsole(false);
            yield return new WaitForSeconds(waitBefore);
            SetIconRenderer(OrdealUtils.GetScaleLockSprite(highestScaleBalance));
            UpdateConsoleDisplay(highestScaleBalance.ToString(), SCALE_LOCK_TEXT, false);
            EnableConsole(true);
            if (waitAfter > 0f) {
                yield return new WaitForSeconds(waitAfter);
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
                    SetStartingVariables(4, 0); // reset the starting variables to their default
                    this.anim.gameObject.SetActive(false);
                });
            }
        }

        public void EnableConsole(bool enable) {
            PlayDisplayToggleSound();
            this.anim.Play(enable ? "enable" : "disable", 1, 0f);
        }

        private void PlayDisplayToggleSound() {
            AudioController.Instance.PlaySound3D("holomap_power_off", MixerGroup.TableObjectsSFX, Instance.transform.position, 1f, 0f, new AudioParams.Pitch(0.9f));
        }

        private void Initialise() {
            anim = transform.GetChild(0).GetComponent<Animator>();
            leftRenderer = anim.transform.GetChild(0).GetComponent<SpriteRenderer>();
            counterText = anim.transform.GetChild(1).GetComponent<TextMeshPro>();
            subtitleText = counterText.transform.GetChild(0).GetComponent<TextMeshPro>();
            anim.transform.position = new(0f, -4.5f, 5f); // set initial position beneath the playing table
        }

        public static void ValidateOrdealManagers() {
            if (Instance == null) {
                LobotomyPlugin.Log.LogDebug("[ValidateOrdealManagers] Setting up OrdealDisplayConsole");
                GameObject obj = Instantiate(AssetManager.ordealCounterPrefab, BoardManager.Instance.transform.parent);
                m_Instance = obj.AddComponent<OrdealDisplayConsole>();
                Instance.Initialise();


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
