using DiskCardGame;
using Infiniscryption.Core.Helpers;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;

namespace Infiniscryption.Spells.Patchers
{
    public static class SpellCards
    {
        internal static void RegisterCustomCards()
        {
            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Kettle_of_Avarice",
                    "Kettle of Avarice",
                    0, 0, // attack/health
                    "A jug that allows you to draw two more cards from your deck.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("kettle_of_avarice"))
                .SetGlobalSpell()
                .SetCost(bloodCost: 1)
                .AddAbilities(DrawTwoCards.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Anger_of_the_Gods",
                    "Anger of the Gods",
                    0, 0, // attack/health
                    "For when nothing else will do the trick.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("anger_of_all"))
                .SetGlobalSpell()
                .SetRare()
                .SetCost(bloodCost: 2)
                .AddAbilities(DestroyAllCardsOnDeath.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Lightning",
                    "Lightning",
                    0, 0, // attack/health
                    "A perfectly serviceable amount of damage.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("lightning_bolt"))
                .SetTargetedSpell()
                .SetCost(bloodCost: 1)
                .AddAbilities(DirectDamage.AbilityID, DirectDamage.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Backpack",
                    "Trip to the Store",
                    0, 0, // attack/health
                    "Send one of your creatures on a trip to the store. Who knows what they will come back with.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("backpack"))
                .SetGlobalSpell()
                .SetCost(bloodCost: 2)
                .AddAbilities(Ability.RandomConsumable);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Rot_Healing",
                    "Rot Healing",
                    0, 0, // attack/health
                    "Restores just a little bit of health.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("plague_doctor"))
                .SetTargetedSpell()
                .SetCost(bonesCost: 1)
                .AddAbilities(DirectHeal.AbilityID, DirectHeal.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Dammed_up",
                    "Dammed Up",
                    0, 0, // attack/health
                    "So many dams...")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("dammed_up"))
                .SetTargetedSpell()
                .SetCost(bloodCost: 1)
                .AddAbilities(Ability.AllStrike, Ability.CreateDams);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Irritate",
                    "Irritate",
                    0, 0, // attack/health
                    "This is what happens when you poke the bear...or wolf.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("snarling_wolf"))
                .SetTargetedSpell()
                .SetCost(bonesCost: 2)
                .AddAbilities(AttackBuff.AbilityID, DirectDamage.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Compost",
                    "Compost",
                    0, 0, // attack/health
                    "Time to recycle those old bones.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("compost"))
                .SetGlobalSpell()
                .SetCost(bonesCost: 5)
                .AddAbilities(DrawTwoCards.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Fetch",
                    "Go Fetch",
                    0, 0, // attack/health
                    "Good doggy.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("wolf_fetch"))
                .SetGlobalSpell()
                .AddAbilities(Ability.QuadrupleBones);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Body",
                    "Body Without a Soul",
                    0, 3, // attack/health
                    "An husk to be used and discarded.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("body"))
                .SetTargetedSpellStats()
                .SetCost(bonesCost: 2)
                .AddAbilities(GiveStats.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Soul",
                    "Soul Without a Body",
                    0, 0, // attack/health
                    "A disembodied soul. It will empower one of your creatures.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("soul"))
                .SetTargetedSpell()
                .SetCost(bloodCost: 1)
                .AddAbilities(Ability.Evolve, GiveSigils.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Desire",
                    "Another's Desire",
                    2, 0, // attack/health
                    "Nothing short of victory will suffice.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("desire"))
                .SetTargetedSpellStats()
                .SetCost(bloodCost: 1)
                .AddAbilities(Ability.Brittle, GiveStatsSigils.AbilityID);

            CardManager.New(InfiniscryptionSpellsPlugin.CardPrefix,
                    "Spell_Hope",
                    "Hope",
                    1, 1, // attack/health
                    "A rare, fleeting feeling. Hold onto it.")
                .SetDefaultPart1Card()
                .SetPortrait(AssetHelper.LoadTexture("hope"))
                .SetGlobalSpellStats()
                .SetRare()
                .SetCost(bonesCost: 2)
                .AddAbilities(GiveStats.AbilityID);
        }
    }
}