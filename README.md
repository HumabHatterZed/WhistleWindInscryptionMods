# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's mod, this is my own take on adding Lobotomy Corp's abnormalities into Inscryption.  The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay, while still being "balanced".

Features:
* **```84```** obtainable cards
* **```41```** obtainable abilities
* **```6```** starter decks for Kaycee's Mod
* **```3```** challenges and **```1```** cheat
* **```1```** custom node
* Plus a few combat events!

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1901
* API_dev-API v2.4.1

## ‼️ IMPORTANT NOTICE FOR ANYONE UPDATING TO v1.1.0 OR ABOVE ‼️
The mod's configuration file has been renamed to **```wstl.inscryption.lobotomycorp.cfg```**.

Any changes made in the old config file will **NOT** carry over and must be changed in the new config file after loading the game at least once.

The old config can be safely deleted at your convenience.

## 🩹 Known Bugs and Issues
### Curses Mod Challenge Incompatibility
The Boss Revenge challenge from Infiniscryption's Curses mod will override the Abnormal Boss challenge if both are activate at once (you'll stll be able to play the run).

The Abnormal Boss challenge will still work for Royal since Boss Revenge does not affect him.

--------------------------

If you encounter any other issues or bugs, or you just want to give some feedback, you can @ me on the Inscryption Modding Discord.

## 💌 Credits/Acknowledgements
Special mentions for Arackulele, divsionbyz0rro, and julien-perge for having public GitHubs I can ~~steal~~borrow code from.

Shoutout to James Veug's ReadmeMaker mod for providing the cost sprites I use in this ReadMe; you're a lifesaver!

Thank you to Rengar, yam the nokia, and everyone on the modding Discord that's reported bugs to me!

Special thanks to Orochi Umbra for being my play tester and providing feedback during testing. I appreciate it!

## ⚖️ Changelog
For a list of previous updates and a full description of the current update, refer to the FULL_CHANGELOG included in the mod package.

See Closing Notes for info on future updates.

<details>
<summary>Latest Update - v1.2.2</summary>

### v1.2.2 - The 'Who Let Me Code' patch (10/6/2022)
* 🩹 Bug fixes
  * Fixed the following cards not being obtainable as card choices
    * Judgement Bird
    * One Sin and Hundreds of Good Deeds
    * Plague Doctor
    * Yang
    * Yin
    * You're Bald...
  * Fixed ReadMe displaying incorrect cost for Theresia

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
  * Fixed visual bugs with Regenerator, Fungal Infector, and Cursed abilities
  * Fixed Magical Girl Heart's ability not working
  * Fixed 1.76 MHz's cost being wrong
  * Fixed First Brother, Second Brother, Fourth Brother incorrect stats
  * Fixed Fungal Infector not affecting cards that were affected in previous battles
  * Fixed Singing Machine not having an emission
  * Fixed Queen of Hatred and Magical Girl D not functioning as intended
  * Fixed PLague Doctor special ability not activating when on the opponent's side of the board
  * Fixed placeholder descriptions for Grave of Cherry Blossoms, The Little Prince still being present
  * Fixed Witness ability's Rulebook entry displaying an incorrect cost
* 🔧 Tweaks
  * Reworked Conductor ability to give passive buffs
  * Nettles Clothes ability now shows added abilities
  * The Naked Nest and The Naked Worm are now part of the Insect tribe
  * Tweaked Broodmother, Queen Nest, Serpent's Nest abilities to have drawn cards inherit merged sigils
  * Broodmother, Gift Giver, Queen Nest, Serpent's Nest, Corrector abilities now have opponent support
  * Tweaked sigil icons for Team Leader, Group Healer, Conductor
  * Tweaked a number of cards' descriptions to better fit the game
