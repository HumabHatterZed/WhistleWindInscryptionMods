﻿using BepInEx;
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

        internal const string PluginGuid = "zorro.inscryption.infiniscryption.spells";
        internal const string PluginName = "New Infiniscryption Spells";
        internal const string PluginVersion = "1.0.0";
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

        private void Awake()
        {
            Log = base.Logger;

            Harmony harmony = new(PluginGuid);

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
                SpellCards.RegisterCustomCards(harmony);

            // This makes sure that all cards with the spell ability are properly given all of the various components of a spell
            CardManager.ModifyCardList += delegate (List<CardInfo> cards)
            {
                foreach (CardInfo card in cards)
                {
                    if (card.IsTargetedSpell())
                    {
                        if (!card.hideAttackAndHealth && (card.baseHealth > 0 || card.baseAttack > 0))
                            card.SetTargetedSpellStats();
                        else
                            card.SetTargetedSpell();
                    }

                    if (card.IsGlobalSpell())
                    {
                        if (!card.hideAttackAndHealth && (card.baseHealth > 0 || card.baseAttack > 0))
                            card.SetGlobalSpellStats();
                        else
                            card.SetGlobalSpell();
                    }
                }
                return cards;
            };

            Logger.LogInfo($"{PluginName} is loaded!");
        }
    }
}