# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's mod of the same subject, this mod is my own take on translating the abnormalities of Lobotomy Corporation into Inscryption.
The cards in this mods are designed to be as faithful to their original counterparts as possible while still being 'balanced'.

Current Features:
* 94 obtainable cards
* 51 obtainable abilities
* 6 starter decks for Kaycee's Mod
* 8 challenges/cheats
* 1 custom choice node
* 4 custom bosses
* And a few combat events!

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1902
* API_dev-API v2.4.1
* Infiniscryption-Spell_Card_Toolkit-2.0.0

## ‼️ IMPORTANT NOTICE ‼️
**THIS MOD NOW REQUIRES `INFINISCRYPTION'S SPELL CARD TOOLKIT` IN ORDER TO FUNCTION!!**

Please make sure you have Spell Card Toolkit installed before playing!

## 🩹 Known Bugs and Issues
### Custom Boss Challenges
Any challenge that changes the vanilla bosses or replaces them with custom bosses is incompatibible with this mod's challenges that do the same (Abnormal Bosses, Apocalypse, etc.).

--------------------------

If you encounter any other issues or bugs, or you just want to give some feedback, you can @ me on the Inscryption Modding Discord.

## 💌 Credits/Acknowledgements
Special mentions for Arackulele, divsionbyz0rro, and julien-perge for having public GitHubs I can ~~steal~~borrow code from.

Shoutout to James Veug's ReadmeMaker mod for providing the cost sprites I use in this ReadMe; you're a lifesaver!

Thank you to Rengar, yam the nokia, and everyone on the modding Discord that's reported bugs to me!

Special thanks to Orochi Umbra for being my play tester and providing feedback during testing. I appreciate it!

## ⚖️ Changelog -- Latest Update - v2.0.0; "The One, Perfect Book" (?/??/202?)
To see the details of the current update, please see the FULL_CHANGELOG (included with the mod file when you download it)

Alternatively, you can check out the wiki on GitHub!

## 🛠️ Configuration Options

|NAME|DEFAULT|DESCRIPTION|
|:-|:-|:-|
|Enable Mod|True|Enables this mod's content.|
|No Donators|False|Prevents 7 abnormalities from being obtainable in-game (Backward Clock through Honoured Monk on the ReadMe).|
|No Ruina|False|Prevents Ruina abnormalities from being obtainable in-game.|
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
 
## ✨ Abilities

<details>
<summary>View abilities</summary>

<br>

**NOTES**
* **Totem** indicates the ability can be found on totem bases and on cards from den trials.
* **Stacks** means the ability can stack with itself, activating once for every instance of the ability.
<br>

