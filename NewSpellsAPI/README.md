# Spell Cards
This is an updated version of DivisionByZ0rro's original [Spell Card Toolkit](https://inscryption.thunderstore.io/package/Infiniscryption/Spell_Card_Toolkit/).
This mod seeks to improve upon what was left behind through bug fixes and minor content additions.

New additions and features include:
- Opponent support for Leshy playing spell cards
- The ability to cancel playing a spell card once you've selected it
- The ability to create spell cards that display their stats during battle
- Non- and global spell cards support for built-in sigils, allowing them to work properly on these kinds of cards
- 3 new sigils: Give Stats, Give Sigils, Give Stats and Sigils
- 4 new cards: Soul Without a Body, Body Without a Soul, Another's Desire, Hope

This mod is meant to **replace** the original version, not be used alongside it.
Attempting to do so will more than likely cause problems!

#

This mod contains special abilities that allow you to create a new type of card called a 'spell'. Spells are cards that:

- Do not need a space on board to resolve
- Die immediately when played.

In other words, these are cards you play entirely because they have an immediate effect.
There are also some additional sigils in this pack that might be useful for you when creating spells.

There are two types of spells:

**Targeted Spells:** These have an effect on one specific space on the board. Use this type if you want to use sigils like 'Direct Damage' (included in this pack) or something like the Beaver's dam creation ability.

**Global Spells:** These have an immediate, global effect when played. If you attach a sigil that expects to be in a specific slot on board, there may be unexpected behavior. For example, the Beaver's dam ability will more than likely give Leshy a free dam.

#

With this new version, you can also make spell cards display their stats.

By default, these kinds of cards can have their stats boosted at the campfire; you can disable this in the config.

## Credits
Original mod by DivisionByZ0rro.

This mod would not be possible without signifcant contributions from the Inscryption Modding discord channel.