* ⚖️ Balancing
  * Yang event now only removes 1 card of the relevant cards at random instead of both
  * Funeral of the Dead Butterflies is no longer Rare
  * Notes from a Crazed Researcher no longer has Volatile
  * WhiteNight no longer heals taken damage
  * Rebalanced The Dreaming Current, Silent Orchestra, Chairs, Funeral of the Dead Butterflies
  * Buffed Void Dream Rooster, Singing Machine Health
  * Nerfed Silent Orchestra stats
* ➕ Additions
  * Added custom encounters
  * Added 3 Challenges and 1 Cheat
  * Added new config options
  * Added 2 new starter decks
  * Added 10 death cards
</details>

## 🛠️ Configuration Options

|NAME|DEFAULT|DESCRIPTION|
|:-|:-|:-|
|Enable Mod|True|Enables this mod's content.|
|No Donators|False|Prevents 7 abnormalities from being obtainable in-game (Backward Clock through Honoured Monk on the ReadMe)|
|Card Choice at Start|False|Adds an Abnormality choice node to the start of every region.|
|All Modular|False|Makes all non-special abilities modular.|
|Abnormal Bosses|False|Part 1 Only - Bosses will only play Abnormality cards.|
|Abnormal Encounters|False|Part 1 Only - All regular battles will only use Abnormality cards.|
|Miracle Worker|False|Part 1 Only - Leshy will play Plague Doctor during regular battles. Beware the Clock.|
|Better Rare Chances|False|Part 1 Only - Raises the chance of getting a Rare card from the abnormal choice node.|
|Special Abilities in Rulebook|False|Adds Rulebook entries for each special ability.|
|Reveal Select Descriptions|False|Changes the Rulebook entries of certain abilities to properly describe their effect.|

## ⚜️ Special/Hidden Abilities
A number of cards added by this mod possess special abilities that are not found in the Rulebook.

In the spirit of the original game, I won't detail the abilities, only which cards possess one and what triggers it.

You'll need to play with the cards in order to figure out their special ability does 🙃.
<details>
<summary>View specials</summary>
 
|CARD|TRIGGER|
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
|Army in Pink|Reacts to adjacent cards.|
|Melting Love|Reacts to taking damage.|
|Yang|Reacts to adjacent cards.|
|Giant Tree Sap|Reacts to being sacrificed.|
|Big Bird|Reacts to ally cards.|
|Plague Doctor|Reacts to ability activation.|
|Blue Star|Reacts to combat phase.| 
 </details>
 
## ✨ Abilities

<details>
<summary>View abilities</summary>

<br>

**NOTES**
* **Totem** indicates the ability can be found on totem bases and on cards from den trials.
* **Stacks** means the ability can stack with itself, activating once for every instance of the ability.
<br>

