using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Appearance_MiracleWorker()
        {
            MiracleWorkerAppearance.appearance = CardHelper.CreateAppearance<MiracleWorkerAppearance>(pluginGuid, "MiracleWorkerAppearance").Id;
        }
    }
    public class MiracleWorkerAppearance : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverridePixelPortrait()
        {
            int blessings = SaviourBossUtils.Blessings(base.Card);
            return LobotomyPlugin.UpdateDoctorPixelPortrait(blessings);
        }
        public override void ApplyAppearance()
        {
            int blessings = SaviourBossUtils.Blessings(base.Card);
            base.Card.RenderInfo.portraitOverride = LobotomyPlugin.PlagueDoctorPortraits[Mathf.Min(11, blessings)];
            base.Card.RenderInfo.forceEmissivePortrait = base.Card.RenderInfo.forceEmissivePortrait || blessings >= 11;
        }
        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ResetAppearance() => base.Card.RenderInfo.portraitOverride = null;
    }
}
