using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
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
            const string rulebookDescription = "At the start of the owner's turn, cards occupying this space are transformed into Flowers if they are close to death. A Flower is defined as: 1 Power, 1 Health, Healing Strike.";

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
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Slot.Card != null && playerUpkeep == base.Slot.IsPlayerSlot;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.Slot.Card.Health == 1 || base.Slot.Card.Health <= base.Slot.Card.MaxHealth / 3)
            {
                if (base.Slot.Card.LacksAllTraits(Trait.Terrain, AbnormalPlugin.BloomingFlower, AbnormalPlugin.ImmuneToInstaDeath, Trait.Giant))
                {
                    yield return base.Slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_flower"));
                    yield return new WaitForSeconds(0.4f);
                }
            }
        }
    }
}
