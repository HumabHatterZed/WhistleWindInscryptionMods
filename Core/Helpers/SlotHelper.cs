using DiskCardGame;
using GBC;
using InscryptionAPI.Helpers;
using InscryptionAPI.Items;
using InscryptionAPI.Items.Extensions;
using InscryptionAPI.Saves;
using InscryptionAPI.Slots;
using System.Collections.Generic;
using UnityEngine;
using static InscryptionAPI.Slots.SlotModificationManager;

namespace WhistleWind.Core.Helpers
{
    public static class SlotHelper
    {
        public static ModificationType New<T>(
            string pluginGuid, string internalName,
            Texture2D slotTexture, Texture2D pixelTexture = null,
            Texture2D rulebookTexture = null,
            string rulebookName = null,
            string rulebookDescription = null
            )
            where T : SlotModificationBehaviour
        {
            ModificationType retval = SlotModificationManager.New(pluginGuid, internalName, typeof(T), slotTexture, pixelTexture);
            if (rulebookTexture != null)
            {
                retval.SetRulebook(rulebookName ?? internalName, rulebookDescription, rulebookTexture);
            }
            return retval;
        }

        public static Dictionary<CardTemple, Texture2D> BuildTextureDictionary(Texture2D nature, Texture2D tech = null, Texture2D undead = null, Texture2D magic = null)
        {
            return new()
            {
                { CardTemple.Nature, nature },
                { CardTemple.Undead, undead ?? nature },
                { CardTemple.Tech, tech ?? nature },
                { CardTemple.Wizard, magic ?? nature }
            };
        }

        public static void SetSlotTexture(this CardSlot slot, Info info)
        {
            if (slot is PixelCardSlot pixel)
            {
                pixel.SetSlotTexture(info);
            }
            else
            {
                slot.SetTexture(info.Texture[SaveManager.SaveFile.GetSceneAsCardTemple() ?? CardTemple.Nature]);
            }
        }

        public static SlotModificationBehaviour GetSlotBehaviour(this CardSlot slot)
        {
            return slot.GetComponent<SlotModificationBehaviour>();
        }
    }
}
