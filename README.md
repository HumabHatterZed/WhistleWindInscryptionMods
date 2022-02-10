# WhistleWind's Lobotomy Corp Mod
Inspired by KingSlime's own mod of this topic, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.
The cards in this mod are designed to be as faithful to their original counterparts as possible within the framework of Inscryption's gameplay.
To this end, a number of custom abilities and special abilities have been diligently crafted with unique and, for some, broken effects.
Now for the numbers: this mod adds 70 obtainable (aka non-minion or transformation) cards and 37 custom abilities.

## Known Issues
### LogLevel 'Debug' causing lag
If your BepInEx console is set to output 'Debug' or 'All' log levels, you WILL experience lag spikes whenever dealing with cards with special abilities.
This includes vanilla cards such as Ants or Tentacles, as well as the Rulebook entries for said abilities.

_'Debug' logging is disabled by default, so if you haven't messed with it you should be golden._

To prevent this lag, go to the [Logging.Console] section of your BepInEx config file, then for setting 'LogLevel' remove the value 'Debug' if it's present'.
If you have it set to 'All', set it to something else, like the default value.

## Dependencies
### BepInEx-BepInExPack_Inscryption v5.4.17
### API_dev-API v1.13.0+

## Recommended non-dependencies
### Sigil Art Fix by Memez4Life OR Inscryption Unofficial Patch by UnofficialScrybePatches
Found as part of the Inscryption Unofficial Patch mod, this will fix any instances where more than 3 sigils are added to a card at once.
The sigils still work even without this, but it does look better.

## Credits
Shoutout to Arackulele, divisionbyz0rro, and julien-perge for having public GitHubs for me to draw from.
Code based on "borrowed" code from these creators is denoted by a comment within the respective file.
Additional shoutout to James Veug and his ReadMeMaker mod. I didn't use it to make this ReadMe, but I am using the images he's provided.

## Special Abilities
A number of cards added by this mod possess hidden, special abilities not found in the Rulebook.
In the spirit of the original game, I won't be listing the effects here, only which cards possess a special ability and what triggers it.
You'll need to mess with the cards to figure out exactly what they do 🙃.

* Nameless Fetus
	* Reacts to being sacrificed.
* Bloodbath
	* Reacts to cards being sacrificed.
* Magical Girl H
	* Reacts to cards dying.
* Nothing There
	* Reacts to dying.
* Magical Girl D
	* Reacts to upkeep.
* Der Freiscütz
	* Reacts to dealing damage and direct damage.
* Crumbling Armour
	* Reacts to adjacent cards.
* Magical Girl S
	* Reacts to adjacent cards.
* Mountain of Smiling Bodies
	* Reacts to killing cards.
* CENSORED
	* Reacts to killing cards.
* Today's Shy Look
	* Reacts to being drawn.
* Judgement Bird
	* Reacts to dealing damage.
* Snow Queen
	* Reacts to being played.
* Army in Pink
	* Reacts to adjacent cards.

## Abilities
Abilities marked with a * are available as totem bases.
* Punisher ☠️ *
	* When a card bearing this sigil is struck, the striker is killed.
* Bloodfiend 🩸 *
	* When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.
* Martyr ✟ *
	* When a card bearing this sigil dies, all allied creatures gain 2 Health.
* Aggravating ⏰
	* While this card is on the board, all opposing cards gain 1 Power.
* Team Leader 🐺
	* While this card is on the board, all other ally cards gain 1 Power.
* Idol 🛐
	* While this card is on the board, all opposing cards lose 1 Power.
* Conductor 🎼
	* When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.
* Woodcutter 🪓 *
	* When a card moves into the space opposing this card, deal damage equal to this card's Power to it.
* Frozen Heart ❄️
	* When this card dies, the killer gains 1 Health.
* Frost Ruler 👑
	* When this card is played, create a Frozen Heart in the opposing spaces to its left and right if they are occupied, and a Block of Ice if they are empty. A Frozen Heart is defined as: 0 Power, 1 Health. A Block of Ice is defined as: 0 Power, 3 Health.
* Roots 🌵 *
	* When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.
* Brood Mother 🕷️ *
	* When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.
* Cursed 🥀
	* When a card bearing this sigil dies, turn the killer into a copy of this card.
* Healer ❤️
	 * This card will heal a selected ally for 2 Health.
* Queen Nest 🐝 *
	* When a card bearing this sigil is played, a Worker Bee is created in your hand. Create an additional Worker Bee whenever another card dies.
* Bitter Enemies 💢 *
	* A card bearing this sigil gains 1 Power when another card on this board also has this sigil.
* Courageous ⚔️
	* Adjacent cards lose up to 2 Health but gain 1 Power for every 1 Health lost via this effect. Affected cards will not go below 1 Health.
* Serpent's Nest 🪱 *
	* When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.
* Assimilator 🖤
	* When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.
* Group Healer 💖
	* While a card bearing this sigil is on the board, all allies regain 1 Health at the end of the opponent's turn.
* Reflector 🪞
	* When this card is struck, the striker is dealt damage equal to the striker's Power.
* Flag Bearer 🚩
	* Adjacent cards gain 2 Health.
* Grinder ⚙️
	* This card gains the stats of the cards sacrificed to play it.
* The Train 🚂
	* One turn after this card is played, kill all cards on the board. If the card is not the ticket taker, only kill the card's allies at a 10% chance.
* Burning 🔥
	* The opposing card takes 1 damage at the end of their turn.