|NAME|DESCRIPTION|TOTEM|
|:-|:-|:-:|:-:|
|Punisher|When a card bearing this sigil is struck, the striker is killed.|✔|
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|✔|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|✔|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.||
|Idol|While this card is on the board, all opposing cards lose 1 Power.||
|Conductor|While this card is on the board, adjacent cards gain 1 Power. After 1 turn on the board, all ally cards gain 1 Power instead. After 2 turns, also reduce the opposing card's Power by 1. After 3 turns, also gain Power equal to the number of cards on this side of the board.||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.||
|Frozen Heart|When this card dies the killer gains 1 Health.||
|Ruler of Frost|When this card is played, create a Block of Ice in each opposing space to the left and right of this card. If either slot is occupied by a card with 1 Health, kill it and create a Frozen Heart in its place.||
|Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|✔|
|Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health, Fledgling.|✔|
|Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.|✔|
|Healer|On turn's end, heal a selected ally for 2 Health.||
|Queen Nest|While this card is on the board, create a Worker Bee in your hand whenever another card dies. A Worker Bee is defined as: 1 Power, 1 Health.|✔|
|Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also bears this sigil.|✔|
|Courageous|If an adjacent card has more than 1 Health, it loses 1 Health and gains 1 Power. This effect can activate twice for a maximum of -2 Health and +2 Power. Stat changes persist until battle's end.||
|Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|✔|
|Assimilator|When a card bearing this sigil attacks an opposing creature and it perishes, this card gains 1 Power and 1 Health.|✔|
|Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.||
|Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.|✔|
|Flag Bearer|While this card is on the board, adjacent cards gain 2 Health.||
|Grinder|A card bearing this sigil gains the stats of the cards sacrificed to play it.|✔|
|The Train|Activate: Pay 10 bones to kill all cards on the board, including this card. Cards killed this way do not drop bones.||
|Cowardly|The opposing card takes 1 damage at the end of their owner's turn.|✔|
|Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.|✔|
|Volatile|When a card bearing this sigil dies, adjacent and opposing cards are dealt 10 damage.|✔|
|Gift Giver|When a card bearing this sigil is played, create a random card in your hand.||
|Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|✔|
|Scrambler|Targeted Spell: Give the target this card's stats then scramble its stats.||
|Gardener|When an ally card dies, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health.||
|Made of Slime|Adjacent cards with greater than 1 Health are turned into Slimes at the start of the owner's turn. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.||
|Marksman|You may choose which opposing space a card bearing this sigil strikes.||
|Protector|Adjacent cards take 1 less damage from attacks.||
|Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||
|Alchemist|Activate: Pay 3 bones to discard your current hand and draw cards equal to the number of cards discarded.||
|Time Machine|Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck based on their power level.||
|Nettle Clothes|When a card bearing this sigil is played, create a random Brother in all empty slots on the owner's side of the board. This card gains special abilities depending on what Brothers are on the board.||
|Fungal Infector|At the end of the owner's turn, adjacent cards gain 1 Spore. Cards with Spore take damage equal to their Spore at turn's end and create a Spore Mold Creature in their slot on death. A Spore Mold Creature is defined as: [ Spore ] Power, [ Spore ] Health.||
|Witness|Activate: Pay 2 bones to increase a selected card's Health by 2 and increase their taken damage by 1. This effect stacks up to 3 times per card.||
|Corrector|A card bearing this sigil has its stats randomly changed according to its cost.|✔|
|Thick Skin|A card bearing this sigil takes 1 less damage from attacks.|✔|
|One-Sided Strike|When a card bearing this sigil strikes a card, deal 1 additional damage if the struck card cannot attack this card.|✔|
|Copycat|When a card bearing this sigil is played, become a copy of the opposing card if it exists. There is a chance that the copy will be imperfect.|✔|
|Cat Lover|When a card bearing this sigil is played, add a random cat card to your hand.|✔|
|Cowardly|A card bearing this sigil gains 1 Power if an ally has the Cat Lover sigil. Otherwise lose 1 Power.||
|Neutered|A card bearing this sigil has their Power reduced to 0. This sigil is lost on upkeep.||
|Neutered Latch|Activate: Pay 6 Bones to choose a creature to gain the Neutered sigil. This can only be used once per turn.||
|Rightful Heir|Activate: Pay 5 Bones to choose a creature to be transformed into a Pumpkin. Give the transformed card the Fledgling sigil if it is an ally.||
|Greedy Healing|A card bearing this sigil gains 2 Health at the end of its turn. If 2 turns pass without this card taking damage, this card will perish.|✔|
|Strengthen Target|Spells only: The affected card gains this card's stats.||
|Imbue Target|Spells only: The affected card gains this card's sigils.||
|Enhance Target|Spells only: The affected card gains this card's stats and sigils.||
|False Throne|Once per turn, choose a creature to gain the Neutered sigil. Create a copy of the selected card in your hand with its cost reduced to 0.||
|Nihil|While this card is on the board, gain 1 Power for each empty board slot. On turn's end, deal damage to all other cards on the board equal to this card's Power.||
|Apostle|*Thou wilt abandon flesh and be born again.*||
|True Saviour|*My story is nowhere, unknown to all.*||
|Confession and Pentinence|*Activate: Keep faith with unwavering resolve.*||
</details>

## 📜 Obtainable Cards

<details>
<summary>View cards</summary>

<br>

**KEY**
* Singleton - Can only have one copy in your deck at a time.
* Toxic - Kills survivors when eaten at the Campfire.
* Spell - Can be played on top of other cards, dies upon play.
* Special - Can only be obtained once per run through a unique method.
* X - Varies
* M - Mirror
* T - Passing Time
<br>

