using DiskCardGame;
using Pixelplacement;
using System.Collections;
using TMPro;
using UnityEngine;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod {
    public class OrdealCounterManager : Singleton<OrdealCounterManager> {
        public static Sprite dawnSprite;
        public static Sprite noonSprite;
        public static Sprite duskSprite;
        public static Sprite midnightSprite;

        private Animator anim;
        private SpriteRenderer leftRenderer;
        private TextMeshPro counterText;

        public int amountLeft;

        public static void ValidateCounter() {
            if (OrdealCounterManager.Instance == null) {
                LobotomyPlugin.Log.LogDebug("[OrdealCounterManager] Setting up managers");

                GameObject obj = Instantiate(AssetManager.ordealCounterPrefab, BoardManager.Instance.transform.parent);
                m_Instance = obj.AddComponent<OrdealCounterManager>();
                Instance.Initialise();

                GameObject obj2 = Instantiate(AssetManager.ordealBannerPrefab, TextDisplayer.Instance.transform.parent);
                OrdealBannerManager.m_Instance = obj2.AddComponent<OrdealBannerManager>();
                OrdealBannerManager.Instance.Initialise();
            }
            else {
                LobotomyPlugin.Log.LogDebug("[OrdealCounterManager] Managers exist");
            }
        }

        private void Initialise() {
            anim = Instance.transform.GetChild(0).GetComponent<Animator>();
            leftRenderer = anim.transform.GetChild(0).GetComponent<SpriteRenderer>();
            counterText = anim.transform.GetChild(1).GetComponent<TextMeshPro>();

            anim.transform.position = new(0f, -4.5f, 5f);
        }

        public void UpdateConsole(int ordealTier, int startingAmount) {
            amountLeft = startingAmount;
            counterText.text = this.amountLeft.ToString();
            leftRenderer.sprite = ordealTier switch {
                0 => dawnSprite,
                1 => noonSprite,
                2 => duskSprite,
                3 => midnightSprite,
                _ => null
            };
        }
        public void SetShown(bool shown) {
            if (shown) {
                this.anim.gameObject.SetActive(true);
                this.anim.Play("enter", 0, 0f);
                Tween.Position(LeshyAnimationController.Instance.transform, new Vector3(0f, 6f, 9f), 1f, 0.5f);
            }
            else {
                amountLeft = 0;
                leftRenderer.sprite = null;
                counterText.text = "";
                counterText.color = Color.black;
                this.anim.Play("exit", 0, 0f);
                Tween.Position(LeshyAnimationController.Instance.transform, new Vector3(0f, 4.75f, 9f), 1f, 0.5f);
                CustomCoroutine.WaitThenExecute(0.5f, delegate {
                    this.anim.gameObject.SetActive(false);
                });
            }
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

        public IEnumerator UpdateAmountLeft(int amountKilled, float waitTime = 0.125f) {
            if (this.amountLeft == 0)
                yield break;

            for (int i = 0; i < Mathf.Abs(amountKilled); i++) {
                AudioController.Instance.PlaySound3D("holomap_power_off", MixerGroup.TableObjectsSFX, Instance.transform.position, 1f, 0f, new AudioParams.Pitch(0.9f));

                this.amountLeft += amountKilled < 0 ? 1 : -1;
                if (this.amountLeft == 0) {
                    counterText.color = Color.red;
                }
                counterText.text = this.amountLeft.ToString();
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}
