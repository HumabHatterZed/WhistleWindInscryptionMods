using DiskCardGame;
using UnityEngine;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Appearance_MiniGiantPortrait() {
            MiniGiantPortrait.appearance = CardHelper.CreateAppearance<MiniGiantPortrait>(pluginGuid, "MiniGiantPortrait").Id;
        }
    }
    public class MiniGiantPortrait : GiantAnimatedPortrait {
        public static Appearance appearance;

        public override void ApplyAppearance() {
            // can't call LiveRenderCard.ApplyAppearance cause it crashes for some reason
            RenderStatsLayer statsLayer = base.Card.StatsLayer;
            if (statsLayer is not RenderLiveStatsLayer) {
                GameObject statsLayerObj = statsLayer.gameObject;
                statsLayer.PreserveMaterialOnDestroy = true;
                Object.DestroyImmediate(statsLayer);
                RenderLiveStatsLayer renderLiveStatsLayer = this.AddStatsLayerComponent(statsLayerObj);
                renderLiveStatsLayer.FindBendableRenderer();
                SetMaterialTiling[] componentsInChildren = renderLiveStatsLayer.GetComponentsInChildren<SetMaterialTiling>(includeInactive: true);
                foreach (SetMaterialTiling setMaterialTiling in componentsInChildren) {
                    if (setMaterialTiling.MaterialIndex == 0) {
                        setMaterialTiling.AdjustOffset(new Vector2(2f, 1f));
                    }
                }
            }

            base.Card.RenderInfo.prefabPortrait = base.Card.Info.AnimatedPortrait;
            base.Card.RenderInfo.hidePortrait = true;
        }

        public override RenderLiveStatsLayer AddStatsLayerComponent(GameObject statsLayerObj) {
            return statsLayerObj.AddComponent<RenderLiveStatsLayer>();
        }
    }
}
