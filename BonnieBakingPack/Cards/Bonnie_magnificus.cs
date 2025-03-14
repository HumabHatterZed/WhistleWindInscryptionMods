using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBonnieMagnificus()
        {
            CardInfo bon = CardManager.New(pluginPrefixM, "bonnie", "Cake Witch", 1, 1, "A young witch that conjures bejeweled confections.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetPortrait(GetTexture("bonnie_magnificus.png"))
                .AddAbilities(FreshFoodMagnificus.ability) // activated don't work?
                .SetOnePerDeck();

            CardInfo bon2 = CardManager.New(pluginPrefixM, "bunnie", "Rabid Rabbit", 2, 2, "Magick flows through the veins of all things living. To wield this purest of living arcana is to become a beast.")
                .AddMagnificus()
                .SetPortrait(GetTexture("bunnie_magnificus.png"))
                .AddAbilities(FreshIngredientsMagnificus.Id)
                .AddSpecialAbilities(BunnieAttackAbility.SpecialAbility)
                .SetOnePerDeck();

            if (ScrybeCompat.P03Enabled)
            {
                bon.AddMetaCategories(ScrybeCompat.WizardRegion);
            }

            ScrybeCompat.SetManaCost(bon, 1);
            ScrybeCompat.SetManaCost(bon2, 1);
        }
    }
}
