using DiskCardGame;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod {
    public class OrdealBannerManager : Singleton<OrdealBannerManager> {
        private Image banner;
        private Animator anim;

        private Text bannerTitle;
        private Text bannerSubtitle;
        private Text bannerDescription;

        public bool Displaying => bannerTitle.gameObject.activeSelf;

        public void Initialise() {
            Transform textCanvas = Instance.transform.GetChild(0);
            Canvas bannerCanvas = textCanvas.GetComponent<Canvas>();
            Canvas referenceCanvas = TextDisplayer.Instance.transform.GetChild(0).GetComponent<Canvas>();
            bannerCanvas.worldCamera = referenceCanvas.worldCamera;
            bannerCanvas.sortingLayerID = referenceCanvas.sortingLayerID;
            bannerCanvas.sortingLayerName = referenceCanvas.sortingLayerName;

            anim = Instance.GetComponent<Animator>();
            bannerTitle = textCanvas.GetChild(0).GetChild(0).GetComponent<Text>();
            bannerSubtitle = bannerTitle.transform.GetChild(0).GetComponent<Text>();
            bannerDescription = bannerTitle.transform.GetChild(1).GetComponent<Text>();
            banner = bannerDescription.transform.GetChild(0).GetComponent<Image>();

            Instance.transform.position = new(0f, 0f, 1f);
            bannerDescription.transform.localPosition = new(0f, -54f, 0f);
        }

        public void UpdateBanner(OrdealType type, int tier) {
            bannerTitle.text = OrdealUtils.GetOrdealTitle(type, tier);
            bannerSubtitle.text = OrdealUtils.GetOrdealSubtitle(type, tier);
            bannerDescription.text = OrdealUtils.GetOrdealIntroDescription(type, tier);
            bannerTitle.color = bannerSubtitle.color = bannerDescription.color = OrdealUtils.GetOrdealColor(type);
            Color color = bannerTitle.color;
            color.a = 0.5f;
            banner.color = color;
        }
        public void UpdateBannerOutro(OrdealType type, int tier) {
            bannerDescription.text = OrdealUtils.GetOrdealOutroDescription(type, tier);
        }

        public void DisplayBanner(OrdealType ordeal, bool intro) {
            base.StartCoroutine(DisplayBannerEnumerator(ordeal, intro));
        }
        public IEnumerator DisplayBannerEnumerator(OrdealType ordeal, bool intro) {
            LobotomyPlugin.Log.LogInfo($"[OrdealBannerManager.DisplayBanner] [{ordeal}] Intro:{intro}");
            string audioName = ordeal.ToString() + "_" + (intro ? "start" : "end");
            AudioController.Instance.PlaySound2D(audioName, MixerGroup.TableObjectsSFX);
            ShowBanner();
            yield return new WaitForSeconds(3f);
            HideBanner();
            yield return new WaitForSeconds(1.5f);
        }


        public void ShowBanner() {
            bannerTitle.gameObject.SetActive(true);
            anim.Play("fade_in", 0, 0f);
        }
        public void HideBanner() {
            anim.Play("fade_out", 0, 0f);
            CustomCoroutine.WaitThenExecute(3f, delegate {
                bannerTitle.gameObject.SetActive(false);
            });
        }
    }
}
