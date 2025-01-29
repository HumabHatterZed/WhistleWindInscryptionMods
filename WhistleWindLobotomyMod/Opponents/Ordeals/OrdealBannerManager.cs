using DiskCardGame;
using UnityEngine;
using UnityEngine.UI;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public class OrdealBannerManager : Singleton<OrdealBannerManager>
    {
        private Image banner;
        private Animator anim;

        private Text bannerTitle;
        private Text bannerDescription;

        public bool Displaying => bannerTitle.gameObject.activeSelf;

        public void Initialise()
        {
            Transform textCanvas = Instance.transform.GetChild(0);
            Canvas bannerCanvas = textCanvas.GetComponent<Canvas>();
            Canvas referenceCanvas = TextDisplayer.Instance.transform.GetChild(0).GetComponent<Canvas>();
            bannerCanvas.worldCamera = referenceCanvas.worldCamera;
            bannerCanvas.sortingLayerID = referenceCanvas.sortingLayerID;
            bannerCanvas.sortingLayerName = referenceCanvas.sortingLayerName;

            anim = Instance.GetComponent<Animator>();
            bannerTitle = textCanvas.GetChild(0).GetChild(0).GetComponent<Text>();
            bannerDescription = bannerTitle.transform.GetChild(0).GetComponent<Text>();
            banner = bannerDescription.transform.GetChild(0).GetComponent<Image>();

            Instance.transform.position = new(0f, 0f, 1f);
            bannerDescription.transform.localPosition = new(0f, -54f, 0f);
        }

        public void UpdateBanner(OrdealType type, int tier)
        {
            bannerTitle.text = tier switch { 0 => "Dawn", 1 => "Noon", 2 => "Dusk", 3 => "Midnight", _ => "Error" } + " of " + type.ToString();
            bannerDescription.text = OrdealUtils.GetOrdealIntroDescription(type, tier);
            bannerTitle.color = bannerDescription.color = type switch
            {
                OrdealType.Green => GameColors.Instance.limeGreen,
                OrdealType.Violet => GameColors.Instance.purple,
                OrdealType.Crimson => GameColors.Instance.glowRed,
                OrdealType.Amber => GameColors.Instance.orange,
                OrdealType.Indigo => GameColors.Instance.blue,
                _ => GameColors.Instance.gray
            };
            Color color = bannerTitle.color;
            color.a = 0.5f;
            banner.color = color;
        }
        public void UpdateBannerOutro(OrdealType type, int tier)
        {
            bannerDescription.text = OrdealUtils.GetOrdealOutroDescription(type, tier);
        }
        public void ShowBanner()
        {
            bannerTitle.gameObject.SetActive(true);
            anim.Play("fade_in", 0, 0f);
        }
        public void HideBanner()
        {
            anim.Play("fade_out", 0, 0f);
            CustomCoroutine.WaitThenExecute(3f, delegate
            {
                bannerTitle.gameObject.SetActive(false);
            });
        }
    }
}
