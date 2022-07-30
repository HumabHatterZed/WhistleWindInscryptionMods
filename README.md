# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's own mod of this topic, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.

The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay, while still being "balanced".

### Major update v1.1.0 now released!

This mod currently adds all abnormalities from Lobotomy Corp. Broken down, this means:
* **```84```** obtainable cards (excluding evolutions, minions, event-exclusives)
* **```41```** abilities (excluding event-exclusive ones)
* **```16```** special abilities
* **```4```** starter decks for Kaycee's Mod
* **```1```** card choice node
* Plus a few special events!

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1901
* API_dev-API v2.4.0

## ‼️ NOTICE ON v1.1.0 CHANGES ‼️
The mod's configuration file has been renamed to **```wstl.inscryption.lobotomycorp.cfg```**.

Any changes made in the old config file will **NOT** carry over and must be changed in the new config file after loading the game at least once.

The old config can be safely deleted at your convenience.

A number of other internal changes have been made that _shouldn't_ break your game, but just in case it is recommended that you start a new run after updating.

Please see the full changelog (included in the download package) for a full breakdown of what exactly has changed.

Card and ability changes will be listed in the Changelog section below, of course.

## 🩹 Known Bugs and Issues
### Hard to tell!
Because of the way Thunderstore works I can't (as far as I know) update this section of the ReadMe without updating the entire mod, so you'll have to check the Issues tab on the GitHub to see an accurate list of current bugs, issues, etc.

If you encounter any issues or bugs please report them to me asap by contacting me on the modding Discord or by opening an issue on the GitHub.

Feel free to contact me with any other feedback you have too!

## 💌 Credits
Shoutout to Arackulele, divisionbyz0rro, and julien-perge for having public GitHubs for me to reference back to and ~~steal~~borrow code from.

Special mention to James Veug's ReadmeMaker mod for providing the cost sprites I use; you're a lifesaver!

Big thanks to Rengar, yam the nokia, and everyone else on the modding Discord for reporting bugs to me!

## ⚖️ Changelog
For the full changelog, please refer to the .md file included in the mod package.

### v1.1.0 - First Major Update(tm) (?/?/202?)
* Bug fixes
  * Fixed custom death cards not being properly added to the game
  * Fixed Nothing There and Express Train to Hell being selectable at card merge or campfire nodes
  * Fixed Martyr ability causing the game to freeze when there are no valid targets to be healed
  * Fixed Quick Draw and Woodcutter abilities causing the game to freeze in certain scenarios
  * Judgement Bird's special ability no longer affects cards with Repulsive ability
  * Mirror of Adjustment now properly displays the Mirror stat icon
* Tweaks
  * Tweaked Bloodbath's special ability to better indicate when it is activated whilst Bloodbath is in your hand
  * Assimilator and Bloodfiend abilities now check if the base card is dead before activating. This should prevent visual glitches
  * Updated Rulebook entry for Judgement Bird's special ability
  * Connected Nameless Fetus's head to its body in both of its sprites
  * Minor change to Mirror of Adjustment's description
* Balancing
  * Changed Queen Bee's stats from (0,5) --> (1,3)
  * Changed Bloodbath's stats and gave it the Spilled Blood stat icon
  * Express Train to Hell cost increased from (FREE) --> (x6)
  * The Train ability cost reduced from (x12) --> (x6)
  * Blue Star 1 now only takes 1 turn to evolve and is no longer Singleton (one per deck)
  * Blue Star 1 no longer has Idol ability
  * Blue Star 2 no longer has Idol ability
  * Army in Pink no longer has Undying ability
  * Army in Black buffed from 2 Power --> 4 Power
  * Grave of Cherry Blossoms nerfed from 3 Health --> 2 Health
* Additions
  * Added 4 starter decks
  * Added card choice node
  * Added 2 new config options
  * Added 2 new mini-events
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
  * Added ability Nettles
  * Added ability Witness
  * Added ability Corrector
  * Added ability Alchemist
  * Added ability Time Machine

## 🛠️ Configuration Options
* Enable Mod
  * DEFAULT: True
  * What it says on the tin; this config enables/disables this mod's content.
* No Donators
  * DEFAULT: False
  * Prevents Donator-class abnormalities from being obtainable in-game (Backward Clock... Honoured Monk), like the original Lobotomy Corp.
* Choice Node at Start
  * DEFAULT: False
  * Adds the custom card choice node to the start of each region.
* All Modular
  * DEFAULT: False
  * Makes _most_ custom abilities modular, meaning they can be found on totem bases and on cards from the den trial.
* Special Abilities in Rulebook
  * DEFAULT: False
  * Adds entries for the special abilities to the Rulebook for your viewing pleasure.
