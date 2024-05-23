<details>
<summary>View Changelog</summary>

# Plugin GUID: whistlewind.inscryption.abnormalsigils

## v1.1.2 - Minor patch (5/23/2024)
### 🩹 Bug fixes
- Fixed Conductor sigil crashing the game when multiple copies are on the same side of the board
- Adjusted Conductor sigil's Rulebook descriptions
### 🔧 Tweaks
- The Mechanical tribe is now replaced with the Machine tribe if Tribal Libary (sic) is installed (previously replaced with the Android tribe)
- If Tribal Libary is installed, the icon and rewardback for the Guardian, Plant, Machine, Humanoid, and Fairy tribes will be replaced with custom ones
### ⚖️ Balancing
- Power given by the Conductor sigil is no longer affected by temporary mods or other passive attack sigils
- Reverted previous change to Conductor sigil - no longer gives a minimum of 1 Power

## v1.1.1 - Slime and Dine (2/19/2024)
### 🩹 Bug fixes
- Fixed activated sigils having no dialogue on selecting invalid target
- Fixed Assimilator's powerlevel being incorrect
- Fixed Gardener sigil activating when the base card is killed
- Fixed Made of Slime sigil affecting Terrain cards
- Fixed Made of Slime and Gardener interaction where created cards would double their sigil amount when killed
### 🔧 Tweaks
- Changed name of Slimes stat icon --> Loving Slimes
- Changed icon for Loving Slimes
- Adjusted OnDie effect of Made of Slime
### ⚖️ Balancing
- Reworked Slimes to SL/2, 3 Bones
- Slimes created by Made of Slime sigil no longer inherit the parent card's Health and costs
### ➕ Additions
- Added Bloodletter ability

## v1.1.0 - Fast and Slow (1/22/2024)
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
- The Train ability no longer affects Giant or Uncuttable cards
- Fixed a number of abilities' visual effects breaking when triggered on a facedown card
- Fixed Passing Time stat icon briefly showing numbers when a card is first played
- Fixed Greedy Healing's death sequence not working correctly
### 🔧 Tweaks
- SniperSelectSlot.AIEvaluate target is now overridable
- Status effect card mod infos are now nonCopyable
- Ruler of Frost ability now has different invalid target dialogue depending on the context
- Adjusted Ruler of Frost's description to be clearer (if longer :s)
- Copycat ability now triggers OnResolveOnBoard sigils if it transforms immediately after being played
- Copycat ability now has dialogue for when it cannot copy the opposing card
- Copycat ability now triggers BEFORE most other sigils on the base card
- Copycat ability now copies temporary mods as well
- Witness ability effect now uses the status effect system instead of extended properties
- Opportunistic ability icon is now flipped for the opponent
- Improved Opportunistic logic
- Reworked The Train ability to activate on resolve instead of after 3 turns
- Overhauled Made of Slime ability's effect
- Changed description of Made of Stone to indicate modded abilities it is immune to
- Cursed ability now removes temporary mods that are nonCopyable or aren't from a Totem
- Scrambler sigil now uses similar logic to Corrector
- Slime now has LovingSlime Trait and Slimes stat icon
- Made a number of tweaks to dialogue
- Adjusted custom tribe icons' positions
- Cards with Sigil Power stat icon now display their stats when moused over while in the hand
- Adjusted Queen Nest description to be clearer on activation requirement: 'card dies' --> 'card is killed'
### ⚖️ Balancing
- <span style='color:limegreen'>Reduced Ruler of Frost's activation cost from 3 --> 2 Bones
- <span style='color:limegreen'>Reduced Rightful Heir ability's starting activation cost from 3 --> 2 Bones
- <span style='color:limegreen'>Ruler of Frost ability now accounts for the base card having Touch of Death
- Rebalanced Little Witch's Friend to 1/2, 3 Bones; now has Fae tribe alongside Insect tribe
- <span style='color:red'>Third Brother now has Sharp Quills instead of Reflector
- Opponents using the Healer ability will now prioritise cards with 1 Health and depriortise cards at max Health or higher
- <span style='color:red'>Idol ability no longer affects cards with Made of Stone
- Corrector ability's temporary mod is now marked as 'nonCopyable'
- <span style='color:limegreen'>Increased probability of Corrector and Scrambler sigils giving Attack from 33% --> 40%
- <span style='color:limegreen'>Increased power value of some costs in Corrector ability's calculation
    - 4 Energy now counts for 7 pts (from 6)
    - 5 Energy now counts for 9 pts (from 8)
    - 4 Blood now counts for 24 pts (from 20)
    - Blood costs above 5 now use a different formula, resulting in overall higher values (why are your cards over 5 Blood anyway?)
- <span style='color:limegreen'>Opportunistic ability now triggers even if the target has Sharp Quills or Reflector
- <span style='color:red'>Opportunistic ability no longer triggers even if the attacker has shields
- <span style='color:red'>Persistent and Piercing abilities no longer deal additional or overkill damage respectively
- <span style='color:red'>Witness sigil powerlevel increased from 1 --> 2
- <span style='color:red'>Worker Bees no longer inherit mods from the base card
### ➕ Additions
- Added the following abilities:
    - Binding Strike, Nimble-Footed, High-Strung, Refresh Decks, Return Card To Hand, Persecutor, Left-Veering Strike, Right-Veering Strike
- Added the following status effects:
    - Haste, Bind, Prudence
- Added LovingSlime Trait
- Added Slimes stat icon
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