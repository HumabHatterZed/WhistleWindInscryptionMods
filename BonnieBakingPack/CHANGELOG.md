<details>
<summary>View Changelog</summary>

# Plugin GUID: whistlewind.inscryption.bonniesbakingpack

## v2.1.1 - Minor Patch (2/?/2024)
### 🩹 Bug fixes
- Fixed Plague Doctor portrait not updating correctly in Act 2
- Fixed Confession sigil causing a death loop when activated
- Fixed WhiteNight removing the boss card during the Apocalypse Bird fight
- Fixed Adoration special ability softlocking when the right-adjacent card is killed
- Fixed Transformer sigil not appearing in the Rulebook
- Fixed Grave of Cherry Blossoms not having any sigils
- Fixed custom items not being loaded into the game
- Grammar fixes for various rulebook entries
### 🔧 Tweaks
- Adjusted Scorching Girl's emission sprite
- Made adjustments to the WhiteNight sequence
- WhiteNight no longer has the Apostle trait
- WhiteNight defeat sequence and Time Machine acitvation sequence now have unique behaviour when triggered during custom boss fights
- (Achievements API only) Changed the names and descriptions of the three hidden achievements, changed them to no longer be hidden.
### ⚖️ Balancing
- <span style='color:red'>Increased cost of Blue Star from 2 --> 3 Blood
- <span style='color:red'>Increased cost of Blue Star (2nd forme) from 3 --> 4 Blood
- Rebalanced Melting Love from 6 Health, 3 Blood --> 5 Health, 5 Bones
- Replaced Grave of Cherry Blossoms's sigils (Sharp Quills, Bloodfiend) with Bloodletter
- Adoration special ability now only triggers at 1 Health
### ➕ Additions
- Added support for Pack Management API's encounter pack beta