* Reveal Select Descriptions
  * DEFAULT: False
  * Changes the description of certain abilities to actually describe what they do.

## ⚜️ Special Abilities
A number of cards added by this mod possess special behaviour not normally found in the Rulebook.
In the spirit of the original game, I won't be listing the effects here, only which cards possess a special ability and what triggers it.
You'll need to mess with the cards to figure out exactly what they do 🙃.
<details>
<summary>View specials</summary>
 
|Card|Trigger|
|:-|:-|
|Nameless Fetus|Reacts to being sacrificed.|
|Bloodbath|Reacts to cards being sacrificed.|
|Magical Girl H|Reacts to cards dying.|
|Nothing There|Reacts to dying.|
|Der Freiscütz|Reacts to dealing damage.|
|Crumbling Armour|Reacts to adjacent cards.|
|Magical Girl S|Reacts to adjacent cards.|
|Mountain of Smiling Bodies|Reacts to killing cards.|
|CENSORED|Reacts to killing cards.|
|Judgement Bird|Reacts to attacking cards.|
|Today's Shy Look|Reacts to being drawn.|
|Army in Pink|Reacts to cards dying.|
|Melting Love|Reacts to taking damage.|
|Yang|Reacts to being adjacent to another card.|
|Giant Tree Sap|Reacts to being sacrificed.|
|Big Bird|Reacts to other card on the board.|
 </details>
 
## ✨ Abilities
**Note: 'Totem' indicates the ability can be found on totem bases and on cards from den trials.**
<details>
<summary>View abilities</summary>

|Name|Description|Totem|
|:-|:-|:-:|
|Punisher|When a card bearing this sigil is struck, the striker is killed.|Yes|
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|Yes|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|Yes|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.||
|Idol|While this card is on the board, all opposing cards lose 1 Power.||
|Conductor|When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.||
|Frozen Heart|When this card dies, the killer gains 1 Health.||
|Ruler of Frost|When this card is played, create a Block of Ice in the opposing adjacent slots if they are empty. Otherwise, if the occupying card has 1 Health, kill it and create a Frozen Heart in its place. A Block of Ice and a Frozen Heart are both defined as: 0 Power, 1 Health.||
|Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|Yes|
|Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.|Yes|
|Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.||
|Healer|This card will heal a selected ally for 2 Health.||
|Queen Nest|When a card bearing this sigil is played, a Worker Bee is created in your hand. Create an additional Worker Bee whenever another card dies.||
|Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also bears this sigil.|Yes|
|Courageous|If an adjacent card has more than 1 Health, it loses 1 Health and gains 1 Power. This effect can activate twice for a maximum of -2 Health and +2 Power. Stat changes persist until battle's end.||
|Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|Yes|
|Assimilator|When a card bearing this sigil attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.||
|Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.||
|Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.||
|Flag Bearer|Adjacent cards gain 2 Health.||
|Grinder|This card gains the stats of the cards sacrificed to play it.||
|The Train|Activate: Pay 12 bones to kill all cards on the board, including this card. Cards killed this way do not drop bones.||
|Burning|The opposing card takes 1 damage at the end of their turn.||
|Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.||
|Volatile|When this card dies, adjacent and opposing cards are dealt 10 damage. (identical to Detonator)|Yes|
|Gift Giver|When this card is played, create a random card in your hand.||
|Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|Yes|
|Scrambler|When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble that card's stats.|Yes|
|Gardener|When an ally card dies, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health.||
|Slime|A card bearing this sigil takes 1 less damage from attacks. Additionally, cards placed adjacent to this card are turned into Slimes.||
|Marksman|You may choose which opposing space a card bearing this sigil strikes. (identical to Sniper)||
|Protector|Adjacent cards take 1 less damage from attacks.||
|Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||
|Alchemist|Activate: Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.||
|Time Machine|Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck based on their power level.||
|Clothes Made of Nettles|When a card bearing this sigil is played, create random Brothers in empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.||
|Spores|Adjacent cards gain 1 Spore and take damage equal to their Spore at the end of each turn. If a card with Spore is killed, create a Spore Mold Creature in that card's slot whose stats are equal to the card's Spore.||
|Witness|Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.||
|Corrector|A card bearing this sigil has its stats randomly changed according to its cost.|Yes|
|Apostle|*Thou wilt abandon flesh and be born again.*|||
|True Saviour|*My story is nowhere, unknown to all.*|||
|Confession and Pentinence|*Activate: Keep faith with unwavering resolve.*|||
</details>

