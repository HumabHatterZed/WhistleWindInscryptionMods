using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.PackManagement;
using InscryptionAPI;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Triggers;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void AddAbilities()
        {
            StatIconInfo bingusStatInfo = ScriptableObject.CreateInstance<StatIconInfo>();
            bingusStatInfo.rulebookName = "Infinity";
            bingusStatInfo.rulebookDescription = "The value represented with this sigil will be infinite.";
            bingusStatInfo.SetIcon(TextureHelper.GetImageAsTexture("infiniteSigil.png", Assembly));
            bingusStatInfo.appliesToAttack = true;
            bingusStatInfo.appliesToHealth = true;
            bingusStatInfo.SetDefaultPart1Ability();

            BingusStatIcon.Icon = StatIconManager.Add(pluginGuid, bingusStatInfo, typeof(BingusStatIcon)).Id;
        }
    }

    public class BingusAbility : SpecialCardBehaviour, IGetAttackingSlots
    {
        public static SpecialTriggeredAbility SpecialAbility;

        public List<CardSlot> GetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots)
        {
            return currentSlots;
            base.PlayableCard.TemporaryMods.Add(new CardModificationInfo(1, 0));
            AudioController.Instance.PlaySound2D("glitch");
            Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.3f);
            Application.Quit();
            return currentSlots;
        }

        public bool RespondsToGetAttackingSlots(bool playerIsAttacker, List<CardSlot> originalSlots, List<CardSlot> currentSlots) => true;

        public int TriggerPriority(bool playerIsAttacker, List<CardSlot> originalSlots) => 0;
    }
    public class BingusStatIcon : VariableStatBehaviour
    {
        public static SpecialStatIcon Icon;
        public override SpecialStatIcon IconType => Icon;

        public override int[] GetStatValues()
        {
            return new int[] { int.MaxValue / 2, int.MaxValue / 2 };
        }
    }
}
