<details>
<summary>View Changelog</summary>

# Plugin GUID: whistlewind.inscryption.abnormalsigils

## v1.1.0 - Fast and Slow ()
### 🧱 Structural
- Reworked how status effects function and their implementation
### 🩹 Bug fixes
- Fixed softlock when playing in Grimora or Magnificus Act (status effects will not render in these Acts!)
- Fixed softlock when playing in Act 2
- Fixed ForcedWhiteEmission appearance behaviour not forcing the colour correctly
- Fixed Frost Ruler not letting the player target occupied slots
- Fixed status-giving abilities giving more than they should
- Fixed Scrambler ability activating incorrectly for non-Spell cards
- Copycat ability no longer copies Giant or Uncuttable cards
- Fixed Corrector ability giving 1 less Health than it should
- Fixed Protector ability triggering when it shouldn't
- Fixed Healer ability always targeting the same card when used by the opponent
- Fixed tribe choice reward backs being off-centre
- Fixed sigils that give passive stat buffs not working correctly when stacked
- Added missing Global Spell support to Scrambler sigil
### 🔧 Tweaks
- Made a number of tweaks to dialogue
- Adjusted custom tribe icons' positions
- SniperSelectSlot.AIEvaluate target is now overridable
- Copycat ability now triggers OnResolveOnBoard sigils if it transforms immediately after being played
- Copycat ability now has dialogue for when it cannot copy the opposing card
- Copycat ability now triggers BEFORE most other sigils on the base card
- Copycat ability now copies temporary mods as well
- Witness ability effect now uses the status effect system instead of extended properties
- Opportunistic sigil is now flipped for the opponent
- Changed description of Made of Stone to indicate it provides immunity to Idol and Punisher
- Reworked The Train ability to activate on resolve instead of after 3 turns
- The Train ability no longer affects Giant or Uncuttable cards
- Cursed now removes temporary mods that are nonCopyable or aren't from a Totem
- Scrambler sigil now uses similar logic to Corrector
### ⚖️ Balancing
- Reduced Rightful Heir ability's starting activation cost from 3 --> 2 Bones
- Reduced Pumpkin's Health from 2 --> 1
- Reduced Sapling's Health from 2 --> 1
- Third Brother now has Sharp Quills instead of Reflector
- Adjusted Queen Nest description to be clearer on activation requirement: 'card dies' --> 'card is killed'
- Opponents using the Healer ability will now prioritise cards with 1 Health and depriortise cards at max Health or higher
- Idol no longer affects cards with Made of Stone
- Corrector temporary mod is now marked as nonCopyable
- Increased probability of Corrector sigil giving Attack from 33% --> 40%
### ➕ Additions
- Added the following abilities:
    - Binding Strike, Nimble-Footed, High-Strung, Refresh Decks, Return Card To Hand
- Added the following status effects:
    - Haste, Bind, Prudence
- Added more helper methods to StatusEffectManager
- Added a new section to the rulebook containing all status effects for the current Act - these entries are separate from the regularly added rulebook entries
    - Added a new field to FullStatusEffect 'AddNormalRulebookEntry' to control whether the regular rulebook entry should be added as well - false by default
    - Added FullStatusEffect.SetAddNormalEntry()
- FullStatusEffect now stores a list of its StatusMetaCategories
- Added 'Status Effect Overflow' - cards with more than 5 active status effects will gain the option a list of the overflowed statuses in the Rulebook
- Added ReduceStatusEffectBehaviour for automatically reducing an effect's severity on upkeep
- Added dialogue for when the Cursed sigil cannot transform a card

## v1.0.2 - Minor patch (7/26/2023)
### 🩹 Bug fixes
- Fixed Nettle Clothes softlock when killing Brother cards
- Fixed Nettle Clothes gaining sigil from Brother cards that die before fully resolving

## v1.0.1 - Status Effect Refactor (7/23/2023)
### 🧱 Structural
- Refactored how Status Effects are internally created
- Fixed the ReadMe

## v1.0.0 - Initial release (7/22/2023)
### ➕ Additions
    - Moved the following abilities from WhistleWind's Lobotomy Mod:
        - Punisher
        - Bloodfiend
        - Martyr
        - Aggravating
        - Team Leader
        - Idol
        - Conductor
        - Woodcutter
        - Frozen Heart
        - Ruler of Frost
        - Roots
        - Broodmother
        - Cursed
        - Healer
        - Queen Nest
        - Bitter Enemies
        - Courageous
        - Serpent's Nest
        - Assimilator
        - Group Healer
        - Reflector
        - Flag Bearer
        - Grinder
        - The Train
        - Scorching
        - Regenerator
        - Volatile
        - Gift Giver
        - Piercing
        - Scrambler
        - Gardener
        - Made of Slime
        - Marksman
        - Protector
        - Quick Draw
        - Alchemist
        - Nettle Clothes
        - Sporogenic
        - Witness
        - Corrector
    - Added the following abilities:
        - Neutered, Neutered Latch, Return to Nihil, False Throne, Rightful Heir, Opportunistic, Cycler, Barreler, Follow the Leader, Persistent
    - Added the following stat icons:
        - Nihil, Passing Time, Sigil Power

</details>