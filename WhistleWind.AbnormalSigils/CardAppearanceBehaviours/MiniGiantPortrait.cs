using DiskCardGame;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Appearance_MiniGiantPortrait()
        {
            MiniGiantPortrait.appearance = CardHelper.CreateAppearance<MiniGiantPortrait>(pluginGuid, "MiniGiantPortrait").Id;
        }
    }
    public class MiniGiantPortrait : GiantAnimatedPortrait
    {
        public static Appearance appearance;

        public override void ApplyAppearance()
        {
            base.Card.RenderInfo.prefabPortrait = base.Card.Info.AnimatedPortrait;
            base.Card.RenderInfo.hidePortrait = true;
        }

        public override RenderLiveStatsLayer AddStatsLayerComponent(GameObject statsLayerObj)
        {
            RenderLiveStatsLayer renderLiveStatsLayer = statsLayerObj.AddComponent<RenderLiveStatsLayer>();
            return renderLiveStatsLayer;
        }
    }
}
