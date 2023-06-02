# Abnormal Sigils

A library of 52 unique sigils based on the abnormalities from Lobotomy Corporation and its related media.

Also includes:
- 5 custom Tribes
- 6 custom Traits
- 4 custom CardMetaCategories
- 3 custom CardAppearanceBehaviours

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1902
* API_dev-API v2.11.1

## 🔗 Compatibility

### Tribal Libary (sic)
If you have [Tribal Libary](https://inscryption.thunderstore.io/package/The_Unwanted_but_Useful_Libraries/Tribal_Libary/) installed,
my custom Tribes will be replaced with equivalents from Tribal Libary.

## 💌 Credits/Acknowledgements
Artwork and coding:
- WhistleWind

Testing and Feedback:
- Orochi Umbra

## ✨ Abilities
\*Modular means that the sigil can be found on Totem bases.

|NAME|MODULAR|STACKABLE|POWERLEVEL|DESCRIPTION|
|:-|:-:|:-:|:--|
|Punisher			|X	|X	|4	|When [creature] is struck, the striker is killed.|
|Bloodfiend			|✓	|✓	|3	|When this card deals damage, it gains 1 Health.|
|Martyr				|✓	|✓	|1	|When [creature] dies, all allied creatures gain 2 Health.|
|Aggravating		|X	|✓	|-3	|While this card is on the board, all opposing creatures gain 1 Power.|
|Team Leader		|X	|X	|5	|While this card is on the board, all allied creatures gain 1 Power.|
|Idol				|X	|✓	|5	|While this card is on the board, all opposing creatures lose 1 Power.|
|Conductor			|X	|X	|3	|Affected cards gain Power equal to half this card's Power. Over the next 3 turns: affect adjacent -> allied -> all other cards and double the Power gained.|
|Woodcutter			|X	|X	|4	|When a creature moves into the space opposite this card, they take damage equal to this card's Power.|
|Frozen Heart		|X	|X	|-1	|When [creature] dies, the killer gains 2 Health.|
|Ruler of Frost		|X	|X	|4	|Once per turn, pay 3 Bones to choose a space on the board. If the space is occupied by a killable card, transform it into a Frozen Heart. Otherwise create a Block of Ice.|
|Roots				|✓	|X	|3	|When this card is played, create Thorny Vines on adjacent empty spaces. A Thorny Vines is defined as: 0 Power, 1 Health, Sharp Quills.|
|Broodmother		|X	|X	|3	|When [creature] is struck, create a Spiderling in your hand. A spiderling is defined as: 0 Power, 1 Health, Fledgling.|
|Cursed				|✓	|X	|0	|When [creature] dies, the killer transforms into a copy of this card.|
|Healer				|X	|X	|2	|At the end of the owner's turn, they will choose one of their cards to heal by 2 Health.|
|Queen Nest			|X	|X	|4	|Whenever another card dies, create a Worker Bee in your hand. A Worker Bee is defined as: 1 Power, 1 Health.|
|Bitter Enemies		|✓	|✓	|1	|This card gains 1 Power for each other card on the board that also bears this sigil.|
|Courageous			|X	|X	|3	|Creatures adjacent to this card lose up to 2 Health. For each point of Heath lost, the affected creature gains 1 Power. This effect cannot kill cards.|
|Serpent's Nest		|X	|X	|4	|When [creature] is struck, the striker gains 1 Worms.|
|Assimilator		|X	|✓	|4	|When this card kills an opposing card, it gains 1 Power and 1 Health.|
|Group Healer		|X	|X	|4	|At the start of its owner's turn, this card will heal all allies that have taken damage by 1 Health.|
|Reflector			|✓	|X	|2	|When this card is struck, the striker is dealt damage equal to the striker's Power.|
|Flag Bearer		|X	|✓	|3	|While this card is on the board, adjacent creatures gain 2 Health.|
|Grinder			|X	|X	|3	|This card gains the stats of the creatures sacrificed to play it.|
|The Train			|X	|X	|5	|When this card is played, kill all creatures on the board. Creatures killed this way do not drop bones.|
|Scorching			|✓	|✓	|2	|The creature opposing this card takes 1 damage at the end of its owner's turn.|
|Regenerator		|✓	|✓	|3	|At the start of its owner's turn, this card heals adjacent cards by 1 Health.|
|✓olatile			|X	|X	|0	|When this card dies, adjacent and opposing cards are dealt 10 damage.|
|Gift Giver			|X	|X	|3	|When [creature] is played, create a random card in your hand.|
|Piercing			|X	|X	|2	|Damage dealt by this card cannot be negated or reduced by sigils such as Repulsive or Thick Skin.|
|Scrambler			|X	|X	|3	|When [creature] is sacrificed, give its stats to the sacrificing card then scramble its new stats. Works with Spells.|
|Gardener			|X	|X	|4	|When an allied card is killed, create a Sapling in their place. A Sapling is defined as: 0 Power, 2 Health, Bone Digger.|
|Made of Slime		|X	|X	|4	|At the start of the owner's turn, this card transforms adjacent creatures with more than 1 Health into Slimes. A Slime is defined as: 1 Power, X - 1 Health, Made of Slime.|
|Protector			|X	|✓	|3	|Creatures adjacent to this card take 1 less damage when struck.|
|Alchemist			|X	|X	|3	|Pay 2 Energy to discard your current hand and draw cards equal to the amount you discarded.
|Nettle Clothes		|X	|X	|5	|When this card is played, fill all empty spaces on the owner's side of the board with random Brothers. This card gains sigils based on allied Brothers.|
|Sporogenic			|X	|X	|2	|"Creatures adjacent to this card gain 1 Spores at the end of its owner's turn. This sigil activates before other sigils.|
|Witness			|X	|X	|1	|Pay 2 Bones to increase the selected creature's Health by 2 and their taken damage by 1. This effect stacks up to 3 times.|
|Corrector			|✓	|X	|2	|When [creature] is drawn, randomly change its stats according to its play cost. Higher costs yields higher stat totals.|
|Thick Skin			|✓	|✓	|2	|Whenever [creature] takes damage, reduce that damage by 1.|
|Opportunistic		|✓	|✓	|2	|[creature] deals 1 additional damage when striking a card that cannot attack it.|
|Copycat			|✓	|X	|2	|This gains the sigils and stats of the first card to be played in the opposing space.|
|Neutered			|X	|X	|-3	|[creature] has its Power reduced to 0. At the start of the owner's turn, remove this sigil.|
|Neutered Latch		|X	|X	|4	|Once per turn, pay [sigilcost:2 Bones] to choose a creature to gain the Neutered sigil, then increase this sigil's activation cost by 2 Bones.|
|Rightful Heir		|X	|X	|3	|Once per turn, pay [sigilcost:3 Bones] to transform a chosen creature into a Pumpkin, then increase this sigil's activation cost by 1 Bone until battle's end. A Pumpkin is defined as: 0 Power, 2 Health, Fledgling.|
|Greedy Healing		|✓	|X	|3	|At the end of its owner's turn, this card gains 2 Health. If 2 turns pass without this card taking damage, it will die.|
|False Throne		|X	|X	|4	|Once per turn, pay 2 Health to give Neutered to a chosen creature, then create a free, unaltered copy of it in your hand.|
|Return to Nihil	|X	|X	|5	|At the end of the owner's turn, deal damage to all other cards on the board equal to this card's Power.|
|Follow the Leader	|X	|X	|2	|At the end of its owner's turn, this card moves in the sigil's direction, looping around the owner's side of the board. Allied creatures towards this card in the sigil's direction as far as possible.|
|Barreler			|✓	|X	|1	|At the end of the owner's turn, this card moves in the sigil's direction to the end of the board, moving any cards in the way.|
|Cycler				|✓	|X	|1	|At the end of the owner's turn, this card moves in the sigil's direction, looping around the owner's side of the board.|

## 🛠️ Configuration Options

### Enable Mod
#### Default value: True
Enables this mod's content.

### Disable Abilities
#### Default Value: None
#### Possible Values: 
Disables abilities based on type group, preventing them from being seen in the Rulebook or obtained as totem bases.
This overrides any settings in Make Modular.

### Make Modular
#### Default Value: None
#### Possible Values: 
Forces abilities to be modular based on type group, meaning they can be found on totem bases.

## 🕓 Closing Notes

