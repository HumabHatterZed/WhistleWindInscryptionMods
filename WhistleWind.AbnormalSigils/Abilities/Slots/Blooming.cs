using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Slots;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Slot_Blooming() {
            const string rulebookName = "Blooming";
            const string rulebookDescription = "A card occupying this space has its Health raised by 3. Starting next turn: at the end of the owner's turn, reduce the Health given by 1. Cards cannot be killed by this effect.";

            Texture2D texture = TextureLoader.LoadTextureFromFile("slotBlooming_act1.png", Assembly);
            Dictionary<CardTemple, Texture2D> slotTextures = SlotHelper.BuildTextureDictionary(
                texture, texture, TextureLoader.LoadTextureFromFile("slotBlooming_grimora.png", Assembly), texture
                );

            BloomingSlot.ID = SlotModificationManager.New(pluginGuid, "BloomingSlot", typeof(BloomingSlot), slotTextures,
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
    public class BloomingSlot : SlotModificationBehaviour, IPassiveHealthBuff, IOnSnapshotTakenStoreInteger {
        public static SlotModificationManager.ModificationType ID { get; internal set; }
        public int Potency { get; set; } = 3;
        private int _potency = -1;
        private bool firstTurnBuffer = true;
        public int GetPassiveHealthBuff(PlayableCard target) {
            if (target == base.Slot.Card) {
                if (_potency != -1) {
                    Potency = _potency;
                    _potency = -1;
                }
                return Potency;
            }
            return 0;
        }
        public override IEnumerator Cleanup(SlotModificationManager.ModificationType replacement) {
            if (base.Slot.Card != null && base.Slot.Card.Health - Potency <= 0) {
                yield return base.Slot.Card.Heal(1);
            }
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Slot.IsPlayerSlot == playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            if (_potency != -1) {
                Potency = _potency;
            }

            if (firstTurnBuffer) {
                firstTurnBuffer = false;
                yield break;
            }

            bool cardIsAlive = base.Slot.Card != null && base.Slot.Card.Health > 0 && !base.Slot.Card.Dead;
            Potency--;
            yield return new WaitForEndOfFrame();
            if (cardIsAlive && base.Slot.Card.Health <= 0) {
                yield return base.Slot.Card.Heal(1);
            }

            if (Potency == 0) {
                yield return base.Slot.ClearSlotModification();
            }
        }

        public int RetrieveIntegerToStore() {
            return Potency;
        }

        public IEnumerator OnReceiveInteger(int value) {
            _potency = value;
            firstTurnBuffer = false; // prevent first-turn buffer from triggering again
            yield break;
        }
    }
}
