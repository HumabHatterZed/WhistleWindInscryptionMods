using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Helpers;
using System.Reflection;

namespace BonniesBakingPack
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(InscryptionAPIPlugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.SoftDependency)]
    public partial class BakingPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Log = base.Logger;
            Assembly = Assembly.GetExecutingAssembly();
            HarmonyInstance.PatchAll(Assembly);

            AddAbilities();
            AddCards();

            if (PackAPI.Enabled)
                PackAPI.CreateCardPack();

            Log.LogInfo("Loaded Bonnie's Baking Pack. It's time to get baking!");
        }

        private void OnDisable() => HarmonyInstance.UnpatchSelf();

        internal static class PackAPI
        {
            internal static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.packmanager");
            internal static void CreateCardPack()
            {
                Log.LogDebug("PackManager is installed.");
                PackInfo pack = PackManager.GetPackInfo<PackInfo>(pluginPrefix);
                pack.Title = pluginName;
                pack.SetTexture(TextureHelper.GetImageAsTexture("bbp_pack.png", Assembly));
                pack.Description = $"[count] ingredients for all your pastry-making needs! Includes milk, eggs, sugar, and red.";
                pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
            }
        }

        private static readonly Harmony HarmonyInstance = new Harmony(pluginGuid);
        internal static ManualLogSource Log;
        internal static Assembly Assembly;

        public const string pluginGuid = "whistlewind.inscryption.bonniesbakingpack";
        public const string pluginPrefix = "bbp";
        public const string pluginName = "Bonnie's Baking Pack";
        private const string pluginVersion = "0.0.1";
    }
}
