## FULL CHANGELOG

### v1.2.2 - The 'Who Let Me Code' patch (10/6/2022)
* 🩹 Bug fixes
  * Fixed the following cards not being obtainable as card choices
    * You're Bald...
    * Plague Doctor
    * Yin
    * Yang
    * Judgement Bird
    * One Sin and Hundreds of Good Deeds

### v1.2.1 - Minor patch (9/26/2022)
* 🧱 General
  * Fixed inaccurate information in the ReadMe
  * CENSORED's ability now has opponent compatibility
* 🩹 Bug fixes
  * Fixed Hatred special not properly checking for other Magical Girls
* 🔧 Tweaks
  * Improved rulebook entry descriptions for special abilities

### v1.2.0 - Close Encounters of the Abnormal Kind (9/18/2022)
* 🧱 General
  * Adjusted the descriptions for some configurations to reflect new changes/be clearer.
  * Fixed inaccurate information in the ReadMe
  * Added PackManager compatibility
* 🩹 Bug fixes
  * Fixed visual bug related to interactions with Regenerator and facedown cards
  * Fixed visual bug related to Cursed ability activating when the killer has also died
  * Fixed visual bug where created Spore Mold Creatures would glow when they shouldn't
  * Fixed 1.76 MHz's cost being 3 bones instead of 2 bones
  * Fixed First Brother's Health being 2 not 1
  * Fixed Second Brother's Health being 2 not 1
  * Fixed Fourth Brother's Heakth being 1 not 2
  * Fixed Fungal Infector not affecting cards that were affected in previous battles
  * Fixed Singing Machine not having an emission
  * Fixed Queen of Hatred not switching back from Tired forme
  * Fixed Magical Girl D not showing dialogue on evolve
  * Fixed Plague Doctor special ability not activating when on the opponent's side of the board
  * Fixed placeholder descriptions for Grave of Cherry Blossoms, The Little Prince still being present
  * Fixed Witness ability's Rulebook entry displaying an incorrect cost
* 🔧 Tweaks
  * Reworked Conductor ability to now give passive Power rather than draw Chair cards
  * Nettles Clothes ability now shows added abilities
  * The Naked Nest and The Naked Worm are now part of the Insect tribe
  * Redid the dialogue for the Abnormality choice node, no longer plays in KCM
  * Tweaked Broodmother, Queen Nest, Serpent's Nest abilities to have drawn cards inherit merged sigils
  * Tweaked Gift Giver ability to have drawn cards inherit merged sigils IF Gift Giver is possessed by Laetitia
  * Broodmother, Gift Giver, Corrector abilities now have opponent support
  * Rewrote rulebook entry for Queen Nest
  * Tweaked a number of cards' descriptions to better fit the game
* ⚖️ Balancing
  * Yang event now only removes 1 card of the relevant cards at random instead of both
  * Funeral of the Dead Butterflies is no longer Rare
  * Notes from a Crazed Researcher no longer has Volatile
  * WhiteNight no longer heals taken damage
  * Buffed Singing Machine's Health from 4 --> 8
  * Buffed Void Dream Rooster's Health from 2 --> 3
  * Rebalanced Funeral of the Dead Butterflies to be (3,3) stats, 3 Blood, Double Strike
  * Changed The Dreaming Current from (3,2) stats, 2 Blood cost, Rampager --> (4,2) stats, 3 Blood cost, Rampager and Waterborne
  * Nerfed Silent Orchestra's stats from (3,6) --> (1,5)
  * Increased Worker Bee's cost from FREE --> 1 Bone
* ➕ Additions
  * Added custom encounters for each region
  * Added starter deck Lonely Friends
    * Scorched Girl, Laetitia, Child of the Galaxy
  * Added starter deck Blood Machines
    * We Can Change Anything, All-Around Helper, Singing Machine
  * Added config option Abnormal Bosses
  * Added config option Abnormal Encounters
  * Added config option Better Rare Chances
  * Added config option Miracle Worker
  * Added challenge Abnormal Bosses
  * Added challenge Abnormal Encounters
  * Added challenge Abnormal Encounters
  * Added challenge Miracle Worker
  * Added cheat Better Rare Chances
  * Added 10 death cards
  * Added opponent-only cards: Guardian Apostle, Moleman Apostle, Rudolta (mule version), Skeleton Shrimp, Crumpled Can

### v1.1.1 - Broken Shovel patch (8/26/2022)
* 🧱 General
  * Fixed ReadMe's description of Sapling showing the wrong Power
  * Fixed ReadMe's description of Giant Tree being incorrectly formatted
  * Removed an duplicate entry in the ReadMe of Lady Facing the Wall
  * Changed ReadMe's description of Nothing There to display X/X for stats
* 🩹 Bug fixes
  * Fixed Gardener not activating at all
  * Fixed Magical Girl S and Army in Pink's special abilities activating whilst in hand
  * Fixed Omni Strike not attacking Giant cards properly
