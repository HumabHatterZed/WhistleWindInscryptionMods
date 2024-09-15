<details>
<summary>View Changelog</summary>

# Plugin GUID: whistlewind.inscryption.abnormalsigils

## v2.0.0 - ()
### 🧱 Structural
- Overhauled status effects
    - Renamed several fields and methods
    - Improved status effect icon appearances and implementation
    - Status effects now have max trigger priority
- Added functionality for node-related Traits
- Modified how cards and abilities are created
- Changed how Speed is calculated to be independent of Bind and Haste components
- Most abilities now have rulebook entries in Act 3, Grimora, and Magnificus's acts - abilities not fully tested for these acts, expect bugs
- Changed Left-Veering and Right-Veering Strike to use GetOpposingSlots instead of SetUpAttackSequence
- Changed Woodcutter to inherit from Sentry
### 🔧 Tweaks
- Overhauled behaviour of Conductor sigil
- Overhauled behaviour of Barreler sigil
- Overhauled Pebble status effect
- Changed icons for Conductor sigil
- Changed icons for Barreler sigil
- Changed Lonely sigil's icons
- Renamed Lonely to Pebble Giver
- Updated descriptions for status effects to use similar technical language
- Updated icons for Rightful Heir sigil to reflect its activation cost
- Updated artwork for Jack
- Updated See More sigil icons
- Updated Assimilator sigil icon
- Updated Opportunistic sigil pixel icon
- Updated Flagellation status effect icons
- Updated Haste and Bind statis effect icons
- Updated dialogue for Ruler of Frost when there are no valid targets
- Redid dialogue for when a Brother card dies while a card with Nettle Clothes is on the board
- Updated sigil descriptions to be more concise, follow similar formatting
- Renamed Refresh Decks sigil to Grand Reopening
- Renamed Status Effect Overflow sigil to See More
- Renamed Return Card to Hand sigil to Creature Retrieval
- Renamed Flagellated status effect to Flagellation
- Renamed Bitter Enemies sigil to Vendetta
- Renamed Little Witch's Friend card to "Wee Witch's Friend"
- Spore Mold Creature renamed to Spore Mold Beast
- Spore Mold Creatures now inherit the name of the card they were created from
- Frozen Heart card is now considered Terrain
- Sigils that inherit from ActivatedSelectSlotBehaviour are now usable by the opponent on upkeep
- Sigil Power stat icon no longer gives a minimum of 1 Health
- Changed tje order in which cards are damaged by Return to Nihil sigil
- Frozen Heart sigil will now give double Health to any card with Woodcutter, not just cards with a specific name
- Barreler sigil now displaces moved cards randomly
### 🩹 Bug fixes
- Fixed activated select slot sigils triggering when there are no valid targets on the board
- Fixed Witness sigil using an outdated description
- Fixed Worms status effect not letting Infested cards attack the right-most ally card
- Fixed Bitter Enemies sigil using an incorrect description
- Fixed Alchemist sigil breaking in Act 2 when trying to activate after the deck is exhausted
- Fixed False Throne altering persistent CardModificationInfos
- Fixed learned ability dialogue not triggering
### ⚖️ Balancing
- Lonely - reduced powerlevel from 3 -> 2
- Bloodfiend - reduced powerlevel from 3 -> 2
- Gift Giver - increased powerlevel from 3 -> 4
- Bloodletter - reduced powerlevel from 4 -> 3
- Opportunistic - changed to activate against cards with Loose Tail and an intact tail status
- Refresh Deck - changed to discard the player's current hand and draw a new opening hand after resetting the deck piles
- Nettle Clothes - reduced powerlevel from 5 -> 4
- Nettle Clothes - changed to only create Brothers in adjacent slots rather than all friendly slots
- Witness - reduced activation cost from 2 Bones -> 1 Bone
- Ruler of Frost - targeting cards now requires an additional 2 Bones - empty spaces still cost 2 Bones to target
- Ruler of Frost - no longer affects Terrain and Pelt cards
- Frozen Heart - changed to give 1 Power and 1 Health instead of 2 Health
- Frozen Heart - reduced powerlevel from -1 -> -3
- Healer - now removes a random negative status effect from targeted cards
- Healer - reduced health gained from 2 -> 1
- Binding Strike - Bind inflicted is now equal to half the attacking card's powerlevel
- Spores - reduced powerlevel from -1 -> 0
- Worms - Infested cards now deprioritise Terrain and Pelt cards
- Worms - increased powerlevel from -2 -> -1
- Little Witch's Friend - rebalanced from 1/2, 3 Bones -> 1/1, 2 Bones, Detonator
- Little Witch's Friend - no longer possesses the Insect tribe
- Hammer - replaced Made of Stone with Pin Down
- Block of Ice - removed Mighty Leap sigil
- First Brother - replaced Double Strike with Persistent
- All Brother cards - removed play cost, reduced Health to 1
### ➕ Additions
- Added dialogue for when first encountering a status effect
- Added dialogue explaining status overflow
- Added extension methods for clearing status effects from a card
- Added custom trigger IOnStatusEffectAdded
- Added 10+ sigils:
    - Damsel, Abusive, Shove Aside, Pin Down, Mind Strike, Unyielding, Spilling, Flower Queen, Healing Strike, Finger Tapping
- Added 1 stat icons:
    - Flower Power
- Added 3 status effects:
    - Fervent Adoration, Grief, Sinking
- Added 2 slot modifications:
    - Flooded, Blooming
### 💣 Removals
- Removed Volatile sigil
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
- Reduced Ruler of Frost's activation cost from 3 --> 2 Bones
- Reduced Rightful Heir ability's starting activation cost from 3 --> 2 Bones
- Ruler of Frost ability now accounts for the base card having Touch of Death
- Rebalanced Little Witch's Friend to 1/2, 3 Bones; now has Fae tribe alongside Insect tribe
- Third Brother now has Sharp Quills instead of Reflector
- Opponents using the Healer ability will now prioritise cards with 1 Health and depriortise cards at max Health or higher
- Idol ability no longer affects cards with Made of Stone
- Corrector ability's temporary mod is now marked as 'nonCopyable'
- Increased probability of Corrector and Scrambler sigils giving Attack from 33% --> 40%
- Increased power value of some costs in Corrector ability's calculation
    - 4 Energy now counts for 7 pts (from 6)
    - 5 Energy now counts for 9 pts (from 8)
    - 4 Blood now counts for 24 pts (from 20)
    - Blood costs above 5 now use a different formula, resulting in overall higher values (why are your cards over 5 Blood anyway?)
- Opportunistic ability now triggers even if the target has Sharp Quills or Reflector
- Opportunistic ability no longer triggers even if the attacker has shields
- Persistent and Piercing abilities no longer deal additional or overkill damage respectively
- Witness sigil powerlevel increased from 1 --> 2
- Worker Bees no longer inherit mods from the base card
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