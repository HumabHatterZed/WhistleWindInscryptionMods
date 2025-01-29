using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateShoolMice(CardInfo mouse, CardInfo bot)
        {
            // Shool Mouse
            CardInfo shool = CardManager.New(pluginPrefix, "mouseShool", "Shool Mouse", 1, 1, "A young mouse, spirited and full of potential.")
                .AddAct1().SetDefaultPart1Card()
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

            if (ScrybeCompat.P03Enabled)
            {
                shool.AddMetaCategories(ScrybeCompat.NatureRegion);
                ghool.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot1.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
        }
    }
}