|NAME|STATS|COST|SIGILS|TRAITS|TRIBES|
|:-|:-:|:-:|:-:|:-:|:-:|
|Standard Training-Dummy Rabbit|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/g6cUUvP.png">||||
|Scorched Girl|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Volatile|||
|One Sin and Hundreds of Good Deeds|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Martyr|||
|Magical Girl H|2/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|One-Sided Strike|Singleton||
|⤷ The Queen of Hatred|7/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Airborne|Singleton||
|Happy Teddy Bear|3/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky||Reptile|
|Red Shoes|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills, Guardian|||
|Theresia|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Healer|||
|Old Lady|1/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Stinky|||
|Nameless Fetus|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Worthy Sacrifice, Undying|Goat||
|The Lady Facing the Wall|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Punisher|Toxic||
|Nothing There|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Mighty Leap|Rare||
|1.76 MHz|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Leader, Annoying|||
|Singing Machine|0/8|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Team Leader, Aggravating|||
|The Silent Orchestra|1/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Conductor|Rare||
|Warm-Hearted Woodsman|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Woodcutter|||
|The Snow Queen|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/r1Q62Ck.png">|Frost Ruler|||
|Big Bird|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Neutered Latch|Singleton|Avian|
|All-Around Helper|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter, Bifurcated Strike|||
|Snow White's Apple|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Roots|Toxic||
|Spider Bud|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Broodmother||Insect|
|Beauty and the Beast|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Cursed||Hooved, Insect|
|Plague Doctor|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Airborne, Healer|Singleton||
|Don't Touch Me|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Punisher, Guardian|Toxic||
|Rudolta of the Sleigh|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Sprinter, Gift Giver||Hooved|
|Queen Bee|0/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Queen Nest||Insect|
|Bloodbath|0/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||Goat||
|Opened Can of WellCheers|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sprinter, Waterborne|||
|Alriune|4/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Sprinter||Hooved|
|Forsaken Murderer|4/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">||||
|Child of the Galaxy|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Flag Bearer, Bone Digger|||
|Punishing Bird|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flying, Punisher|Singleton, Toxic|Avian|
|Little Red Riding Hooded Mercenary|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman, Bitter Enemies|||
|Big and Will be Bad Wolf|3/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bitter Enemies||Canine|
|You're Bald...|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Fecundity|||
|Fragment of the Universe|1/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Piercing|||
|Crumbling Armour|0/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Courageous|||
|Judgement Bird|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman|Singleton|Avian|
|Apocalypse Bird|3/12|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Omni Strike, Bifurcated Strike|Special|Avian|
|Magical Girl D|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Fledgling|Singleton||
|⤷ The King of Greed|2/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Hefty|Singleton||
|The Little Prince|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Spores|||
|Laetitia|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Gift Giver|||
|Funeral of the Dead Butterflies|3/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Double Strike||Insect|
|Der Freischütz|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Marksman, Bifurcated Strike|Rare||
|Dream of a Black Swan|2/5|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Clothes Made of Nettles|Rare|Avian|
|The Dreaming Current|3/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Rampager|||
|The Burrowing Heaven|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Burrower, Sharp Quills|||
|Magical Girl S|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Protector|Rare, Singleton||
|⤷ The Knight of Despair|2/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bifurcated Strike, Piercing|Rare, Singleton||
|The Naked Nest|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Serpent's Nest|Toxic||
|Mountain of Smiling Bodies|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Assimilator|Rare||
|Schadenfreude|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Quick Draw, Touch of Death|||
|The Heart of Aspiration|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Leader|||
|Notes from a Crazed Researcher|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Flag Bearer|||
|Flesh Idol|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Group Healer, Annoying|||
|Giant Tree Sap|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Morsel, Undying||
|Mirror of Adjustment|**M**/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Woodcutter|||
|Shelter from the 27th of March|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Aggravating, Repulsive, Imbue Target|Spell||
|Fairy Festival|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Bloodfiend|||
|Meat Lantern|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Punisher, Mighty Leap|Toxic||
|We can Change Anything|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Grinder|||
|Express Train to Hell|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|The Train|Rare, Singleton, Terrain||
|Scarecrow Searching for Wisdom|1/3|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Bloodfiend|||
|Dimensional Refraction Variant|4/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Amorphous|||
|CENSORED|6/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Bloodfiend|Rare||
|Skin Prophecy|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Witness|||
|Portrait of Another World|0/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Reflector|||
|Today's Shy Look|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|Blue Star|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Fledgling|Rare||
|⤷ Blue Star|2/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Assimilator, Omni Strike|Rare||
|You Must be Happy|0/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Scrambler|Spell||
|Luminous Bracelet|0/0|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/jnK5NEz.png">|Greedy Healing|Spell||
|Behaviour Adjustment|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Corrector|||
|Old Faith and Promise|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Alchemist|||
|Porccubus|1/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/o1qsSmA.png">|Touch of Death|Toxic||
|Void Dream|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Fledgling, Flying||Hooved|
|⤷ Void Dream|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Stinky||Hooved, Avian|
|Grave of Cherry Blossoms|0/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Sharp Quills, Bloodfiend||
|The Firebird|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Burning, Flying||Avian|
|Yin|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Regenerator|Singleton||
|Yang|2/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Strafe, Waterborne|Singleton||
|Backward Clock|0/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Time Machine|Rare, Singleton, Terrain||
|Il Pianto della Luna|1/7|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Group Healer|||
|Army in Pink|3/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Protector, Guardian|Rare||
|Ppodae|1/1|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/iJN52Ow.png">|Stinky, Fledgling||Canine|
|⤷ Ppodae|3/2|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/cEvPoTk.png">|Stinky||Canine|
|Parasite Tree|0/3|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Gardener|||
|Melting Love|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">|Made of Slime|Rare, Toxic||
|Honoured Monk|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Fledgling|||
|⤷ Clouded Monk|4/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/nR7Ce9J.png">||||
|Magical Girl C|1/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Burning|Singleton||
|⤷ Servant of Wrath|1/4|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Double Strike, Burning x2|Singleton|Reptile|
|Price of Silence|T/T+1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">||||
|Pinocchio|0/0|<img align="center" src="https://i.imgur.com/GeMgIce.png"><img align="center" src="https://i.imgur.com/czecyiH.png">|Copycat|||
|Nosferatu|2/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Bloodfiend x2|||
|The Road Home|1/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Cat Lover|||
|Scaredy Cat|0/1|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/UENa3ep.png">|Cowardly|||
|Ozma|2/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Rightful Heir|||
|Silent Girl|2/2|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|Trifurcated Strike|Rare||
|The Adult Who Tells Lies|1/6|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/vIrzRRC.png">|False Throne|Special||
|The Jester of Nihil|0/15|<img align="center" src="https://i.imgur.com/H6vESv7.png"><img align="center" src="https://i.imgur.com/1c6PTpq.png">|Nihil|Special||
</details>

