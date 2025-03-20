using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreatePoliceWolves()
        {
            CardInfo wolf = CardManager.New(pluginPrefix, "policeWolf", "Police Wolf", 2, 2, "An officer of the law, quick to respond to any trouble.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("policeWolf.png"), GetTexture("policeWolf_emission.png"))
                .SetPixelPortrait(GetTexture("policeWolf_pixel.png"))
                .AddTribes(Tribe.Canine)
                .AddTraits(Trait.Wolf)
                .AddAbilities(Ability.CorpseEater);

            CardInfo spirit = CardManager.New(pluginPrefixG, "spiritWolf", "Spirit Wolf", 2, 1, "THE DEGENERATED SPIRIT OF A ONCE-FEARSOME PREDATOR OF THE FOREST.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(4)
                .SetPortraitAndEmission(GetTexture("spiritWolf.png"), GetTexture("spiritWolf_emission.png"))
                .AddTraits(Trait.Wolf);

            CardInfo bot = CardManager.New(pluginPrefix3, "copstable", "Cop.stable", 1, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("firewolf.png"))
                .SetPixelPortrait(GetTexture("firewolf_pixel.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Electric", Ability.DoubleStrike));

            CardInfo pig = CardManager.New(pluginPrefixM, "werewizard", "Werewizard", 2, 2, "A wizard afflicted with a terrible curse, fated to endless hunger.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetPortrait(GetTexture("werewizard.png"))
                .AddAbilities(ScrybeCompat.GetMagnificusAbility("Gem Reckoning", Ability.GemDependant));

            ScrybeCompat.SetManaCost(pig, 2);
            if (ScrybeCompat.P03Enabled)
            {
                wolf.AddMetaCategories(ScrybeCompat.NatureRegion);
                spirit.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.TechRegion);
                pig.AddMetaCategories(ScrybeCompat.WizardRegion);
            }
        }
    }
}
