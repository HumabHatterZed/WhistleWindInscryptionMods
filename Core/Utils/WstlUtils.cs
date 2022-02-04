using System;
using System.Collections.Generic;
using System.Reflection;
using APIPlugin;
using DiskCardGame;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public static class WstlUtils
    {
		// Ripped wholesale from GrimoraMod and SigilADay_julienperge
		#region Cards
		public static void Add(
			string name, string displayName,
			string description,
			int baseHealth, int baseAttack,
			int bloodCost, int bonesCost,
			byte[] defaultTexture,
			Ability ability = Ability.NUM_ABILITIES,
			SpecialAbilityIdentifier specialAbility = null,
			Tribe tribe = Tribe.NUM_TRIBES,
			SpecialTriggeredAbility triggeredAbility = SpecialTriggeredAbility.NUM_ABILITIES,
			CardMetaCategory metaCategory = CardMetaCategory.NUM_CATEGORIES,
			CardComplexity complexity = CardComplexity.Simple,
			byte[] emissionTexture = null,
			List<Texture> decals = null,
			List<CardAppearanceBehaviour.Appearance> appearanceBehaviour = null,
			IceCubeIdentifier iceCubeId = null,
			EvolveIdentifier evolveId = null,
			TailIdentifier tailId = null,
			List<Trait> traits = null,
			bool onePerDeck = false)
		{
			var abilities = new List<Ability>();
			if (ability != Ability.NUM_ABILITIES)
			{
				abilities.Add(ability);
			}

			var specialAbilities = new List<SpecialAbilityIdentifier>();
			if (specialAbility != null)
			{
				specialAbilities.Add(specialAbility);
			}

			var tribes = new List<Tribe>();
			if (tribe != Tribe.NUM_TRIBES)
			{
				tribes.Add(tribe);
			}

			var triggeredAbilities = new List<SpecialTriggeredAbility>();
			if (triggeredAbility != SpecialTriggeredAbility.NUM_ABILITIES)
			{
				triggeredAbilities.Add(triggeredAbility);
			}

			Add(
				name, displayName, description,
				baseHealth, baseAttack,
				bloodCost, bonesCost, defaultTexture, abilities, specialAbilities, tribes, triggeredAbilities,
				metaCategory, complexity, emissionTexture, decals, appearanceBehaviour, iceCubeId, evolveId, tailId, traits, onePerDeck);
		}

		public static void Add(
			string name, string displayName,
			string description,
			int baseHealth, int baseAttack,
			int bloodCost, int bonesCost,
			byte[] defaultTexture,
			List<Ability> abilities,
			List<SpecialAbilityIdentifier> specialAbilities,
			List<Tribe> tribes,
			List<SpecialTriggeredAbility> triggeredAbilities = null,
			CardMetaCategory metaCategory = CardMetaCategory.NUM_CATEGORIES,
			CardComplexity complexity = CardComplexity.Simple,
			byte[] emissionTexture = null,
			List<Texture> decals = null,
			List<CardAppearanceBehaviour.Appearance> appearanceBehaviour = null,
			IceCubeIdentifier iceCubeId = null,
			EvolveIdentifier evolveId = null,
			TailIdentifier tailId = null,
			List<Trait> traits = null,
			bool onePerDeck = false)
		{
			var metaCategories = new List<CardMetaCategory>();

			switch (metaCategory)
			{
				case CardMetaCategory.Rare:
				case CardMetaCategory.GBCPlayable:
					metaCategories.Add(metaCategory);
					break;
				case CardMetaCategory.ChoiceNode:
					metaCategories = CardUtils.getNormalCardMetadata;
					break;
			}
			decals ??= new List<Texture>();
			traits ??= new List<Trait>();
			appearanceBehaviour ??= new List<CardAppearanceBehaviour.Appearance>();
			abilities ??= new List<Ability>();
			specialAbilities ??= new List<SpecialAbilityIdentifier>();
			tribes ??= new List<Tribe>();

			CardInfo cardInfo = ScriptableObject.CreateInstance<CardInfo>();

			cardInfo.name = name;
			cardInfo.displayedName = displayName;
			cardInfo.baseHealth = baseHealth;
			cardInfo.baseAttack = baseAttack;
			cardInfo.metaCategories = metaCategories;
			cardInfo.cardComplexity = complexity;
			cardInfo.temple = CardTemple.Nature;
			cardInfo.description = description;
			cardInfo.cost = bloodCost;
			cardInfo.bonesCost = bonesCost;
			cardInfo.abilities = abilities;
			cardInfo.tribes = tribes;
			cardInfo.decals = decals;
			cardInfo.appearanceBehaviour = appearanceBehaviour;
			cardInfo.traits = traits;
			cardInfo.onePerDeck = onePerDeck;

			var texture = ImageUtils.LoadTextureFromResource(defaultTexture);
			/*cardInfo.portraitTex = Sprite.Create(
				texture, CardUtils.DefaultCardArtRect, new Vector2(0.5f, 0.5f));
			*/

			var emissionTex = ImageUtils.LoadTextureFromResource(defaultTexture);
			if (emissionTexture != null)
			{
				emissionTex = ImageUtils.LoadTextureFromResource(emissionTexture);
			}

			NewCard.Add(name, displayName, baseAttack, baseHealth,
				metaCategories, complexity, CardTemple.Nature,
				description, false, bloodCost, bonesCost, 0, null,
				SpecialStatIcon.None, tribes, traits, triggeredAbilities, abilities,
				null, specialAbilitiesIdsParam: specialAbilities,
				null, null, null, null, false,
				onePerDeck, appearanceBehaviour, texture,
				null, null, null, emissionTex, null, decals,
				evolveId: evolveId, iceCubeId: iceCubeId, tailId: tailId);

			//NewCard.Add(cardInfo, specialAbilitiesIdsParam: specialAbilities, iceCubeId: iceCubeId, evolveId: evolveId);
		}
		#endregion

		#region Abilities
		public static AbilityInfo CreateInfoWithDefaultSettings(
			string rulebookName, string rulebookDescription, string dialogue,
			bool addModular = false, bool isPassive = false, bool canStack = false,
			bool withDialogue = true, int powerLevel = 0)
		{
			AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
			info.powerLevel = powerLevel;
			info.rulebookName = rulebookName;
			info.rulebookDescription = rulebookDescription;
			info.passive = isPassive;
			info.canStack = canStack;
			if (addModular)
			{
				info.metaCategories = new List<AbilityMetaCategory>(){
					AbilityMetaCategory.Part1Modular,
					AbilityMetaCategory.Part1Rulebook};

			}
			else
			{
				info.metaCategories = new List<AbilityMetaCategory>()
				{AbilityMetaCategory.Part1Rulebook};
			}

			if (withDialogue)
			{
				info.abilityLearnedDialogue = SetAbilityInfoDialogue(dialogue);
			}
			return info;
		}
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

		// Input method.  Grabs resource file and converts to Texture
		public static NewAbility CreateAbility<T>(
			byte[] texture, string rulebookName, string rulebookDescription,
			string dialogue, int powerLevel = 0, bool addModular = false,
			bool isPassive = false, bool canStack = false)
			where T : AbilityBehaviour
		{
			return CreateAbility<T>(
				ImageUtils.LoadTextureFromResource(texture),
				rulebookName, rulebookDescription,
				dialogue, powerLevel, addModular, isPassive, canStack);
		}
		// Uses the above info to generate AbilityInfo (Need both for the conversion from byte[] to Texture?)
		public static NewAbility CreateAbility<T>(
			Texture texture,
			string rulebookName,
			string rulebookDescription,
			string dialogue,
			int powerLevel = 0,
			bool addModular = false,
			bool isPassive = false,
			bool canStack = false)
			where T : AbilityBehaviour
		{
			return CreateAbility<T>(
				CreateInfoWithDefaultSettings(
					rulebookName, rulebookDescription,
					dialogue, addModular, isPassive, canStack, powerLevel: powerLevel), texture);
		}
		// Instantiates ability
		private static NewAbility CreateAbility<T>(
			AbilityInfo info,
			Texture texture)
			where T : AbilityBehaviour
		{
			Type type = typeof(T);
			// instantiate
			var newAbility = new NewAbility(
				info, type, texture, GetAbilityId(info.rulebookName));
			// Get static field
			FieldInfo field = type.GetField("ability",
				BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
			field.SetValue(null, newAbility.ability);

			return newAbility;
		}

		#endregion

		#region SpecialAbilities
		public static StatIconInfo CreateSpecialInfoWithDefaultSettings(
			string rulebookName, string rulebookDescription, Texture iconGraphic,
			bool appliesToHealth = false, bool appliesToAttack = false, bool inRulebook = false)
		{
			StatIconInfo specialInfo = ScriptableObject.CreateInstance<StatIconInfo>();
			specialInfo.rulebookName = rulebookName;
			specialInfo.rulebookDescription = rulebookDescription;
			specialInfo.appliesToHealth = appliesToHealth;
			specialInfo.appliesToAttack = appliesToAttack;
			specialInfo.iconGraphic = iconGraphic;

			if (inRulebook || Plugin.SpecialsInRulebook)
			{
				specialInfo.metaCategories = new List<AbilityMetaCategory>(){
					AbilityMetaCategory.Part1Rulebook};
			}
			else
            {
				specialInfo.metaCategories = new List<AbilityMetaCategory>()
				{
					AbilityMetaCategory.None
				};
			}
			return specialInfo;
		}

		// Input method.  Grabs resource file and converts to Texture
		public static NewSpecialAbility CreateSpecialAbility<T>(
			byte[] iconGraphic,
			string rulebookName, string rulebookDescription,
			bool appliesToHealth = false, bool appliesToAttack = false, bool inRulebook = false)
			where T : SpecialCardBehaviour
		{
			return CreateSpecialAbility<T>(
				ImageUtils.LoadTextureFromResource(iconGraphic),
				rulebookName, rulebookDescription,
				appliesToHealth, appliesToAttack, inRulebook);
		}

		public static NewSpecialAbility CreateSpecialAbility<T>(
			Texture iconGraphic,
			string rulebookName, string rulebookDescription,
			bool appliesToHealth, bool appliesToAttack, bool inRulebook)
			where T : SpecialCardBehaviour
		{
			return CreateSpecialAbility<T>(
				CreateSpecialInfoWithDefaultSettings(
					rulebookName, rulebookDescription, iconGraphic,
					appliesToHealth, appliesToAttack, inRulebook));
		}
		
		public static NewSpecialAbility CreateSpecialAbility<T>(StatIconInfo specialInfo)
			where T : SpecialCardBehaviour
		{
			Type type = typeof(T);

			var newSpecialAbility = new NewSpecialAbility(type, GetSpecialAbilityId(specialInfo.rulebookName), statIconInfo: specialInfo);

			return newSpecialAbility;
		}
		
		#endregion
		
		public static AbilityIdentifier GetAbilityId(string rulebookName)
		{
			return AbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, rulebookName);
		}
		public static SpecialAbilityIdentifier GetSpecialAbilityId(string rulebookName)
		{
			return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, rulebookName);
		}
	}
}