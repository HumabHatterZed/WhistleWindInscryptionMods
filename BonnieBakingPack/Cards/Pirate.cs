using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePirates()
        {
            // Pirate
            CardInfo pirate = CardManager.New(pluginPrefix, "pirate", "Pirate", 0, 1, "A scallywag with a great treasure. He reminds me of a certain someone...")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("pirate.png"), GetTexture("pirate_emission.png"))
                .SetPixelPortrait(GetTexture("pirate_pixel.png"))
                .AddAbilities(Ability.Submerge, Ability.BoneDigger);

            CardInfo plunderer = CardManager.New(pluginPrefixG, "plunderer", "Jolly Roger", 2, 2, "THERE IS NO GREATER JOY THAN THE FULFILLMENT OF GREED.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(8)
                .SetPortraitAndEmission(GetTexture("plunderer.png"), GetTexture("plunderer_emission.png"))
                .AddAbilities(Ability.Loot);

            CardInfo bot = CardManager.New(pluginPrefix3, "pirateTrojan", "Trojan", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(2)
                .SetPortrait(GetTexture("pirateTrojan.png"));

            CardInfo mage = CardManager.New(pluginPrefixM, "pirateWizard", "Pirate Sorcerer", 1, 3, "An austentatious disgrace to the name of every honest wizard.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetGemsCost(GemType.Orange, GemType.Blue)
                .SetPortrait(GetTexture("wizardPirate.png"))
                .AddAbilities(ScrybeCompat.GetMagnificusAbility("Mox Strafe", Ability.SkeletonStrafe), ScrybeCompat.GetMagnificusAbility("Made of Gold", Ability.QuadrupleBones));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Anchored", Ability.None);
                plunderer.AddAbilities(ability);
            }

            if (ScrybeCompat.P03Enabled)
            {
                pirate.AddMetaCategories(ScrybeCompat.NatureRegion);
                plunderer.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
                mage.AddMetaCategories(ScrybeCompat.WizardRegion);

                Ability ability = ScrybeCompat.GetP03Ability("Armor Giver", Ability.None);
                Ability ability2 = ScrybeCompat.GetP03Ability("Shield Absorption", Ability.None);
                bot.AddAbilities(ability, ability2);
            }
            else
            {
                bot.AddAbilities(Ability.LatchBrittle);
            }
        }
    }
}