## v2.1.0 - Into the Twilight (1/22/2024)
### 🩹 Bug fixes
- Fixed Honoured Monk having the wrong portrait
- Fixed The Road Home softlocking the game when played by Leshy
- Fixed Abnormal Bosses config replacing Grizzly Bears with Guardian Apostles in Part 1 during bosses
- Fixed Nosferatu not evolving into the correct forme
- Fixed WhiteNight not being immune to Touch of Death
- Fixed blessings being added incorrectly when Plague Doctor changes sides
- Fixed learned dialogue for Marksman and Quick Draw not playing
- Fixed some WhiteNight-related dialogue not playing correctly
- Fixed StarSound special ability targeting the wrong slots
- Fixed Behaviour Adjustment's incorrect cost from 3 Bones --> 3 Energy
- Fixed Judgement Bird special ability interaction with Repulsive cards
- Fixed Trapper boss phase 2 only using a select few modded cards - it should now draw from a larger pool
- Fixed Abnormal Trapper boss still using vanilla cards during the final phase
- Fixed some stat spell cards not showing stats
- Fixed Tiphereth B being obtainable from the Sefirot choice node
- Fixed Nothing There evolving immediately upon being revealed
- Fixed Today's Neutral Expression retaining the Undying sigil when played by the opponent
- Fixed Luminous Bracelet not showing its stats
- Fixed Queen of Hatred not recovering from exhaustion
### 🔧 Tweaks
- Twilight starter deck can now be unlocked by completing the Final Apocalypse challenge
- Minor adjustments to some sequences
- Adjusted icon for Start with a Beast cheat
- Adjusted description of Abnormal Bosses challenge to specify the affected bosses
- Angela is now unlocked when the player has at least 2 distinct Sephirah cards in their deck when moving to a Sefirot choice node
- Added extra indicators for when Bless triggers
- Bless special ability will no longer affect Giant and Uncuttable cards (Mule cards are still affected)
- Chance for Bless to create a Heretic apostle is now tied to the game's seed instead of being fully random
- Blessings are now given to whomever owns the good doctor
- Changed challenge icon for 'Start with a Beast'
- Adjusted how Blind Rage calculates slots to target
- Quick Draw icon is now flipped when possessed by the opponent
- Made a number of tweaks to dialogue
- WhiteNight now uses the Terrain stat layout
- Improved Adoration special ability's effect
- Child of the Galaxy is now singleton
- Modified how Smile special ability behaves on death; does not change the actual effect
### ⚖️ Balancing
- <span style='color:limegreen'>Increased Skin Prophecy's Health from 2 --> 3
- <span style='color:limegreen'>Increased Spider Bud's Health from 2 --> 3
- <span style='color:limegreen'>Increased Tiphereth A's Health from 2 --> 3
- <span style='color:limegreen'>Reduced Notes from a Crazed Researcher's cost from 4 --> 3 Bones
- Rebalanced Express Train to Hell from 0/4, 4 Bones --> 0/1, 2 Energy
- Rebalanced Der Freischütz from 1/1, Bifurcated Strike, Sniper --> 2/2, Sniper, Persistent
- Rebalanced We Can Change Anything from 0/2 --> 1/1
- Rebalanced Nothing There (final) from 9/9, Piercing, Thick Skin x2 --> 8/8, Piercing, Persistent
- Reworked The Snow Queen from 1/2, 5 Bones --> 2/2, 2 Blood
- Reworked Melting Love from 4/3 --> Slimes/6
- Reworked Silent Girl from 2/1, Trifurcated Strike --> 2/2, Persecutor
- Rebalanced All-Around Helper from 1/3; 4 Energy --> 1/2; 3 Energy
- Rebalanced The Burrowing Heaven from 1 Blood -> 2 Bones
- Rebalanced Child of the Galaxy from whatever it was --> 1 Blood, Lonely, targeted spell
- Reworked Yesod from 0/1; Hoarder, Corrector --> 2/3, Hoarder
- Reworked Chesed from 0/5; Thick Skin, Healer; 1 Blood --> 1/4; Regenerator, Healer; 4 Energy
- Rebalanced Hokma from 2/3 --> 1/4
- Rebalanced Angela from 3/3; Ruler of Frost, Unkillable --> 2/3; Ruler of Frost, Persecutor
- Gebura now has Persistent instead of Piercing
- <span style='color:red'>Reduced Beauty and the Beast's Power from 1 --> 0
- <span style='color:red'>Reduced Power of Mountain of Smiling Bodies 2 from 3 --> 2
- <span style='color:red'>Reduced Power of Mountain of Smiling Bodies 3 from 5 --> 3
- <span style='color:red'>Mountain of Smiling Bodies now loses 1 Power and 1 Health when reverting to a previous forme
- <span style='color:red'>Increased Tiphereth B's cost from 2 --> 3 Energy
- <span style='color:red'>Reduced Big and Will Be Bad Wolf's Health from 4 --> 3
- <span style='color:red'>Red Riding Hooded Mercenary no longer has Persistent ability
- <span style='color:red'>Judgement Bird's special ability no longer affects FaceDown cards unless Bird has Persistent
- <span style='color:limegreen'>Beauty and the Beast now has 'KillsSurvivors' trait
- <span style='color:limegreen'>Scorched Girl now has 'KillsSurvivors' trait
- <span style='color:limegreen'>Time Machine now lets you choose one of three cards to remove, rather than doing it randomly
- Adjusted some encounter blueprints' balance
- Changed the CardTemple of some cards
### ➕ Additions
- Added Final Apocalypse challenge and boss
- Added Achievements API support (6 achievements)
- Added 2 new items: Single Recall in a Bottle and Total Recall in a Bottle
- Added 3 new encounters
- Added 'Reskin Sigils' config, controlling whether Sentry and Sniper should be renamed and reskinned while this mod is installed
### 💣 Removals
- Removed Magic Bullet special ability
- Removed unused dialogue events

## v2.0.2 - Minor patch (7/29/2023)
### 🩹 Bug fixes
- Fixed incorrect play cost for Hokma (2 Bones --> 2 Blood)
- Fixed 'Start With' Cheats adding extra copies when restarting a run using the retry button

## v2.0.1 - Minor patch (7/25/2023)
### 🩹 Bug fixes
- Fixed Bloodbath evolutions not being correctly added to the game
- Fixed softlock when a card with a Totem-given Fledgling sigil evolves
### 🔧 Tweaks
- Increased point count of Miracle Worker challenge (12 --> 60) to better indicate its difficulty
### ➕ Additions
- Added dialogue to help indicate when Bless special ability has activated

## v2.0.0 - The One Perfect Book (7/22/2023)
Note that in the future, updates and changes to modded sigils will be found in the Abnormal Sigils changelog.

