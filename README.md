# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's own mod of this topic, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.

The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay.

This mod currently adds **71** obtainable cards and **39** custom abilities.

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.17
* API_dev-API v1.13.0+ (non-Kaycee version)
* Sigil Art Fix by Memez4Life (not required but recommended)

## 🩹 Known Issues
### Kaycee's Mod incompatibility
This mod is currently incompatible with Kaycee's Mod, so you'll have to switch to vanilla Inscryption.

### LogLevel 'Debug' causing lag
If your BepInEx console is set to output 'Debug' or 'All' log levels, you WILL experience lag spikes whenever dealing with cards with special abilities.
This includes vanilla cards such as Ants or Tentacles, as well as the Rulebook entries for said abilities.

__'Debug' logging is disabled by default, so if you haven't messed with the config you should be golden.__

To prevent this lag, go to the [Logging.Console] section of your BepInEx config file, then for setting 'LogLevel' remove the value 'Debug' if it's present'.
If you have it set to 'All', set it to something else, like the default value.

### Kopie's Rulebook Expander possibly affecting added abilities
This has not been tested, but as I understand it Rulebook Expander makes all abilities modular (can be found on totems).
Assuming this does affect this mod's abilities, it isn't game breaking, don't feel compelled to disable Rulebook Expander if you have it active.

## ⚖️ Changelog
* see CHANGELOG.md

## 💌 Credits
Shoutout to Arackulele, divisionbyz0rro, and julien-perge for having public GitHubs for me to reference back to.
I'm not an expert coder by any measure, so your work has been incredibly helpful in letting make the mod I wanted to make.

Additional shoutout to James Veug and his ReadMeMaker mod. I found it very helpful in making this thing look minimally presentable.

## 🛠️ Configuration Options
* Enable Mod
  * DEFAULT: true
  * What it says on the tin, this config enables/disables this mod's content.
* Special Abilities in Rulebook
  * DEFAULT: false
  * Adds the special abilities to the Rulebook for your viewing pleasure. NOTE: will cause laggier lag (see Known Issues).
* Reveal Select Descriptions
  * DEFAULT: false
  * Changes the description of certain abilities to actually describe what they do.
* All Modular
  * DEFAULT: false
  * Makes all custom abilities modular, meaning they can be found on totems bases and on cards from the den trial.

## ⚜️ Special Abilities
A number of cards added by this mod possess hidden, special abilities not found in the Rulebook.
In the spirit of the original game, I won't be listing the effects here, only which cards possess a special ability and what triggers it.
You'll need to mess with the cards to figure out exactly what they do 🙃.
|Card|Trigger|
|-|-|
|Nameless Fetus|Reacts to being Sacrificed|
|Bloodbath|Reacts to cards being sacrifice|
|Magical Girl H|Reacts to cards dying.|
|Nothing There|Reacts to dying.|
|Der Freiscütz|Reacts to dealing damage.|
|Crumbling Armour|Reacts to adjacent cards.|
|Magical Girl S|Reacts to adjacent cards.|
|Mountain of Smiling Bodies|Reacts to killing cards|
|CENSORED|Reacts to killing cards|
|Today's Shy Look|Reacts to being drawn.|
|Judgement Bird|Reacts to attacking cards|
|Snow Queen|Reacts to being played|
|Army in Pink|Reacts to cards dying.|

