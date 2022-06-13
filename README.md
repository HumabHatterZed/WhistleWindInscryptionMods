# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's own mod of this topic, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.

The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay, while still being "balanced".

This mod currently adds **71** obtainable cards and **38** custom abilities, plus **13** special abilities.

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1901
* API_dev-API v2.4.0+

## 🩹 Known Issues
### Passive ability buffs causing lag (Aggravating, Idol, Bitter Enemies, Team Leader, Flag Bearer)
As I understand it, the above-listed abilities cause the game to slow down when active, with this lag getting worse the more instances are on the board.
This is an issue with the API itself, and is being worked on by the team.
There is no solution I can give, so if your computer isn't the best or you just don't want to deal with it, it would be best to avoid playing cards with those sigils.
Sorry :( .

## ⚖️ Changelog
* see CHANGELOG.md

## 💌 Credits
Shoutout to Arackulele, divisionbyz0rro, and julien-perge for having public GitHubs for me to reference back to/~~steal~~borrow from.
I'm not an expert coder by any measure, so your work has been incredibly helpful in letting make the mod I wanted to make.

Additional shoutout to James Veug and his ReadMeMaker mod. I found it very helpful in making this thing look minimally presentable.

## 🛠️ Configuration Options
* Enable Mod
  * DEFAULT: true
  * What it says on the tin, this config enables/disables this mod's content.
* Special Abilities in Rulebook
  * DEFAULT: false
  * Adds the special abilities to the Rulebook for your viewing pleasure. NOTE: will cause lag (see Known Issues: LogLevel 'Debug').
* Reveal Select Descriptions
  * DEFAULT: false
  * Changes the description of certain abilities to actually describe what they do.
* All Modular
  * DEFAULT: false
  * Makes all custom abilities modular, meaning they can be found on totem bases and on cards from the den trial.

## ⚜️ Special Abilities
A number of cards added by this mod possess special behaviour not normally found in the Rulebook.
In the spirit of the original game, I won't be listing the effects here, only which cards possess a special ability and what triggers it.
You'll need to mess with the cards to figure out exactly what they do 🙃.
|Card|Trigger|
|-|-|
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

## ✨ Abilities
**Note: Modular indicates the ability can be found on totem bases or on cards from den trials.**
|Name|Description|Modular?|
|-|-|:-:|
|Punisher|When a card bearing this sigil is struck, the striker is killed.|Yes|
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|Yes|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|Yes|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.||
|Idol|While this card is on the board, all opposing cards lose 1 Power.||
|Conductor|When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.||
|Frozen Heart|When this card dies, the killer gains 1 Health.||
|Frost Ruler|When this card is played, create a Frozen Heart in the opposing spaces to its left and right if they are occupied, and a Block of Ice if they are empty. A Frozen Heart is defined as: 0 Power, 1 Health. A Block of Ice is defined as: 0 Power, 3 Health.||
|Root|When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.|Yes|
|Broodmother|When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.|Yes|
|Cursed|When a card bearing this sigil dies, turn the killer into a copy of this card.||
|Healer|This card will heal a selected ally for 2 Health.||
|Queen Nest|When a card bearing this sigil is played, a Worker Bee is created in your hand. Create an additional Worker Bee whenever another card dies.||
|Bitter Enemies|A card bearing this sigil gains 1 Power when another card on this board also bears this sigil.|Yes|
|Courageous|Adjacent cards lose up to 2 Health but gain 1 Power for every 1 Health lost via this effect. These stats persist until battle's end.||
|Serpent's Nest|When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.|Yes|
|Assimilator|When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.||
|Group Healer|While this card is on the board, all allies whose Health is below its maximum regain 1 Health at the end of the opponent's turn.||
|Reflector|When this card is struck, the striker is dealt damage equal to the striker's Power.||
|Flag Bearer|Adjacent cards gain 2 Health.||
|Grinder|This card gains the stats of the cards sacrificed to play it.||
|The Train|One turn after this card is played, kill all cards on the board. If this card is not the ticket taker, kill only the card's allies at a 10% chance.||
|Burning|The opposing card takes 1 damage at the end of their turn.||
|Regenerator|Adjacent cards gain 1 Health at the end of the opponent's turn.||
|Volatile|When this card dies, adjacent and opposing cards are dealt 10 damage. (identical to Detonator)|Yes|
|Gift Giver|When this card is played, create a random card in your hand.||
|Piercing|When this card strikes a card, deal 1 overkill damage if applicable.|Yes|
|Scrambler|When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble that card's stats.|Yes|
|Gardener|When an ally card dies, create a Sapling in their place. A slot is defined as: 1 Power, 3 Health.||
|Slime|A card bearing this sigil takes 1 less damage from attacks. Additionally, cards placed adjacent to this card are turned into Slimes.||
|Marksman|You may choose which opposing space a card bearing this sigil strikes. (identical to Sniper)||
|Protector|Adjacent cards take 1 less damage from attacks.||
|Quick Draw|When a creature moves into the space opposite this card, they take 1 damage.||
|Apostle|*Thou wilt abandon flesh and be born again.*|||
|True Saviour|*My story is nowhere, unknown to all.*|||
|Confession and Pentinence|*Keep faith with unwavering resolve.*|||
# 📜 List of Obtainable and Minion Cards
**Notes: Singleton means you can only have a single copy of that card in your deck  per run (like Ouroboros).
Poisonous means the card will kill survivors at the campfire when eaten.**

<!---
<img align="center" src="https://tinyurl.com/34daekbw">
--->
|Name|ATK|HP|Cost|Sigils|Traits|Tribes|
|-|:-:|:-:|:-:|:-:|:-:|:-:|
|Standard Training-Dummy Rabbit|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png">||||
|Scorched Girl|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Volatile|||
|One Sin|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Martyr|||
|Magical Girl H|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||Singleton||
|The Queen of Hatred|7|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Airborne|Singleton||
|Happy Teddy Bear|3|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">|Stinky|||
|Red Shoes|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sharp Quills<br>Guardian|||
|Theresia|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Healer|||
|Old Lady|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Stinky|||
|Nameless Fetus|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Worthy Sacrifice<br>Undying|Goat||
|The Lady Facing the Wall|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Punisher|||
|Nothing There|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">||Rare||
|The Lady Facing the Wall|0|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Punisher|||
|1.76 MHz|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Leader<br>Annoying|||
|Singing Machine|0|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Team Leader<br>Aggravating|||
|The Silent Orchestra|2|5|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Conductor|Rare||
|Chairs|0|2||Leader|||
|Warm-Hearted Woodsman|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Woodcutter|||
|The Snow Queen|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Frost Ruler|||
|Frozen Heart|0|1||Frozen Heart|||
|Block of Ice|0|1|||||
|Big Bird|2|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">||Singleton|Avian|
|All-Around Helper|1|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter<br>Bifurcated Strike|||
|Snow White's Apple|1|3|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Roots|||
|Thorny Vines|0|1||Sharp Quills|||
|Spider Bud|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Broodmother||Insect|
|Spiderling|0|1||Fledgling||Insect|
|^Spider Brood|1|3||||Insect|
|Beauty and the Beast|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Cursed||Hooved<br>Insect|
|Plague Doctor|0|3|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Airborne<br>Healer|Singleton||
|Don't Touch Me|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Punisher<br>Guardian|||
|Rudolta of the Sleigh|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter<br>Gift Giver||Hooved|
|Queen Bee|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Queen Nest||Insect|
|Worker Bee|1|1||||Insect|
|Bloodbath|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||Goat||
|Opened Can of WellCheers|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sprinter<br>Waterborne|||
|Alriune|4|5|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Sprinter||Hooved|
|Forsaken Murderer|4|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">||||
|O-01-55|||||||
|Punishing Bird|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Flying<br>Punisher|Singleton|Avian|
|Little Red Riding Hooded Mercenary|2|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Marksman<br>Bitter Enemies|||
|Big and Will be Bad Wolf|3|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Bitter Enemies||Canine|
|You're Bald...|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Fecundity|||
|O-03-60|||||||
|Crumbling Armour|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Courageous|||
|Judgement Bird|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Marksman|Singleton|Avian|
|O-02-63|||||||
|Magical Girl D|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fledgling|Singleton||
|^The King of Greed|4|5|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Hefty|Singleton||
|O-04-66|||||||
|O-04-66|||||||
|Laetitia|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Gift Giver|||
|Laetitia's Friend|2|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|||Insect|
|Funeral of the Dead Butterflies|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Bifurcated Strike|Rare||
|F-02-70|||||||
|The Dreaming Current|3|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Sprinter<br>Hefty|||
|The Burrowing Heaven|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">||||
|Magical Girl S|2|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Protector|Rare<br>Singleton||
|The Knight of Despair|2|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Bifurcated Strike<br>Piercing|Rare<br>Singleton||
|The Naked Nest|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Serpent's Nest|Poisonous||
|Naked Worm|1|1|||||
|Mountain of Smiling Bodies|2|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Assimilator|Rare||
|Schadenfreude|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Quick Draw<br>Touch of Death|||
|The Heart of Aspiration|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Leader|||
|Notes from a Crazed Researcher|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Flag Bearer<br>Volatile|||
|Flesh Idol|0|4|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png">|Group Healer<br>Annoying|||
|T-09-80|||||||
|Mirror of Adjustment|M|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Woodcutter|||
|Shelter from the 27th of March|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Aggravating<br>Repulsive|||
|Fairy Festival|1|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Bloodfiend|||
|Meat Lantern|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Punisher<br>Mighty Leap|||
|We can Change Anything|0|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Grinder|||
|Express Train to Hell|0|4|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">|The Train|Rare<br>Singleton||
|Scarecrow Searching for Wisdom|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Bloodfiend|||
|Dimensional Refraction Variant|4|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Amorphous|||
|CENSORED|6|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png">|Bloodfiend|Rare||
|CENSORED|1|1|||||
|T-09-90|||||||
|Portrait of Another World|0|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Reflector|||
|Today's Shy Look|1|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">||||
|Blue Star|4|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png">|Idol<br>Fledgling|Rare||
|^Blue Star|4|4|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png">|Assimilator<br>Moon Strike|Rare||
|You Must be Happy|0|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png">|Scrambler|||
|Luminous Bracelet|0|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png">|Regenerator|||
|O-09-96|||||||
|T-09-97|||||||
|Porccubus|1|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png">|Touch of Death|Poisonous||
|Void Dream|0|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Fledgling<br>Flying||Hooved|
|^Void Dream|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Stinky<br>Flying||Hooved<br>Avian|
|Grave of Cherry Blossoms|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png">|Sharp Quills<br>Bloodfiend||
|The Firebird|1|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Burning<br>Flying||Avian|
|O-05-102|||||||
|O-07-103|||||||
|D-09-104|||||||
|D-01-105|||||||
|Army in Pink|2|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fecundity<br>Protector|Rare||
|Ppodae|1|1|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png">|Stinky<br>Fledgling||Canine|
|^Ppodae|3|2|<img align="center" src="https://tinyurl.com/2p86btxk"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png">|Stinky||Canine|
|Parasite Tree|0|3|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Gardener|||
|Sapling|1|2|||||
|Melting Love|4|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">|Made of Slime|Rare<br>Poisonous||
|Slime|0|0||Made of Slime|||
|Honoured Monk|2|1|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png">|Fledgling|||
|Clouded Monk|4|2|<img align="center" src="https://tinyurl.com/5xp6vxj7"><img align="center" src="https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png">||||

## Future Plans
Hey, you made it to the bottom of this ReadMe, congrats! Below's a couple-a things I've got rattlin' around in my head. These may or may not be implemented in the future, depending on how things go.

* Add remaining abnormalities (denoted under Cards by their classification numbers). This one is _definitely_ going to happen. Definitely. Maybe.
* Further balancing, tweaking of cards and abilities.
<!--- * Add a custom card choice node for abnormalities, based on their Lob Corp risk level.
* Expansion pack of Library of Ruina abnormalities. Also maybe add the Wonderlab abnormalities, but let's not get too far ahead of ourselves.
* Custom challenges and starter decks.
* Ordeal battle node? Depends on whether ordeals are abnormalities or not, I'll have check the _Lore_.
* Items?? Possibily convert some tool abnormalities into items (or not, I'll figure it out).
* Maybe bosses??? they can't be _that_ hard to make and implement, I'm sure. --->
