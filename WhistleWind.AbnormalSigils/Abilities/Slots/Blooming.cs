using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Slots;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Slot_Blooming() {
            const string rulebookName = "Blooming";
            const string rulebookDescription = "A card occupying this space has its Health raised by the number of Blooming slots on its owner's side of the board. The occupying card does not die if Blooming is removed from this space.";

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
    public class BloomingSlot : SlotModificationBehaviour, IPassiveHealthBuff {
        public static SlotModificationManager.ModificationType ID { get; internal set; }

        public int GetPassiveHealthBuff(PlayableCard target) {
            if (target == base.Slot.Card) {
                return BoardManager.Instance.GetSlotsCopy(base.Slot.IsPlayerSlot).Count(x => x.GetSlotModification() == ID);
            }
            return 0;
        }
        public override IEnumerator Cleanup(SlotModificationManager.ModificationType replacement) {
            if (base.Slot.Card != null && base.Slot.Card.Health - 1 <= 0) {
                yield return base.Slot.Card.Heal(1);
            }
        }
    }
}
