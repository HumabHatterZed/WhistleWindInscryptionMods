# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's mod, this is my own take on adding Lobotomy Corp's abnormalities into Inscryption.  The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay, while still being "balanced".

Features:
* **```91```** obtainable cards
* **```51```** obtainable abilities
* **```8```** starter decks for Kaycee's Mod
* **```7```** challenges and **```1```** cheat
* **```1```** custom node
* **```4```** custom bosses
* Plus a few combat events!

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1901
* API_dev-API v2.4.1
* Infiniscryption-Spell_Card_Toolkit-2.0.0

## ‼️ IMPORTANT NOTICES ‼️
The mod's configuration file has been renamed to **```wstl.inscryption.lobotomycorp.cfg```**.

Any changes made in the old config file will **NOT** carry over and must be changed in the new config file after loading the game at least once.

The old config can be safely deleted at your convenience.

<br>

**THIS MOD NOW REQUIRES INFINISCRYPTION'S SPELL CARD TOOLKIT IN ORDER TO FUNCTION!!**

Please make sure you have Spell Card Toolkit installed before playing!

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

<details>
<summary>Latest Update - v2.0.0</summary>

COPY OVER FROM FULL_CHANGELOG
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

# Stat Icons
|CARD|DESCRIPTION|
|:-|:-|
|Passing Time|The value represented by this sigil will be equal to the number of turns that have passed since this card resolved on the board.|
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
|Magical Girl H|Reacts to cards dying and to ally cards.|
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
|Magical Girl C|Reacts to adjacent cards.|
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
|Punisher|When a card bearing this sigil is struck, the striker is killed.|✔||
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|✔|✔|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|✔|✔|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.|||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.|||
|Idol|While this card is on the board, all opposing cards lose 1 Power.|||
|Conductor|While this card is on the board, adjacent cards gain 1 Power. After 1 turn on the board, all ally cards gain 1 Power instead. After 2 turns, also reduce the opposing card's Power by 1. After 3 turns, also gain Power equal to the number of cards on this side of the board.|||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.|||
|Frozen Heart|When this card dies, the killer gains 1 Health.|||
|Ruler of Frost|When this card is played, create a Block of Ice in each opposing space to the left and right of this card. If either slot is occupied by a card with 1 Health, kill it and create a Frozen Heart in its place.|||
|Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|✔||
|Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health, Fledgling.|✔||
|Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.|✔||
|Healer|This card will heal a selected ally for 2 Health.|||
|Queen Nest|While this card is on the board, create a Worker Bee in your hand when a card dies. A Worker Bee is defined as: 1 Power, 1 Health.|✔||
|Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also bears this sigil.|✔|✔|
|Courageous|If an adjacent card has more than 1 Health, it loses 1 Health and gains 1 Power. This effect can activate twice for a maximum of -2 Health and +2 Power. Stat changes persist until battle's end.|||
|Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|✔||
|Assimilator|When a card bearing this sigil attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.|✔|✔|
|Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.|||
|Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.|✔||
|Flag Bearer|While this card is on the board, adjacent cards gain 2 Health.||✔|
|Grinder|This card gains the stats of the cards sacrificed to play it.|✔||
|The Train|Activate: Pay 10 bones to kill all cards on the board, including this card. Cards killed this way do not drop bones.|||
|Cowardly|The opposing card takes 1 damage at the end of their owner's turn.|✔|✔|
|Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.|✔||
|Volatile|When this card dies, adjacent and opposing cards are dealt 10 damage.|✔||
|Gift Giver|When this card is played, create a random card in your hand.|||
|Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|✔|✔|
|Scrambler|Targeted Spell: Give the target this card's stats then scramble its stats.|||
|Gardener|When an ally card dies, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health.|||
|Made of Slime|Adjacent cards with greater than 1 Health are turned into Slimes at the start of the owner's turn. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.|||
|Marksman|You may choose which opposing space a card bearing this sigil strikes.|||
|Protector|Adjacent cards take 1 less damage from attacks.|||
|Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||✔|
|Alchemist|Activate: Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.|||
|Time Machine|Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck based on their power level.|||
|Nettle Clothes|When a card bearing this sigil is played, create a random Brother in all empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.|||
|Fungal Infector|At the end of the owner's turn, adjacent cards gain 1 Spore. Cards with Spore take damage equal to their Spore at turn's end and create a Spore Mold Creature in their slot on death. A Spore Mold Creature is defined as: [ Spore ] Power, [ Spore ] Health.|||
|Witness|Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.|||
|Corrector|A card bearing this sigil has its stats randomly changed according to its cost.|✔||
|Thick Skin|A card bearing this sigil takes 1 less damage from attacks.|✔|✔|
|One-Sided Strike|When a card bearing this sigil strikes a card, deal 1 additional damage if the struck card cannot attack this card.|✔|✔|
|Copycat|When a card bearing this sigil is played, become a copy of the opposing card if it exists. There is a chance that the copy will be imperfect.|✔||
|Cat Lover|When a card bearing this sigil is played, add a random cat card to your hand.|✔||
|Cowardly|A card bearing this sigil gains 1 Power if an ally has the Cat Lover sigil. Otherwise lose 1 Power.|||
|Neutered|A card bearing this sigil has their Power reduced to 0. This sigil is lost on upkeep.|||
|Neutered Latch|Activate: Pay 6 Bones to choose a creature to gain the Neutered sigil. This can only be used once per turn.|||
|Strengthen Target|Spells only: The affected card gains this card's stats.|||
|Imbue Target|Spells only: The affected card gains this card's sigils.|||
|Emnhance Target|Spells only: The affected card gains this card's stats and sigils.|||
|Lingering Power|Spells only: Displays the Power of this card.|||
|Lingering Health|Spells only: Displays the Health of this card.|||
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
* **Toxic** - Kills survivors when eaten at the Campfire.
* **Spell** - Can be played on top of other cards, dies upon play.
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
|The Lady Facing the Wall|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Punisher|Toxic||
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
|Snow White's Apple|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Roots|Toxic||
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
|Apocalypse Bird|3/12|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Omni Strike<br>Bifurcated Strike|Rare<br>Singleton|Avian|
|Magical Girl D|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|Singleton||
|⤷ The King of Greed|2/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Hefty|Singleton||
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
|Magical Girl S|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Protector|Rare<br>Singleton||
|⤷ The Knight of Despair|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bifurcated Strike<br>Piercing|Rare<br>Singleton||
|The Naked Nest|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Serpent's Nest|Toxic||
|⤷ Naked Worm|1/1|||||
|Mountain of Smiling Bodies|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Assimilator|Rare||
|Schadenfreude|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Quick Draw<br>Touch of Death|||
|The Heart of Aspiration|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Leader|||
|Notes from a Crazed Researcher|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flag Bearer<br>Volatile|||
|Flesh Idol|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Group Healer<br>Annoying|||
|Giant Tree Sap|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Morsel<br>Undying||
|Mirror of Adjustment|**M**/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Woodcutter|||
|Shelter from the 27th of March|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Aggravating<br>Repulsive<Imbue Target>|Spell||
|Fairy Festival|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bloodfiend|||
|Meat Lantern|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Punisher<br>Mighty Leap|||
|We can Change Anything|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Grinder|||
|Express Train to Hell|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|The Train|Rare<br>Singleton||
|Scarecrow Searching for Wisdom|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Bloodfiend|||
|Dimensional Refraction Variant|4/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Amorphous|||
|CENSORED|6/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Bloodfiend|Rare||
|⤷ CENSORED|**X**/1|**X**|**X**||**X**|
|Skin Prophecy|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Witness|||
|Portrait of Another World|0/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Reflector|||
|Today's Shy Look|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|Blue Star|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Fledgling(2)|Rare||
|⤷ Blue Star|2/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Assimilator<br>Omni Strike|Rare||
|You Must be Happy|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Lingering Health<br>Scrambler|Spell||
|Luminous Bracelet|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Regenerator|||
|Behaviour Adjustment|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Corrector|||
|Old Faith and Promise|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Alchemist|||
|Porccubus|1/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Touch of Death|Toxic||
|Void Dream|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Fledgling<br>Flying||Hooved|
|⤷ Void Dream|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Stinky||Hooved<br>Avian|
|Grave of Cherry Blossoms|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills<br>Bloodfiend||
|The Firebird|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Cowardly<br>Flying||Avian|
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
|Melting Love|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Made of Slime|Rare<br>Toxic||
|⤷ Slime|1/**X-1**|**X**|**X**<br>Made of Slime|||
|Honoured Monk|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|||
|⤷ Clouded Monk|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">||||
|Magical Girl C|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Burning|Singleton||
|⤷ Servant of Wrath|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Double Strike<br>Burning x2|Singleton|Reptile|
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
|Better Rare Chances|Raises the chance of getting a Rare card from the abnormal choice node.|-10|

## 🎰 Abnormality Card Choice Node
This special node can be found randomly after battles, and draws exclusively from this mod's pool of cards.

All obtainable cards in this mod have an internal Risk Level (Zayin, Teth, He, Waw, Aleph) based on their relative power level.  For each card choice, the game chooses a random Risk Level (based on the below probabilities) then selects a random card that has that Risk Level.

There's a low chance that instead of a regular card, you'll instead be offered a Rare card.  This is a separate probability roll that occurs after the initial roll, and it doesn't take Risk Level into account when determining which card to offer.

|Map #|Zayin|Teth|He|Waw|**Rare**|
|:-:|:-:|:-:|:-:|:-:|:-:|
|1|40%|30%|20%|10%|**0%/2%***|
|2|30%|30%|20%|20%|**2%/5%***|
|3|25%|25%|25%|25%|**5%/10%***|

* Probability with the Better Rare Chances cheat enabled

## 🕓 Closing Notes
Surprise patch! Nothing new here outside of some internal optimisation.

The next major update (1.3 or 2.0, whichever I decide best fits) won't come out for...a long time. I have a lot of things I want to add (not just new cards ;>) and a lot to learn.

See you next time, and thanks for playing my mod! <3

Current plans:
* Further balancing, tweaking, bug fixes, and general refinement of what's currently added (feedback is welcomed and encouraged!).
* Expansion pack of Library of Ruina abnormalities
