using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.PixelCard;
using InscryptionAPI.Resource;
using InscryptionAPI.Slots;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.StatusEffects;
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
        public const string pluginPrefixGBC = "wstlGBC";
        public const string pluginName = "Abnormal Sigils";
        private const string pluginVersion = "2.0.0";

        internal static ManualLogSource Log;
        internal static Assembly Assembly;
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
        public static Trait LovingSlime = GuidManager.GetEnumValue<Trait>(pluginGuid, "LovingSlime");
        public static Trait ImmuneToInstaDeath = GuidManager.GetEnumValue<Trait>(pluginGuid, "ImmuneToInstaDeath");
        public static Trait Orchestral = GuidManager.GetEnumValue<Trait>(pluginGuid, "Orchestral");
        public static Trait ImmuneToAilments = GuidManager.GetEnumValue<Trait>(pluginGuid, "ImmuneToAilments");

        public static Trait CannotGiveSigils = GuidManager.GetEnumValue<Trait>(pluginGuid, "CannotGiveSigils");
        public static Trait CannotGainSigils = GuidManager.GetEnumValue<Trait>(pluginGuid, "CannotGainSigils");
        public static Trait CannotBoostStats = GuidManager.GetEnumValue<Trait>(pluginGuid, "CannotBoostStats");
        public static Trait CannotCopyCard = GuidManager.GetEnumValue<Trait>(pluginGuid, "CannotCopyCard");

        private void OnDisable() => HarmonyInstance.UnpatchSelf();
        private void Awake()
        {
            Log = base.Logger;
            Assembly = Assembly.GetExecutingAssembly();
            AbnormalConfigManager.Instance.BindConfig();

            if (!AbnormalConfigManager.Instance.EnableMod)
                Logger.LogWarning($"{pluginName} is disabled in the configuration. This will likely break things.");
            else
            {
                HarmonyInstance.PatchAll(Assembly);

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
            if (TribalAPI.Enabled)
            {
                TribalAPI.UseTribalTribes();
            }
            else
            {
                Texture2D anthro = TextureLoader.LoadTextureFromFile("tribeAnthropoid.png");
                Texture2D anthroBack = TextureLoader.LoadTextureFromFile("tribeAnthropoid_reward.png");
                Texture2D botanical = TextureLoader.LoadTextureFromFile("tribeBotanic.png");
                Texture2D botanicalBack = TextureLoader.LoadTextureFromFile("tribeBotanic_reward.png");
                Texture2D divine = TextureLoader.LoadTextureFromFile("tribeDivine.png");
                Texture2D divineBack = TextureLoader.LoadTextureFromFile("tribeDivine_reward.png");
                Texture2D fae = TextureLoader.LoadTextureFromFile("tribeFae.png");
                Texture2D faeBack = TextureLoader.LoadTextureFromFile("tribeFae_reward.png");
                Texture2D mechanic = TextureLoader.LoadTextureFromFile("tribeMechanical.png");
                Texture2D mechanicBack = TextureLoader.LoadTextureFromFile("tribeMechanical_reward.png");

                TribeAnthropoid = TribeManager.Add(pluginGuid, "AnthropoidTribe", anthro, true, anthroBack);
                TribeBotanic = TribeManager.Add(pluginGuid, "BotanicalTribe", botanical, true, botanicalBack);
                TribeDivine = TribeManager.Add(pluginGuid, "DivineTribe", divine, true, divineBack);
                TribeFae = TribeManager.Add(pluginGuid, "FaerieTribe", fae, true, faeBack);
                TribeMechanical = TribeManager.Add(pluginGuid, "MechanicalTribe", mechanic, true, mechanicBack);
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
            StatIcon_Slime();
            StatIcon_Nihil();
        }
        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        private void AddCards() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));
        private void AddAbilities()
        {
            AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities)
            {
                abilities.Find(x => x.Info.name == "MadeOfStone").Info.rulebookDescription = "A [creature] is immune to the effects of Touch of Death, Stinky, Punisher, Cursed, and Idol.";

                StatusEffectManager.SyncStatusEffects();
                return abilities;
            };

            // v1.0
            Ability_Punisher();
            Ability_Bloodfiend();
            Ability_Martyr();
            Ability_Aggravating();
            Ability_TeamLeader();
            Ability_Idol();
            
            Ability_Conductor();
            StatusEffect_Fervent();

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
            StatusEffect_Prudence();

            Ability_Corrector();

            // v2.0L
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
            Ability_Bloodletter();
            Ability_LeftStrike();
            Ability_RightStrike();

            Ability_NimbleFoot();
            Ability_HighStrung();
            StatusEffect_Haste();

            Ability_BindingStrike();
            StatusEffect_Bind();
            
            Ability_Persecutor();
            Ability_Lonely();
            StatusEffect_Pebble();
            StatusEffect_Grief();

            // 2.0A
            Ability_Damsel();
            Ability_Abusive();

            // Specials
            Ability_FalseThrone();
            Ability_ReturnToNihil();

            Ability_ReturnCard();
            Ability_RefreshDecks();
            Ability_SeeMore();

            SlotModificationManager.New(pluginGuid, "Test", typeof(Test), TextureLoader.LoadTextureFromFile("slotPavedRoad.png", Assembly))
                .SetRulebook("Paved Road", "A space with this effect will look neat and cool and awesome", TextureLoader.LoadTextureFromFile("slotPavedRoad1.png", Assembly), SlotModificationManager.ModificationMetaCategory.Part1Rulebook);

            //SlotModificationManager.SyncSlotModificationList();
        }

        public class Test :SlotModificationBehaviour
        {

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
                TribeMechanical = TribalLibary.Plugin.machineTribe;
                TribeBotanic = TribalLibary.Plugin.plantTribe;

                TribeManager.TribeInfo divineTribe = TribeManager.NewTribes.FirstOrDefault(x => x.tribe == TribeDivine);
                divineTribe.icon = TextureHelper.GetImageAsTexture("tribeDivine.png", Assembly).ConvertTexture();
                divineTribe.cardback = TextureHelper.GetImageAsTexture("tribeDivine_reward.png");

                TribeManager.TribeInfo faeTribe = TribeManager.NewTribes.FirstOrDefault(x => x.tribe == TribeFae);
                faeTribe.icon = TextureHelper.GetImageAsTexture("tribeFae.png", Assembly).ConvertTexture();
                faeTribe.cardback = TextureHelper.GetImageAsTexture("tribeFae_reward.png", Assembly);

                TribeManager.TribeInfo anthropoidTribe = TribeManager.NewTribes.FirstOrDefault(x => x.tribe == TribeAnthropoid);
                anthropoidTribe.icon = TextureHelper.GetImageAsTexture("tribeAnthropoid.png", Assembly).ConvertTexture();
                anthropoidTribe.cardback = TextureHelper.GetImageAsTexture("tribeAnthropoid_reward.png", Assembly);

                TribeManager.TribeInfo mechanicalTribe = TribeManager.NewTribes.FirstOrDefault(x => x.tribe == TribeMechanical);
                mechanicalTribe.icon = TextureHelper.GetImageAsTexture("tribeMechanical.png", Assembly).ConvertTexture();
                mechanicalTribe.cardback = TextureHelper.GetImageAsTexture("tribeMechanical_reward.png", Assembly);

                TribeManager.TribeInfo botanicTribe = TribeManager.NewTribes.FirstOrDefault(x => x.tribe == TribeBotanic);
                botanicTribe.icon = TextureHelper.GetImageAsTexture("tribeBotanic.png", Assembly).ConvertTexture();
                botanicTribe.cardback = TextureHelper.GetImageAsTexture("tribeBotanic_reward.png", Assembly);
            }

        }
    }
}