Pixel icons were contributed by [Arakulele](https://inscryption.thunderstore.io/package/Arackulele/).

## Does this pack add any cards?
It can, but it doesn't by default. If you want my example cards added to the card pool, go to the config file 'zorro.infiniscryption.sigils.cfg' and set 'AddCards' to true.

This will add the following cards:

- **Kettle of Avarice**: 1 Blood, draws two cards.
- **Anger of the Gods**: 1 Blood, destroys all creatures on board (Rare).
- **Lightning**: 1 Blood, deals 2 damage to a card slot.
- **Trip to the Store**: 1 blood, generates a random consumable.
- **Rot Healing**: 1 Bone, heals for two.
- **Irritate**: 2 Bones, does one damage but increases attack by one.
- **Go Fetch**: Free to cast, generates 4 bones when cast.
- **Compost**: 3 Bones, draws two cards.
- **Soul Without a Body**: 1 Blood, gives its sigils to a target card.
- **Body Without a Soul**: 2 Bones, gives its stats to a target card.
- **Another's Desire**: 1 Blood, gives its stats and sigils to a target card.
- **Hope**: 2 Bones, gives its stats to all player cards.

These cards are not meant to be balanced, but rather to demonstrate how the mod works (hence why they are not added by default).

## Requirements
As with most mods, you need [BepInEx](https://inscryption.thunderstore.io/package/BepInEx/BepInExPack_Inscryption/) installed. 

You will also need the [API](https://inscryption.thunderstore.io/package/API_dev/API/) installed.

## I want to make a spell - how does this work?
When a spell is played, it will fire either three or four triggers (depending upon the type of spell) in this specific order.

1. PlayFromHand
2. ResolveOnBoard (if this is a targeted spell, the card's slot will be set to the slot that was targeted - otherwise, the card slot will be null)
3. SlotTargetedForAttack (only if this is a targeted spell)
4. Die

As a card developer, it is up to you to put sigils (either existing or custom) on the card to actually make it have an effect when played.

<!---->

## Target selection for targeted cards
When it comes time to select a target for a targeted card, the game will ask the card if it responds to being targeted at that card slot using the 'RespondsToSlotTargetedForAttack' override. It will only allow you to target a spell at a slot if the card says it will respond to being pointed at that slot. Additionally, if the card responds to 'ResolveOnBoard,' the game will allow you to target *any* of the player's four slots.

The sigils included in this pack are built so that the ones that do harm will respond 'false' when pointed at a player card and 'true' when pointed at an opposing card (and vice versa for sigils that are beneficial). Note that this means if you combine a positive and negative effect on the same card (for example, a spell that increases attack by one but also damages the card for one), that card will be able to target both friendly and enemy cards.

If you are adding sigils that are intended to be used on targeted spells, you need to make sure that the override bool 'RespondsToSlotTargetedForAttack' correctly identifies what slots should be targetable.

## What sigils are in this pack?
So far we have the following:

- **Draw Twice ("zorro.infiniscryption.sigils.Draw Twice")**: Draw the top card of your main deck and the top card of your side deck when this card dies.
- **Cataclysm ("zorro.infiniscryption.sigils.Cataclysm")**: Kill every card on board when this card dies.
- **Direct Damage ("zorro.infiniscryption.sigils.Direct Damage")**: Deals one damage to the targeted card slot. This ability stacks, so if you put two on a card, it will deal two damage. This only targets opponent cards.
- **Direct Healing ("zorro.infiniscryption.sigils.Direct Heal")**: Heals the targeted card for one. This can overheal. This ability stacks. This only targets player cards.
- **Attack Up ("zorro.infiniscryption.sigils.Attack Up")**: Increases the targeted card's attack by one for the rest of the battle. This only targets player cards.
- **Attack Down ("zorro.infiniscryption.sigils.Attack Down")**: Decreases the targeted card's attack by one for the rest of the battle. This only targets opponent cards.
- **Gain Control ("zorro.infiniscryption.sigils.Gain Control")**: Gains control of the targeted creature, but only if there is an empy slot for that creature to move into. Functionally similar to the fishook item.
- **Give Stats ("zorro.infiniscryption.sigils.Give Stats")**: Gives the spell card's stats to the targeted creature for the rest of the battle.
- **Give Sigils ("zorro.infiniscryption.sigils.Give Sigils")**: Gives the spell card's sigils (excluding this sigil) to the targeted creature for the rest of the battle.
- **Give Stats and Sigils ("zorro.infiniscryption.sigils.Give Stats and Sigils")**: A combination of Give Stats and Give Sigils. Does not stack with either of them.

## Split-, Tri-, and Omni Strike
These sigils do **nothing** for global spells, but behave as you would expect for targeted spells. Be careful when putting Split Strike on a targeted spell, as it will behave exactly as expected, which is not necessarily intuitive. Rather than affecting the targeted space, it will affect the spaces on either side.

The spell will trigger once for each targeted slot, **but only the SlotTargetedForAttack and ResolveOnBoard triggers will fire multiple times**. The PlayFromHand and Die triggers will only happen once.

So, for example:
- A spell with Omni Strike and Create Dams will attempt to put a Dam in every space on the board.
- A spell with Tri Strike and Direct Damage will deal one damage to the targeted space and both adjacent spaces.
- A spell with Split Strike and Explode On Death will only explode once.

Note that abilities that modify the way cards attack (custom "strike" sigils) are not supported - only Split-, Tri-, and AllStrike.

## Adding a spell through the API
The best way to add a spell using the API is to also create a reference to this mod's DLL in your project and use the custom extension method helpers "SetGlobalSpell()" or "SetTargetedSpell()" to turn a card into a spell:

```c#
using InscryptionAPI;
using Infiniscryption.Spells.Sigils;

CardManager.New("Spell_Kettle_of_Avarice", "Kettle of Avarice", 0, 0, "A jug that allows you to draw two more cards from your deck.")
           .SetDefaultPart1Card()
           .SetPortrait(AssetHelper.LoadTexture("kettle_of_avarice"))
           .SetGlobalSpell() // Makes this card into a global spell
           .SetCost(bloodCost: 1)
           .AddAbilities(DrawTwoCards.AbilityID);

CardManager.New("Spell_Lightning", "Lightning", 0, 0, "A perfectly serviceable amount of damage.")
           .SetDefaultPart1Card()
           .SetPortrait(AssetHelper.LoadTexture("lightning_bolt"))
           .SetTargetedSpell() // Makes this card into a targeted spell
           .SetCost(bloodCost: 1)
           .AddAbilities(DirectDamage.AbilityID, DirectDamage.AbilityID);
```

With the new version, you can use "SetTargetedSpellStats()" and "SetGlobalSpellStats()" to create a spell that displays its stats.

Additionally, if you want to make sure your card can NEVER be buffed, you can use "SetNeverBoostStats()" to mark your card as always being ineligible for stat buffing.

## Adding a spell through JSON Loader
To add a spell using JSON loader, you simply need to add either the global spell or the targeted spell special ability to the card:

```json
{
  "specialAbilities": [ "zorro.infiniscryption.sigils.Spell (Global)" ]
}

{
  "specialAbilities": [ "zorro.infiniscryption.sigils.Spell (Targeted)" ]
}
```

Spell cards with health or attack greater than 0 will automatically be set to show their stats during battle;
this can be disabled by setting "hideAttackAndHealth" to "true" when creating your card.

To create a spell card that can never be buffed at the campfire, you need to add this mod's special Trait:
```json
{
  "traits": [ "zorro.infiniscryption.sigils.NeverBoostStats" ]
}
```
Don't worry about adding the Stat Icon to your card; the mod will do it for you.

## A Personal Message from DivisionByZ0rro (7/18/2022)
It's been a while since you've heard from me. Life changes quickly. I got a bad case of Covid, I had family members get seriously injured, and was just generally unavailable for a while. 

Working on this and other Inscryption mods has been an amazing collaborative journey over the past months. Ever since I completed Inscryption for the first time in the fall of 2021, I spent all of my spare time (and then some) working on modding this game and being a part of an incredible community. But unfortunately, things change, and I cannot keep this up moving forward. I simply don't have the same amount of spare time that I used to, and it's time for me to move on.

I have nothing but gratitude for everyone who supported me and helped me accomplish what I have been able to accomplish. I know I'm leaving work unfinished, but I know that would be true no matter when I called it quits.

If anybody wants to continue any of my work, I hereby grant unrestricted permission for anyone to fork any of projects and make it their own moving forward. This work was always a passion project for the community, and I would be honored if anyone on the community wanted to continue that work. Please feel free to copy anything I've done and use it for yourself.

Thanks for everything,
/0
