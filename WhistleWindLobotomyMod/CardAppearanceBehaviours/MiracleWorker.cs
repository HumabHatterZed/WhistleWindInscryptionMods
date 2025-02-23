using InscryptionAPI.PixelCard;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;

namespace WhistleWindLobotomyMod
{
    public partial class Appearances
    {
        private static void AddMiracleWorkerAppearance()
        {
            MiracleWorkerAppearance.appearance = CardHelper.CreateAppearance<MiracleWorkerAppearance>(LobotomyPlugin.pluginGuid, "MiracleWorkerAppearance").Id;
        }
    }
    public class MiracleWorkerAppearance : PixelAppearanceBehaviour
    {
        public static Appearance appearance;
        public override Sprite OverridePixelPortrait()
        {
            int blessings = SaviourBossUtils.Blessings(base.Card);
            return PlagueDoctorCreator.UpdateDoctorPixelPortrait(blessings);
        }
        public override void ApplyAppearance()
        {
            int blessings = SaviourBossUtils.Blessings(base.Card);
            base.Card.RenderInfo.portraitOverride = PlagueDoctorCreator.PlagueDoctorPortraits[Mathf.Min(11, blessings)];
            base.Card.RenderInfo.forceEmissivePortrait = base.Card.RenderInfo.forceEmissivePortrait || blessings >= 11;
        }
        public override void OnPreRenderCard() => ApplyAppearance();
        public override void ResetAppearance() => base.Card.RenderInfo.portraitOverride = null;
    }
}
