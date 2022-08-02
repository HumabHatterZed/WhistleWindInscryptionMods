## Changelog
### v1.1.0 - The First Major Update(tm)
* Groundwork
  * Changed file name for config file (see ReadMe for more info on this)
  * Added some new game patches
  * Rearranged the order of the configs in the config file
  * All abilities now have an icon for Act 2, if you want to mess around with them in Act 2 for whatever reason
* Bug fixes
  * Fixed custom death cards not being properly added to the game
  * Fixed Nothing There and Express Train to Hell being selectable at card merge or campfire nodes
  * Fixed Martyr ability causing the game to freeze when there are no valid targets to be healed
  * Fixed Quick Draw and Woodcutter abilities causing the game to freeze in certain scenarios
  * Fixed certain custom save data only saving per run and not per save
  * Judgement Bird's special ability no longer affects cards with Repulsive ability
  * Mirror of Adjustment now properly displays the Mirror stat icon
* Tweaks
  * Tweaked Bloodbath's special ability to better indicate when it is activated whilst Bloodbath is in your hand
  * Assimilator and Bloodfiend abilities now check if the base card is dead before activating. This should prevent visual glitches
  * Updated Rulebook entry for Judgement Bird's special ability
  * Connected Nameless Fetus's head to its body in both of its sprites
  * Minor change to Mirror of Adjustment's description
  * Changed Mirror of Adjustment to use the default stat layout
  * Assimilator, Queen Nest, Cursed, Regenerator, Reflector, Grinder abilities are now modular
  * Plague Doctor now changes appearance based on the number of times it's healed cards
* Balancing
  * Increased Queen Bee's Health from 5 --> 6
  * Changed Bloodbath's stats and gave it the Spilled Blood stat icon
    * Bloodbath 0 (0,3) --> (0,1)
    * Bloodbath 1 (1,3) --> (0,1)
    * Bloodbath 2 (2,3) --> (0,2)
    * Bloodbath 3 (3,3) --> (1,3)
  * Express Train to Hell cost increased from (FREE) --> (x6)
  * The Train ability cost reduced from (x12) --> (x6)
  * Blue Star 1 now only takes 1 turn to evolve
  * Blue Star 1 is no longer singleton (one per deck)
  * Blue Star 1 no longer has Idol ability
  * Blue Star 2 no longer has Idol ability
  * Army in Pink no longer has Undying ability
  * Army in Black buffed from 2 Power --> 4 Power
  * Grave of Cherry Blossoms nerfed from 3 Health --> 2 Health
  * Judgement Bird is a Rare card again
  * WhiteNight's Apostles are no longer immune to Touch of Death
  * Scythe Apostle now has 2 Power, Double Strike ability
  * Crumbling Armour Health's nerfed from 4 --> 3
  * Parasite Tree's cost reduced from 2 blood --> 1 blood
  * Rebalanced Made of Slime to activate on upkeep, no longer inherits the killed card's Power
  * CENSORED buffed from 2 Health --> 3 Health
  * CENSORED's minions now inherit the full Power of the killed card
  * We Can Change Anything buffed from 1 Health --> 2 Health
* Additions
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
  * Added mini-event for Apocaylpse Bird
  * Added mini-event for Yin and Yang
  * Added Yin and Yang mini-event
  * Added Apocalypse Bird mini-event
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
  * Added ability Spores
  * Added ability Clothes Made of Nettles
  * Added ability Witness
  * Added ability Corrector
  * Added ability Alchemist
  * Added ability Time Machine
  * Added special ability Giant Tree Sap
  * Added special ability Big Bird

### v1.0.7 - Martyr bug fix (7/22/2022)
* Bug fixes
  * Fixed Martyr ability softlocking when there aren't any other valid cards
  * Melting Love can now be found as a rare card
  * Judgement Bird is now found as a common choice instead of a rare
* Tweaks
  * Changed sigil icons of activated abilities to better indicate their nature
  * Martyr ability now longer changes your view during combat
* General
  * Internal refactoring and some other stuff from the 1.1 development branch that I can't recall

### v1.0.5 & v1.0.6 - Nothing Angels patch (7/3/2022)
* Bug fixes
  * Fixed Apostles not entering Downed state when killed
  * Fixed Nothing There not being properly added to the deck
  * Fixed Apostle Spear emission not showing
  * WhiteNight event works again
* Tweaks
  * Dreaming Current now has Rampager instead of Sprinter and Hefty
  * Reverted some cards' emissions to the default colour
* Balancing
  * Select cards can no longer be used at the Campfire or Mycologists

### v1.0.3 & v1.0.4 - Mountains of Coloured Text patch (6/29/2022)
* Bug fixes
  * Fixed Assimilator ability not doing proper checks on the base Card
  * Fixed Assimilator ability not properly checking for MoSB evolutions (v1.0.4)
* Tweaks
  * Leshy's eyes now turn red during the WhiteNight event
  * Changed colour of text relating to WhiteNight event
  * Tweaked Assimilator ability OnDie trigger to be specific to MoSB

### v1.0.2 - Prayer and Bees patch (6/28/2022)
* Bug fixes
  * Fixed Queen Nest ability softlocking when Queen Bee is dropped by the Mule
* Tweaks
  * Tweaked Confession ability to make Heretic sequence smoother
* Balancing
  * Cards from the WhiteNight event no longer drop bones when killed
  * Hundreds of Good Deeds now dies if Confession is activated during a boss

### v1.0.1 - Bones and Trains patch (6/27/2022)
* Groundwork
  * Removed the fourth zero from the in-game version number to be consistent with the Thunderstore version number
* Bug Fixes
  * Fixed Boons of the Bone Lord not giving bones
  * Fixed cards not dropping bones if a copy was previously killed by The Train
  * Fixed The Train ability being free to activate
* Tweaks
  * Confession ability changed to an activated-type ability
  * Fixed an error in the README regarding The Train ability's description
  * Can no longer activate The Train ability if there are no other cards on the board
* Balancing
  * Increased The Train ability activation cost 10 --> 12

### v1.0.0 - Initial release (6/26/2022)
* Additions
  * 71 Cards
  * 38 Abilities
  * 13 Special abilities
