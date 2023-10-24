# Plugin GUID: whistlewind.inscryption.abnormalsigils

<details>
<summary>View Changelog</summary>

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
- Fixed tribe choice reward backs being off-centre

### 🔧 Tweaks
- Witness ability effect now uses the status effect system instead of extended properties
- Changed Copycat's effect:
    - "A card bearing this sigil will transform into a copy of the first creature it opposes, retaining its own sigils."
- Idol no longer affects cards with Made of Stone
- Changed description of Made of Stone to indicate it provides immunity to Idol and Punisher
- Opportunistic sigil is now flipped for the opponent

### ➕ Additions
- Added the following abilities:
    - Binding Strike, Nimble-Footed, High-Strung
- Added the following status effects:
    - Haste, Bind, Prudence
- Added more helper methods to StatusEffectManager
- Added a new section to the rulebook containing all status effects for the current Act - these entries are separate from the regularly added rulebook entries
    - Added a new field to FullStatusEffect 'AddNormalRulebookEntry' to control whether the regular rulebook entry should be added as well - false by default
    - Added FullStatusEffect.SetAddNormalEntry()
- FullStatusEffect now stores a list of its StatusMetaCategories
- Added 'Status Effect Overflow' - cards with more than 5 active status effects will gain the option a list of the overflowed statuses in the Rulebook

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