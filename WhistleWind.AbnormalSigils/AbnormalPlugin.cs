using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.PixelCard;
using InscryptionAPI.Resource;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("community.inscryption.patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("tribes.libary", BepInDependency.DependencyFlags.SoftDependency)]

    public partial class AbnormalPlugin : BaseUnityPlugin
    {
        public const string pluginGuid = "whistlewind.inscryption.abnormalsigils";
        public const string pluginPrefix = "wstl";
        public const string pluginName = "Abnormal Sigils";
        private const string pluginVersion = "1.0.0";

        internal static ManualLogSource Log;
        private static readonly Harmony HarmonyInstance = new(pluginGuid);

        public static Tribe TribeDivine;
        public static Tribe TribeFae;
        public static Tribe TribeBotanic;
        public static Tribe TribeAnthropoid;
        public static Tribe TribeMechanical;

        public static Trait Boneless = GuidManager.GetEnumValue<Trait>(pluginGuid, "Boneless");
        public static Trait SwanBrother = GuidManager.GetEnumValue<Trait>(pluginGuid, "SwanBrother");
        public static Trait NakedSerpent = GuidManager.GetEnumValue<Trait>(pluginGuid, "NakedSerpent");
        public static Trait SporeFriend = GuidManager.GetEnumValue<Trait>(pluginGuid, "SporeFriend");
        public static Trait ImmuneToInstaDeath = GuidManager.GetEnumValue<Trait>(pluginGuid, "ImmuneToInstaDeath");
        public static Trait Executioner = GuidManager.GetEnumValue<Trait>(pluginGuid, "Executioner");
        public static Trait Orchestral = GuidManager.GetEnumValue<Trait>(pluginGuid, "Orchestral");

        public static CardMetaCategory CannotGiveSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGiveSigils");
        public static CardMetaCategory CannotGainSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGainSigils");
        public static CardMetaCategory CannotBoostStats = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotBoostStats");
        public static CardMetaCategory CannotCopyCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotCopyCard");

        private void OnDisable() => HarmonyInstance.UnpatchSelf();

        private void Awake()
        {
            Log = base.Logger;
            AbnormalConfigManager.Instance.BindConfig();

            if (!AbnormalConfigManager.Instance.EnableMod)
                Logger.LogWarning($"{pluginName} is disabled in the configuration. This will likely break things.");
            else
            {
                HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

                AddResources();
                AbnormalDialogueManager.GenerateDialogueEvents();

                InitTribes();
                AddAbilities();
                AddSpecialAbilities();
                AddAppearances();

                AddCards();

                Logger.LogInfo($"{pluginName} loaded!");
            }
        }
        private void InitTribes()
        {
            Log.LogDebug("Loading tribes...");
            if (TribalAPI.Enabled)
            {
                TribalAPI.UseTribalTribes();
            }
            else
            {
                Texture2D anthro = TextureLoader.LoadTextureFromFile("tribeAnthropoid");
                Texture2D botanical = TextureLoader.LoadTextureFromFile("tribeBotanic");
                Texture2D divine = TextureLoader.LoadTextureFromFile("tribeDivine");
                Texture2D fae = TextureLoader.LoadTextureFromFile("tribeFae");
                Texture2D mechanic = TextureLoader.LoadTextureFromFile("tribeMechanical");

                TribeAnthropoid = TribeManager.Add(pluginGuid, "AnthropoidTribe", anthro, true, null);
                TribeBotanic = TribeManager.Add(pluginGuid, "BotanicalTribe", botanical, true, null);
                TribeDivine = TribeManager.Add(pluginGuid, "DivineTribe", divine, true, null);
                TribeFae = TribeManager.Add(pluginGuid, "FaerieTribe", fae, true, null);
                TribeMechanical = TribeManager.Add(pluginGuid, "MechanicalTribe", mechanic, true, null);
            }
        }

        private void AddResources()
        {
            List<string> decalStrings = new()
            {
                "decalSpore",
                "decalWorms"
            };

            foreach (string name in decalStrings)
            {
                for (int i = 0; i < 3; i++)
                {
                    string resource = $"{name}_{i}";
                    Texture2D texture = TextureLoader.LoadTextureFromFile($"{resource}");
                    ResourceBankManager.AddDecal(pluginGuid, resource, texture);
                    PixelCardManager.AddGBCDecal(pluginGuid, resource, texture);
                }
            }
        }
        private void AddSpecialAbilities()
        {
            AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));

            StatIcon_Time();
            StatIcon_SigilPower();
            StatIcon_Nihil();
        }
        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
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
            Ability_Persistent();
            Ability_Courageous();

            Ability_SerpentsNest();
            StatusEffect_Worms();

            Ability_Assimilator();
            Ability_GroupHealer();
            Ability_Reflector();
            Ability_FlagBearer();
            Ability_Grinder();
            Ability_TheTrain();
            Ability_Scorching();
            Ability_Regenerator();
            Ability_Volatile();
            Ability_GiftGiver();
            Ability_Piercing();
            Ability_Scrambler();
            Ability_Gardener();
            Ability_Slime();
            Ability_Protector();

            // v1.1
            Ability_Alchemist();
            Ability_Nettles();

            Ability_Sporogenic();
            StatusEffect_Spores();

            Ability_Witness();
            Ability_Corrector();

            // v2.0
            Ability_ThickSkin();
            Ability_OneSided();
            Ability_Copycat();
            Ability_YellowBrickRoad();
            Ability_Neutered();
            Ability_NeuteredLatch();
            Ability_RightfulHeir();
            Ability_GreedyHealing();
            Ability_Cycler();
            Ability_Barreler();

            // Specials
            Ability_FalseThrone();
            Ability_ReturnToNihil();
        }

        public static class SpellAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("zorro.inscryption.infiniscryption.spells");
        }

        public static class TribalAPI
        {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("tribes.libary");
            public static void UseTribalTribes()
            {
                Log.LogDebug("Tribal Libary detected. Using its tribes instead.");
                TribeDivine = TribalLibary.Plugin.guardianTribe;
                TribeFae = TribalLibary.Plugin.fairyTribe;
                TribeAnthropoid = TribalLibary.Plugin.humanoidTribe;
                TribeMechanical = TribalLibary.Plugin.androidTribe;
                TribeBotanic = TribalLibary.Plugin.plantTribe;
            }

        }

        public static CardInfo MakeCard(
            string cardName, string displayName = null,
            string description = null,
            int attack = 0, int health = 0,
            int blood = 0, int bones = 0, int energy = 0
            )
        {
            return CardHelper.NewCard(false, pluginPrefix, cardName, displayName, description, attack, health, blood, bones, energy);
        }

        public static void CreateCard(CardInfo cardInfo) => CardManager.Add(pluginPrefix, cardInfo);
    }
}
