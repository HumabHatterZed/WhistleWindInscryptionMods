# WhistleWind's Abnormal Sigils

A library of [number] sigils based on the abnormalities from Lobotomy Corporation and Library of Ruina. A required dependency for the 2.0 version of my Lobotomy Corporation Mod.

## ⚙️ Dependencies
* BepInEx-BepInExPack_Inscryption v5.4.1902
* API_dev-API v2.7.2

## 🔗 Compatibility

### Spell Card Toolkit
If you have [Spell Card Toolkit](https://inscryption.thunderstore.io/package/Infiniscryption/Spell_Card_Toolkit/) installed,
the abilities Scrambler, Target Gain Stats, Target Gain Sigils, and Target Gain Stats and Sigils will be altered to be compatible with Spell cards, and will function correctly when used on them.

### Tribal Libary (sic)
If you have [Tribal Libary](https://inscryption.thunderstore.io/package/Infiniscryption/Spell_Card_Toolkit/) installed,
some cards added by this mod will gain an additional tribe taken from Tribal Libary.

## 💌 Credits/Acknowledgements


## ⚖️ Changelog -- Latest Update - v1.0.0; Initial release (?/??/202?)
To see the details of the current update, please see the FULL_CHANGELOG (included with the mod file when you download it)

Alternatively, you can check out the wiki on GitHub!

## 🛠️ Configuration Options

### Enable Mod
#### Default value: True
Enables this mod's content.

### Disable Mod Abilities
#### Default Value: 0
Disables this mod's abilities by group, preventing them from appearing in the Rulebook or in-game.
Uses a flag system to determine groups:
1 - 
|NAME|DEFAULT|DESCRIPTION|
|:-|:-|:-|
|Enable Mod|TRUE|Enables this mod's content.|
|Disable Mod Abilities|0|Disables certain types of abilites, preventing them from being seen in the Rulebook or obtained as totem bases.|
|Make Mod Abilities Modular|0|Makes certain types of abilites modular, making them obtainable as totem bases.|

## ✨ Abilities

**Totem** indicates the ability can be found on totem bases and on cards from den trials by default.

|NAME|DESCRIPTION|TOTEM|
|:-|:-|:-:|
|Punisher|When a card bearing this sigil is struck, the striker is killed.|✔|
|Bloodfiend|When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.|✔|
|Martyr|When a card bearing this sigil dies, all allied creatures gain 2 Health.|✔|
|Aggravating|While this card is on the board, all opposing cards gain 1 Power.||
|Team Leader|While this card is on the board, all other ally cards gain 1 Power.||
|Idol|While this card is on the board, all opposing cards lose 1 Power.||
|Conductor|While this card is on the board, gain Power equal to the number of cards affected by this sigil's effect. Over the next 3 turns: adjacent cards gain 1 Power, ally cards gain 1 Power, opposing cards gain 1 Power.||
|Woodcutter|When a card moves into the space opposing this card, deal damage equal to this card's Power to it.||
|Frozen Heart|When this card dies the killer gains 1 Health.||
|Ruler of Frost|Once per turn, pay 3 Bones to either create a Block of Ice in a chosen empty slot, or turn a chosen card whose Health is less than or equal to this card's Power into a Frozen Heart.||
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
|Scorching|The opposing card takes 1 damage at the end of their owner's turn.|✔|
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
|Alchemist|Activate: Pay 3 Bones to discard your current hand and draw cards equal to the amount discarded.||
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

## 🕓 Closing Notes

