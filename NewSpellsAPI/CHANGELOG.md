# PluginGUID: zorro.inscryption.infiniscryption.spells

## 1.1.3 (?/?/2023)
- Fixed Global Spells behaving like Targeted Spells
- Fixed a few typos
- Tweaked Act 2 descriptions for Targeted and Global Spells.

## 1.1.2 (6/1/2023)
- Fixed Spells not working correctly in Act 2

## 1.1.1 (5/20/2023)
- Fixed Give Stats giving the wrong stats

## 1.1.0 (2/4/2023)
- Added opponent support for spell cards and all built-in sigils
- Added Global Spell support for ability 'Gain Control'
- Fixed Targeted Spells always being playable if they have the ability Brittle
- Fixed Targeted Spells double targeting the left adjacent slot while possessing Split Strike
- Fixed Targeted Spells that cost Blood softlocking due to no valid targets existing after sacrifices
- Reduced how long 'Gain Control' takes to finish moving a card
- 'Attack Down' can no longer be used on cards with 0 Power

## 1.0.0 (1/28/2023)
- Added 3 new sigils: Give Stats, Give Sigils, Give Stats and Sigils
- Added 4 new cards: Soul Without a Body, Body Without a Soul, Desire, Hope
- Added new compatibility to some sigils so they work with regular cards and global spell cards
- Changed artwork for spell card backgrounds
- Changed artwork for Direct Heal
- Fixed artwork appearing blurry in-game
- Spell cards no longer force the player to play them if selected
- Minor code optimisations
- Minor changes to some dialogue

#

<details>
<summary>Original version's changelog</summary>

## 2.0.1
- A final message from DivisionByZorro

## 2.0.0
- Updated documentation for Kaycee's Mod API and required that API as a dependency.

## 1.2.7
- Added pixel icons for compatibility with GBC mode

## 1.2.6
- Prevented the game from soft locking if you back out of casting a spell partway through sacrificing creatures.

## 1.2.5
- Fixed texture loading defect to prevent crashes when spell cards appear in certain situations for the first time.
- Updated mod to have a dependency on the unofficial patch as opposed to the standalone visually stackable sigils mod.

## 1.2.4
- Added the fishhook sigil

## 1.2.3
- Bad manifest.json. My bad. :(

## 1.2.2
- Updated to be dependent on the Stackable Sigils mod. This makes spell creation with modular sigils far more user friendly.

## 1.2.1
- Fixed defect with Attack Up and Attack Down where they were not properly attaching to cards.
- Fixed defect where sometimes creatures could not be played after casting targeted spells.
- Added more example cards to the pool.

## 1.2.0
- Added targeting logic for targeting spells. They will now only allow you to select valid targets.
- Added support for split strike, tri strike, and all strike
- Added modular, stackable sigils for spell creation.

## 1.1.0
- Added support for targeted spells.
- Fixed card animations

## 1.0.0
- Initial version. Adds global spells.
</details>
