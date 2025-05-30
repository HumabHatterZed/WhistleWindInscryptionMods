using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.P03KayceeRun.Cards;
using Infiniscryption.PackManagement;
using Infiniscryption.Spells;
using InscryptionAPI;
using InscryptionAPI.Ascension;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;
using InscryptionAPI.Helpers;
using InscryptionAPI.TalkingCards.Create;
using MagnificusMod;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static InscryptionAPI.Slots.SlotModificationManager;

namespace BonniesBakingPack
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInDependency(InscryptionAPIPlugin.ModGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(InfiniscryptionSpellsPlugin.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("zorro.inscryption.infiniscryption.packmanager", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("Lily.BOT", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("tribes.libary", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ScrybeCompat.GrimoraGuid, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ScrybeCompat.P03Sigil, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ScrybeCompat.P03Guid, BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(ScrybeCompat.MagnificusGuid, BepInDependency.DependencyFlags.SoftDependency)]
    public partial class BakingPlugin : BaseUnityPlugin
    {
        private void AddCards()
        {
            CreateFood();
            CreateBonnie();
            CreateBunnie();
            CreateBonnieGrimora();
            CreateBonnieMagnificus();
            CreateBonnieDisk();
            CreateBunnieDisk();
            CreateBingus();
            CreateMice();
            CreateMeanMice();
            CreateLoudMice();
            CreatePhoneMice();
            CreatePoliceWolves();
            CreateCats();
            CreateDogs();
            CreateBunnies();
            CreatePandas();
            CreateMoose();
            CreatePirates();
            CreateProtagonists();
            CreateEtherealLadies();

            Tribe rodent = 0, feline = 0;
            if (Chainloader.PluginInfos.ContainsKey("Lily.BOT"))
            {
                rodent = GuidManager.GetEnumValue<Tribe>("Lily.BOT", "rodent");
                feline = GuidManager.GetEnumValue<Tribe>("Lily.BOT", "feline");
            }
            else if (Chainloader.PluginInfos.ContainsKey("tribes.libary"))
            {
                rodent = GuidManager.GetEnumValue<Tribe>("tribes.libary", "rodent");
                feline = GuidManager.GetEnumValue<Tribe>("tribes.libary", "feline");
            }

            if (rodent != 0)
            {
                foreach (CardInfo info in Act1Cards)
                {
                    if (info.name.StartsWith("bbp_act1_mouse"))
                        info.tribes = new() { rodent };
                    else if (info.name.EndsWith("cat") || info.name.EndsWith("pirate"))
                        info.tribes = new() { feline };
                }
            }
        }

        private void Awake()
        {
            Log = base.Logger;
            Configs = base.Config;
            Assembly = Assembly.GetExecutingAssembly();

            SplitByAct = Config.Bind("General", "All Acts", false, "Should each Scrybe's card pack be choosable in any act?");
            BingusCrash = Config.Bind("General", "Bingus Flubbed It", false, "Has Bingus been played for the first time?");

            HarmonyInstance.PatchAll(Assembly);
            if (ScrybeCompat.GrimoraEnabled)
            {
                HarmonyInstance.PatchAll(typeof(GrimoraPatches));
            }
            if (ScrybeCompat.P03SigilsEnabled)
            {
                BonnieAct3.Register();
                HarmonyInstance.PatchAll(typeof(P03Patches));
                HarmonyInstance.PatchAll(typeof(BonnieAct3));
            }
            if (ScrybeCompat.MagnificusEnabled)
            {
                HarmonyInstance.PatchAll(typeof(MagnificusPatches));
            }

            AddAbilities();
            LadyAbility.Add();
            AbilityManager.ModifyAbilityList += delegate (List<AbilityManager.FullAbility> abilities)
            {
                abilities.Find(x => x.Id == Ability.DrawCopyOnDeath).Info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
                abilities.Find(x => x.Id == Ability.GainBattery).Info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);
                abilities.Find(x => x.Id == Ability.Sentry).Info.AddMetaCategories(AbilityMetaCategory.Part1Rulebook);

                if (ScrybeCompat.GrimoraEnabled)
                {
                    abilities.Find(x => x.Id == ScrybeCompat.GetGrimoraAbility("Haunting Call", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("hauntingCall_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetGrimoraAbility("Skin Crawler", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("skinCrawler_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetGrimoraAbility("Slasher", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("slasher_pixel.png"));
                }
                if (ScrybeCompat.P03SigilsEnabled)
                {
                    abilities.Find(x => x.Id == ScrybeCompat.GetP03Ability("Fire Strike When Fueled", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("fireWhenFueled_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetP03Ability("Fuel Siphon", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("fuelSiphon_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetP03Ability("Electric", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("electric_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetP03Ability("Tinkerer", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("tinkerer_pixel.png"));
                }
                if (ScrybeCompat.MagnificusEnabled)
                {
                    abilities.Find(x => x.Id == ScrybeCompat.GetMagnificusAbility("Bone Marrow", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("boneMarrow_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetMagnificusAbility("Dead Draw", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("deadDraw_pixel.png"));
                    abilities.Find(x => x.Id == ScrybeCompat.GetMagnificusAbility("Resurrection", Ability.None))?.Info.SetPixelAbilityIcon(GetTexture("resurrection_pixel.png"));
                }

                return abilities;
            };

            AddCards();
            StarterDeckManager.New(pluginGuid, "Basic Baking Pack",
                GetTexture("starterDeck.png"), new string[3] { "bbp_act1_bonnie", "bbp_act1_meetBun", "bbp_act1_whiteDonut" }
                );

            StarterDeckManager.New(pluginGuid, "Bony Baking Pack",
                GetTexture("starterDeck2.png"), new string[5] { "bbp_grimora_whiteDonut", "bbp_grimora_whiteDonut", "bbp_grimora_mouseGhool", "bbp_grimora_mousenapper", "bbp_grimora_killerMouse" }
                );

            StarterDeckManager.New(pluginGuid, "Bot Baking Pack",
                GetTexture("starterDeck3.png"), new string[4] { "bbp_act3_phoneMouse", "bbp_act3_anonymouse", "bbp_act3_copstable", "bbp_act3_copstable" }
                );

            StarterDeckManager.New(pluginGuid, "Bauble Baking Pack;1",
                GetTexture("starterDeck4.png"), new string[4] { "bbp_magnificus_witness", "bbp_magnificus_occultist", "bbp_magnificus_occultist", "bbp_magnificus_lich" }
                );

            StarterDeckManager.ModifyDeckList += delegate (List<StarterDeckManager.FullStarterDeck> decks)
            {
                if (!ScrybeCompat.GrimoraEnabled)
                {
                    decks.RemoveAll(x => x.Info.title == "Bony Baking Pack");
                }

                if (!ScrybeCompat.P03Enabled)
                {
                    decks.RemoveAll(x => x.Info.title == "Bot Baking Pack");
                }

                if (!ScrybeCompat.MagnificusEnabled)
                {
                    decks.RemoveAll(x => x.Info.title == "Bauble Baking Pack;1");
                }

                return decks;
            };

            CreateCardPack();

            AssetBundle bundle = AssetBundle.LoadFromStream(Assembly.GetManifestResourceStream("BonniesBakingPack.bonniebaking"));
            AudioClips = new()
            {
                bundle.LoadAsset<AudioClip>("bonnie_bonk"),
                bundle.LoadAsset<AudioClip>("panda_gun")
            };

            Log.LogInfo("Loaded Bonnie's Baking Pack. It's baking time!");
        }

        internal static class ScrybeCompat
        {
            internal const string P03Guid = "zorro.inscryption.infiniscryption.p03kayceerun";
            internal const string P03Sigil = "zorro.inscryption.infiniscryption.p03sigillibrary";
            internal const string GrimoraGuid = "arackulele.inscryption.grimoramod";
            internal const string MagnificusGuid = "silenceman.inscryption.magnificusmod";

            internal static CardMetaCategory GrimoraChoice = GuidManager.GetEnumValue<CardMetaCategory>(GrimoraGuid, "GrimoraModChoiceNode");
            internal static CardMetaCategory NeutralRegion = GuidManager.GetEnumValue<CardMetaCategory>(P03Guid, "NeutralRegionCards");
            internal static CardMetaCategory NatureRegion = GuidManager.GetEnumValue<CardMetaCategory>(P03Guid, "NatureRegionCards");
            internal static CardMetaCategory TechRegion = GuidManager.GetEnumValue<CardMetaCategory>(P03Guid, "TechRegionCards");
            internal static CardMetaCategory WizardRegion = GuidManager.GetEnumValue<CardMetaCategory>(P03Guid, "WizardRegionCards");
            internal static CardMetaCategory UndeadRegion = GuidManager.GetEnumValue<CardMetaCategory>(P03Guid, "UndeadRegionCards");

            internal static bool MagnificusEnabled => Chainloader.PluginInfos.ContainsKey(MagnificusGuid);
            internal static bool GrimoraEnabled => Chainloader.PluginInfos.ContainsKey(GrimoraGuid);
            internal static bool P03Enabled => Chainloader.PluginInfos.ContainsKey(P03Guid);
            internal static bool P03SigilsEnabled => Chainloader.PluginInfos.ContainsKey(P03Sigil);

            public static bool IsP03Run => P03Enabled && SaveFile.IsAscension && SaveManager.SaveFile.IsPart3;
            public static bool IsGrimoraRun => GrimoraEnabled && SaveFile.IsAscension && SaveManager.SaveFile.IsGrimora;
            internal static bool IsMagnificusRun => false; // sub

            internal static Ability GetGrimoraAbility(string rulebookName, Ability fallback)
            {
                Ability ab = GuidManager.GetEnumValue<Ability>(GrimoraGuid, rulebookName);
                if (AbilityManager.AllAbilities.AbilityByID(ab) != null)
                    return ab;

                return fallback;
            }
            internal static Ability GetP03Ability(string rulebookName, Ability fallback)
            {
                Ability ab = GuidManager.GetEnumValue<Ability>(P03Sigil, rulebookName);
                if (AbilityManager.AllAbilities.AbilityByID(ab) != null)
                    return ab;

                return fallback;
            }
            internal static Ability GetP03RunAbility(string rulebookName, Ability fallback)
            {
                Ability ab = GuidManager.GetEnumValue<Ability>(P03Guid, rulebookName);
                if (AbilityManager.AllAbilities.AbilityByID(ab) != null)
                    return ab;

                return fallback;
            }
            internal static Ability GetMagnificusAbility(string rulebookName, Ability fallback)
            {
                Ability ab = GuidManager.GetEnumValue<Ability>(MagnificusGuid, rulebookName);
                if (AbilityManager.AllAbilities.AbilityByID(ab) != null)
                    return ab;

                return fallback;
            }
            internal static Opponent.Type GetP03Boss(string name, Opponent.Type fallback)
            {
                if (P03Enabled)
                    return GuidManager.GetEnumValue<Opponent.Type>(P03Guid, name);

                return fallback;
            }

            internal static void SetFuel(CardInfo card, int fuel)
            {
                card.SetExtendedProperty("FuelManager.StartingFuel", Mathf.Min(4, fuel));
            }
            internal static void AddPart3Decal(CardInfo card, Texture2D decal)
            {
                card.AddPart3Decal(decal);
            }

            internal static void SetManaCost(CardInfo card, int manaCost)
            {
                card.SetBloodCost(manaCost);
                card.SetExtendedProperty("ManaCost", true);
            }

            internal static bool HasManaCost(CardInfo card)
            {
                return card.GetExtendedPropertyAsBool("ManaCost") ?? false && card.BloodCost > 0;
            }

            internal static Assembly MagnificusAsm
            {
                get
                {
                    if (_magnificusAsm == null)
                    {
                        _magnificusAsm = Assembly.GetAssembly(typeof(SigilCode.MoxCycling));
                    }
                    return _magnificusAsm;
                }
            }
            private static Assembly _magnificusAsm = null;
        }
        private static void CreateCardPack()
        {
            PackInfo act1Pack = PackManager.GetPackInfo<PackInfo>(pluginPrefix);
            act1Pack.Title = pluginName;
            act1Pack.SetTexture(TextureHelper.GetImageAsTexture("bbp_pack.png", Assembly));
            act1Pack.Description = $"14 delicious ingredients for all your pastry-making needs!";
            act1Pack.ValidFor.Clear();
            act1Pack.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);

            PackInfo pack2 = PackManager.GetPackInfo<PackInfo>(pluginPrefixG);
            pack2.Title = "Bonnie's Bony Pack";
            pack2.SetTexture(TextureHelper.GetImageAsTexture("bbp_pack_grimora.png", Assembly));
            pack2.Description = $"14 devilish ingredients for all your grave-raising needs! Now featuring ghosts and ghoulies!";
            pack2.ValidFor.Clear();
            pack2.ValidFor.Add(PackInfo.PackMetacategory.GrimoraPack);

            PackInfo pack3 = PackManager.GetPackInfo<PackInfo>(pluginPrefix3);
            pack3.Title = "Bonnie's Bot Pack";
            pack3.SetTexture(TextureHelper.GetImageAsTexture("bbp_pack_act3.png", Assembly));
            pack3.Description = $"14 dismantled ingredients for all your manufacturing needs! Allergy warning: contains nuts and bolts.";
            pack3.ValidFor.Clear();
            pack3.ValidFor.Add(PackInfo.PackMetacategory.P03Pack);

            PackInfo packM = PackManager.GetPackInfo<PackInfo>(pluginPrefixM);
            packM.Title = "Bonnie's Bauble Pack";
            packM.SetTexture(TextureHelper.GetImageAsTexture("bbp_pack_magnificus.png", Assembly));
            packM.Description = $"14 dazzling ingredients for all your thaumoturgical needs!";
            packM.ValidFor.Clear();
            packM.ValidFor.Add(PackInfo.PackMetacategory.MagnificusPack);

            if (SplitByAct.Value)
            {
                act1Pack.ValidFor.Add(PackInfo.PackMetacategory.GrimoraPack);
                act1Pack.ValidFor.Add(PackInfo.PackMetacategory.P03Pack);
                act1Pack.ValidFor.Add(PackInfo.PackMetacategory.MagnificusPack);

                pack2.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
                pack2.ValidFor.Add(PackInfo.PackMetacategory.P03Pack);
                pack2.ValidFor.Add(PackInfo.PackMetacategory.MagnificusPack);

                pack3.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
                pack3.ValidFor.Add(PackInfo.PackMetacategory.GrimoraPack);
                pack3.ValidFor.Add(PackInfo.PackMetacategory.MagnificusPack);

                packM.ValidFor.Add(PackInfo.PackMetacategory.LeshyPack);
                packM.ValidFor.Add(PackInfo.PackMetacategory.GrimoraPack);
                packM.ValidFor.Add(PackInfo.PackMetacategory.P03Pack);
            }
        }

        internal static Texture2D GetTexture(string fileName) => TextureHelper.GetImageAsTexture(fileName, Assembly);
        internal static Texture2D GetTexture(string fileName, Assembly asm) => TextureHelper.GetImageAsTexture(fileName, asm);

        internal static FaceAnim MakeFaceAnim(string openName, string closedName = null)
        {
            Texture2D openTex = GetTexture(openName);
            Rect rect = new(0f, 0f, openTex.width, openTex.height);
            Sprite openSprite = Sprite.Create(openTex, rect, new(0.5f, 0f), 100f);

            if (closedName != null)
            {
                Texture2D closedTex = GetTexture(closedName);
                Sprite closedSprite = Sprite.Create(closedTex, rect, new(0.5f, 0f), 100f);
                return new(openSprite, closedSprite);
            }

            return new(openSprite, null);
        }
        internal static InscryptionAPI.Dialogue.CustomLine NewLine(string dialogue, Emotion emotion) => new() { text = dialogue, emotion = emotion };

        private void OnDisable() => HarmonyInstance.UnpatchSelf();

        public static readonly List<CardInfo> Act1Cards = new();
        public static readonly List<CardInfo> GrimoraCards = new();
        public static readonly List<CardInfo> P03Cards = new();
        public static readonly List<CardInfo> MagnificusCards = new();

        internal static ConfigEntry<bool> BingusCrash;
        internal static ConfigEntry<bool> SplitByAct;

        internal static List<AudioClip> AudioClips;

        private static readonly Harmony HarmonyInstance = new(pluginGuid);
        internal static ManualLogSource Log;
        internal static Assembly Assembly;
        internal static ConfigFile Configs;

        public const string pluginGuid = "whistlewind.inscryption.bonniesbakingpack";
        public const string pluginPrefix = "bbp_act1";
        public const string pluginPrefixG = "bbp_grimora";
        public const string pluginPrefix3 = "bbp_act3";
        public const string pluginPrefixM = "bbp_magnificus";

        public const string pluginName = "Bonnie's Baking Pack";
        private const string pluginVersion = "1.1.1";
    }

    internal static class Extensions
    {
        /// <remarks>
        /// Must be called AFTER SetRare or SetPart...Card
        /// </remark>
        internal static CardInfo AddAct1(this CardInfo info)
        {
            if (!BakingPlugin.Act1Cards.Contains(info))
                BakingPlugin.Act1Cards.Add(info);

            if (info.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare))
            {
                info.SetGBCPlayable(info.temple);
            }
            else
            {
                info.AddMetaCategories(CardMetaCategory.GBCPack);
            }
            return info.SetCardTemple(CardTemple.Nature);
        }
        /// <remarks>
        /// Must be called AFTER SetRare or SetPart...Card
        /// </remark>
        internal static CardInfo AddGrimora(this CardInfo info, bool cardChoice = true)
        {
            if (!BakingPlugin.GrimoraCards.Contains(info))
                BakingPlugin.GrimoraCards.Add(info);

            if (cardChoice)
            {
                if (BakingPlugin.ScrybeCompat.GrimoraEnabled && info.LacksCardMetaCategory(CardMetaCategory.Rare))
                    info.AddMetaCategories(BakingPlugin.ScrybeCompat.GrimoraChoice);
                
                info.AddMetaCategories(CardMetaCategory.TraderOffer);
            }
            else info.metaCategories.Clear();

            return info.SetCardTemple(CardTemple.Undead);
        }
        /// <remarks>
        /// Must be called AFTER SetRare or SetPart...Card
        /// </remark>
        internal static CardInfo AddP03(this CardInfo info, bool overrideRandom = false)
        {
            if (!BakingPlugin.P03Cards.Contains(info))
                BakingPlugin.P03Cards.Add(info);

            if (!overrideRandom && info.LacksCardMetaCategory(CardMetaCategory.Rare))
                info.AddMetaCategories(CardMetaCategory.Part3Random);

            if (info.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare))
            {
                info.AddMetaCategories(CardMetaCategory.TraderOffer);
            }
            else info.metaCategories.Clear();

            return info.SetCardTemple(CardTemple.Tech);
        }
        /// <remarks>
        /// Must be called AFTER SetRare or SetPart...Card
        /// </remark>
        internal static CardInfo AddMagnificus(this CardInfo info, bool overrideChoice = false)
        {
            if (!BakingPlugin.MagnificusCards.Contains(info))
                BakingPlugin.MagnificusCards.Add(info);

            if (info.HasAnyOfCardMetaCategories(CardMetaCategory.ChoiceNode, CardMetaCategory.Rare) && !overrideChoice)
            {
                info.AddMetaCategories(CardMetaCategory.TraderOffer);
            }
            else info.metaCategories.Clear();

            return info.SetCardTemple(CardTemple.Wizard);
        }
    }
}
