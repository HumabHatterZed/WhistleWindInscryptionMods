# Plugin GUID: whistlewind.inscryption.lobotomycorp

## v2.0.0 - The One Perfect Book (?/?/2023)
Note that in the future, updates and changes to modded sigils will be found in the Abnormal Sigils changelog.

### 🧱 Structural
    - Split the majority of custom sigils (and any associated spawned cards) into their own separate mod Abnormal Sigils
    - Added 2 new dependencies: New Spell Card Toolkit, Abnormal Sigils
    - Reformatted the configuration file (set values will more than likely not carry over)
    - Hidden card abilities are now their own separate special abilities rather than being entwined with base sigil code
### 🩹 Bug fixes
    - Abnormality card choice now properly displays and clears dialogue
    - Fixed custom challenges not working properly in Act 1
    - Fixed custom death cards not being created correctly in some circumstances
    - Fixed certain encounters being broken
    - <<WhiteNight>> event no longer softlocks when there are multiple Plague Doctors in play
    - <<Nothing There>> is no longer copyable by Goo Mage
    - <<Guardian Apostle>> no longer revives immediately after being Downed
    - <<Gift Giver>> ability no longer gives copies of singleton cards you already own
### 🔧 Tweaks
    - <<Singing Machine>> no longer looks like a Terrain card
    - <<Dragon>> cards no longer display stats, gained new artwork
    - Cleaned up rulebook entries to be clearer in their description and (hopefully) easier to read
    - Ability <<Volatile>> now uses a custom flipped icon when used by opponents
    - Ability <<The Train>> changed to trigger on resolve rather than on activate
    - Starter Deck <<Road to Oz>> now includes The Road Home (replacing Wolf Cub) and Ozma
    - Starter Deck <<Magical Girls!>> now includes Magical Girl C
    - Abnormality choice node probabilities are now consistent for both Part 1 and Kaycee's Mod
        - Probabilities changed to (0%, 2%, 5%) and (2%, 5%, 10%) with Better Rare Chances cheat enabled
    - Abnormality choice node can now offer multiple Rare cards as choices (this doesn't change the overall chance of finding a Rare)
    - Increased point amount of Better Rare Chances from -15 --> -10
    - Adjusted flow of some dialogue
    - Adjusted some card portraits to be cleaner/visually better
    - Leshy can now trigger special events on his side of the board (you don't get the rewards though)
    - <<Miracle Worker>> challenge will now play Plague Doctor during boss fights and on a random turn, no longer shows activation sequence every battle
    - Plague Doctor uses a separate, per-run counter if played by Leshy
### ➕ Additions
#### Cards
    - Added the following cards:
        - Magical Girl C, Price of Silence, Pinocchio, Nosferatu, The Way Home, Ozma, Silent Girl (Rare)
    - Added the following special cards:
        - The Adult Who Tells Lies, Jester of Nihil, Malkuth, Yesod, Hod, Netzach, Tiphereth A and Tiphereth B, Gebura, Chesed, Binah, Hokma, Angela
    - Added 5 custom Tribes: Divine, Fae, Anthropoid, Mechanical, Botanic
    - Added the following starter decks (* = Replaces the primary card if Ruina cards are disabled in the config):
        - Random (3 randomly selected mod cards)
        - People Pleasers (Today's Shy Look, Pinocchio/Mirror of Adjustment*, Behaviour Adjustment)
        - Freak Show (Beauty and the Beast, Void Dream Queen Bee)
        - Apocrypha (Fragment of the Universe, Skin Prophecy, Price of Silence/1.76MHz*)
        - Keter (Bloodbath, The Burrowing Heaven, The Snow Queen)
    - Added pixel sprites for all cards and made them available in Act 2
#### Abilities
    - Added the following abilities:
        - Neutered, Neutered Latch, Return to Nihil, False Throne, Rightful Heir, Opportunistic, Cycler, Follow the Leader, Persistent
    - Added the following stat icons:
        - Nihil, Passing Time, Sigil Power
    - Added the following special abilities:
        - Cowardly, The Homing Instinct
    - Added the following status effects:
        - Spores, Worms
    - Abilities can now be used by cards in Act 2
    - Removed Marksman and Quick Draw, the mod now uses reskinned versions of Sniper and Sentry
#### Other
    - Added new configuration options relating to Part 1 and events
    - Added the following cheats:
        - Start with a Beast, Start with a Jester, Start with a Liar
    - Added Sefirot card choice node
### ⚖️ Gameplay Changes
#### Cards
    - <<All-Around Helper>> cost changed from 2 Blood --> 4 Energy
    - <<Apocalypse Bird>> Health increased from 8 --> 12, given Made of Stone
    - <<Apostles>> no longer have Uncuttable traits for all non-heretical variants
    - <<Apostles>> given custom Apostle trait (replacing Terrain trait), making them unsacrificable for all variants
    - <<Downed Apostles>> Health reduced to 1 for all variants
    - <<Downed Apostles>> no longer have Repulsive sigil
    - <<Spear Apostle>> Power increased from 3 --> 4
    - <<Army in Black>> reworked into Targeted Spell with Volatile ability
        - <<Army in Pink>> special ability now triggers when 3 ally cards die
    - <<Backward Clock>> cost changed from 4 Bones --> 2 Energy
    - <<Behaviour Adjustment>> cost changed from 4 Bones --> 3 Energy
    - <<Big Bird>> given Cycler ability
    - <<Bloodbath 2>> cost reduced from 2 Blood --> 1 Blood
    - <<Bloodbath 3>> rebalanced from 1/3, 3 Blood --> 1/2, 2 Blood
    - <<Blue Star>> reworked into:
        - Forme 1: 0/4, Fledgling, 2 Blood
        - Forme 2: 0/4; Fledgling, Idol; 3 Blood
        - Forme 3: 4/4; Fledgling, All Strike; 4 Blood
    - <<The Burrowing Heaven>> cost changed from 3 Bones --> 1 Blood, sigils are now Guardian, Sentry, Health reduced from 2 --> 1
    - <<CENSORED>> rebalanced from 6/3, 4 Blood --> 4/3, 3 Blood
    - <<Clouded Monk>> Health increased from 2 --> 3
    - <<Dimensional Refraction Variant>> reworked from 4/4, 3 Blood --> Sigil Power, 2 Blood
    - <<Il Pianto Della Luna>> Health increased from 6 --> 7
    - <<Child of the Galaxy>> reworked into 0/3; 1 Blood; Flag Bearer, Bone Digger
    - <<Don't Touch Me>> cost changed from 2 Bones --> 2 Energy, given Terrain trait
    - <<The Dreaming Current>> given Barreler ability, replacing Rampager ability
    - <<Second Brother>> gained Piercing ability
    - <<Third Brother>> Health reduced from 3 --> 2
    - <<Fourth Brother>> Health reduced from 2 --> 1
    - <<Fifth Brother>> gained Burning ability, replacing Sharp
    - <<Sixth Brother>> gained Thick Skin ability, replacing Stinky
    - <<Express Train to Hell>> reworked into:
        - Rare version: 0/4, Group Healer, 4 Bones
        - Item version: Global Spell, The Train, 2 Blood
    - <<Fairy Festival>> Health increased from 1 --> 2
    - <<Flesh Idol>> reworked into 0/3; 2 Bones; Aggravating, Fledgling (2)
    - <<Funeral of the Dead Butterflies>> rebalanced from 3/3, 3 Blood --> 1/3, 2 Blood
    - <<Giant Tree Sap>> Cost changed from 4 --> 3 Bones, is now Rare
        - Special ability triggers less often but is now inheritable
    - <<Happy Teddy Bear>> reworked into 1/6; 6 Bones; Guardian
    - <<Judgement Bird>>'s special ability no longer affects Terrain and Pelt cards, cursor now changes to indicate what slots are affected
    - <<King of Greed>> rebalanced from 4/5, Hefty, 2 Blood --> 2/5, Cycler, 1 Blood
    - <<Luminous Bracelet>> reworked into 0/2 Targeted Spell with Greedy Healing, Give Stats and Sigils; 2 Energy
    - <<Magical Girl D>> cost reduced from 2 Blood --> 1, Health reduced from 3 --> 2, renamed to The King of Greed
    - <<Magical Girl H>> Power reduced from 2 --> 1, gained Opportunistic ability
    - <<Queen of Hatred>> Power increased from 7 --> 8
    - <<Magical Girl S>> Power reduced from 2 --> 1, no longer Rare, renamed to The Knight of Despair
    - <<Melting Love>> Health increased from 2 --> 3
    - <<1.76 MHz>> reworked from 0/3; Annoying, Leader; 2 Bones --> 2/1, Annoying, 3 Energy
    - <<Mountain of Smiling Bodies 2>> cost reduced from 3 Blood --> 2
    - <<Mountain of Smiling Bodies 3>> Power increased from 4 --> 5, cost reduced from 4 Blood --> 3
    - <<Nameless Fetus>> cost reduced from 5 Bones --> 3 Bones
    - <<Notes from a Crazed Researcher>> reworked into Targeted Spell with 2/0, Brittle, Give Stats and Sigils, 4 Bones
    - <<Nothing There Final>> given sigils Piercing, Thick Skin x2
    - <<Old Faith and Promises>> cost changed from 2 Bones --> 3 Energy
    - <<One Sin and Hundreds of Good Deeds>> cost reduced from 2 Bones --> 1 Bone
    - <<Porccubus>> Health reduced from 2 --> 1
    - <<Queen Bee>> Health reduced from 6 --> 4
    - <<Little Red Riding Hooded Mercenary>> reworked into 2/5 3 Blood; Sniper, Persistent; Crimson Scar
    - <<Big and Will Be Bad Wolf>> reworked into 3/4 3 Blood, Assimilator; Crimson Scar
    - <<Sapling>> reworked from 0/2 free --> 0/2, Bone Digger, 2 Bones, Terrain
    - <<Scarecrow Searching for Wisdom>> rebalanced from 1/3, 5 Bones --> 1/2, 4 Bones
    - <<Schadenfreude>> rebalanced from 0/1; Quick Draw, Touch of Death; 4 Bones --> 1/1, Quick Draw, 3 Energy
    - <<Scorched Girl>> cost reduced from 3 Bones --> 2
    - <<Shelter from the 27th of March>> reworked into Targeted Spell with Repulsive, Aggravating, Give Sigils; 3 Energy
    - <<Spider Buff>> cost reduced from 4 Bones --> 3 Bones
    - <<Chairs>> Power reduced from 1 --> 0
    - <<Silent Orchestra>> rebalanced from 1/5 --> 2/6
    - <<Silent Machine>> rebalanced from 0/8, 2 Blood --> 0/3, 1 Blood
    - <<The Snow Queen>> cost reduced from 6 Bones --> 5, Health reduced from 3 --> 2
    - <<Snow White's Apple>> Health reduced from 3 --> 1
    - <<Snow White's Vines>> gained the Terrain Trait
    - <<Spider Bud>> cost reduced from 4 Bones --> 3
    - <<The Firebird>> Power increased from 1 --> 2
    - <<The Naked Nest>> now has the custom "Naked Serpent" Trait
    - <<The Naked Worm>> now has the custom "Naked Serpent" Trait
    - <<Theresia>> cost changed from 1 Bone --> 2 Energy
    - <<Today's Shy Look>> special ability tweaked to randomise when multiple copies are drawn at once
    - <<Standard Training-Dummy Rabbit>> rebalanced from 0/1, 1 Bone --> 0/2, 1 Energy
    - <<The Lady Facing the Wall>> rebalanced from 0/2, Punisher --> 1/2, Sharp Quills
    - <<We Can Change Anything>> rebalanced from 1/2 --> 0/3
    - <<WhiteNight>> Health reduced from 666 --> 66, replaced Terrain trait with Apostle, gained immunity to Touch of Death, Punisher
        - Can now be killed by regular cards, with a different reward if done so
    - <<You Must Be Happy>> reworked into Targeted Spell with 0/2, Scrambler, 2 Energy
    - <<You're Bald...>> stats and cost changed from 1/1, 3 Bones --> 0/2, 2 Energy
    - <<Ttungsil>> no longer has Fledgling ability
#### Abilities
    - <<Apostle>> now prevents damage and death while WhiteNight is an ally
    - <<Broodmother>> powerlevel reduced from 4 --> 3
    - <<Burning>> renamed to Scorching
    - <<Nettle Clothes>> now uses custom Trait "Swan Brother" for effect, no longer deals damage to the base card upon Brother cards dying
    - <<Martyr>> now activates when sacrificed, added additional effect:
        - "When a card bearing this sigil dies, all allied creatures gain 2 Health [ and lose all negative status effects ]."
    - <<Corrector>> powerlevel reduced from 3 --> 2
    - <<Frozen Heart>> now heals 2 Health instead of 1
    - <<Fungal Infector>> renamed to Sporogenic, reworked to be:
        - "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils."
    - <<Piercing>> reworked to be:
        - "Damage dealt by this card cannot be negated or reduced by sigils such as Armoured or Thick Skin. Deal 1 overkill damage when attacking a card."
    - <<Serpent's Nest>> is no longer obtainable as a totem bottom, reworked to be:
        - "When a card bearing this sigil is struck, the striker gains 1 Worms."
    - <<Conductor>> reworked to be:
        - "The effect of this sigil will change over the next 3 turns. This turn: do nothing."
    - <<Ruler of Frost>> reworked to be:
        - "Activate: Once per turn, pay 3 Bones to choose a space on the board. If the space is occupied by a killable card, transform it into a Frozen Heart. Otherwise create a Block of Ice."
    - <<The Train>> reworked to be:
        - "Three turns after this card is played, kill all creatures on the board. Creatures killed this way do not drop bones."
#### Other
    - Starter decks <<Road to Oz>>, <<Magical Girls!>>, <<Twilight>> now require completing the respective in-game event before unlocking the deck
        - This can be overriden in the config by-the-by
### 💣 Removals
    - Removed emission sprites from some terrain cards

<details>
<summary>Pre-2.0 Updates</summary>

## v1.3.1 - Final Pre-2.0 Update (1/28/2023)
### 🧱 General
    - Adjusted sprite of All-Around Helper
    - Changed artwork for Group Healer to be more distinct from Team Leader
    - Minor optimisations
### 💣 Removals
    - Removed special behaviour from Quick Draw and Woodcutter due to API fixing Sentry softlocking

## v1.3.0 - Futureproofing Update (12/31/2022)
### 🧱 General
    - Added compatibility features for upcoming 2.0 update
    - Refactored some internal systems
    - Tweaked card sprites
    - Adjusted descriptive text of challenges Miracler Worker and Better Rare Chances
### 🩹 Bug fixes
    - Fixed Plague Doctor's portrait not correctly updating mid-battle
    - Fixed abnormality choice node visual bug relating to card deck
    - Fixed certain singleton cards being reobtainable after certain events
### ➕ Additions
    - Added starter deck support for Part 1
    - Added 1 new starter deck: Random Mod Cards
    - Added new config 'EXTRA RANDOM CARDS' for adding extra mod cards to the Random Mod Cards starter deck (Part 1 and KCM)

## v1.2.5 - Bug fix (11/23/2022)
### 🩹 Bug fixes
    - Actually fixed Mountain of Smiling Bodies softlocking when dying

## v1.2.4 - Big Boy patch (11/22/2022)
### 🩹 Bug fixes
    - Fixed cards with custom evolutions evolving into the wrong forme when played by Leshy
    - Reverted prior change to Mountain of Smiling Bodies

## v1.2.3 - Bodies of Apostles patch (11/21/2022)
### 🧱 General
    - Mod now unpatches itself OnDisable
### 🩹 Bug fixes
    - Fixed downed Apostles not evolving into their correct forme
    - Mountain of Smiling Bodies now checks if card slot is null when killed after evolving
### 🔧 Tweaks
    - Rewrote Woodcutter ability to use logic from API's Sentry fix
    - Quick Draw now inherits from Sentry
### 💣 Removals
    - Removed some debugging items
    - Removed unnecessary patches

## v1.2.2 - The 'Who Let Me Code' patch (10/6/2022)
### 🩹 Bug fixes
    - Fixed the following cards not being obtainable as card choices
      - Judgement Bird
      - One Sin and Hundreds of Good Deeds
      - Plague Doctor
      - Yang
      - Yin
      - You're Bald...

## v1.2.1 - Minor patch (9/26/2022)
### 🧱 General
    - Fixed inaccurate information in the ReadMe
    - CENSORED's ability now has opponent compatibility
### 🩹 Bug fixes
    - Fixed Hatred special not properly checking for other Magical Girls
### 🔧 Tweaks
    - Improved rulebook entry descriptions for special abilities

## v1.2.0 - Close Encounters of the Abnormal Kind (9/18/2022)
### 🧱 General
    - Adjusted the descriptions for some configurations to reflect new changes/be clearer.
    - Fixed inaccurate information in the ReadMe
    - Added PackManager compatibility
### 🩹 Bug fixes
    - Fixed visual bug related to interactions with Regenerator and facedown cards
    - Fixed visual bug related to Cursed ability activating when the killer has also died
    - Fixed visual bug where created Spore Mold Creatures would glow when they shouldn't
    - Fixed 1.76 MHz's cost being 3 bones instead of 2 bones
    - Fixed First Brother's Health being 2 not 1
    - Fixed Second Brother's Health being 2 not 1
    - Fixed Fourth Brother's Health being 1 not 2
    - Fixed Fungal Infector not affecting cards that were affected in previous battles
    - Fixed Singing Machine not having an emission
    - Fixed Queen of Hatred not switching back from Tired forme
    - Fixed Magical Girl D not showing dialogue on evolve
    - Fixed Plague Doctor special ability not activating when on the opponent's side of the board
    - Fixed placeholder descriptions for Grave of Cherry Blossoms, The Little Prince still being present
    - Fixed Witness ability's Rulebook entry displaying an incorrect cost
### 🔧 Tweaks
    - Reworked Conductor ability to now give passive Power rather than draw Chair cards
    - Nettles Clothes ability now shows added abilities
    - The Naked Nest and The Naked Worm are now part of the Insect tribe
    - Redid the dialogue for the Abnormality choice node, no longer plays in KCM
    - Tweaked Broodmother, Queen Nest, Serpent's Nest abilities to have drawn cards inherit merged sigils
    - Tweaked Gift Giver ability to have drawn cards inherit merged sigils IF Gift Giver is possessed by Laetitia
    - Broodmother, Gift Giver, Corrector abilities now have opponent support
    - Rewrote rulebook entry for Queen Nest
    - Tweaked a number of cards' descriptions to better fit the game
### ⚖️ Balancing
    - Yang event now only removes 1 card of the relevant cards at random instead of both
    - Funeral of the Dead Butterflies is no longer Rare
    - Notes from a Crazed Researcher no longer has Volatile
    - WhiteNight no longer heals taken damage
    - Buffed Singing Machine's Health from 4 --> 8
    - Buffed Void Dream Rooster's Health from 2 --> 3
    - Rebalanced Funeral of the Dead Butterflies to be (3,3) stats, 3 Blood, Double Strike
    - Changed The Dreaming Current from (3,2) stats, 2 Blood cost, Rampager --> (4,2) stats, 3 Blood cost, Rampager and Waterborne
    - Nerfed Silent Orchestra's stats from (3,6) --> (1,5)
    - Increased Worker Bee's cost from FREE --> 1 Bone
### ➕ Additions
    - Added custom encounters for each region
    - Added starter deck Lonely Friends
        - Scorched Girl, Laetitia, Child of the Galaxy
    - Added starter deck Blood Machines
        - We Can Change Anything, All-Around Helper, Singing Machine
    - Added config option Abnormal Bosses
    - Added config option Abnormal Encounters
    - Added config option Better Rare Chances
    - Added config option Miracle Worker
    - Added challenge Abnormal Bosses
    - Added challenge Abnormal Encounters
    - Added challenge Abnormal Encounters
    - Added challenge Miracle Worker
    - Added cheat Better Rare Chances
    - Added 10 death cards
    - Added opponent-only cards: Guardian Apostle, Moleman Apostle, Rudolta (mule version), Skeleton Shrimp, Crumpled Can

## v1.1.1 - Broken Shovel patch (8/26/2022)
### 🧱 General
    - Fixed ReadMe's description of Sapling showing the wrong Power
    - Fixed ReadMe's description of Giant Tree being incorrectly formatted
    - Removed an duplicate entry in the ReadMe of Lady Facing the Wall
    - Changed ReadMe's description of Nothing There to display X/X for stats
### 🩹 Bug fixes
    - Fixed Gardener not activating at all
    - Fixed Magical Girl S and Army in Pink's special abilities activating whilst in hand
    - Fixed Omni Strike not attacking Giant cards properly
### 🔧 Tweaks
    - Changed emissions of Parasite Tree, Sapling, and The Little Prince to not obscure their cost
    - Tweaked Army in Pink's special ability
### ⚖️ Balancing
    - Buffed Apocalypse Bird's Power from 2 --> 3
    - Buffed Army in Black's Power from 0 --> 1
    - Buffed Void Dream's Power from 0 --> 1
    - Increased Spider Brood's cost from FREE --> 1 Blood

## v1.1.0 - First Major Update™ (8/22/2022)
### 🧱 General
    - Changed file name for config file (see above for more information on this)
    - Rearranged the order of the configs in the config file
    - Added opponent AI compatibility for Sniper and Marksman abilities
    - Bifurcated Strike, Trifurcated Strike, and Double Strike now add stackable extra attacks for Sniper and Marksman abilities
    - Omni Strike now attacks the base card's opposing slot if they aren't a Giant card rather than only the leftmost slot
    - All abilities now have an icon for Act 2 if you wish to mess around with them in Act 2 - NOTE: Act 2 is not supported and has not been playtested
    - Fixed inaccurate information in the ReadMe
### 🩹 Bug fixes
    - Fixed custom death cards not being properly added to the game
    - Fixed Assimilator and Bloodfiend still activating when the base card has died
    - Fixed Martyr ability causing the game to freeze when there are no valid targets to be healed
    - Fixed Quick Draw and Woodcutter abilities causing the game to freeze in certain scenarios
    - Fixed Gardener ability activating when not on the board
    - Fixed Gardener ability causing the game to freeze when the dead card's slot isn't empty
    - Fixed Ruler of Frost ability causing the game to freeze when the dead card's slot isn't empty
    - Fixed Cursed ability affecting Giant cards
    - Fixed Flag Bearer ability revoking the Health buff under certain situations
    - Fixed Regenerator ability killing adjacent cards when they are at max Health
    - Fixed incorrect Regenerator ability description
    - Fixed Magical Girl H's special ability not accounting for certain situations
    - Fixed Judgement Bird's special ability not accounting for Airborne or Repulsive
    - Fixed Submerged cards not flipping when targeted by Judgement Bird
    - Fixed the Mirror of Adjustment not properly displaying the Mirror stat icon
    - Fixed Nothing There and Express Train to Hell being selectable hosts/sacrifices at card merge and campfire nodes
### 🔧 Tweaks
    - Assimilator, Queen Nest, Cursed, Regenerator, Reflector, Grinder abilities are now modular
    - Made a number of abilities stackable (see Abilities section for more information)
    - Tweaked Bloodbath's special ability to better indicate to the player when it has activated whilst in hand
    - Snow White's Apple now kills survivors at the Campfire
    - Plague Doctor now changes its appearance based on the number of times it has healed cards (change persists even if you reset mid-battle)
    - Piercing ability now has different behaviour when possessed by Staff Apostle
    - Added placeholder text for when all 3 Magical Girls are on the same side of the board
    - Updated Nameless Fetus's sprites
    - Updated WhiteNight's sprite and emission
    - Mirror of Adjustment now uses the default stat layout
    - Made minor changes to various card and ability descriptions
    - Cards killed by certain event cards no longer activate triggers. This is to prevent softlocks relating to certain ability combinations
### ⚖️ Balancing
    - Queen Nest ability no longer creates a Worker Bee when played
    - Made of Slime ability now gives created cards 1 Power, no longer affects cards with 1 Health
    - Cursed ability no longer affects card with the Uncuttable trait or the Made of Stone ability
    - Changed Bloodbaths' stats and gave them the Spilled Blood stat icon
    - Cards created by the Roots ability now inherit the base card's sigils
    - Minions created by Gardener now inherit the dead card's sigils
    - Minions created by CENSORED now inherit the full Power of the killed card
    - Army in Pink's special ability now creates 4 copies of Army in Black in hand when triggered
    - Bloodbath 1, 2, and 3 now all the have Spilled Blood stat icon
    - Buffed CENSORED's Health from 2 --> 3
    - Buffed Queen Bee's Health from 5 --> 6
    - Buffed Snow Queen's Health from 2 --> 3
    - Buffed Scarecrow Searching for Wisdom's Health from 2 --> 3
    - Buffed Luminous Bracelet's Health from 1 --> 2
    - Buffed Opened Can of WellCheers's Health from 1 --> 2
    - Buffed We Can Change Anything's stats from (0,1) --> (1,2)
    - Increased Express Train to Hell's cost from FREE --> 6 Bones
    - Reduced The Train ability's activation cost from 12 Bones --> 6 Bones
    - Reduced Parasite Tree's cost from 2 Blood --> 1 Blood
    - Rebalanced 1.76 MHz to 2 Bones cost
    - Rebalanced Blue Star 1 to have (0,2) stats, Fledgling
    - Rebalanced Blue Star 2 to have (2,6) stats, Assimilator, Omni Strike
    - Rebalanced Flesh Idol to have (0,2) stats, 3 Bones cost
    - Rebalanced Crumbling Armour to have (0,3) stats, 5 Bones cost
    - Rebalanced Scythe Apostle from 3 Power, Woodcutter --> 2 Power, Double Strike
    - Rebalanced Army in Pink to have (3,3) stats, Protector, Clinger
    - Rebalanced Army in Black to have (0,1) stats, Volatile, Brittle, 0 cost
    - Nerfed Bloodbath's Health from 3 --> 1
    - Nerfed Bloodbath 1's Health from 3 --> 1 
    - Nerfed Bloodbath 2's Health from 3 --> 2
    - Nerfed Bloodbath 3's Power from 3 --> 1
### ➕ Additions
    - Big Bird and Blue Star now possess special abilities
    - Added starter deck First Day
      - One Sin, Fairy Festival, Old Lady
    - Added starter deck Road to Oz
      - Wolf Cub, Scarecrow Searching for Wisdom, Warm-Hearted Woodsman
    - Added starter deck Magical Girls!
      - Magical Girl H, Magical Girl D, Magical Girl S
    - Added starter deck Twilight
      - Punishing Bird, Big Bird, Punishing Bird
    - Added card choice node
    - Added config option No Donators
    - Added config option Card Choice at Start
    - Added combat event for Apocaylpse Bird
    - Added combat event for Yin and Yang
    - Added card Child of the Galaxy
    - Added card Fragment of the Universe
    - Added card Apocalypse Bird
    - Added card The Little Prince
    - Added card Dream of a Black Swan
    - Added card Giant Tree Sap
    - Added card Skin Prophecy
    - Added card Behaviour Adjustment
    - Added card Old Faith and Promise
    - Added card Yin
    - Added card Yang
    - Added card Backward Clock
    - Added card Il Pianto della Luna
    - Added ability Fungal Infector
    - Added ability Clothes Made of Nettles
    - Added ability Witness
    - Added ability Corrector
    - Added ability Alchemist
    - Added ability Time Machine
    - Added special ability Giant Tree Sap
    - Added special ability Big Bird

## v1.0.7 - Martyr bug fix (7/22/2022)
### 🩹 Bug fixes
    - Fixed Martyr ability softlocking when there aren't any other valid cards
    - Melting Love can now be found as a rare card
    - Judgement Bird is now found as a common choice instead of a rare
### 🔧 Tweaks
    - Changed sigil icons of activated abilities to better indicate their nature
    - Martyr ability now longer changes your view during combat
### ⚖️ Balancing
    - Reduced One Sin's cost from 4 Bones --> 2 Bones

## v1.0.5 & v1.0.6 - Nothing Angels patch (7/3/2022)
### 🩹 Bug fixes
    - Fixed Apostles not entering Downed state when killed
    - Fixed Nothing There not being properly added to the deck
    - Fixed Apostle Spear emission not showing
    - WhiteNight event works again
### 🔧 Tweaks
    - Dreaming Current now has Rampager instead of Sprinter and Hefty
    - Reverted some cards' emissions to the default colour
### ⚖️ Balancing
    - Select cards can no longer be used at the Campfire or Mycologists

## v1.0.3 & v1.0.4 - Mountains of Coloured Text patch (6/29/2022)
### 🩹 Bug fixes
    - Fixed Assimilator ability not doing proper checks on the base Card
    - Fixed Assimilator ability not properly checking for MoSB evolutions (v1.0.4)
### 🔧 Tweaks
    - Leshy's eyes now turn red during the WhiteNight event
    - Changed colour of text relating to WhiteNight event
    - Tweaked Assimilator ability OnDie trigger to be specific to MoSB

## v1.0.2 - Prayer and Bees patch (6/28/2022)
### 🩹 Bug fixes
    - Fixed Queen Nest ability softlocking when Queen Bee is dropped by the Mule
### 🔧 Tweaks
    - Tweaked Confession ability to make Heretic sequence smoother
### ⚖️ Balancing
    - Cards from the WhiteNight event no longer drop bones when killed
    - Hundreds of Good Deeds now dies if Confession is activated during a boss

## v1.0.1 - Bones and Trains patch (6/27/2022)
### 🧱 General
    - Removed the fourth zero from the in-game version number to be consistent with the Thunderstore version number
### 🩹 Bug Fixes
    - Fixed Boons of the Bone Lord not giving bones
    - Fixed cards not dropping bones if a copy was previously killed by The Train
    - Fixed The Train ability being free to activate
### 🔧 Tweaks
    - Confession ability changed to an activated-type ability
    - Fixed an error in the README regarding The Train ability's description
    - Can no longer activate The Train ability if there are no other cards on the board
### ⚖️ Balancing
    - Increased The Train ability activation cost 10 --> 12

## v1.0.0 - Initial release (6/26/2022)
### ➕ Additions
    - 71 Cards
    - 38 Abilities
    - 13 Special abilities

</details>