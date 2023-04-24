# WhistleWind's Lobotomy Corp Mod

### Face the Fear. Make the Future.

Inspired by KingSlime's mod, this is my own take on translating the abnormalities of Lobotomy Corporation into Inscryption.
The cards in this mod are designed to be as faithful to their original counterparts as possible while still being "balanced".

Now featuring the abnormalities from Library of Ruina!

Current Features:
- 90 obtainable cards
- 11 starter decks
- 11 challenges and cheats
- 2 new choice nodes

## ‼️ REGARDING 2.0 ‼️
With the 2.0 release, a number of structural changes have been made you need to be aware of.

Firstly, this mod's abilities have been split off into their own mod which you will need to download as well.
This mod now also requires [New Spell Card Toolkit](https://inscryption.thunderstore.io/package/WhistleWind/New_Spell_Card_Toolkit/), so be sure to get both!

The config file has also been reformatted, so any changes you've made to it will need to be manually carried over. Sorry :(.

## 🩹 Known Bugs and Issues
### Custom Bosses
Boss challenges Abnormal Bosses, Emerald, Nihil, Apocalypse, and Rapture will override/be overridden by other mods that replace vanilla bosses.
Your game will still be functional.

--------------------------

If you encounter any issues or bugs, or you just want to give some feedback, you can find me on the Inscryption Modding Discord.

## 🔗 Compatibility

### Tribal Libary (sic)
If you have [Tribal Libary](https://inscryption.thunderstore.io/package/The_Unwanted_but_Useful_Libraries/Tribal_Libary/) installed,
my custom Tribes will be replaced with equivalents from Tribal Libary.

### Pack Management API
If you have [Pack Management API](https://inscryption.thunderstore.io/package/Infiniscryption/Pack_Management_API/) installed,
a custom card pack will be created representing this mod. That's all :).

## 💌 Credits/Acknowledgements
Artwork and coding:
- WhistleWind

Testing and Feedback:
- Orochi Umbra, Rengar, yam the nokia

ReadMe cost icons:
- James Veug (from ReadmeMaker)

Special thanks to Arackulele, divsionbyz0rro, and julien-perge for having public GitHubs I could ~~steal~~borrow code from.

## 🛠️ Configuration Options

<details>
<summary>Config</summary>

### Enable Mod
#### Default value: True
Enables this mod's content.

### Special Abilities in Rulebook
#### Default Value: False
Adds Rulebook entries for hidden abilities, describing their effect and what card possesses it.

<details>
<summary>Config.Cards</summary>

### Disable Cards
#### Default value: None
Removes cards of the specified risk level from the pool of obtainable cards.

### Disable Donators
#### Default value: False
Removes the following abnormalities from the pool of obtainable cards:
- Backward Clock, Il Pianto della Luna, Army in Pink, Ppodae, Parasite Tree, Melting Love, Honoured Monk.

### Disable Ruina
#### Default value: False
Removes the following abnormalities from the pool of obtainable cards:
- Magical Girl C, Price of Silence, Nosferatu, The Road Home, Ozma, Silent Girl.

</details>
</details>

<details>
<summary>Gameplay</summary>

### Starter Deck
#### Default value: 0
PART 1 ONLY - Replaces your starting cards with one of this mod's custom decks.
- 0 - Default Deck
- 1 - One Sin, Fairy Festival, Old Lady
- 2 - Scorched Girl, Laetitia, Child of the Galaxy
- 3 - We Can Change Anything, All-Around Helper, Singing Machine
- 4 - Nothing There, Today's Shy Look, Pinocchio / Mirror of Adjustment
- 5 - Snow White's Apple, The Snow Queen, 
- 5 - The Road Home / Snow White's Apple, Warm-Hearted-Woodsman, Wisdom Scarecrow, Ozma / The Snow Queen
- 6 - Magical Girl S, Magical Girl H, Magical D, Magical Girl C / Laetitia
- 7 - Punishing Bird, Big Bird, Judgement Bird
- 8 - 3 Random Mod Cards
- 9 - Random Mod Deck (1-7)

<details>
<summary>Gameplay.Challenges</summary>

### Abnormal Bosses
#### Default value: False
PART 1 ONLY - Bosses will only use Abnormality cards.

### Abnormal Encounters
#### Default value: False
PART 1 ONLY - All regular battles will only use Abnormality cards.

### Miracle Worker
#### Default value: False
PART 1 ONLY - Leshy will play Plague Doctor during regular battles. Beware the Clock.

</details>

<details>
<summary>Gameplay.Cheats</summary>

### Better Rare Chances
#### Default value: False
PART 1 ONLY - Raises the chance of getting a Rare card from the abnormal choice node.

</details>

<details>
<summary>Gameplay.Nodes</summary>

### Disable Events
#### Default value: False
Disables special in-game events added by this mod.

### Disable Choice Node
#### Default value: False
Prevents the abnormal card choice node from appearing.

### Disable Sefirot Node
#### Default value: False
Prevents the sefirot card choice node from appearing.

### Choice Node at Start
#### Default value: False
Each new region will have an abnormal choice node at its start.

### Sefirot Node at Start
#### Default value: False
Each new region will have a sefirot choice node at its start.

</details>
</details>

## ⚜️ Special/Hidden Abilities
A number of cards added by this mod possess special abilities that are not found in the Rulebook.

In the spirit of the original game, I won't detail the abilities, only which cards possess one and what triggers it.

You'll need to play with the cards in order to figure out their special ability does 🙃.
 
## ✨ Abilities

|NAME|DESCRIPTION|
|:---|:----------|
|Time Machine|Activate: End the current battle or phase and remove this card from the player's deck. Remove an additional card from the deck based on their power level.||
|Apostle|*Thou wilt abandon flesh and be born again.*||
|True Saviour|*My story is nowhere, unknown to all.*||
|Confession and Pentinence|*Activate: Keep faith with unwavering resolve.*||

## 📜 Obtainable Cards

<details>
<summary>View Cards</summary>

**KEY**
- Toxic - Kills survivors when eaten at the Campfire.
- Spell - Can be played on top of other cards, dies upon play.
- Special - Can only be obtained once per run, usually via a unique method.
- Singleton - Can only have one copy in your deck at a time.
- X - Stat varies
- M - Mirror stat icon
- S - Sacrifice stat icon
- T - Passing Time stat icon
- R - Sigil Power stat icon

<br>

|NAME|STATS|COST|SIGILS|TRAITS|TRIBES|
|:---|:---:|:--:|:----:|:----:|:----:|

</details>

## 🗃️ Starter Decks


## 💀 Challenges and Cheats


## 🎰 Abnormality Card Choice Node
This special node can be found randomly after battles, and draws exclusively from this mod's pool of cards.

All obtainable cards in this mod have an internal Risk Level (Zayin, Teth, He, Waw, Aleph) based on their relative power level.
For each card choice, the game chooses a random Risk Level (based on the below probabilities) then selects a random card that has that Risk Level.

There's a low chance that instead of a regular card, you'll instead be offered a Rare card.
This is a separate probability roll that occurs after the initial roll, and it doesn't take Risk Level into account when determining which card to offer.

|Map #|Zayin|Teth|He|Waw|**Rare**|
|:-:|:-:|:-:|:-:|:-:|:-:|
|1|40%|30%|20%|10%|**0%/*2%***|
|2|30%|30%|20%|20%|**2%/*5%***|
|3|25%|25%|25%|25%|**5%/*10%***|

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
