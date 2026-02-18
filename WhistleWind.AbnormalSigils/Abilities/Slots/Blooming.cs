using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Slots;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Slot_Blooming() {
            const string rulebookName = "Blooming";
            const string rulebookDescription = "At the end of the owner's turn, siphon 1 Health from the card in this space and heal the opposing card. If this space is struck directly, remove this effect.";

            Texture2D texture = TextureLoader.LoadTextureFromFile("slotBlooming_act1.png", Assembly);
            Dictionary<CardTemple, Texture2D> slotTextures = SlotHelper.BuildTextureDictionary(
                texture, texture, TextureLoader.LoadTextureFromFile("slotBlooming_grimora.png", Assembly), texture
                );

            BloomingSlot.Id = SlotModificationManager.New(pluginGuid, "BloomingSlot", typeof(BloomingSlot), slotTextures,
                SlotModificationManager.BuildAct2SpriteSetFromSpriteSheetTexture(TextureLoader.LoadTextureFromFile("slotBlooming_pixel.png", Assembly))
                ).SetRulebook(rulebookName, rulebookDescription,
                    TextureLoader.LoadTextureFromFile("slotBlooming_rulebook.png", Assembly),
                    SlotModificationManager.ModificationMetaCategory.Part1Rulebook,
                    SlotModificationManager.ModificationMetaCategory.Part3Rulebook,
                    SlotModificationManager.ModificationMetaCategory.GrimoraRulebook,
                    SlotModificationManager.ModificationMetaCategory.MagnificusRulebook)
                .SetRulebookP03Sprite(slotTextures[CardTemple.Tech])
                .SetRulebookGrimoraSprite(TextureLoader.LoadTextureFromFile("slotBlooming_rulebook_grimora.png", Assembly));
        }
    }

    /// <summary>
    /// At the end of the owner's turn, if the occupying card is injured, siphon 1 Health from the opposing card to the occupying card.
    /// </summary>
    public class BloomingSlot : SlotModificationBehaviour, IOnCardDealtDamageDirectly {
        public static SlotModificationManager.ModificationType Id;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
            => base.Slot.IsPlayerSlot == playerTurnEnd && base.Slot.Card != null;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            if (ViewManager.Instance.CurrentView != View.Board) {
                ViewManager.Instance.SwitchToView(View.Board);
            }

            yield return base.Slot.Card.Heal(-1);
            if (base.Slot.Card.Health == 0) {
                yield return base.Slot.Card.Die(false, null);
            }

            if (base.Slot.opposingSlot.Card != null && base.Slot.opposingSlot.Card.Health < base.Slot.opposingSlot.Card.MaxHealth) {
                yield return base.Slot.opposingSlot.Card.Heal(1);
            }
        }

        public bool RespondsToCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            return opposingSlot == base.Slot;
        }

        public IEnumerator OnCardDealtDamageDirectly(PlayableCard attacker, CardSlot opposingSlot, int damage) {
            yield return base.Slot.ClearSlotModification();
        }
    }
}