### 🧱 Structural
- Separated sigils into own mod: Abnormal Sigils
- Added 2 new mod dependencies: New Spell Card Toolkit, Abnormal Sigils
- Removed BepInEx as a dependency (redundant due to API)
- Reformatted the configuration file (set values will more than likely not carry over)
- Card and abilities now work and appear in Act 2
- Improved sigil code to no longer include card-specific effects; these effects are now special abilities
- Sniper and Sentry sigils will be reskinned and renamed while this mod is active
### 🩹 Bug fixes
- Abnormality card choice now correctly displays and clears dialogue
- Fixed custom challenges not working properly in Act 1
- Fixed custom death cards not being created correctly in some circumstances
- Fixed broken encounters
- Gift Giver ability no longer gives copies of owned singleton cards
- WhiteNight event no longer softlocks when there are multiple Plague Doctors in play
- Nothing There is no longer copyable by Goo Mage
- Guardian Apostle no longer revives immediately after being Downed
### 🔧 Tweaks
- Singing Machine no longer looks like a Terrain card
- Dragon cards given new appearances, no longer display their stats
- Improved sigil rulebook description to be clearer, less cluttered
- Volatile ability now uses a custom flipped icon when used by opponents
- Sporogenic and Serpent's Nest abilities can now stack
- Starter Deck 'Road to Oz' now includes The Road Home (replacing Wolf Cub) and Ozma
- Starter Deck 'Magical Girls!' now includes Magical Girl C
- Abnormality choice node probabilities changed to (0%, 2%, 5%) by default and (2%, 5%, 10%) with Better Rare Chances cheat enabled
    - Applies to both Part 1 and KCM
- Abnormality choice node can now offer multiple Rare cards as choices
- Increased point amount of Better Rare Chances (-15 --> -10)
- Adjusted flow of some dialogue
- Improved some cards' portraits
- Leshy can now trigger special events on his side of the board
    - You will not receive the rewards for doing so however
- Miracle Worker challenge now plays Plague Doctor during a random turn and will trigger during boss battles
    - activation sequence no longer plays every battle
- Plague Doctor uses a separate, per-run counter if played by Leshy
- Bless special ability can no longer trigger multiple times per battle
- Replaced Marksman and Quick Draw sigils with the vanilla Sniper and Sentry sigils
### ➕ Additions
#### Cards
- Added the following cards:
    - Magical Girl C, Price of Silence, Pinocchio, Nosferatu, The Way Home, Ozma, Silent Girl (Rare)
- Added the following special cards:
    - The Adult Who Tells Lies, Jester of Nihil, Malkuth, Yesod, Hod, Netzach, Tiphereth A and Tiphereth B, Gebura, Chesed, Binah, Hokma, Angela
