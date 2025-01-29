using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateCats()
        {
            // Cat
            CardInfo cat = CardManager.New(pluginPrefix, "cat", "Cat", 1, 1, "A refined cat with elite tastes, in more ways than one.")
                .SetDefaultPart1Card().AddAct1()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("cat.png"), GetTexture("cat_emission.png"))
                .SetPixelPortrait(GetTexture("cat_pixel.png"))
                .AddAbilities(Ability.Morsel);

            CardInfo nine = CardManager.New(pluginPrefixG, "nine", "Nine", 1, 1, "A REFINED FOX WAITING PATIENTLY FOR ITS TIME TO COME.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetBonesCost(2)
                .SetPortraitAndEmission(GetTexture("nine.png"), GetTexture("nine_emission.png"))
                .AddAbilities(Ability.DrawCopyOnDeath)
                .AddSpecialAbilities(NineAbility.SpecialAbility);

            CardInfo bot = CardManager.New(pluginPrefix3, "felinebot", "F4T C4T", 0, 2)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("felinebot.png"))
                .AddAbilities(Ability.ConduitNull, ScrybeCompat.GetP03RunAbility("Mine Cryptocurrency", Ability.CreateBells));

            if (ScrybeCompat.P03Enabled)
            {
                cat.AddMetaCategories(ScrybeCompat.NatureRegion);
                nine.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.TechRegion);
            }
        }
    }
}
