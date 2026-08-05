using DiskCardGame;
using GBC;
using InscryptionAPI.Card;
using InscryptionAPI.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils {
    public partial class AbnormalPlugin {
        private void Slot_Flooded() {
            const string rulebookName = "Flooded";
            const string rulebookDescription = "At the end of the round, deal 1 damage to the occupying card if it is not Airborne or face down, then reduce this effect's duration by 1.";

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

            Dictionary<PixelBoardSpriteSetter.BoardTheme, PixelBoardSpriteSetter.BoardThemeSpriteSet> slot_pixel_sheet = SlotModificationManager.BuildAct2SpriteSetFromSpriteSheetTexture(
                TextureLoader.LoadTextureFromFile("slotFlooded_pixel.png", Assembly));

            FloodedSlot.ID = SlotModificationManager.New(pluginGuid, "FloodedSlot", typeof(FloodedSlot), slotTextures,
                slot_pixel_sheet
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

            FloodedSlotShallow.ID = SlotModificationManager.New(pluginGuid, "FloodedSlotShallow", typeof(FloodedSlotShallow),
                slotTextures2,
                SlotModificationManager.BuildAct2SpriteSetFromSpriteSheetTexture(TextureLoader.LoadTextureFromFile("slotFlooded_pixel_2.png", Assembly))
                ).SetSharedRulebook(FloodedSlot.ID);

        }
    }

    /// <summary>
    /// At the end of the round, deal 1 damage to the occupying card if it is not Airborne or face down, then reduce this effect's duration by 1.
    /// </summary>
    public class FloodedSlot : SlotModificationBehaviour, IOpponentTurnEnd, IOnSnapshotTakenStoreInteger {
        public static SlotModificationManager.ModificationType ID { get; internal set; }
        public int Severity { get; set; }
        private int _severity = -1;
        public bool RespondsToOpponentTurnEnd(bool opponentTurnSkipped) => true;
        public IEnumerator OnOpponentTurnEnd(bool opponentTurnSkipped) {
            // use to re-set Severity when Flooding via Scenario Overseer
            // not sure why this works but whatev
            if (_severity != -1) {
                Severity = _severity;
            }
            yield return HelperMethods.ChangeCurrentView(View.Board);
            if (base.Slot.Card != null && CardIsGrounded(base.Slot.Card)) {
                yield return base.Slot.Card.TakeDamage(1, null);
            }

            Severity--;
            if (Severity < 2) {
                Debug.Log("Shallow");
                yield return base.Slot.SetSlotModification(FloodedSlotShallow.ID);
            }
        }
        public int OpponentTurnEndPriority(bool opponentTurnSkipped) => 0;

        public int RetrieveIntegerToStore() {
            return Severity;
        }

        public IEnumerator OnReceiveInteger(int value) {
            _severity = value;
            if (_severity < 2) {
                yield return base.Slot.SetSlotModification(FloodedSlotShallow.ID);
            }
        }

        public static bool CardIsGrounded(PlayableCard card) {
            return !card.FaceDown && card.LacksAbility(Ability.Flying);
        }

        public static bool SlotIsFlooded(CardSlot slot) {
            SlotModificationManager.ModificationType mod = slot.GetSlotModification();
            return mod == ID || mod == FloodedSlotShallow.ID;
        }
    }

    public class FloodedSlotShallow : FloodedSlot {
        public new static SlotModificationManager.ModificationType ID { get; internal set; }
        public override IEnumerator OnUpkeep(bool playerUpkeep) => base.Slot.ClearSlotModification();
    }
}