## ✨ Abilities
||Name|Description|Modular?|
|-|-|-|:-:|
||Punisher|When a card bearing this sigil is struck, the striker is killed.|Yes|
||Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|Yes|
||Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|Yes|
||Aggravating|While this card is on the board, all opposing cards gain 1 Power.||
||Team Leader|While this card is on the board, all other ally cards gain 1 Power.||
||Idol|While this card is on the board, all opposing cards lose 1 Power.||
||Conductor|When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.||
||Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.||
||Frozen Heart|When this card dies, the killer gains 1 Health.||
||Frost Ruler|When this card is played, create a Frozen Heart in the opposing spaces to its left and right if they are occupied, and a Block of Ice if they are empty. A Frozen Heart is defined as: 0 Power, 1 Health. A Block of Ice is defined as: 0 Power, 3 Health.||
||Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|Yes|
||Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.|Yes|
||Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.||
||Healer|This card will heal a selected ally for 2 Health.||
||Queen Nest|When a card bearing this sigil is played, a Worker Bee is created in your hand. Create an additional Worker Bee whenever another card dies.||
||Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also has this sigil.|Yes|
||Courageous|Adjacent cards lose up to 2 Health but gain 1 Power for every 1 Health lost via this effect. Affected cards will not go below 1 Health.||
||Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|Yes|
||Assimilator|When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.||
||Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.||
||Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.||
||Flag Bearer|Adjacent cards gain 2 Health.||
||Grinder|This card gains the stats of the cards sacrificed to play it.||
||The Train|One turn after this card is played, kill all cards on the board. If this card is not the ticket taker, kill only the card's allies at a 10% chance.||
||Burning|The opposing card takes 1 damage at the end of their turn.||
||Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.||
||Volatile|When this card dies, adjacent and opposing cards are dealt 10 damage. (identical to Detonator)|Yes|
||Gift Giver|When this card is played, create a random card in your hand.||
||Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|Yes|
||Scrambler|When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble that card's stats.|Yes|
||Gardener|When an ally card dies, create a Sapling in their place. A slot is defined as: 1 Power, 3 Health.||
||Slime|A card bearing this sigil takes 1 less damage from attacks. Additionally, cards placed adjacent to this card are turned into Slimes.||
||Marksman|You may choose which opposing space a card bearing this sigil strikes. (identical to Sniper)||
||Protector|Adjacent cards take 1 less damage from attacks.||
||Irritating|The creature opposing this card gains 1 Power.||
||Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||
||Apostle|*Thou wilt abandon flesh and be born again.*|||
||True Saviour|*My story is nowhere, unknown to all.*|||
||Confession and Pentinence|*Keep faith with unwavering resolve.*|||
# 📜 List of Obtainable and Minion Cards
<!---
<img align="center" src="https://tinyurl.com/34daekbw">
--->
|Name|Atk|HP|Cost|Sigils|Traits|Tribes|
|-|:-:|:-:|:-:|:-:|:-:|:-:|
|Standard Training-Dummy Rabbit|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png">||||
|Scorched Girl|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Volatile|||
|One Sin|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Martyr|||
|Magical Girl H|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||Rare||
|Happy Teddy Bear|3|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">|Stinky|||
|Red Shoes|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sharp Quills, Guardian|||
|Theresia|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Healer|||
|Old Lady|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Stinky|||
|Nameless Fetus|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Worthy Sacrifice, Undying|Goat||
|The Lady Facing the Wall|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Punisher|||
|Nothing There|3|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">||Rare||
|The Lady Facing the Wall|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Punisher|||
|1.76 MHz|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Leader, Irritating|||
|Singing Machine|0|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Team Leader, Aggravating|||
|The Silent Orchestra|2|5|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Conductor|Rare||
|Chairs|0|2||Leader|||
|Warm-Hearted Woodsman|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Woodcutter|||
|The Snow Queen|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Frost Ruler|||
|Frozen Heart|0|1||Frozen Heart|||
|Block of Ice|0|1|||||
|Big Bird|2|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|||Avian|
|All-Around Helper|1|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter, Bifurcated Strike|||
|Snow White's Apple|1|3|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Roots|||
|Thorny Vines|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Sharp Quills|||
|Spider Bud|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Broodmother||Insect|
|Spiderling|0|2||Fledgling||Insect|
|Beauty and the Beast|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Cursed||Hooved, Insect|
|Plague Doctor|0|3|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Airborne, Healer|||
|Don't Touch Me|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Punisher, Guardian|||
|Rudolta of the Sleigh|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter, Gift Giver||Hooved|
|Queen Bee|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Queen Nest||Insect|
|Worker Bee|1|1||||Insect|
|Bloodbath|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||Goat||
|Opened Can of WellCheers|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sprinter, Waterborne|||
|Alriune|4|5|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Sprinter||Hooved|
|Forsaken Murderer|4|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">||||
|O-01-55|||||||
|Punishing Bird|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Flying, Punisher||Avian|
|Little Red Riding Hooded Mercenary|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Marksman, Bitter Enemies|||
|Big and Will be Bad Wolf|3|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Bitter Enemies||Canine|
|You're Bald...|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Fecundity|||
|O-03-60|||||||
|Crumbling Armour|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Courageous|||
|Judgement Bird|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Marksman||Avian|
|O-02-63|||||||
|Magical Girl D|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fledgling|||
|O-04-66|||||||
|O-04-66|||||||
|Laetitia|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Gift Giver|||
|Laetitia's Friend|2|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|||Insect|
|Funeral of the Dead Butterflies|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Bifurcated Strike|Rare||
|F-02-70|||||||
|The Dreaming Current|3|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter, Hefty|||
|The Burrowing Heaven|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">||||
|Magical Girl S|2|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Protector|Rare||
|The Naked Nest|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Serpent's Nest|||
|Naked Worm|1|1|||||
|Mountain of Smiling Bodies|2|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Assimilator|Rare||
|Schadenfreude|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Quick Draw, Touch of Death|||
|The Heart of Aspiration|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Leader|||
|Notes from a Crazed Researcher|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Flag Bearer, Volatile|||
|Flesh Idol|0|4|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Group Healer, Irritating|||
|T-09-80|||||||
|Mirror of Adjustment|M|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Woodcutter|||
|Shelter from the 27th of March|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Aggravating, Repulsive|||
|Fairy Festival|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Bloodfiend|||
|Meat Lantern|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Punisher, Mighty Leap|||
|We can Change Anything|0|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Grinder|||
|Express Train to Hell|0|4|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">|The Train|Rare||
|Scarecrow Searching for Wisdom|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Bloodfiend|||
|Dimensional Refraction Variant|4|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Amorphous|||
|CENSORED|6|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png">|Bloodfiend|Rare||
|CENSORED|1|1|||||
|T-09-90|||||||
|Portrait of Another World|0|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Reflector|||
|Today's Shy Look|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||||
|Blue Star|4|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png">|Idol, Fledgling|Rare||
|You Must be Happy|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Scrambler|||
|Luminous Bracelet|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Regenerator|||
|O-09-96|||||||
|T-09-97|||||||
|Porccubus|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Touch of Death|||
|Void Dream|0|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Fledgling, Flying||Hooved|
|Grave of Cherry Blossoms|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sharp Quills, Bloodfiend||
|The Firebird|1|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Burning, Flying||Avian|
|O-05-102|||||||
|O-07-103|||||||
|D-09-104|||||||
|D-01-105|||||||
|Army in Pink|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fecundity, Protector|Rare|
|Ppodae|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Stinky, Fledgling|||
|Parasite Tree|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Gardener||
|Sapling|1|2|||||
|Melting Love|4|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Made of Slime|Rare|
|Slime|0|0||Made of Slime|||
|Honoured Monk|2|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fledgling||

## Future Plans
Hey, you made it to the bottom of this ReadMe, congrats! Below's a couple-a things I've got rattlin' around in my head.

* Add remaining abnormalities (denoted under Cards by their classification numbers)
	* Since some of the unadded cards are currently planned on using Kaycee Mod sigils, this will also coincide with this mod becoming Kaycee-compatible.
* Add custom choice node for mod cards (part of overall plan to rebalance the cards once this thing's gotten some feedback and I've gotten some rest.)
* Additional expansion pack(s) relating to the sequel game
* Another custom node?
