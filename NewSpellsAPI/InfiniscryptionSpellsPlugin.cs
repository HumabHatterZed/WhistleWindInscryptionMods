using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using DiskCardGame;
using HarmonyLib;
using Infiniscryption.Spells.Patchers;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using System.Collections.Generic;
using System.Reflection;

namespace Infiniscryption.Spells
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class InfiniscryptionSpellsPlugin : BaseUnityPlugin
    {

        internal const string OriginalPluginGuid = "zorro.infiniscryption.sigils"; // This was the ID in previous versions

        public const string PluginGuid = "zorro.inscryption.infiniscryption.spells";
        internal const string PluginName = "New Infiniscryption Spells";
        internal const string PluginVersion = "1.2.2";
        internal const string CardPrefix = "ZSPL";

        internal static ManualLogSource Log;

        private bool AddCards =>
            Config.Bind("InfiniscryptionSpells",
                "AddCards", false,
                new ConfigDescription("If true, this will add the sample cards to the card pool.")).Value;
        private bool AllowStatBoost =>
            Config.Bind("InfiniscryptionSpells",
                "AllowStatBoost", true,
                new ConfigDescription("If true, this will allow stat-showing spells to be buffed at campfires.")).Value;

        private bool AllowCardMerge =>
            Config.Bind("InfiniscryptionSpells",
                "AllowCardMerge", true,
                new ConfigDescription("If true, this will allow spell cards to gain and transfer their sigils.")).Value;

        internal static bool SpellMerge { get; private set; }
        private void Awake()
        {
            Log = base.Logger;

            Harmony harmony = new(PluginGuid);

            SpellMerge = AllowCardMerge;
            harmony.PatchAll(typeof(SpellBehavior));

            // patch only if true
            if (AllowStatBoost)
            {
                var baseMethod = typeof(CardStatBoostSequencer).GetMethod(nameof(CardStatBoostSequencer.GetValidCards), BindingFlags.NonPublic | BindingFlags.Instance);
                var patchMethod = typeof(SpellBehavior).GetMethod(nameof(SpellBehavior.AllowStatBoostForSpells));
                harmony.Patch(baseMethod, postfix: new(patchMethod));
            }

            TargetedSpellAbility.Register();
            GlobalSpellAbility.Register();
            InstaGlobalSpellAbility.Register();
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(SpellBehavior.SpellBackgroundAppearance).TypeHandle);
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(SpellBehavior.RareSpellBackgroundAppearance).TypeHandle);

            DrawTwoCards.Register();
            DestroyAllCardsOnDeath.Register();
            DirectDamage.Register();
            DirectHeal.Register();
            AttackBuff.Register();
            AttackNerf.Register();
            Fishhook.Register();
            GiveStats.Register();
            GiveSigils.Register();
            GiveStatsSigils.Register();

            if (AddCards)
                SpellCards.RegisterCustomCards();

            // This makes sure that all cards with the spell special ability are properly given all of the various components of a spell
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards)
                {
                    if (card.IsTargetedSpell() && card.SpecialStatIcon != TargetedSpellAbility.Icon)
                    {
                        card.SetTargetedSpell();
                        if (!card.hideAttackAndHealth && (card.baseHealth > 0 || card.baseAttack > 0))
                            card.SetHideStats(false);
                    }

                    if (card.IsGlobalSpell() && (card.SpecialStatIcon != GlobalSpellAbility.Icon || card.SpecialStatIcon != InstaGlobalSpellAbility.Icon))
                    {
                        if (card.IsInstaGlobalSpell())
                            card.SetInstaGlobalSpell();
                        else
                            card.SetGlobalSpell();

                        // if show stats
                        if (!card.hideAttackAndHealth && (card.baseHealth > 0 || card.baseAttack > 0))
                            card.SetHideStats(false);

                    }
                }
                return cards;
            };

            Logger.LogInfo($"{PluginName} is loaded!");
        }
    }
}