* 🔧 Tweaks
  * Changed emissions of Parasite Tree, Sapling, and The Little Prince to not obscure their cost
  * Tweaked Army in Pink's special ability
* ⚖️ Balancing
  * Buffed Apocalypse Bird's Power from 2 --> 3
  * Buffed Army in Black's Power from 0 --> 1
  * Buffed Void Dream's Power from 0 --> 1
  * Increased Spider Brood's cost from FREE --> 1 Blood

### v1.1.0 - First Major Update(tm) (8/22/2022)
* 🧱 General
  * Changed file name for config file (see above for more information on this)
  * Rearranged the order of the configs in the config file
  * Added opponent AI compatibility for Sniper and Marksman abilities
  * Bifurcated Strike, Trifurcated Strike, and Double Strike now add stackable extra attacks for Sniper and Marksman abilities
  * Omni Strike now attacks the base card's opposing slot if they aren't a Giant card rather than only the leftmost slot
  * All abilities now have an icon for Act 2 if you wish to mess around with them in Act 2 - NOTE: Act 2 is not supported and has not been playtested
  * Fixed inaccurate information in the ReadMe
* 🩹 Bug fixes
  * Fixed custom death cards not being properly added to the game
  * Fixed Assimilator and Bloodfiend still activating when the base card has died
  * Fixed Martyr ability causing the game to freeze when there are no valid targets to be healed
  * Fixed Quick Draw and Woodcutter abilities causing the game to freeze in certain scenarios
  * Fixed Gardener ability activating when not on the board
  * Fixed Gardener ability causing the game to freeze when the dead card's slot isn't empty
  * Fixed Ruler of Frost ability causing the game to freeze when the dead card's slot isn't empty
  * Fixed Cursed ability affecting Giant cards
  * Fixed Flag Bearer ability revoking the Health buff under certain situations
  * Fixed Regenerator ability killing adjacent cards when they are at max Health
  * Fixed incorrect Regenerator ability description
  * Fixed Magical Girl H's special ability not accounting for certain situations
  * Fixed Judgement Bird's special ability not accounting for Airborne or Repulsive
  * Fixed Submerged cards not flipping when targeted by Judgement Bird
  * Fixed the Mirror of Adjustment not properly displaying the Mirror stat icon
  * Fixed Nothing There and Express Train to Hell being selectable hosts/sacrifices at card merge and campfire nodes
* 🔧 Tweaks
  * Assimilator, Queen Nest, Cursed, Regenerator, Reflector, Grinder abilities are now modular
  * Made a number of abilities stackable (see Abilities section for more information)
  * Tweaked Bloodbath's special ability to better indicate to the player when it has activated whilst in hand
  * Snow White's Apple now kills survivors at the Campfire
  * Plague Doctor now changes its appearance based on the number of times it has healed cards (change persists even if you reset mid-battle)
  * Piercing ability now has different behaviour when possessed by Staff Apostle
  * Added placeholder text for when all 3 Magical Girls are on the same side of the board
  * Updated Nameless Fetus's sprites
  * Updated WhiteNight's sprite and emission
  * Mirror of Adjustment now uses the default stat layout
  * Made minor changes to various card and ability descriptions
  * Cards killed by certain event cards no longer activate triggers. This is to prevent softlocks relating to certain ability combinations
* ⚖️ Balancing
  * Queen Nest ability no longer creates a Worker Bee when played
  * Made of Slime ability now gives created cards 1 Power, no longer affects cards with 1 Health
  * Cursed ability no longer affects card with the Uncuttable trait or the Made of Stone ability
  * Changed Bloodbaths' stats and gave them the Spilled Blood stat icon
  * Cards created by the Roots ability now inherit the base card's sigils
  * Minions created by Gardener now inherit the dead card's sigils
  * Minions created by CENSORED now inherit the full Power of the killed card
  * Army in Pink's special ability now creates 4 copies of Army in Black in hand when triggered
  * Bloodbath 1, 2, and 3 now all the have Spilled Blood stat icon
  * Buffed CENSORED's Health from 2 --> 3
  * Buffed Queen Bee's Health from 5 --> 6
  * Buffed Snow Queen's Health from 2 --> 3
  * Buffed Scarecrow Searching for Wisdom's Health from 2 --> 3
  * Buffed Luminous Bracelet's Health from 1 --> 2
  * Buffed Opened Can of WellCheers's Health from 1 --> 2
  * Buffed We Can Change Anything's stats from (0,1) --> (1,2)
  * Increased Express Train to Hell's cost from FREE --> 6 Bones
  * Reduced The Train ability's activation cost from 12 Bones --> 6 Bones
  * Reduced Parasite Tree's cost from 2 Blood --> 1 Blood
  * Rebalanced 1.76 MHz to 2 Bones cost
  * Rebalanced Blue Star 1 to have (0,2) stats, Fledgling
  * Rebalanced Blue Star 2 to have (2,6) stats, Assimilator, Omni Strike
  * Rebalanced Flesh Idol to have (0,2) stats, 3 Bones cost
  * Rebalanced Crumbling Armour to have (0,3) stats, 5 Bones cost
  * Rebalanced Scythe Apostle from 3 Power, Woodcutter --> 2 Power, Double Strike
  * Rebalanced Army in Pink to have (3,3) stats, Protector, Clinger
  * Rebalanced Army in Black to have (0,1) stats, Volatile, Brittle, 0 cost
  * Nerfed Bloodbath's Health from 3 --> 1
  * Nerfed Bloodbath 1's Health from 3 --> 1 
  * Nerfed Bloodbath 2's Health from 3 --> 2
  * Nerfed Bloodbath 3's Power from 3 --> 1
