using DiskCardGame;
using InscryptionAPI.Card;

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

            CardInfo skele = CardManager.New(pluginPrefixG, "skelemouse", "Skelemouse", 1, 1, "JUST A REGULAR SKELETON MOUSE.")
                .SetDefaultPart1Card().AddGrimora()
                .SetPortraitAndEmission(GetTexture("skelemouse.png"), GetTexture("skelemouse_emission.png"))
                .AddAbilities(Ability.Brittle);

            CardInfo bot = CardManager.New(pluginPrefix3, "mousebot", "M0U53", 2, 4)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(6)
                .SetPortrait(GetTexture("mousebot.png"));

            CardInfo mage1 = CardManager.New(pluginPrefixM, "mouseWizard_green", "Mouse Wizard", 1, 2, "A mouse that has learned to wield the magick of Mox.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetGemsCost(GemType.Green)
                .SetPortrait(GetTexture("mouseWizard_green.png"));

            CardInfo mage2 = CardManager.New(pluginPrefixM, "mouseWizard_orange", "Mouse Wizard", 1, 2)
                .AddMagnificus()
                .SetGemsCost(GemType.Orange)
                .SetPortrait(GetTexture("mouseWizard_orange.png"));

            CardInfo mage3 = CardManager.New(pluginPrefixM, "mouseWizard_blue", "Mouse Wizard", 1, 2)
                .AddMagnificus()
                .SetGemsCost(GemType.Blue)
                .SetPortrait(GetTexture("mouseWizard_blue.png"));

            if (ScrybeCompat.P03Enabled)
            {
                mouse.AddMetaCategories(ScrybeCompat.NatureRegion);
                skele.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
                mage3.AddMetaCategories(ScrybeCompat.WizardRegion);
            }
            CreateShoolMice(mouse, bot, mage1, mage2, mage3);
        }
    }
}