|NAME|DESCRIPTION|TOTEM|STACKS|
|:-|:-|:-:|:-:|
|Punisher|When a card bearing this sigil is struck, the striker is killed.|Yes||
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|Yes|Yes|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|Yes|Yes|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.|||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.|||
|Idol|While this card is on the board, all opposing cards lose 1 Power.|||
|Conductor|While this card is on the board, adjacent cards gain 1 Power. After 1 turn on the board, all ally cards gain 1 Power instead. After 2 turns, also reduce the opposing card's Power by 1. After 3 turns, also gain Power equal to the number of cards on this side of the board.|||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.|||
|Frozen Heart|When this card dies, the killer gains 1 Health.|||
|Ruler of Frost|When this card is played, create a Block of Ice in each opposing space to the left and right of this card. If either slot is occupied by a card with 1 Health, kill it and create a Frozen Heart in its place.|||
|Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|Yes||
|Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health, Fledgling.|Yes||
|Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.|Yes||
|Healer|This card will heal a selected ally for 2 Health.|||
|Queen Nest|While this card is on the board, create a Worker Bee in your hand when a card dies. A Worker Bee is defined as: 1 Power, 1 Health.|Yes||
|Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also bears this sigil.|Yes|Yes|
|Courageous|If an adjacent card has more than 1 Health, it loses 1 Health and gains 1 Power. This effect can activate twice for a maximum of -2 Health and +2 Power. Stat changes persist until battle's end.|||
|Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|Yes||
|Assimilator|When a card bearing this sigil attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.|Yes|Yes|
|Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.|||
|Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.|Yes||
|Flag Bearer|Adjacent cards gain 2 Health.||Yes|
|Grinder|This card gains the stats of the cards sacrificed to play it.|Yes||
|The Train|Activate: Pay 6 bones to kill all cards on the board, including this card. Cards killed this way do not drop bones.|||
|Burning|The opposing card takes 1 damage at the end of their owner's turn.|Yes|Yes|
|Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.|Yes||
|Volatile|When this card dies, adjacent and opposing cards are dealt 10 damage.|Yes||
|Gift Giver|When this card is played, create a random card in your hand.|||
|Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|Yes|Yes|
|Scrambler|When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble that card's stats.|Yes||
|Gardener|When an ally card dies, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health.|||
|Made of Slime|Adjacent cards with greater than 1 Health are turned into Slimes at the start of the owner's turn. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.|||
|Marksman|You may choose which opposing space a card bearing this sigil strikes.|||
|Protector|Adjacent cards take 1 less damage from attacks.|||
|Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||Yes|
|Alchemist|Activate: Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.|||
|Time Machine|Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck based on their power level.|||
|Nettle Clothes|When a card bearing this sigil is played, create a random Brother in all empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.|||
|Fungal Infector|At the end of the owner's turn, adjacent cards gain 1 Spore. Cards with Spore take damage equal to their Spore at turn's end and create a Spore Mold Creature in their slot on death. A Spore Mold Creature is defined as: [ Spore ] Power, [ Spore ] Health.|||
|Witness|Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.|||
|Corrector|A card bearing this sigil has its stats randomly changed according to its cost.|Yes||
|Apostle|*Thou wilt abandon flesh and be born again.*|||
|True Saviour|*My story is nowhere, unknown to all.*|||
|Confession and Pentinence|*Activate: Keep faith with unwavering resolve.*|||
</details>

## 📜 Obtainable Cards

<details>
<summary>View cards</summary>

<br>

**KEY**
* **Singleton** - Can only have one copy in your deck at a time.
* **Poisonous** - Kills survivors when eaten at the Campfire.
* **X** - Variable, typically based on another card or status effect.
* **M** - Mirror, gains Power equal to the opposing card's Power.
<br>

