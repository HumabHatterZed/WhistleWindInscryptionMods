using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBonnieGrimora()
        {
            CardInfo bon = CardManager.New(pluginPrefixG, "bonnie", "Bonnie", 1, 1, "A WICKEDDDDKIND  GIRL WIT H FRIENDLLY INTENTIONNS. .")
                .SetRare().AddGrimora()
                .SetBloodCost(2)
                .SetPortraitAndEmission(GetTexture("bonnie_grimora.png"), GetTexture("bonnie_grimora_emission.png"))
                .SetPixelPortrait(GetTexture("bonnie_grimora_pixel.png"))
                .AddAbilities(FreshFood.ability)
                .SetOnePerDeck();

            CardInfo bon2 = CardManager.New(pluginPrefixG, "bunnie", "Leppy", 2, 2, "A WICKED HEAT BURNS IN ME. MY TEETH BURROW INTO SOFT NECK AND GLORIOUS LIFE PAINTS MY CHEST. MY MUSCLES GROW TENSE AND I BITE DOWN. HARDER, HARDER, HARDER. UNTIL THE FLESH SEPARATES FROM THE BONE. A CRESCENDO. LIGHTS DANCE ACROSS MY VISION AND LIFE MIXES WITH CLEAR ECTASY, ALL FLOWING DOWN FROM ME. THE HEAT LEAVES ME ONCE MORE. BUT NEVER FOR LONG. I WISH IT WOULD LEAVE ME. I WISH I COULD LIVE AMONG THE SHEEP AS A DEFANGED WOLF. I LONG FOR ITS RETURN. I LONG FOR THE TASTE OF SWEET CANDY BETWEEN MY LIPS, YOUR SKIN BETWEEN MY NAILS AND YOUR EYES LIKE A FISH WITHOUT WATER. GUILT AND PLEASURE FIGHT WITHIN ME. MY SPINE SHIVERS FROM THEIR PASSIONATE CLASH. THE HEAT HAS LEFT. BUT NEVER FOR LONG.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBloodCost(2)
                .SetPortraitAndEmission(GetTexture("bunnie_grimora.png"), GetTexture("bunnie_grimora_emission.png"))
                .AddAbilities(FreshIngredients.ability)
                .SetOnePerDeck();

            bon2.metaCategories.Clear();

            if (ScrybeCompat.P03Enabled)
            {
                bon.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bon2.AddMetaCategories(ScrybeCompat.UndeadRegion);
            }
        }
    }
}
