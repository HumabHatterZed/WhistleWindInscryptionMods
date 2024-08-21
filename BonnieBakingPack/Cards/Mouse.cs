using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateMice()
        {
            // Mouse
            CardInfo mouse = CardManager.New(pluginPrefix, "mouse", "Mouse", 2, 4, "Just a regular, law-abiding mouse.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(2)
                .SetPortraitAndEmission(GetTexture("mouse.png"), GetTexture("mouse_emission.png"))
                .SetPixelPortrait(GetTexture("mouse_pixel.png"));

            CardManager.New(pluginPrefix, "skelemouse", "Skelemouse", 1, 1, "JUST A REGULAR SKELETON MOUSE.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(1)
                .SetPortraitAndEmission(GetTexture("skelemouse.png"), GetTexture("skelemouse_emission.png"))
                .AddAbilities(Ability.Brittle);

            CardInfo bot = CardManager.New(pluginPrefix, "mousebot", "M0U53", 2, 4)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(6)
                .SetPortrait(GetTexture("mousebot.png"));

            if (ScrybeCompat.P03Enabled)
            {
                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
            CreateShoolMice(mouse, bot);
        }
    }
}