- Added the following starter decks (* = Replaces the primary card if Ruina cards are disabled in the config):
    - Random (3 randomly selected mod cards)
    - People Pleasers (Today's Shy Look, Pinocchio/Mirror of Adjustment*, Behaviour Adjustment)
    - Freak Show (Beauty and the Beast, Void Dream Queen Bee)
    - Apocrypha (Fragment of the Universe, Skin Prophecy, Price of Silence/1.76MHz*)
    - Keter (Bloodbath, The Burrowing Heaven, The Snow Queen)
- Added the following Tribes:
    - Anthropoid, Botanic, Divine, Fae, Mechanical
- Added the following Traits:
    - Boneless, SwanBrother, NakedSerpent, SporeFriend, ImmuneToInstaDeath, Orchestral
- Added pixel sprites for all cards
#### Abilities
- Added the following abilities:
    - Neutered, Neutered Latch, Return to Nihil, False Throne, Rightful Heir, Opportunistic, Cycler, Barreler, Follow the Leader, Persistent
- Added the following stat icons:
    - Nihil, Passing Time, Sigil Power
- Added the following special abilities:
    - Cowardly, The Homing Instinct
- Added the following status effects:
    - Spores, Worms
- Abilities can now be used by cards in Act 2
#### Other
- Added new configuration options
- Added the following cheats:
    - Start with a Beast, Start with a Jester, Start with a Liar
- Added Sefirot card choice node
### ⚖️ Gameplay Changes
#### Cards
- All-Around Helper: Cost changed (2 Blood --> 4 Energy)
- Apocalypse Bird: Health increased (8 --> 12), given Made of Stone
- Apostles: Replaced Terrain trait with Apostle trait, removed Uncuttable trait
- Downed Apostles: Health reduced to 1 for all variants, removed Repulsive sigil
- Spear Apostle: Power increased from 3 --> 4
- Army in Black: Reworked into Targeted Spell with Volatile
- Army in Pink: special ability now triggers when 3 ally cards die
- Backward Clock: Cost changed (4 Bones --> 2 Energy)
- Behaviour Adjustment: Cost changed (4 Bones --> 3 Energy)
- Big Bird: Given Cycler ability
- Bloodbath 2: Cost reduced (2 --> 1 Blood)
- Bloodbath 3: Rebalanced (1/3; 3 Blood --> 1/2, 2 Blood)
- Blue Star: Reworked into:
    - Forme 1: 0/4; Fledgling; 2 Blood
    - Forme 2: 0/4; Fledgling, Idol; 3 Blood
    - Forme 3: 4/4; Fledgling, All Strike; 4 Blood
- The Burrowing Heaven: Reworked into 0/1; Guardian, Sentry; 1 Blood
- CENSORED: Rebalanced (6/3, 4 Blood --> 4/3, 3 Blood)
- Clouded Monk: Cost reduced (3 Blood --> 2 Blood)
- Dimensional Refraction Variant: Reworked (4/4; 3 Blood --> 0/1; Sigil Power; 2 Blood)
- Il Pianto Della Luna: Health increased (6 --> 7)
- Child of the Galaxy: Reworked into 1/1; Flag Bearer, Bone Digger; 1 Blood
- Don't Touch Me: Cost changed (2 Bones --> 2 Energy), given Terrain trait
- Brothers: Given Terrain trait
- Second Brother: Given Piercing ability, Power reduced (0 --> 1)
- Third Brother: Health reduced (3 --> 2)
- Fourth Brother: Health reduced (2 --> 1)
- Fifth Brother: Replaced Sharp Quills with Scorching
- Sixth Brother: Replaced Stinky with Thick Skin
- Flesh Idol: Reworked into 0/4; 2 Bones; Aggravating, Fledgling (2)
- Funeral of the Dead Butterflies: Rebalanced (3/3, 3 Blood --> 1/3, 2 Blood)
- Giant Tree Sap: Cost reduced (4 --> 3 Bones), is now Rare
- Happy Teddy Bear: Reworked into 1/5; Guardian; 6 Bones
- King of Greed: Rebalanced (4/5, Hefty, 2 Blood --> 2/5, Cycler, 1 Blood)
- Luminous Bracelet: Reworked into 0/2 Targeted Spell; Greedy Healing, Give Stats and Sigils; 2 Energy
- Magical Girl D: Rebalanced (3 Health; 2 Blood --> 2 Health; 1 Blood), renamed to The King of Greed
- Magical Girl H: Power reduced (2 --> 1), given Opportunistic ability
- Queen of Hatred: Power increased (7 --> 8)
- Magical Girl S: Power reduced (2 --> 1), no longer Rare, renamed to The Knight of Despair
- Melting Love: Health increased (2 --> 3)
- 1.76 MHz: Reworked (0/3; Annoying, Leader; 2 Bones --> 2/1; Annoying; 3 Energy)
- Mountain of Smiling Bodies 2: Cost reduced (3 Blood --> 2)
- Mountain of Smiling Bodies 3: Rebalanced (4 Power; 4 Blood --> 5 Power; 3 Blood)
- Nameless Fetus: Cost reduced (5 --> 3 Bones)
- Notes from a Crazed Researcher: Reworked into Targeted Spell; 2/0; Brittle, Give Stats and Sigils; 4 Bones
- Nothing There Final: Given Piercing, Thick Skin x2 sigils
- Old Faith and Promises: Cost changed (2 Bones --> 3 Energy)
- One Sin and Hundreds of Good Deeds: Cost reduced (2 Bones --> 1 Bone)
- Porccubus: Health reduced (2 --> 1)
- Queen Bee: Health reduced (6 --> 4)
- Little Red Riding Hooded Mercenary: Reworked into 2/5; Sniper, Persistent; 3 Blood; Crimson Scar
- Big and Will Be Bad Wolf: Reworked into 3/4; Assimilator; 3 Blood; Crimson Scar
- Sapling: Reworked (0/2; free --> 0/2; Bone Digger, 2 Bones; Terrain)
- Scarecrow Searching for Wisdom: Rebalanced (1/3, 5 Bones --> 1/1, 4 Bones)
- Schadenfreude: Rebalanced (0/1; Quick Draw, Touch of Death; 4 Bones --> 1/1; Sentry; 3 Energy)
- Scorched Girl: Cost reduced (3 --> 2 Bones)
- Shelter from the 27th of March: Reworked into Targeted Spell; 0/0 ; Repulsive, Aggravating, Give Sigils; 3 Energy
- Spider Buff: Cost reduced (4 --> 3 Bones)
- Chairs: Power reduced (1 --> 0)
- Silent Orchestra: Rebalanced (1/5 --> 2/6)
- Silent Machine: Rebalanced (0/8, 2 Blood --> 0/3, 1 Blood)
- The Snow Queen: Rebalanced (3 Health 6 Bones --> 2 Health; 5 Bones)
- Snow White's Apple: Health reduced from 3 --> 1
- Snow White's Vines: gained the Terrain Trait
- The Firebird: Power increased (1 --> 2)
- The Naked Nest: Given NakedSerpent Trait
- The Naked Worm: Given NakedSerpent Trait
- Theresia: Cost changed (1 Bone --> 2 Energy)
- Today's Shy Look: Special ability tweaked to randomise when multiple copies are drawn at once
- Standard Training-Dummy Rabbit: Rebalanced (0/1, 1 Bone --> 0/2, 1 Energy)
- The Lady Facing the Wall: Rebalanced (0/2; Punisher --> 1/2; Sharp Quills)
- We Can Change Anything: Power reduced (1 --> 0)
- WhiteNight: Health reduced from 666 --> 66, replaced Terrain trait with Apostle, added ImmuneToInstaDeath trait
    - Can now be killed by regular cards, with a different reward if done so
- You Must Be Happy: Reworked into Targeted Spell, 0/2; Scrambler; 2 Energy
- You're Bald...: Reworked (1/1, 3 Bones --> 0/2, 2 Energy)
- Ttungsil: Removed Fledgling ability
#### Abilities
- Apostle: Now prevents damage and death while WhiteNight is an ally
- Broodmother: Powerlevel reduced (4 --> 3)
- Burning: Renamed to Scorching
- Nettle Clothes: Now considers cards with SwanBrother trait, no longer deals damage to the base card upon Brother cards dying
- Martyr: Can now activate when sacrificed, added additional effect:
    - "When a card bearing this sigil dies, all allied creatures gain 2 Health [ and lose all negative status effects ]."
- Corrector: Powerlevel reduced (3 --> 2)
- Frozen Heart: Healing amount changed (1 --> 2)
- Fungal Infector: Renamed to Sporogenic, reworked to be:
    - "Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils."
- Piercing: Reworked to be:
    - "Damage dealt by this card cannot be negated or reduced by sigils such as Armoured or Thick Skin. Deal 1 overkill damage when attacking a card."
- Serpent's Nest: No longer obtainable as a totem bottom, reworked to be:
    - "When a card bearing this sigil is struck, the striker gains 1 Worms."
- Conductor: Reworked to be:
    - "The effect of this sigil will change over the next 3 turns. This turn: do nothing."
- Ruler of Frost: Reworked to be:
    - "Activate: Once per turn, pay 3 Bones to choose a space on the board. If the space is occupied by a killable card, transform it into a Frozen Heart. Otherwise create a Block of Ice."
- The Train: Reworked to be:
    - "Three turns after this card is played, kill all creatures on the board. Creatures killed this way do not drop bones."
- Sap: Triggers less often, is now inherited from card merging
- Justitia: No longer affects Terrain and Pelt cards, mouse cursor will change when hovering over affectable cards
#### Other
- Starter decks Road to Oz, Magical Girls!, Twilight now require completing the respective in-game event before unlocking the deck
    - This can be overriden in the config by-the-by
### 💣 Removals
- Removed emission sprites from some terrain cards
- Removed Marksman and Quick Draw abilities

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
</details>