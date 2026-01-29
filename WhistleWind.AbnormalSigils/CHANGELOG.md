<details>
<summary>View Changelog</summary>

# Plugin GUID: whistlewind.inscryption.abnormalsigils

## v2.0.0 - Fear and Wonder: Termina ()
### 🧱 Structural
- New Spell Card Toolkit is now a required dependency
- Overhauled status effects
    - Renamed several fields and methods
    - Improved status effect icon appearances and implementation
    - Status effects now have max trigger priority
- Added Driver.PinDownCard() static method for replicating Pin Down sigil's effect
- Modified how cards and abilities are created
- Modified how speed is calculated, read the Speed description for more information
- Most abilities now have rulebook entries in Act 3, Grimora, and Magnificus's acts - abilities not fully tested for these acts, expect bugs
- Changed Left-Veering and Right-Veering Strike to use GetOpposingSlots instead of SetUpAttackSequence
- Changed Woodcutter to inherit from Sentry
### 🔧 Tweaks
- Overhauled Conductor sigil
- Overhauled Barreler sigil
- Overhauled Bitter Enemies (Vendetta) sigil
- Overhauled Pebble status effect
- Status effects now glow to make them easier to see
- Return Card to Hand now works when used by the opponent
- Replaced dialogue for when a Brother card dies while a card with Nettle Clothes is on the board
- Changed how Gift-Laden determines unique cards to give
- Renamed Refresh Decks sigil to Grand Reopening
- Renamed Status Effect Overflow sigil to See More
- Renamed Return Card to Hand sigil to Creature Retrieval
- Renamed Flagellated status effect to Flagellation
- Renamed Bitter Enemies sigil to Vendetta
- Renamed Little Witch's Friend to "Wee Witch's Friend"
- Renamed Greedy Healing to Malignant Regeneration
- Renamed Gift Giver sigil to Gift-Laden
- Renamed Spore Mold Creature to Spore Mold Beast
- Changed icons for Conductor sigil
- Changed icons for Barreler sigil
- Changed Lonely sigil's icons
- Updated descriptions of Persistent and Piercing sigils to account for new sigil effects
- Updated descriptions for status effects to use similar technical language
- Updated icons for Rightful Heir sigil to reflect its activation cost
- Updated artwork for Jack
- Updated See More sigil icons
- Updated Assimilator sigil icon
- Updated Opportunistic sigil icons
- Updated Flagellation status effect icons
- Updated Haste and Bind statis effect icons
- Updated dialogue for Ruler of Frost when there are no valid targets
- Updated sigil descriptions to be more concise, follow similar formatting
- Sigils that modify the base card's attack when it attacks now briefly indicate the final damage value when attacking
- Sped up sequence when gaining or losing a status effect
- Spore Mold Creatures now inherit the name of the card they were created from
- Sporogenic now uses PreTurnEnd trigger for effect, should eliminate any prior trigger weirdness
- Frozen Heart card is now considered Terrain
- Shortened some sigil sequences
- Sigils that inherit from ActivatedSelectSlotBehaviour are now usable by the opponent on upkeep
- Sigil Power stat icon no longer gives a minimum of 1 Health
- Changed tje order in which cards are damaged by Return to Nihil sigil
- Frozen Heart sigil will now give double Health to any card with Woodcutter, not just cards with a specific name
- Barreler sigil now displaces moved cards randomly
- Piercing sigil now implements IShieldPreventedDamage
- Haste gained from High Strung is now applied on each player's turn rather than on round's end
- Pin Down can now affect Uncuttable cards, no longer affects cards marked Giant or NonInstaKill
### 🩹 Bug fixes
- Fixed CardMetaCategories not working
- Fixed status effects not rendering above merged sigils
- Fixed Team Leader and Idol applying more Power gain/loss than intended when stacked with themselves
- Fixed activated select slot sigils triggering when there are no valid targets on the board
- Fixed Witness sigil using an outdated description
- Fixed Witness not working
- Fixed Follow the Leader affecting Pelt and Terrain cards
- Fixed Right-Veering Strike behaving like Left-Veering Strike
- Fixed interaction with Recall Creature where Nettles would retain sigils when replayed
- Fixed Worms status effect not letting Infested cards attack the right-most ally card
- Fixed Bitter Enemies sigil using an incorrect description
- Fixed Alchemist sigil breaking in Act 2 when trying to activate after the deck is exhausted
- Fixed False Throne altering persistent CardModificationInfos
- Fixed Haste gained from High Strung being inconsistent on when it's removed from the card
- Fixed learned ability dialogue not triggering
- Fixed Spiderling not having Fledgling
- Fixed incorrect descriptions, missing words, etc.
### ⚖️ Balancing
- Modified logic for opponent activated sigils to be based on sigil power level - stronger sigils are less likely to be triggered each turn
- Binding Strike - reduced powerlevel from 2 -> 1
- Bloodfiend - healing is now capped at 2 above the card's max health
- Bloodfiend - no longer triggers against Terrain and Pelt cards
- Bloodfiend - reduced powerlevel from 3 -> 2
- Bloodletter - healing is now capped at 2 above the card's max health
- Bloodletter - no longer triggers against Terrain and Pelt cards
- Bloodletter - reduced powerlevel from 4 -> 3
- Copycat - can now copy the Moon and Limoncello
- Copycat - now gains +1 Power when it fails to copy a card - this is removed when it successfully copies a card
- Corrector - modified stat formula to more closely follow Daniel Mullin's (in-game values should be the same or higher)
- Corrector - Energy cost now follows the vanilla formula outside Act 1 for improved compatibility
- False Throne - reworked sigil effect completely, see new description for more info
- False Throne - renamed to Magic Trick
- Frozen Heart - changed to give 1 Power and 1 Health instead of 2 Health
- Frozen Heart - reduced powerlevel from -1 -> -3
- Gift Giver - increased powerlevel from 3 -> 4
- Gift Giver - now disables itself on activation
- Greedy Healing - healing reduced to 1 per turn
- Greedy Healing - changed kill condition to current health exceeding max health by 3 or more
- Healer - reduced health gained from 2 -> 1
- Healer - now removes a random negative status effect from targeted cards
- Healer - now stackable
- Lonely - reduced powerlevel from 3 -> 2
- Lonely - reworked
- Martyr - no longer stackable
- Nettle Clothes - reduced powerlevel from 5 -> 4
- Nettle Clothes - changed to only create Brothers in adjacent slots rather than all friendly slots
- Nettle Clothes - changed singleton id used for tracking added sigils
- Opportunistic - reworked to trigger against injured cards
- Pebble - reworked
- Pebble - raised powerlevel from 2 -> 3
- Persecutor - summoned cards are now considered Terrain, have 1 Health
- Persistent - no longer affects face down cards
- Piercing - now affects face down cards
- Punisher - now activates on card death instead of on taking damage
- Recall Creature - Recalled Fecundity cards now lose Fecundity in KCM/Ascension mode
- Refresh Deck/Grand Reopening - now discards the player's current hand and draws additional cards based on how long the battle's gone on
- Rightful Heir - activation is no longer limited to once per turn
- Rightful Heir - Increased initial activation cost from 1 Bone -> 2 Bones
- Ruler of Frost - targeting cards now requires an additional 2 Bones - empty spaces still cost 2 Bones to target
- Ruler of Frost - kill effect no longer affects Terrain and Pelt cards
- Thick Skin - now only reduces damage from cards; null sources no longer trigger damage reduction
- Thick Skin - no longer modular
- Witness - reduced activation cost from 2 Bones -> 1 Bone
- Spores - increased powerlevel from -1 -> 0
- Sporogenic - reduced powerlevel from 2 -> 0
- Worms - Infested cards now deprioritise Terrain and Pelt cards when targeting
- Worms - increased powerlevel from -2 -> -1
- Worms - no longer affects cards with Made of Stone
- Block of Ice - removed Mighty Leap sigil
- First Brother - replaced Double Strike with Persistent
- All Brother cards - removed play cost, reduced Health to 1
- Little Witch's Friend - reworked from 1/2, 3 Bones -> 1/1, Detonator
- Hammer - replaced Made of Stone with Pin Down
- Little Witch's Friend - no longer possesses the Insect tribe
- Pumpkin Jack - Replaced Cursed sigil with Brittle
- Spiderling - reduced play cost from 3 Bones -> Free
- Spider Brood - reduced Health from 3 -> 2
### ➕ Additions
- Added TargetIconHelper - contains helper methods for creating target icons in Act 1
- Added dialogue for when first encountering a status effect
- Added dialogue explaining status overflow
- Added extension methods for clearing status effects from a card
- Added CardInfo.SetCannotGainSigils, CardInfo.SetCannotGiveSigils, CardInfo.SetCannotCopyCard, CardInfo.SetCannotGainStats, CardInfo.SetBoneless extension methods
    - Added card appearance behaviours fore each corresponding trait/metacategory (rare and common variants)
- Added SetGiftGiverId and SetUniqueCopycat extension methods for CardInfo
- Added custom rulebook section 'Mechanics'
    - Can use MechanicPages.CreateMechanicPage method to add additional pages
- Added custom trigger interfaces IOnStatusEffectAdded, IPlayerTurnEnd, IOpponentTurnEnd
- Added ability class 'CreateTwoCardsAdjacent'
- Status effect's can now be marked Irremovable, preventing their removable using the RemoveStatusEffect(s) extension methods
- Refresh Decks/Grand Reopening now removes temporary mods with singleton id "wstl:RemoveOnRefresh" from all active cards (board, queue, hand)
- Added X new sigils
- Added 1 new stat icons
- Added 4 new status effects
- Added 2 new slot modifications
- Added 3 new Traits
- Added 3 new card appearances
### 💣 Removals
- Removed Volatile sigil
- Removed unnecessary ability patches

## v1.1.3 - Rough Hotfix (1/14/2026)
### 🩹 Bug fixes
- Fixed custom CardMetaCategories not working

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