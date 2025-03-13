using DiskCardGame;
using InscryptionAPI.Card;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateEtherealLadies()
        {
            // The Ethereal Lady
            CardInfo lady = CardManager.New(pluginPrefix, "etherealLady", "Ethereal Lady", 3, 1, "Under her protection, there will be no misery or strife.")
                .SetRare().AddAct1()
                .SetBloodCost(3)
                .SetPortraitAndEmission(GetTexture("etherealLady.png"), GetTexture("etherealLady_emission.png"))
                .SetPixelPortrait(GetTexture("etherealLady_pixel.png"))
                .AddAbilities(Ability.DeathShield, Ability.AllStrike)
                .AddAppearances(LadyAbility.CardAppearance)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck();

            CardInfo queen = CardManager.New(pluginPrefixG, "eternalLady", "Our Eternal Lady", 1, 1, "UNDER HER CARE THERE WILL BE NO SUFFERING OR DEATH.")
                .SetRare().AddGrimora()
                .SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("eternalLady.png"), GetTexture("eternalLady_emission.png"))
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAbilities(Ability.DrawCopyOnDeath, ScrybeCompat.GetGrimoraAbility("Sculptor", Ability.BuffNeighbours))
                .SetOnePerDeck();

            CardInfo bot = CardManager.New(pluginPrefix3, "administrator", "Administrator", 1, 3)
                .SetRare().AddP03()
                .SetEnergyCost(4)
                .SetPortraitAndEmission(GetTexture("administrator.png"), GetTexture("administrator_emission.png"))
                .AddAppearances(LadyAbility.CardAppearance)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAbilities(
                    ScrybeCompat.GetP03Ability("Button Pusher", Ability.DebuffEnemy),
                    ScrybeCompat.GetP03Ability("Combat Research", Ability.BuffNeighbours))
                .SetOnePerDeck();

            CardInfo gem = CardManager.New(pluginPrefixM, "completeLady", "The Lady Complete", 1, 2, "A portrait of perfection; none will ever compare to such beauty.")
                .SetRare().AddMagnificus()
                .SetGemsCost(GemType.Green, GemType.Orange, GemType.Blue)
                .SetPortrait(GetTexture("completeLady.png"))
                .AddAbilities(
                    ScrybeCompat.GetMagnificusAbility("Stimulation", Ability.GainAttackOnKill),
                    ScrybeCompat.GetMagnificusAbility("Stimulation (Health)", Ability.DeathShield),
                    ScrybeCompat.GetMagnificusAbility("Purist", Ability.DebuffEnemy)
                    )
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck();

            if (ScrybeCompat.P03Enabled)
            {
                ScrybeCompat.AddPart3Decal(bot, bot.GetEmissivePortrait().texture);

                lady.AddMetaCategories(ScrybeCompat.NatureRegion);
                queen.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.TechRegion);
                gem.AddMetaCategories(ScrybeCompat.WizardRegion);
            }
        }
    }
}
