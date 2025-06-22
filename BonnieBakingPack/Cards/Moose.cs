using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreateMoose() {
            // Moose
            CardInfo moose = CardManager.New(pluginPrefix, "moose", "Moose", 2, 8, "A mooseterious being with a knack for telling tales.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(3)
                .SetPortraitAndEmission(GetTexture("moose.png"), GetTexture("moose_emission.png"))
                .SetPixelPortrait(GetTexture("moose_pixel.png"))
                .AddTribes(Tribe.Hooved)
                .AddAbilities(Ability.WhackAMole);

            CardInfo moosetro = CardManager.New(pluginPrefixG, "moosetro", "Moosetro", 0, 1, "ALONGSIDE HIS SKELETAL ENTOURAGE, HE TRAVELS THE WORLD SPREADING BEAUTIFUL MOOSEIC.")
                .SetRare().AddGrimora()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("moosetro.png"), GetTexture("moosetro_emission.png"))
                .AddAbilities(ScrybeCompat.GetGrimoraAbility("Sea Shanty", Ability.BuffNeighbours), Ability.SkeletonStrafe); ;

            CardInfo bot = CardManager.New(pluginPrefix3, "digitalMhoost", "Digital Mhoost", 1, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("moosebot.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Phase Through", Ability.StrafePush));

            CardInfo mage = CardManager.New(pluginPrefixM, "mooseAlchemist", "Mooster Alchemist", 0, 3, "The power to condense magick into crystal is a rare and powerful one indeed.")
                .SetRare().AddMagnificus()
                .SetGemsCost(GemType.Green, GemType.Orange)
                .SetPortrait(GetTexture("mooseAlchemist.png"))
                .AddAbilities(ScrybeCompat.GetMagnificusAbility("Gem Absorber", Ability.BuffGems), ScrybeCompat.GetMagnificusAbility("Brewery", Ability.ExplodeGems));

            if (ScrybeCompat.P03Enabled) {
                moose.AddMetaCategories(ScrybeCompat.NatureRegion);
                moosetro.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.UndeadRegion);
                mage.AddMetaCategories(ScrybeCompat.WizardRegion);
            }
        }
    }
}