## 🗃️ Starter Decks
**Level indicates the Challenge Level at which the deck is unlocked (0 means it's always unlocked)**
|Starter Deck|Cards|Level|
|:-|:-|:-:|
|First Day|One Sin and Hundreds of Good Deeds<br>Fairy Festival<br>Old Lady|0|
|Lonely Friends|Scorched Girl<br>Laetitia<br>Child of the Galaxy|2|
|Blood Machines|We Can Do Anything<br>All-Around Helper<br>Singing Machine|4|
|Road to Oz|The Road Home<br>Scarecrow Searching for Wisdom<br>Warm-Hearted Woodsman|8|
|Magical Girls!|Magical Girl D<br>Magical Girl H<br>Magical Girl C|10|
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

All obtainable cards in this mod have an internal Risk Level (Zayin, Teth, He, Waw, Aleph) based on their relative power level.
For each card choice, the game chooses a random Risk Level (based on the below probabilities) then selects a random card that has that Risk Level.

There's a low chance that instead of a regular card, you'll instead be offered a Rare card.
This is a separate probability roll that occurs after the initial roll, and it doesn't take Risk Level into account when determining which card to offer.

|Map #|Zayin|Teth|He|Waw|**Rare**|
|:-:|:-:|:-:|:-:|:-:|:-:|
|1|40%|30%|20%|10%|**0%/2%***|
|2|30%|30%|20%|20%|**2%/5%***|
|3|25%|25%|25%|25%|**5%/10%***|

* Probability with the Better Rare Chances cheat enabled

## 🕓 Closing Notes
IT'S FINALLY OUT!!
This was a massive undertaking, but I'm very proud of how it's turned out!
My favourite part of this update were the custom bosses.
I tried to recreate their original boss fights, so they'll play a little differently than usual Inscryption bosses do.
Nevertheless, I hope you find them enjoyable to fight!

The next (and most likely final) content update will be adding the abnormalities from WonderLab.
This will only consist of new cards, so it won't be super major.

See you next time, and thanks for playing my mod! <3

Current plans:
* Further balancing, tweaking, bug fixes, and general refinement of what's currently added (feedback is welcomed and encouraged!).
