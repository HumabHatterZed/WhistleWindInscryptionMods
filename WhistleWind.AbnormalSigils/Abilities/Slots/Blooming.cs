using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Slot_Blooming()
        {
            const string rulebookName = "Blooming";
            const string rulebookDescription = "At the end of the owner's turn, siphon 1 Health from the opposing creature and heal cards occupying this space.";

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

    public class BloomingSlot : SlotModificationBehaviour
    {
        public static SlotModificationManager.ModificationType Id;

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
            => base.Slot.IsPlayerSlot == playerTurnEnd && base.Slot.opposingSlot.Card != null && base.Slot.Card != null;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            base.Slot.opposingSlot.Card.Anim.LightNegationEffect();
            base.Slot.opposingSlot.Card.HealDamage(-1);
            if (base.Slot.Card.Health < base.Slot.Card.MaxHealth)
            {
                base.Slot.Card.Anim.LightNegationEffect();
                base.Slot.Card.HealDamage(1);
            }
            else
            {
                base.Slot.Card.Anim.StrongNegationEffect();
            }

            yield return new WaitForSeconds(0.2f);
            if (base.Slot.opposingSlot.Card.Health == 0)
                yield return base.Slot.opposingSlot.Card.Die(false, null);

            yield return new WaitForSeconds(0.4f);
        }
    }
}
