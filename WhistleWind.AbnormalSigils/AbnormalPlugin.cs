using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using HarmonyLib;
using Sirenix.Utilities;
using System;
using System.Linq;
using System.Reflection;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Patches;

namespace WhistleWind.AbnormalSigils
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class AbnormalPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.abnormalsigils";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "Abnormal Sigils";
        private const string pluginVersion = "1.0.0";

        internal static ManualLogSource Log;
        private static Harmony HarmonyInstance = new(pluginGuid);

        private void OnDisable()
        {
            HarmonyInstance.UnpatchSelf();
        }

        private void Awake()
        {
            AbnormalPlugin.Log = base.Logger;
            AbnormalConfigManager.Instance.BindConfig();

            if (!AbnormalConfigManager.Instance.EnableMod)
                Logger.LogWarning($"{pluginName} is disabled in the configuration. This will likely break things.");
            else
            {
                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                // If Spells API is installed, patch TargetedSpellAbility
                if (SpellAPI.Enabled)
                {
                    Type classInstance = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                                          from type in asm.GetTypes()
                                          where type.IsClass && type.Name == "TargetedSpellAbility"
                                          select type).Single();

                    var baseMethod = classInstance.GetMethod("GetStatValues");
                    var patchMethod = typeof(SpellRelatedPatches).GetMethod(nameof(SpellRelatedPatches.ShowStatsForTargetedSpells));

                    HarmonyInstance.Patch(baseMethod, postfix: new(patchMethod));
                }

                AbnormalDialogueManager.GenerateDialogueEvents();

                AddAbilities();
                AddSpecialAbilities();
                AddCards();

                Logger.LogInfo($"{pluginName} loaded!");
            }
        }
        private void AddSpecialAbilities()
        {
            AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));

            StatIcon_Time();
        }
        private void AddCards() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));
        private void AddAbilities()
        {
            // v1.0
            Ability_Punisher();
            Ability_Bloodfiend();
            Ability_Martyr();
            Ability_Aggravating();
            Ability_TeamLeader();
            Ability_Idol();
            Ability_Conductor();
            Ability_Woodcutter();
            Ability_FrozenHeart();
            Ability_FrostRuler();
            Ability_Roots();
            Ability_BroodMother();
            Ability_Cursed();
            Ability_Healer();
            Ability_QueenNest();
            Ability_BitterEnemies();
            Ability_Courageous();
            Ability_SerpentsNest();
            Ability_Assimilator();
            Ability_GroupHealer();
            Ability_Reflector();
            Ability_FlagBearer();
            Ability_Grinder();
            Ability_TheTrain();
            Ability_Burning();
            Ability_Regenerator();
            Ability_Volatile();
            Ability_GiftGiver();
            Ability_Piercing();
            Ability_Scrambler();
            Ability_Gardener();
            Ability_Slime();
            Ability_Marksman();
            Ability_Protector();
            Ability_QuickDraw();
            // v1.1
            Ability_Alchemist();
            Ability_Nettles();
            Ability_Spores();
            Ability_SporeDamage();
            Ability_Witness();
            Ability_Corrector();
            // v2.0
            Ability_ThickSkin();
            Ability_OneSided();
            Ability_Copycat();
            Ability_CatLover();
            Ability_Cowardly();
            Ability_Neutered();
            Ability_NeuteredLatch();
            Ability_RightfulHeir();
            Ability_GreedyHealing();

            // Spells
            if (!SpellAPI.Enabled)
                Log.LogDebug("Spells API is not installed, changing ability behaviours.");

            Ability_TargetGainStats();
            Ability_TargetGainSigils();
            Ability_TargetGainStatsSigils();

            // Specials
            Ability_FalseThrone();
            Ability_Nihil();
        }

        public static class SpellAPI
        {
            private static bool? _enabled;
            public static bool Enabled
            {
                get
                {
                    _enabled ??= Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.spells");
                    return (bool)_enabled;
                }
            }
        }

        public static class LobMod
        {
            private static bool? _enabled;
            public static bool Enabled
            {
                get
                {
                    _enabled ??= Chainloader.PluginInfos.ContainsKey("whistlewind.inscryption.lobotomycorp");
                    return (bool)_enabled;
                }
            }
        }
    }
}
