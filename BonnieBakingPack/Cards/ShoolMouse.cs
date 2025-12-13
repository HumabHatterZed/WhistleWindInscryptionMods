using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateShoolMice(CardInfo mouse, CardInfo bot, CardInfo wizard, CardInfo wizard2, CardInfo wizard3) {
            // Shool Mouse
            CardInfo shool = CardManager.New(pluginPrefix, "mouseShool", "Shool Mouse", 1, 1, "A young mouse, spirited and full of potential.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("mouseShool.png"), GetTexture("mouseShool_emission.png"))
                .SetPixelPortrait(GetTexture("mouseShool_pixel.png"))
                .AddAbilities(Ability.Evolve)
                .AddTraits(Trait.Juvenile).SetEvolve(mouse, 1);

            CardInfo ghool = CardManager.New(pluginPrefixG, "mouseGhool", "Ghool Mouse", 0, 1, "A SKITTISH MOUSE, SPIRITLESS AND HOLLOW.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(3)
                .SetPortraitAndEmission(GetTexture("mouseGhool.png"), GetTexture("mouseGhool_emission.png"))
                .SetPixelPortrait(GetTexture("mouseGhool_pixel.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Skin Crawler", Ability.CorpseEater));

            CardInfo bot1 = CardManager.New(pluginPrefix3, "minorMousebot", "Litle M0U53", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("minorMousebot.png"))
                .AddTraits(Trait.Juvenile)
                .SetEvolve(bot, 1)
                .AddAbilities(ScrybeCompat.GetP03Ability("Transforms When Powered", Ability.Evolve));

            CardInfo mage = CardManager.New(pluginPrefixM, "mouseApprentice_green", "Appretice Mouse", 0, 1, "A junior wizard with great potential. A quick learner, despite its poor spelling.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetPortrait(GetTexture("mouseApprentice_green.png"))
                .AddTraits(Trait.Juvenile)
                .SetEvolve(wizard, 1, new CardModificationInfo[] { new CardModificationInfo(0, 1) })
                .AddAbilities(Ability.Evolve);

            CardInfo mage2 = CardManager.New(pluginPrefixM, "mouseApprentice_orange", "Appretice Mouse", 0, 1)
                .AddMagnificus()
                .SetPortrait(GetTexture("mouseApprentice_orange.png"))
                .AddTraits(Trait.Juvenile)
                .SetEvolve(wizard2, 1, new CardModificationInfo[] { new CardModificationInfo(0, 1) })
                .AddAbilities(Ability.Evolve);

            CardInfo mage3 = CardManager.New(pluginPrefixM, "mouseApprentice_blue", "Appretice Mouse", 0, 1)
                .AddMagnificus()
                .SetPortrait(GetTexture("mouseApprentice_blue.png"))
                .AddTraits(Trait.Juvenile)
                .SetEvolve(wizard3, 1, new CardModificationInfo[] { new CardModificationInfo(0, 1) })
                .AddAbilities(Ability.Evolve);

            if (ScrybeCompat.P03Enabled) {
                shool.AddMetaCategories(ScrybeCompat.NatureRegion);
                ghool.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot1.AddMetaCategories(ScrybeCompat.NatureRegion);
                mage.AddMetaCategories(ScrybeCompat.WizardRegion);
            }
        }
    }
}
