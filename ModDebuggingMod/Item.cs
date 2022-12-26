using DiskCardGame;
using InscryptionAPI.Items;
using WhistleWind.Core.Helpers;
using System.Collections;
using UnityEngine;
using static InscryptionAPI.Items.ConsumableItemManager;
using InscryptionAPI.Items.Extensions;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void ItemDebug()
        {
            string rulebookName = "DebugItem";
            string rulebookDescription = "Displays text colour codes";
            ModelType modelType = ModelType.HoveringRune;
            Texture2D texture2D = TextureLoader.LoadTextureFromBytes(Properties.Resources.itemRulebookIcon);

            ConsumableItemManager.New(Plugin.pluginPrefix, rulebookName, rulebookDescription, texture2D, typeof(DebugItem), modelType)
                .SetAct1()
                .SetLearnItemDescription("AAAA");
        }
    }
    public class DebugItem : ConsumableItem
    {
        public override IEnumerator ActivateSequence()
        {
            base.PlayExitAnimation();
            yield return new WaitForSeconds(0.25f);
            yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(
                $"default " +
                $"[c:bR]bR[c:] " +
                $"[c:bG]bG[c:] " +
                $"[c:bB]bB[c:] " +
                $"[c:dB]dB[c:] " +
                $"[c:blGr]blGr[c:] " +
                $"[c:g3]g3[c:] " +
                $"[c:g2]g2[c:] " +
                $"[c:brnO]brnO[c:] " +
                $"[c:g1]g1[c:] " +
                $"[c:lGr]lGr[c:] " +
                $"[c:bSG]bSG[c:] " +
                $"[c:gray]gray[c:] " +
                $"[c:dlGr]dlGr[c:] " +
                $"[c:B]B[c:] " +
                $"[c:G]G[c:] " +
                $"[c:dSG]dSG[c:] " +
                $"[c:R]R[c:] " +
                $"[c:O]O[c:]");
            yield return new WaitForSeconds(0.25f);
        }
    }
}