## 📜 List of Obtainable and Minion Cards
**NOTES**
* **Singleton** means you can only have a single copy of that card in your deck  at a time (like Ouroboros).
* **Poisonous** means the card will kill survivors at the campfire when eaten.
* **X** means that that stat varies (typically because the card in question inherits its stats from somewhere else).
<details>
<summary>View cards</summary>

|Name|Power|Health|Cost|Sigils|Traits|Tribes|
|:-|:-:|:-:|:-:|:-:|:-:|:-:|
|Standard Training-Dummy Rabbit|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">||||
|Scorched Girl|1|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Volatile|||
|One Sin and Hundreds of Good Deeds|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Martyr|||
|Magical Girl H|2|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||Singleton||
|⤷ The Queen of Hatred|7|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Airborne|Singleton||
|Happy Teddy Bear|3|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky|||
|Red Shoes|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills<br>Guardian|||
|Theresia|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Healer|||
|Old Lady|1|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Stinky|||
|Nameless Fetus|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Worthy Sacrifice<br>Undying|Goat||
|The Lady Facing the Wall|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Punisher|||
|Nothing There|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">||Rare||
|The Lady Facing the Wall|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Punisher|||
|1.76 MHz|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Leader<br>Annoying|||
|Singing Machine|0|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Team Leader<br>Aggravating|||
|The Silent Orchestra|2|6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Conductor|Rare||
|⤷ Chairs|0|2||Leader|||
|Warm-Hearted Woodsman|2|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Woodcutter|||
|The Snow Queen|0|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|Frost Ruler|||
|⤷ Frozen Heart|0|1||Frozen Heart|||
|⤷ Block of Ice|0|1|||||
|Big Bird|2|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">||Singleton|Avian|
|All-Around Helper|1|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter<br>Bifurcated Strike|||
|Snow White's Apple|1|3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Roots|||
|Thorny Vines|0|1||Sharp Quills|||
|Spider Bud|0|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Broodmother||Insect|
|Spiderling|0|1||Fledgling||Insect|
|⤷ Spider Brood|1|3||||Insect|
|Beauty and the Beast|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Cursed||Hooved<br>Insect|
|Plague Doctor|0|3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Airborne<br>Healer|Singleton||
|Don't Touch Me|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Punisher<br>Guardian|||
|Rudolta of the Sleigh|2|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter<br>Gift Giver||Hooved|
|Queen Bee|0|5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Queen Nest||Insect|
|⤷ Worker Bee|1|1||||Insect|
|Bloodbath|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||Goat||
|Opened Can of WellCheers|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sprinter<br>Waterborne|||
|Alriune|4|5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Sprinter||Hooved|
|Forsaken Murderer|4|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">||||
|Child of the Galaxy|1|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Flag Bearer<br>Bone Digger|||
|Punishing Bird|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flying<br>Punisher|Singleton|Avian|
|Little Red Riding Hooded Mercenary|2|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman<br>Bitter Enemies|||
|Big and Will be Bad Wolf|3|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bitter Enemies||Canine|
|You're Bald...|1|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Fecundity|||
|Fragment of the Universe|1|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Piercing|||
|Crumbling Armour|0|4|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|Courageous|||
|Judgement Bird|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman|Singleton|Avian|
|Apocalypse Bird|10|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Omni Strike|Rare<br>Singleton|Avian|
|Magical Girl D|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|Singleton||
|⤷ The King of Greed|4|5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Hefty|Singleton||
|The Little Prince|1|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Spores|||
|⤷ Spore Mold Creature|X|X|||||
|Laetitia|1|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Gift Giver|||
|Laetitia's Friend|2|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|||Insect|
|Funeral of the Dead Butterflies|2|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bifurcated Strike|Rare||
|Dream of a Black Swan|2|5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Clothes Made of Nettles|Rare|Avian|
|⤷ First Brother|0|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|Double Strike|||
|⤷ Second Brother|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">||||
|⤷ Third Brother|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|Reflector|||
|⤷ Fourth Brother|0|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|Touch of Death|||
|⤷ Fifth Brother|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|Sharp Quills|||
|⤷ Sixth Brother|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|Stinky|||
|The Dreaming Current|3|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Rampager|||
|The Burrowing Heaven|0|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">||||
|Magical Girl S|2|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Protector|Rare<br>Singleton||
|⤷ The Knight of Despair|2|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bifurcated Strike<br>Piercing|Rare<br>Singleton||
|The Naked Nest|0|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Serpent's Nest|Poisonous||
|⤷ Naked Worm|1|1|||||
|Mountain of Smiling Bodies|2|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Assimilator|Rare||
|Schadenfreude|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Quick Draw<br>Touch of Death|||
|The Heart of Aspiration|1|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Leader|||
|Notes from a Crazed Researcher|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flag Bearer<br>Volatile|||
|Flesh Idol|0|4|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|Group Healer<br>Annoying|||
|Giant Tree Sap|0|2|x4|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Morsel<br>Undying||
|Mirror of Adjustment|M|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Woodcutter|||
|Shelter from the 27th of March|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Aggravating<br>Repulsive|||
|Fairy Festival|1|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bloodfiend|||
|Meat Lantern|1|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Punisher<br>Mighty Leap|||
|We can Change Anything|0|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Grinder|||
|Express Train to Hell|0|1||The Train|Rare<br>Singleton||
|Scarecrow Searching for Wisdom|1|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Bloodfiend|||
|Dimensional Refraction Variant|4|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Amorphous|||
|CENSORED|6|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Bloodfiend|Rare||
|⤷ CENSORED|X|X|||||
|Skin Prophecy|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Witness|||
|Portrait of Another World|0|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Reflector|||
|Today's Shy Look|1|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|Blue Star|0|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Fledgling(2)|Rare<br>Singleton||
|⤷ Blue Star|1|4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Assimilator<br>Omni Strike<br>Idol|Rare<br>Singleton||
|You Must be Happy|0|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Scrambler|||
|Luminous Bracelet|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Regenerator|||
|Behaviour Adjustment|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Corrector|||
|Old Faith and Promise|0|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Alchemist|||
|Porccubus|1|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Touch of Death|Poisonous||
|Void Dream|0|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Fledgling<br>Flying||Hooved|
|⤷ Void Dream|2|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Stinky<br>Flying||Hooved<br>Avian|
|Grave of Cherry Blossoms|0|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills<br>Bloodfiend||
|The Firebird|1|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Burning<br>Flying||Avian|
|Yin|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Regenerator|Singleton||
|Yang|2|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Strafe<br>Waterborne|Singleton||
|Backward Clock|0|1||Time Machine|Rare<br>Singleton||
|Il Pianto della Luna|1|7|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Group Healer|||
|Army in Pink|2|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fecundity<br>Protector|Rare||
|Ppodae|1|1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Stinky<br>Fledgling||Canine|
|⤷ Ppodae|3|2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky||Canine|
|Parasite Tree|0|3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Gardener|||
|⤷ Sapling|1|2|||||
|Melting Love|4|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Made of Slime|Rare<br>Poisonous||
|⤷ Slime|X|X||Made of Slime|||
|Honoured Monk|2|1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|||
|⤷ Clouded Monk|4|2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">||||
</details>