|NAME|STATS|COST|SIGILS|TRAITS|TRIBES|
|:-|:-:|:-:|:-:|:-:|:-:|
|Standard Training-Dummy Rabbit|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">||||
|Scorched Girl|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Volatile|||
|One Sin and Hundreds of Good Deeds|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Martyr|||
|Magical Girl H|2/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||Singleton||
|⤷ The Queen of Hatred|7/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Airborne|Singleton||
|Happy Teddy Bear|3/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky|||
|Red Shoes|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills<br>Guardian|||
|Theresia|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Healer|||
|Old Lady|1/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Stinky|||
|Nameless Fetus|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Worthy Sacrifice<br>Undying|Goat||
|The Lady Facing the Wall|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Punisher|||
|Nothing There|**X**/**X**|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">||Rare||
|1.76 MHz|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Leader<br>Annoying|||
|Singing Machine|0/8|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Team Leader<br>Aggravating|||
|The Silent Orchestra|1/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Conductor|Rare||
|Warm-Hearted Woodsman|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Woodcutter|||
|The Snow Queen|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|Frost Ruler|||
|⤷ Block of Ice|0/1|||||
|⤷ Frozen Heart|0/1||Frozen Heart|||
|Big Bird|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">||Singleton|Avian|
|All-Around Helper|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter<br>Bifurcated Strike|||
|Snow White's Apple|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Roots|Poisonous||
|⤷ Thorny Vines|0/1||**X**<br>Sharp Quills|||
|Spider Bud|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Broodmother||Insect|
|⤷ Spiderling|0/1||Fledgling||Insect|
|  ⤷ Spider Brood|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|||Insect|
|Beauty and the Beast|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Cursed||Hooved<br>Insect|
|Plague Doctor|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Airborne<br>Healer|Singleton||
|Don't Touch Me|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Punisher<br>Guardian|||
|Rudolta of the Sleigh|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter<br>Gift Giver||Hooved|
|Queen Bee|0/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Queen Nest||Insect|
|⤷ Worker Bee|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">|||Insect|
|Bloodbath|0/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||Goat||
|Opened Can of WellCheers|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sprinter<br>Waterborne|||
|Alriune|4/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Sprinter||Hooved|
|Forsaken Murderer|4/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">||||
|Child of the Galaxy|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Flag Bearer<br>Bone Digger|||
|Punishing Bird|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flying<br>Punisher|Singleton|Avian|
|Little Red Riding Hooded Mercenary|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman<br>Bitter Enemies|||
|Big and Will be Bad Wolf|3/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bitter Enemies||Canine|
|You're Bald...|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Fecundity|||
|Fragment of the Universe|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Piercing|||
|Crumbling Armour|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Courageous|||
|Judgement Bird|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman|Singleton|Avian|
|Apocalypse Bird|3/8|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Omni Strike<br>Bifurcated Strike|Rare<br>Singleton|Avian|
|Magical Girl D|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|Singleton||
|⤷ The King of Greed|4/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Hefty|Singleton||
|The Little Prince|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Spores|||
|⤷ Spore Mold Creature|**X**/**X**|**X**|**X**|||
|Laetitia|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Gift Giver|||
|⤷ Laetitia's Friend|2/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|||Insect|
|Funeral of the Dead Butterflies|3/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Double Strike|||
|Dream of a Black Swan|2/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Clothes Made of Nettles|Rare|Avian|
|⤷ First Brother|0/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Double Strike|||
|⤷ Second Brother|0/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|⤷ Third Brother|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Reflector|||
|⤷ Fourth Brother|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Touch of Death|||
|⤷ Fifth Brother|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills|||
|⤷ Sixth Brother|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Stinky|||
|The Dreaming Current|3/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Rampager|||
|The Burrowing Heaven|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Burrower>br>Sharp Quills|||
|Magical Girl S|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Protector|Rare<br>Singleton||
|⤷ The Knight of Despair|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bifurcated Strike<br>Piercing|Rare<br>Singleton||
|The Naked Nest|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Serpent's Nest|Poisonous||
|⤷ Naked Worm|1/1|||||
|Mountain of Smiling Bodies|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Assimilator|Rare||
|Schadenfreude|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Quick Draw<br>Touch of Death|||
|The Heart of Aspiration|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Leader|||
|Notes from a Crazed Researcher|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flag Bearer<br>Volatile|||
|Flesh Idol|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Group Healer<br>Annoying|||
|Giant Tree Sap|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Morsel<br>Undying||
|Mirror of Adjustment|**M**/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Woodcutter|||
|Shelter from the 27th of March|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Aggravating<br>Repulsive|||
|Fairy Festival|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bloodfiend|||
|Meat Lantern|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Punisher<br>Mighty Leap|||
|We can Change Anything|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Grinder|||
|Express Train to Hell|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|The Train|Rare<br>Singleton||
|Scarecrow Searching for Wisdom|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Bloodfiend|||
|Dimensional Refraction Variant|4/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Amorphous|||
|CENSORED|6/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Bloodfiend|Rare||
|⤷ CENSORED|**X**/1|**X**|**X**||**X**|
|Skin Prophecy|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Witness|||
|Portrait of Another World|0/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Reflector|||
|Today's Shy Look|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|Blue Star|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Fledgling(2)|Rare||
|⤷ Blue Star|2/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Assimilator<br>Omni Strike|Rare||
|You Must be Happy|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Scrambler|||
|Luminous Bracelet|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Regenerator|||
|Behaviour Adjustment|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Corrector|||
|Old Faith and Promise|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Alchemist|||
|Porccubus|1/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Touch of Death|Poisonous||
|Void Dream|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Fledgling<br>Flying||Hooved|
|⤷ Void Dream|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Stinky||Hooved<br>Avian|
|Grave of Cherry Blossoms|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills<br>Bloodfiend||
|The Firebird|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Burning<br>Flying||Avian|
|Yin|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Regenerator|Singleton||
|Yang|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Strafe<br>Waterborne|Singleton||
|Backward Clock|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Time Machine|Rare<br>Singleton||
|Il Pianto della Luna|1/7|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Group Healer|||
|Army in Pink|3/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Protector<br>Guardian|Rare||
|Army in Black|2/1||Volatile<br>Brittle|Rare||
|Ppodae|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Stinky<br>Fledgling||Canine|
|⤷ Ppodae|3/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky||Canine|
|Parasite Tree|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Gardener|||
|⤷ Sapling|0/2||**X**|||
|Melting Love|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Made of Slime|Rare<br>Poisonous||
|⤷ Slime|1/**X-1**|**X**|**X**<br>Made of Slime|||
|Honoured Monk|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|||
|⤷ Clouded Monk|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">||||
</details>

## 🗃️ Starter Decks
**Level indicates the Challenge Level at which the deck is unlocked (0 means it's always unlocked)**
|Starter Deck|Cards|Level|
|:-|:-|:-:|
|First Day|One Sin and Hundreds of Good Deeds<br>Fairy Festival<br>Old Lady|0|
|Lonely Friends|Scorched Girl<br>Laetitia<br>Child of the Galaxy|2|
|Road to Oz|Wolf Cub<br>Scarecrow Searching for Wisdom<br>Warm-Hearted Woodsman|3|
|Blood Machines|We Can Do Anything<br>All-Around Helper<br>Singing Machine|4|
|Magical Girls!|Magical Girl H<br>Magical Girl D<br>Magical Girl S|8|
|Twilight|Punishing Bird<br>Big Bird<br>Judgement Bird|13|

## 💀 Challenges and Cheats
|Challenge|Description|Points|
|:-|:-|:-:|
|Abnormal Bosses|Bosses will only play abnormality cards.|30|
|Abnormal Encounters|All regular battles will only use abnormality cards.|20|
|Miracle Worker|Leshy will play Plague Doctor during regular battles. Beware the Clock.|20|
|Better Rare Chances|Raises the chance of getting a Rare card from the abnormal choice node.|-15|

## Custom Card Choice Node
This special node can be found randomly as a card choice after battles and draws exclusively from this mod's pool of cards (including Rares!).

Cards are chosen based on an internal 'Risk Level' system that splits all cards into five categories, based on their general power level.

The chances of cards with a certain 'Risk level' appearing as a choice change as you progress in a run according to the table below.

|Map #|Zayin|Teth|He|Waw|_Rare_|
|:-:|:-:|:-:|:-:|:-:|:-:|
|1|40%|30%|20%|10%|_0%_|
|2|30%|30%|20%|20%|_4%*_|
|3|25%|25%|25%|25%|_8%*_|

_*This percentage is halved in Kaycee's Mod._

## 🕓 Closing Notes
Turns out I suck as a programmer >.<  But seriously, I don't know how I missed these cards not being obtainable, especially considering how important a few of them are for events.

In other news, work on the next major content update has started. There are some pretty substantial changes to a few existing cards, mostly to add a bit more variety in the gameplay (I had to add a new dependency to make it work, but it'll be worth it). No ETA on when it'll be done though :(.

That's all for now. As always, thanks for playing my mod! <3

Current plans:
* Further balancing, tweaking, bug fixes, and general refinement of what's currently added (feedback is welcomed and encouraged!).
* Expansion pack of Library of Ruina abnormalities
