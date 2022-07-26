# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's own mod of this topic, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.

The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay, while still being "balanced".

This mod currently adds all **85** abnormalities from Lobotomy Corp, as well as:
* **41** abilities (excluding event-exclusive ones)
* **15** special abilities
* **4** starter decks for Kaycee's Mod
* **1** card choice node and **1** battle node
* **Plus** a few special events!

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1901
* API_dev-API v2.4.0+

## 🩹 Known Issues
### None so far!
If you find any issues or bugs, please report them to me asap on the modding Discord or on the GitHub page!

## 💌 Credits
Shoutout to Arackulele, divisionbyz0rro, and julien-perge for having public GitHubs for me to reference back to/~~steal~~borrow from.

Special mention to James Veug's ReadmeMaker mod for providing the cost sprites I use; you're a lifesaver!

Big thanks to Rengar, yam the nokia, and everyone else for reporting bugs to me!

## ⚖️ Changelog
For the full changelog, please refer to the .md file included in the mod package.
<details>
<summary>Changelog</summary>

### v1.0.7 - Martyr bug fix (7/22/2022)
* Bug fixes
  * Fixed Martyr ability softlocking when there aren't any other valid cards
  * Melting Love can now be found as a rare card
  * Judgement Bird is now found as a common choice instead of a rare
* Tweaks
  * Changed sigil icons of activated abilities to better indicate their nature

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
</details>

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

## ✨ Abilities
**Note: 'Totem' indicates the ability can be found on totem bases and on cards from den trials.**
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
|Assimilator|When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.||
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
|Corrector|A card bearing this sigil has its stats changed based on its cost. Higher costs yield higher overall stats.|Yes|
|Apostle|*Thou wilt abandon flesh and be born again.*|||
|True Saviour|*My story is nowhere, unknown to all.*|||
|Confession and Pentinence|*Activate: Keep faith with unwavering resolve.*|||

## 📜 List of Obtainable and Minion Cards
**NOTES**<br>
**Singleton** means you can only have a single copy of that card in your deck  at a time (like Ouroboros).<br>
**Poisonous** means the card will kill survivors at the campfire when eaten.<br>
**X** means that that stat varies.

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

## Starter Decks
|Starter Deck|Cards|Level Unlocked|
|:-|:-:|:-:|
|First Day|One Sin and Hundreds of Good Deeds<br>Fairy Festival<br>Old Lady|0|
|Road to Oz|Wolf Cub<br>Scarecrow Searching for Wisdom|<br>Warm-Hearted Woodsman|3|
|Magical Girls!|Magical Girl H<br>Magical Girl D<br>Magical Girl S|9|
|The Black Forest|Punishing Bird<br>Big Bird<br>Judgement Bird|10|

## Future Plans
Congrats on scrolling all the way down here! Below are a few things that I'll be working on in the future, or at least plan on doing.

* Further balancing, tweaks, bug fixes (feedback is always welcome!).
* Expansion pack of Library of Ruina abnormalities?

<!--- * Add a custom card choice node for abnormalities, based on their Lob Corp risk level.
* Custom challenges and starter decks.
* Ordeal battle node? Depends on whether ordeals are abnormalities or not, I'll have check the _Lore_.
* Items?? Possibily convert some tool abnormalities into items (or not, I'll figure it out).
* Maybe bosses??? they can't be _that_ hard to make and implement, I'm sure. --->
