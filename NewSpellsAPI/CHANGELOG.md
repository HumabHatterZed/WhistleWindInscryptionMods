# PluginGUID: zorro.inscryption.infiniscryption.spells

<details>
<summary>View changelog</summary>

## 1.2.5 (Aug 11, 2024)
- Fixed Terrain stat spells being boostable at the campfire
- Fixed stat spells not colouring their stat values correctly in the hand
- Added support for negative value stat spell cards
- Added abstract 'GiveAbility' class
- Reduced 'Give Stats' ability powerlevel from 4 -> 2
- Reduced 'Give Sigils' ability powerlevel from 4 --> 3
- Reduced 'Give Stats and Sigils' ability powerlevel from 8 --> 5
- 'Give Sigils' and 'Give Stats and Sigils' now only give up to 4 of the spell card's sigils
- 'Give' abilities are now stackable
- 'Give' abilities now inherit from the GiveAbility class
- 'Give' abilities are now marked with the extended property 'Spells:GiveAbility'
- Opponents now follow the same logic as the player when playing negative 'Give' spells
- Stat spells in Act 2 now display positive values as green instead of blue

## 1.2.4 (Jan 16, 2024)
- Fixed Blood-costing spell cards not being playable even when there are sufficient sacrifices and targets

## 1.2.3 (Dec 24, 2023)
- Queued spell cards can now be prevented from being played by patching Opponent.QueuedCardIsBlocked
- Made UpdateStatsSpellDisplay and UpdatePlayableStatsSpellDisplay public

## 1.2.2 (Dec 3, 2023)
- Fixed stat spell cards' health not being coloured correctly during battle
- Opponent stat spell cards now reveal their stats when played
- Queued stat spell cards now reveal their stats when moused over
- Adjusted when the target icons created by opponent target spells disappear

## 1.2.1 (Nov 29, 2023)
- Mousing over a stat spell card will reveal the card's stats

## 1.2.0 (Nov 16, 2023)
- Rewrote stat spell logic to -hopefully- be quicker/less laggy
- Added Instant Global Spells, which replicate the original global spell behaviour (played immediately on selection)
- Added SetInstaGlobalSpell() and SetInstaGlobalSpellStats()
- Tweaked Spell stat icon descriptions to differentiate between instant and regular Global Spells

## 1.1.3 (Sept 9, 2023)
- Fixed a few typos
- Fixed example sigils activating incorrectly when attached to non-Spell cards
- Added helper extension AbilityInfo.SetCanMerge() for controlling whether sigils should be transferrable or not - true by default
- Added new config AllowCardMerge for controlling whether spell cards can gain or transfer sigils - true by default
- Tweaked Act 2 descriptions for Targeted and Global Spells.

## 1.1.2 (June 1, 2023)
- Fixed Spells not working correctly in Act 2

## 1.1.1 (May 20, 2023)
- Fixed Give Stats giving the wrong stats

## 1.1.0 (Feb 4, 2023)
- Added opponent support for spell cards and all built-in sigils
- Added Global Spell support for ability 'Gain Control'
- Fixed Targeted Spells always being playable if they have the ability Brittle
- Fixed Targeted Spells double targeting the left adjacent slot while possessing Split Strike
- Fixed Targeted Spells that cost Blood softlocking due to no valid targets existing after sacrifices
- Reduced how long 'Gain Control' takes to finish moving a card
- 'Attack Down' can no longer be used on cards with 0 Power

## 1.0.0 (Jan 28, 2023)
- Added 3 new sigils: Give Stats, Give Sigils, Give Stats and Sigils
- Added 4 new cards: Soul Without a Body, Body Without a Soul, Desire, Hope
- Added new compatibility to some sigils so they work with regular cards and global spell cards
- Changed artwork for spell card backgrounds
- Changed artwork for Direct Heal
- Fixed artwork appearing blurry in-game
- Spell cards no longer force the player to play them if selected
- Minor code optimisations
- Minor changes to some dialogue
</details>

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
