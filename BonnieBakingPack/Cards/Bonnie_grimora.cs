using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBonnieGrimora()
        {
            CardInfo bon = CardManager.New(pluginPrefixG, "bonnie", "Leela", 1, 1, "AMIDST ALL THESE MONSTERS AND GHOULS, THIS HUMAN FITS RIGHT IN.")
                .SetRare().AddGrimora()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("bonnie_grimora.png"), GetTexture("bonnie_grimora_emission.png"))
                .SetPixelPortrait(GetTexture("bonnie_grimora_pixel.png"))
                .AddAbilities(FreshFood.ability)
                .SetExtendedProperty("IsBonnie", true)
                .SetOnePerDeck();

            CardInfo bon2 = CardManager.New(pluginPrefixG, "bunnie", "Leppy", 2, 2, "A WICKED HEAT BURNS IN ME. MY FINGERS DIG INTO YOUR NECK, YOUR SKIN BETWEEN MY NAILS, AS GLORIOUS LIFE FORMS A HALO BENEATH YOU. MY MUSCLES TENSE AND SHUDDER ATOP YOU AS YOU WHIMPER BENEATH ME. SO FRAIL AND HELPLESS, LIKE A LITTLE BUNNY. THE HEAT GROWS UNBEARABLE AND I BITE DOWN INTO YOU. HARDER, HARDER, HARDER. THE TASTE OF YOU PAINTS MY MOUTH AS THE GOODNESS WITHIN YOU SPILLS INSIDE ME. YOUR LIMP PAWS BEAT AGAINST ME, DRUMMING IN TUNE WITH MY RACING HEART. I PRESS MYSELF AGAINST YOU, FLESH GRINDING AGAINST SQUIRMING FLESH. THE HEAT BUILDS, HIGHER AND HIGHER UNTIL IT SNAPS WITHIN ME. YOUR LIFE MIXES WITH MY PLEASURE, TRACING RIVERS DOWN MY SHIVERING THIGHS. YOUR TASTE LINGERS IN MY MOUTH, TUFTS OF BLOODIED FUR BETWEEN MY TEETH, YOUR DULL EYES DROWNING IN SALT WATER. THE HEAT HAS LEFT. BUT NEVER FOR LONG.")
                .SetRare().AddGrimora(false)
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("bunnie_grimora.png"), GetTexture("bunnie_grimora_emission.png"))
                .AddAbilities(FreshIngredients.ability)
                .AddSpecialAbilities(BunnieAttackAbility.SpecialAbility)
                .SetOnePerDeck();

            if (ScrybeCompat.P03Enabled)
            {
                bon.AddMetaCategories(ScrybeCompat.UndeadRegion);
            }
        }
    }
}