## 🗃️ Starter Decks
|Starter Deck|Cards|Level Unlocked|
|:-|:-|:-:|
|First Day|One Sin and Hundreds of Good Deeds<br>Fairy Festival<br>Old Lady|0|
|Road to Oz|Wolf Cub<br>Scarecrow Searching for Wisdom<br>Warm-Hearted Woodsman|3|
|Magical Girls!|Magical Girl H<br>Magical Girl D<br>Magical Girl S|9|
|The Black Forest|Punishing Bird<br>Big Bird<br>Judgement Bird|10|

## 📇 Abnormality Card Choice Node
This node only draws from the pool of obtainable mod cards, including Rares!

Each card has a Risk Level which determines the probability that they'll be selected as a card choice at this node.

The Risk Levels are, in order from least to most rare: Zayin, Teth, He, Waw, Aleph.

For each card choice there is a chance that it will instead draw from the pool of Rare mod cards, bypassing the Risk Level mechanic.

The exact probabilities for each Risk Level and for Rare cards changes as you progress, as seen below.

Note that all Aleph-level cards are Rare cards and thus aren't part of the regular pool.

|Map #|Zayin|Teth|He|Waw|Rare|
|:-:|:-:|:-:|:-:|:-:|:-:|
|1|40%|30%|20%|10%|0%|
|2|30%|30%|20%|20%|5%|
|3|25%|25%|25%|25%|10%|

## 🕓 Future Plans
Congrats on scrolling all the way down here! Below are a few things that I'll be working on in the future, or at least plan on doing.

* Further balancing, tweaks, bug fixes, and general refinement of what's currently available (feedback is welcomed and encouraged!).
* Further expansion pack of Library of Ruina abnormalities
* Challenges and Anti-Challenges (Cheats)

<!---
* Custom challenges.
* Ordeal battle node? Depends on whether ordeals are abnormalities or not, I'll have to check the _Lore_.
* Items?? Possibily convert some tool abnormalities into items (or not, I'll figure it out).
* Maybe bosses??? they can't be _that_ hard to make and implement, I'm sure. --->
