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
        private void Slot_Flooded()
        {
            const string rulebookName = "Flooded";
            const string rulebookDescription = "At the end of the opponent's turn, deal 1 damage to cards occupying this space, then reduce this effect's Severity by 1. Cards that are airborne or face down are unaffected.";
            
            Dictionary<CardTemple, Texture2D> slotTextures = SlotHelper.BuildTextureDictionary(
                TextureLoader.LoadTextureFromFile("slotFlooded_act1.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_act3.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_grimora.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_magnificus.png", Assembly)
                );
            
            Dictionary<CardTemple, Texture2D> slotTextures2 = SlotHelper.BuildTextureDictionary(
                TextureLoader.LoadTextureFromFile("slotFlooded_act1_2.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_act3_2.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_grimora_2.png", Assembly),
                TextureLoader.LoadTextureFromFile("slotFlooded_magnificus_2.png", Assembly)
                );

            FloodedSlot.Id = SlotModificationManager.New(pluginGuid, "FloodedSlot", typeof(FloodedSlot), slotTextures,
                SlotModificationManager.BuildAct2SpriteSetFromSpriteSheetTexture(TextureLoader.LoadTextureFromFile("slotFlooded_pixel.png", Assembly))
                )
                .SetRulebook(rulebookName, rulebookDescription,
                    TextureLoader.LoadTextureFromFile("slotFlooded_rulebook.png", Assembly),
                    SlotModificationManager.ModificationMetaCategory.Part1Rulebook,
                    SlotModificationManager.ModificationMetaCategory.Part3Rulebook,
                    SlotModificationManager.ModificationMetaCategory.GrimoraRulebook,
                    SlotModificationManager.ModificationMetaCategory.MagnificusRulebook
                    )
                .SetRulebookP03Sprite(slotTextures[CardTemple.Tech])
                .SetRulebookGrimoraSprite(TextureLoader.LoadTextureFromFile("slotFlooded_rulebook_grimora.png", Assembly));

            FloodedSlotShallow.Id = SlotModificationManager.New(pluginGuid, "FloodedSlotShallow", typeof(FloodedSlotShallow),
                slotTextures2,
                SlotModificationManager.BuildAct2SpriteSetFromSpriteSheetTexture(TextureLoader.LoadTextureFromFile("slotFlooded_pixel_2.png", Assembly))
                ).SetSharedRulebook(FloodedSlot.Id);

        }
    }

    public class FloodedSlot : SlotModificationBehaviour
    {
        public static SlotModificationManager.ModificationType Id;

        public int Severity = 1;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.Slot.Card != null && !CardIsGrounded(base.Slot.Card))
            {
                yield return base.Slot.Card.TakeDamage(1, null);
            }
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            Severity--;
            if (Severity < 2) yield return base.Slot.SetSlotModification(FloodedSlotShallow.Id);
        }
        public static bool CardIsGrounded(PlayableCard card)
        {
            return !card.FaceDown && card.LacksAbility(Ability.Flying);
        }
    }
    public class FloodedSlotShallow : SlotModificationBehaviour
    {
        public static SlotModificationManager.ModificationType Id;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => !playerTurnEnd && base.Slot.Card != null;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (!base.Slot.Card.FaceDown && base.Slot.Card.LacksAbility(Ability.Flying) && base.Slot.Card.LacksTrait(Trait.Uncuttable))
            {
                yield return base.Slot.Card.TakeDamage(1, null);
            }
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) => base.Slot.SetSlotModification(SlotModificationManager.ModificationType.NoModification);
    }
}
