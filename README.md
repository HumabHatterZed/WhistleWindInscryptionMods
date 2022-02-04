# WhistleWind's Lobotomy Corp Mod
Inspired by KingSlime's wonderful mod, this is my own take on translating Lobotomy Corp's abnormalities into Inscryption.
The cards in this mod are designed to be as faithful to their original counterparts as possible, while still being 'balanced' (heavy air quotes there) relative to vanilla Inscryption.
This mod adds [card] obtainable cards, [ability] custom abilities, and [special] special abilities, with plans to add the remaining abnormalities in the future.

## Known Issues
### Debug lag
If your BepInEx console is set to output 'Debug' or 'All' log levels, you WILL experience massive lag spikes when dealing with cards with special abilities.
This includes vanilla cards such as Ants or Tentacles, as well as the Rulebook itself.

To prevent this, go to the [Logging.Console] section of your BepInEx config file, then for setting 'LogLevel' remove the value 'Debug' If you have it set to log 'All', just set to the default value..
'Debug' logging is disabled by default, so if you haven't messed with it you should be golden.


## Cards
* Standard Testing-Dummy Rabbit
* Scorched Girl
* One Sin
* Queen of Hatred
* Nothing There
* 

## Abilities
* Punisher
  * When a card bearing this sigil is struck, the striker is killed.
* Bloodfiend
  * When a card bearing this sigil deals damage to an opposing card, it gains 1 Health.
* Martyr
  * When a card bearing this sigil dies, all allied creatures gain 2 Health.
* Aggravating
  * While this card is on the board, all opposing cards gain 1 Power.
* Team Leader
  * While this card is on the board, all other ally cards gain 1 Power.
* Idol
  * While this card is on the board, all opposing cards lose 1 Power.
* Conductor
  * When this card is played, create an Ensemble in your hand. Create an additional Ensemble in your hand at the start of your next 2 turns. An Ensemble is defined as: 0 Power, 1 Health.
* Woodcutter
  * When a card moves into the space opposing this card, deal damage equal to this card's Power to it.
* Frozen Heart
  * When this card dies, the killer gains 3 Health.
* Frost Ruler
  * When this card is played, create a Frozen Heart in the opposing spaces to its left and right if they are occupied, and a Block of Ice if they are empty. A Frozen Heart is defined as: 0 Power, 1 Health. A Block of Ice is defined as: 0 Power, 3 Health.
* Roots
  * When this card is played, Vines are created on adjacent empty spaces. A Vine is defined as: 1 Power, 1 Health.
* Brood Mother
  * When a card bearing this sigil is struck, create a Spiderling in your hand. A Spiderling is defined as: 0 Power, 1 Health.
* Cursed
  * When a card bearing this sigil dies, turn the killer into a copy of this card.
* Healer
  * This card will heal a selected ally for 2 Health.
* Queen Nest
  * When a card bearing this sigil is played, a Worker Bee is created in your hand. Create an additional Worker Bee whenever another card dies.
* Bitter Enemies
  * A card bearing this sigil gains 1 Power when another card on this board also has this sigil.
* Courageous
  * Adjacent cards lose up to 2 Health but gain 1 Power for every 1 Health lost via this effect. Affected cards will not go below 1 Health.
* Serpent's Nest
  * When a card bearing this sigil is struck, a Worm is created in your hand and the striker is dealt 1 damage. A worm is defined as: 1 Power, 1 Health.
* Assimilator
  * When a card bearing this sigil kills an enemy card, this card gains 1 Power and 1 Health.
* Group Healer
  * While a card bearing this sigil is on the board, all allies regain 1 Health at the end of the opponent's turn.
* Reflector
  * When this card is struck, the striker is dealt damage equal to the striker's Power.
* Flag Bearer
  * Adjacent cards gain 2 Health.
* Grinder
  * This card gains the stats of the cards sacrificed to play it.
* The Train
  * One turn after this card is played, kill all cards on the board.
* Burning
  * The opposing card takes 1 damage at the end of their turn.
* Regenerator
  * Adjacent cards gain 1 Health at the end of the opponent's turn. (Affected cards will explode if their Health goes 2 higher than their maximum)
* Volatile
  * When this card dies, adjacent and opposing cards are dealt 10 damage.
* Gift Giver
  * When this card dies, create a random card in your hand. (Unique behaviour when held by Laetitia)
* Piercing
  * When this card strikes an opposing card, deal 1 overkill damage.

## Hidden/Special Abilities
A number of cards added by this mod possess special abilities not found in the Rulebook.
In the spirit of the original game, I won't be listing the effects here, only which cards possess a special ability. You'll need to mess with the cards to figure out what they do 🙃.

* Nameless Fetus
* Bloodbath
* Magical Girl H
* Nothing There
* Magical Girl D
* Der Freiscütz
* Crumbling Armour
* Magical Girl S
* Mountain of Smiling Bodies
* CENSORED
* Today's Shy Look

## Configuration Options
* Enable Mod
  * DEFAULT: true
  * What it says on the tin, this config enables/disables this mod's content.
* Special Abilities in Rulebook
  * DEFAULT: false
  * Adds the special abilities to the rulebook for your viewing pleasure. Disclaimer: descriptions may or may not be helpful.
* 
