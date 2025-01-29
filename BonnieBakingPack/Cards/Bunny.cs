using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateBunnies()
        {
            // Bunny
            CardInfo bunny = CardManager.New(pluginPrefix, "bunny", "Bunny", 1, 2, "A delivery driver with a missing sister. Perhaps you've seen her?")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(1)
                .SetPortraitAndEmission(GetTexture("bunny.png"), GetTexture("bunny_emission.png"))
                .SetPixelPortrait(GetTexture("bunny_pixel.png"))
                .AddAbilities(Ability.GainBattery);

            CardInfo duck = CardManager.New(pluginPrefixG, "duckit", "Duckit", 1, 2, "AN ENIGMATIC, TWO-FACED CREATURE. PERHAPS YOU KNOW ITS TRUE IDENTITY?")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(2).SetEnergyCost(2)
                .SetPortraitAndEmission(GetTexture("duckit.png"), GetTexture("duckit_emission.png"))
                .AddSpecialAbilities(DuckRabbitAbility.SpecialAbility)
                .AddAbilities(
                    ScrybeCompat.GetGrimoraAbility("Random Ability", Ability.RandomAbility)
                    );

            CardInfo bot = CardManager.New(pluginPrefix3, "bunbot", "Bunbot", 2, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(4)
                .SetPortrait(GetTexture("bunbot.png"))
                .AddAbilities(
                    ScrybeCompat.GetP03Ability("Hopper", Ability.Strafe),
                    ScrybeCompat.GetP03Ability("Flammable", Ability.ExplodeOnDeath)
                    );

            if (ScrybeCompat.P03Enabled)
            {
                bunny.AddMetaCategories(ScrybeCompat.NatureRegion);
                duck.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NatureRegion);
            }
        }
    }
}
