using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using DiskCardGame;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WhistleWindLobotomyMod
{   
    public static class WstlUtils
    {
        // Base code taken from GrimoraMod and SigilADay_julienperge

        #region Card-adding method
        public static void Add(
            string name, string displayName, string description,
            int baseAttack, int baseHealth, int bloodCost, int bonesCost,
            byte[] defaultTexture, byte[] emissionTexture = null,
            byte[] altTexture = null, byte[] emissionAltTexture = null, byte[] titleTexture = null,
            List<Ability> abilities = null, List<SpecialTriggeredAbility> specialAbilities = null,
            List<CardMetaCategory> metaCategories = null, List<Tribe> tribes = null, List<Trait> traits = null,
            bool isTerrain = false, bool isChoice = false, bool isRare = false,
            List<CardAppearanceBehaviour.Appearance> appearances = null, List<Texture> decals = null,
            string iceCubeName = null, string evolveName = null, int numTurns = 1,
            string tailName = null, byte[] tailTexture = null,
            bool onePerDeck = false, bool hideStats = false,
            int riskLevel = 0, bool isDonator = false
            )
        {
            abilities ??= new();
            specialAbilities ??= new();
            metaCategories ??= new();
            tribes ??= new();
            traits ??= new();
            appearances ??= new();
            decals ??= new();

            Texture2D texture = LoadTextureFromResource(defaultTexture);
            Texture2D emissionTex = null;
            Texture2D altTex = null;
            Texture2D emissionAltTex = null;
            Texture titleTex = null;
            Texture2D tailTex = null;

            if (emissionTexture != null) {
                emissionTex = LoadTextureFromResource(emissionTexture);
            }
            if (altTexture != null) {
                altTex = LoadTextureFromResource(altTexture);
            }
            if (emissionAltTexture != null)
            {
                emissionAltTex = LoadTextureFromResource(emissionAltTexture);
            }
            if (titleTexture != null) {
                titleTex = LoadTextureFromResource(titleTexture);
            }
            if (tailTexture != null) {
                tailTex = LoadTextureFromResource(titleTexture);
            }

            // For later riskLevel system, currently does nothing
            string risk = riskLevel switch
            {
                1 => "ZAYIN", 2 => "TETH", 3 => "HE",
                4 => "WAW", 5 => "ALEPH", _ => null
            };

            CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();

            cardInfo.SetPortrait(texture, emissionTex);
            cardInfo.name = name;
            cardInfo.displayedName = displayName;
            cardInfo.description = description;
            cardInfo.baseAttack = baseAttack;
            cardInfo.baseHealth = baseHealth;
            cardInfo.cost = bloodCost;
            cardInfo.bonesCost = bonesCost;
            cardInfo.abilities = abilities;
            cardInfo.specialAbilities = specialAbilities;
            cardInfo.metaCategories = metaCategories;
            cardInfo.tribes = tribes;
            cardInfo.traits = traits;
            cardInfo.appearanceBehaviour = appearances;
            cardInfo.decals = decals;
            cardInfo.cardComplexity = CardComplexity.Simple;
            cardInfo.temple = CardTemple.Nature;
            cardInfo.onePerDeck = onePerDeck;
            cardInfo.SetExtendedProperty("RiskLevel", risk);
            cardInfo.hideAttackAndHealth = hideStats;
            if (altTex != null)
            {
                cardInfo.SetAltPortrait(altTex);
            }
            //if (emissionAltTex != null)
            //{
            //    cardInfo.SetEmissiveAltPortrait(emissionAltTex);
            //}
            if (isTerrain)
            {
                cardInfo.SetTerrain();
            }
            if (isChoice)
            {
                if (!isDonator || (isDonator && !ConfigUtils.Instance.NoDonators))
                {
                    cardInfo.SetDefaultPart1Card();
                }
            }
            if (isRare)
            {
                if (!isDonator || (isDonator && !ConfigUtils.Instance.NoDonators))
                {
                    cardInfo.SetRare();
                }
                else if (isDonator && ConfigUtils.Instance.NoDonators)
                {
                    cardInfo.appearanceBehaviour = new() { CardAppearanceBehaviour.Appearance.RareCardBackground };
                }
            }
            if (titleTexture != null)
            {
                cardInfo.titleGraphic = titleTex;
            }
            if (iceCubeName != null)
            {
                cardInfo.SetIceCube(iceCubeName);
            }
            if (evolveName != null)
            {
                cardInfo.SetEvolve(evolveName, numTurns);
            }
            if (tailName != null)
            {
                cardInfo.SetTail(tailName, tailTex);
            }
            cardInfo.onePerDeck = onePerDeck;
            CardManager.Add(WstlPlugin.modPrefix, cardInfo);
        }
        #endregion

        #region Ability-adding method
        public static AbilityManager.FullAbility CreateAbility<T>(
            byte[] texture,
            string rulebookName, string rulebookDescription, string dialogue,
            int powerLevel = 0,
            bool addModular = false, bool isPassive = false, bool canStack = false,
            bool opponent = false, bool flipY = false, byte[] customY = null,
            bool overrideModular = false)
            where T:AbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            Texture2D tex = LoadTextureFromResource(texture);
            Texture2D flippedTex = null;
            if (customY != null) { flippedTex = LoadTextureFromResource(customY); }

            List<AbilityMetaCategory> list = new()
            {
                AbilityMetaCategory.Part1Rulebook
            };

            if ((addModular || ConfigUtils.Instance.AllModular) && !overrideModular)
            {
                list.Add(AbilityMetaCategory.Part1Modular);
            }

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.passive = isPassive;
            info.canStack = canStack;
            info.opponentUsable = opponent;
            info.flipYIfOpponent = flipY;
            info.metaCategories = list;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
            info.activated = false;
            if (flippedTex != null) { info.SetCustomFlippedTexture(flippedTex); }

            return AbilityManager.Add(WstlPlugin.modPrefix, info, typeof(T), tex);
        }
        #endregion

        #region Activated ability-adding method
        public static AbilityManager.FullAbility CreateActivatedAbility<T>(
            byte[] texture,
            string rulebookName, string rulebookDescription, string dialogue,
            int powerLevel = 0) where T:ActivatedAbilityBehaviour
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();

            Texture2D tex = LoadTextureFromResource(texture);

            List<AbilityMetaCategory> list = new()
            {
                AbilityMetaCategory.Part1Rulebook
            };

            info.rulebookName = rulebookName;
            info.rulebookDescription = rulebookDescription;
            info.powerLevel = powerLevel;
            info.passive = false;
            info.canStack = false;
            info.opponentUsable = false;
            info.flipYIfOpponent = false;
            info.activated = true;
            info.metaCategories = list;
            info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);

            return AbilityManager.Add(WstlPlugin.modPrefix, info, typeof(T), tex);
        }
        #endregion

        // SpecialAbilities
        public static SpecialTriggeredAbilityManager.FullSpecialTriggeredAbility CreateSpecialAbility<T>(string rulebookName, string rulebookDesc)
            where T : SpecialCardBehaviour
        {
            return SpecialTriggeredAbilityManager.Add(WstlPlugin.modPrefix, rulebookName, typeof(T));
        }

        // Adds AbilityInfo dialogue
        public static DialogueEvent.LineSet SetAbilityInfoDialogue(string dialogue)
        {
            return new DialogueEvent.LineSet(new List<DialogueEvent.Line>()
                {
                    new()
                    {
                        text = dialogue
                    }
                }
            );
        }
        // For loading textures from resource files
        public static Texture2D LoadTextureFromResource(byte[] resourceFile)
        {
            var texture = new Texture2D(2, 2);
            texture.LoadImage(resourceFile);
            texture.filterMode = FilterMode.Point;
            return texture;
        }

        // For debugging
        public static void GetPowerLevels()
        {
            StringBuilder sb = new StringBuilder();
            List<CardInfo> info = InscryptionAPI.Card.CardManager.AllCardsCopy;
            string dl = "|";
            for (int i = 0; i < info.Count; i++)
            {
                if (info[i].name.Contains("wstl_"))
                {
                    string p = $"Powerlevel: {info[i].PowerLevel}";
                    string bc = $"BloodCost: {info[i].BloodCost}";
                    string bb = $"BoneCost: {info[i].BonesCost}";
                    sb.Append(info[i].name + dl + p + dl + bc + dl + bb + "\n");
                }
            }
            File.WriteAllText(Path.Combine(WstlPlugin.Directory, "WSTL_POWERLEVEL.txt"), sb.ToString());
            WstlPlugin.Log.LogDebug($"WSTL_POWERLEVEL.txt generated in plugins folder.");
        }
    }
}