* ➕ Additions
  * Big Bird and Blue Star now possess special abilities
  * Added starter deck First Day
    * One Sin, Fairy Festival, Old Lady
  * Added starter deck Road to Oz
    * Wolf Cub, Scarecrow Searching for Wisdom, Warm-Hearted Woodsman
  * Added starter deck Magical Girls!
    * Magical Girl H, Magical Girl D, Magical Girl S
  * Added starter deck Twilight
    * Punishing Bird, Big Bird, Punishing Bird
  * Added card choice node
  * Added config option No Donators
  * Added config option Card Choice at Start
  * Added combat event for Apocaylpse Bird
  * Added combat event for Yin and Yang
  * Added card Child of the Galaxy
  * Added card Fragment of the Universe
  * Added card Apocalypse Bird
  * Added card The Little Prince
  * Added card Dream of a Black Swan
  * Added card Giant Tree Sap
  * Added card Skin Prophecy
  * Added card Behaviour Adjustment
  * Added card Old Faith and Promise
  * Added card Yin
  * Added card Yang
  * Added card Backward Clock
  * Added card Il Pianto della Luna
  * Added ability Fungal Infector
  * Added ability Clothes Made of Nettles
  * Added ability Witness
  * Added ability Corrector
  * Added ability Alchemist
  * Added ability Time Machine
  * Added special ability Giant Tree Sap
  * Added special ability Big Bird

### v1.0.7 - Martyr bug fix (7/22/2022)
* 🩹 Bug fixes
  * Fixed Martyr ability softlocking when there aren't any other valid cards
  * Melting Love can now be found as a rare card
  * Judgement Bird is now found as a common choice instead of a rare
* 🔧 Tweaks
  * Changed sigil icons of activated abilities to better indicate their nature
  * Martyr ability now longer changes your view during combat
* ⚖️ Balancing
  * Reduced One Sin's cost from 4 Bones --> 2 Bones

### v1.0.5 & v1.0.6 - Nothing Angels patch (7/3/2022)
* 🩹 Bug fixes
  * Fixed Apostles not entering Downed state when killed
  * Fixed Nothing There not being properly added to the deck
  * Fixed Apostle Spear emission not showing
  * WhiteNight event works again
* 🔧 Tweaks
  * Dreaming Current now has Rampager instead of Sprinter and Hefty
  * Reverted some cards' emissions to the default colour
* ⚖️ Balancing
  * Select cards can no longer be used at the Campfire or Mycologists

### v1.0.3 & v1.0.4 - Mountains of Coloured Text patch (6/29/2022)
* 🩹 Bug fixes
  * Fixed Assimilator ability not doing proper checks on the base Card
  * Fixed Assimilator ability not properly checking for MoSB evolutions (v1.0.4)
* 🔧 Tweaks
  * Leshy's eyes now turn red during the WhiteNight event
  * Changed colour of text relating to WhiteNight event
  * Tweaked Assimilator ability OnDie trigger to be specific to MoSB

### v1.0.2 - Prayer and Bees patch (6/28/2022)
* 🩹 Bug fixes
  * Fixed Queen Nest ability softlocking when Queen Bee is dropped by the Mule
* 🔧 Tweaks
  * Tweaked Confession ability to make Heretic sequence smoother
* ⚖️ Balancing
  * Cards from the WhiteNight event no longer drop bones when killed
  * Hundreds of Good Deeds now dies if Confession is activated during a boss

### v1.0.1 - Bones and Trains patch (6/27/2022)
* 🧱 General
  * Removed the fourth zero from the in-game version number to be consistent with the Thunderstore version number
* 🩹 Bug Fixes
  * Fixed Boons of the Bone Lord not giving bones
  * Fixed cards not dropping bones if a copy was previously killed by The Train
  * Fixed The Train ability being free to activate
* 🔧 Tweaks
  * Confession ability changed to an activated-type ability
  * Fixed an error in the README regarding The Train ability's description
  * Can no longer activate The Train ability if there are no other cards on the board
* ⚖️ Balancing
  * Increased The Train ability activation cost 10 --> 12

### v1.0.0 - Initial release (6/26/2022)
* ➕ Additions
  * 71 Cards
  * 38 Abilities
  * 13 Special abilities