* Regenerator 💗
	* Adjacent cards gain 1 Health at the end of the opponent's turn.
* Volatile 💣 *
	* When this card dies, adjacent and opposing cards are dealt 10 damage. (identical to Detonator)
* Gift Giver 🎁
	* When this card is played, create a random card in your hand.
* Piercing 🔱 *
	* When this card strikes a card, deal 1 overkill damage if applicable.
* Scrambler 🎲 *
	* When this card is sacrificed, add its stats onto the card it was sacrificed to, then scramble the card's stats.
* Gardener 🌱
	* When an ally card dies, create a Sapling in their place. A slot is defined as: 1 Power, 3 Health.
* Slime 🦠
	* A card bearing this sigil takes 1 less damage from attacks. Additionally, cards adjacent to this card are turned into Slimes at the start of the owner's turn.
* Hunter 🎯
	* You may choose which opposing space a card bearing this sigil strikes. (identical to Sniper)
* Protector 🛡️
	* Adjacent cards take 1 less damage from attacks.

# List of Obtainable and Minion Cards
#### Standard Training-Dummy Rabbit
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png)

#### Scorched Girl
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png)

#### One Sin
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Magical Girl H
(２,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Happy Teddy Bear
(３,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png)

#### Red Shoes
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Theresia
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Old Lady
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png)

#### Nameless Fetus
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png)

#### The Lady Facing the Wall
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Nothing There
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)

#### 1.76 MHz
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Singing Machine
(０,５)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### The Silent Orchestra
(２,５)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)
##### Chairs
(１,１)

#### Warm-Hearted Woodsman
(２,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### The Snow Queen
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)
#### Frozen Heart
(０,１)
#### Block of Ice
(０,１)

#### Big Bird
(２,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### All-Around Helper
(１,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Snow White's Apple
(１,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png)
#### Thorny Vines
(０,１)

#### Spider Bud
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png)
#### Spiderling
(０,１)

#### Beauty and the Beast
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Plague Doctor
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_3.png)

#### Don't Touch Me
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Rudolta of the Sleigh
(２,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Queen Bee
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)
#### Worker Bee
(１,２)

#### Bloodbath
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Opened Can of WellCheers
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Alriune
(４,５)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)

#### Forsaken Murderer
(４,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_8.png)

#### [ O-01-55 ]

#### Punishing Bird
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Little Red Riding Hooded Mercenary
(２,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Big and Will be Bad Wolf
(３,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### You're Bald...
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png)

#### [ O-03-60 ]

#### Crumbling Armour
(０,2)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)

#### Judgement Bird
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### [ O-02-63 ]

#### Magical Girl D
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### [ O-04-66 ]
#### [ O-04-66 ]

#### Laetitia
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)
#### Little Witch's Friend
(２,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Funeral of the Dead Butterflies
(２,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Der Freiscütz
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### [ F-02-70 ]

#### The Dreaming Current
(３,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### The Burrowing Heaven
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Magical Girl S
(２,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### The Naked Nest
(０,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png)
#### Naked Worm
(１,１)

#### The Mountain of Smiling Bodies
(２,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)

#### Schadenfreude
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_4.png)

#### The Heart of Aspiration
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Notes from a Crazed Researcher
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Flesh Idol
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)

#### [ T-09-80 ]

#### Mirror of Adjustment
(０,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Shelter from the 27th of March
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)

#### Fairy Festival
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Meat Lantern
(１,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### We can Change Anything
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Express Train to Hell
(０,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_1.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_0.png)

#### Scarecrow Searching for Wisdom
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png)

#### Dimensional Refraction Variant
(４,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)

#### CENSORED
(６,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png)
#### (CENSORED)
(１,１)

#### [ T-09-90 ]

#### [ O-09-91 ]

#### Today's Shy Look
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Blue Star
(０,４)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_4.png)

#### You Must Be Happy
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png)

#### Luminous Bracelet
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_5.png)

#### [ O-09-96 ]

#### [ T-09-97 ]

#### Porccubus
(１,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)

#### Void Dream
(０,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_1.png)

#### Grave of Cherry Blossoms
(０,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_2.png)

#### The Firebird
(１,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### [ O-05-102 ]

#### [ O-07-103 ]

#### [ O-07-103 ]

#### [ D-09-104 ]

#### [ D-01-105 ]

#### Army in Pink
(２,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

#### Ppodae
(１,１)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_bone_6.png)

#### Parasite Tree
(０,３)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)
#### Sapling
(１,２)

#### Melting Love
(４,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_3.png)
#### Slime
(１,１)

#### Honoured Monk
(２,２)
![cost_icon](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood.png)![cost](https://raw.githubusercontent.com/JamesVeug/InscyptionReadmeMaker/main/Artwork/Git/cost_blood_2.png)

## Configuration Options
* Enable Mod
  * DEFAULT: true
  * What it says on the tin, this config enables/disables this mod's content.
* Special Abilities in Rulebook
  * DEFAULT: false
  * Adds the special abilities to the rulebook. NOTE: will cause lag (see Known Issues).
* Reveal Select Descriptions
  * DEFAULT: false
  * Changes the description of certain abilities to actually describe what they do.

## Changelog
### v0.44.69.113
Initial release
* Added initial batch of 70 cards

## Future Plans
* Add remaining abnormalities (denoted under Cards by their classification numbers)
* Add custom choice node for mod cards (part of overall plan to rebalance some things once this thing's gotten some feedback)
* Additional expansion pack(s) relating to the sequel game
* Another custom node?
