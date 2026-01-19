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
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("community.inscryption.patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.spells", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("tribes.libary", BepInDependency.DependencyFlags.SoftDependency)]
    public partial class AbnormalPlugin : BaseUnityPlugin {
        public const string pluginGuid = "whistlewind.inscryption.abnormalsigils";
        public const string pluginPrefix = "wstl";
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
        public static Trait Orchestral = GuidManager.GetEnumValue<Trait>(pluginGuid, "Orchestral");
        public static Trait SodaLover = GuidManager.GetEnumValue<Trait>(pluginGuid, "SodaLover");

        public static Trait ImmuneToAilments = GuidManager.GetEnumValue<Trait>(pluginGuid, "ImmuneToAilments");
        public static Trait ImmuneToInstaDeath = GuidManager.GetEnumValue<Trait>(pluginGuid, "ImmuneToInstaDeath");

        public static CardMetaCategory CannotGiveSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGiveSigils");
        public static CardMetaCategory CannotGainSigils = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotGainSigils");
        public static CardMetaCategory CannotBoostStats = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotBoostStats");
        public static CardMetaCategory CannotCopyCard = GuidManager.GetEnumValue<CardMetaCategory>(pluginGuid, "CannotCopyCard");

        internal static RuntimeAnimatorController MiniGiantAnimator { get; private set; }

        private void OnDisable() => HarmonyInstance.UnpatchSelf();
        private void Awake() {
            Log = base.Logger;
            Assembly = Assembly.GetExecutingAssembly();
            InitAssetBundle();

            AbnormalConfigManager.Instance.BindConfig();

            if (!AbnormalConfigManager.Instance.EnableMod)
                Logger.LogWarning($"{pluginName} is disabled in the configuration. This will likely break things.");
            else {
                HarmonyInstance.PatchAll(Assembly);

                AddResources();
                AbnormalDialogueManager.GenerateDialogueEvents();

                InitTribes();
                AddCardAndAbilityVerification(); // call before adding cards/sigils so the events get called
                AddAbilities();
                AddSpecialAbilities();

                AddAppearances();

                AddCards();

                Logger.LogInfo($"{pluginName} loaded!");
            }
        }
        private void InitTribes() {
            if (TribalAPI.Enabled) {
                TribalAPI.UseTribalTribes();
            }
            else {
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

        private void AddResources() {
            List<string> decalStrings = new() {
                "decalSpore",
                "decalWorms"
            };

            foreach (string name in decalStrings) {
                for (int i = 0; i < 3; i++) {
                    string resource = $"{name}_{i}";
                    Texture2D texture = TextureLoader.LoadTextureFromFile($"{resource}.png");
                    ResourceBankManager.AddDecal(pluginGuid, resource, texture);
                    PixelCardManager.AddGBCDecal(pluginGuid, resource, texture);
                }
            }
        }

        private void AddAppearances() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Appearance")).ForEach(mi => mi.Invoke(this, null));
        private void AddCards() => AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("Card")).ForEach(mi => mi.Invoke(this, null));
        private void AddAbilities() {
            #region 1.0L
            Ability_Punisher();
            Ability_Bloodfiend();
            Ability_Martyr();
            Ability_Aggravating();
            Ability_TeamLeader();
            Ability_Idol();

            StatusEffect_Fervent();
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

            StatusEffect_Worms();
            Ability_SerpentsNest();

            Ability_Assimilator();
            Ability_GroupHealer();
            Ability_Reflector();
            Ability_FlagBearer();
            Ability_Grinder();
            Ability_TheTrain();
            Ability_Scorching();
            Ability_Regenerator();
            Ability_GiftGiver();
            Ability_Piercing();
            Ability_Scrambler();
            Ability_Gardener();
            Ability_Slime();
            Ability_Protector();
            #endregion

            #region v1.1L
            Ability_Alchemist();
            Ability_Nettles();

            StatusEffect_Spores();
            Ability_Sporogenic();

            StatusEffect_Prudence();
            Ability_Witness();

            Ability_Corrector();
            #endregion

            #region v2.0L
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
            #endregion

            #region v1.1
            Ability_Bloodletter();
            Ability_LeftStrike();
            Ability_RightStrike();

            StatusEffect_Haste();
            Ability_NimbleFoot();
            Ability_HighStrung();

            StatusEffect_Bind();
            Ability_BindingStrike();

            Ability_Persecutor();

            StatusEffect_Pebble();
            StatusEffect_Grief();
            Ability_Lonely();
            #endregion

            #region Special
            Ability_FalseThrone();
            Ability_ReturnToNihil();

            Ability_ReturnCard();
            Ability_RefreshDecks();
            Ability_SeeMore();
            #endregion

            #region v2.0
            Ability_Damsel();
            Ability_StressResponse();
            Ability_Abusive();
            Ability_Wedge();
            Ability_Driver();
            Ability_Unyielding();

            StatusEffect_Sinking();
            Ability_MindStrike();

            Slot_Flooded();
            Ability_Spilling();

            StatusEffect_FizzyLifterEffect();
            Ability_FizzyLifter();

            StatusEffect_OceanSodaEffect();
            Ability_OceanSoda();

            StatusEffect_PotshotPopEffect();
            Ability_PotshotPop();

            StatusEffect_SurefireDrinkEffect();
            Ability_SurefireDrink();

            Slot_Blooming();
            Ability_FlowerQueen();

            Ability_FingerTapping();

            StatusEffect_Decay();
            Ability_StartingDecay();
            Ability_Understanding();

            Ability_Bleachproof();
            Ability_Challenging();
            Ability_Withering();
            Ability_InfiniteShield();
            Ability_ExplosiveOpening();
            Ability_CardScramble();
            Ability_SoulboundFlesh();
            Ability_DeathPenalty();
            Ability_Ethereal();
            Ability_IntenseVolley();
            Ability_ActivatedGiftGiver();
            Ability_Alluring();
            Ability_Engraved();
            Ability_ActivatedSniper();
            Ability_Shaver();

            #endregion

            StatusEffectPages.AddStatusEntries();
            MechanicPages.AddMechanicEntries();
        }

        private void AddSpecialAbilities() {
            AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("SpecialAbility")).ForEach(mi => mi.Invoke(this, null));
            AccessTools.GetDeclaredMethods(typeof(AbnormalPlugin)).Where(mi => mi.Name.StartsWith("StatIcon")).ForEach(mi => mi.Invoke(this, null));
        }

        internal static void AddCardAndAbilityVerification() {
            CardManager.AllCardsCopy.Find(x => x.name == "!GIANTCARD_MOON")?.SetUniqueCopycat("wstl_miniMoon");
            CardManager.AllCardsCopy.Find(x => x.name == "!GIANTCARD_SHIP")?.SetUniqueCopycat("wstl_littlecello");

            AbilityManager.AllAbilities.AbilityByID(Ability.MadeOfStone)?.Info.SetRulebookDescription("A [creature] is immune to the effects of Touch of Death, Stinky, Punisher, Cursed, and Idol.");
            AbilityManager.AllAbilities.AbilityByID(Ability.SkeletonStrafe)?.Info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook); // littlecello

            CardManager.ModifyCardList += delegate (List<CardInfo> infos) {
                VerifyCustomTribeUsage(infos);

                return infos;
            };

            AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities) {
                StatusEffectManager.SyncStatusEffects();
                VerifyCustomSigilUsage(abilities);

                return abilities;
            };
        }
        internal static void VerifyCustomSigilUsage(List<AbilityManager.FullAbility> abilities) {
            foreach (AbilityManager.FullAbility ability in abilities.Where(x => x.ModGUID == pluginGuid)) {
                List<CardInfo> validCards = CardManager.AllCardsCopy.FindAll(x => x.HasAbility(ability.Id));
                if (validCards.Count == 0) {
                    continue;
                }

                bool rulebook = ability.Info.GetExtendedPropertyAsBool(AbnormalAbilityHelper.ADDTORULEBOOK) == true;
                bool modular = ability.Info.GetExtendedPropertyAsBool(AbnormalAbilityHelper.ADDTORULEBOOK) == true;

                // abilities marked modular are always modular in Act 1
                if (validCards.Exists(x => x.temple == CardTemple.Nature) || modular) {
                    if (rulebook) {
                        ability.Info.metaCategories.Add(AbilityMetaCategory.Part1Rulebook);
                    }
                    if (modular) {
                        ability.Info.metaCategories.Add(AbilityMetaCategory.Part1Modular);
                    }
                }

                if (validCards.Exists(x => x.temple == CardTemple.Tech) && rulebook) {
                    ability.Info.metaCategories.Add(AbilityMetaCategory.Part3Rulebook);
                }

                if (validCards.Exists(x => x.temple == CardTemple.Undead) && rulebook) {
                    ability.Info.metaCategories.Add(AbilityMetaCategory.GrimoraRulebook);
                }

                if (validCards.Exists(x => x.temple == CardTemple.Wizard) && rulebook) {
                    ability.Info.metaCategories.Add(AbilityMetaCategory.MagnificusRulebook);
                }
            }
        }

        internal static void VerifyCustomTribeUsage(List<CardInfo> allCardsCopy) {
            List<CardInfo> act1Cards = allCardsCopy.FindAll(x => x.IsObtainable(CardTemple.Nature));
            TribeManager.GetCustomTribeInfo(TribeFae).tribeChoice = act1Cards.Exists(x => x.IsOfTribe(TribeFae));
            TribeManager.GetCustomTribeInfo(TribeDivine).tribeChoice = act1Cards.Exists(x => x.IsOfTribe(TribeDivine));
            TribeManager.GetCustomTribeInfo(TribeBotanic).tribeChoice = act1Cards.Exists(x => x.IsOfTribe(TribeBotanic));
            TribeManager.GetCustomTribeInfo(TribeMechanical).tribeChoice = act1Cards.Exists(x => x.IsOfTribe(TribeMechanical));
            TribeManager.GetCustomTribeInfo(TribeAnthropoid).tribeChoice = act1Cards.Exists(x => x.IsOfTribe(TribeAnthropoid));
        }

        internal static void InitAssetBundle() {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WhistleWind.AbnormalSigils.abnormalsigils");
            AssetBundle assetBundle = AssetBundle.LoadFromStream(stream);
            MiniGiantAnimator = assetBundle.LoadAsset<RuntimeAnimatorController>("Card_MiniGiant");
            stream.Dispose();
            assetBundle.Unload(false);
        }

        public static class TribalAPI {
            public static bool Enabled => Chainloader.PluginInfos.ContainsKey("tribes.libary");
            public static void UseTribalTribes() {
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
