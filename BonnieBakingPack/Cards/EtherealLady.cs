using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateEtherealLadies()
        {
            // The Ethereal Lady
            CardManager.New(pluginPrefix, "etherealLady", "Ethereal Lady", 3, 1, "Under her protection, there will be no misery or strife.")
                .SetDefaultPart1Card().AddAct1().SetRare()
                .SetBloodCost(3)
                .SetPortraitAndEmission(GetTexture("etherealLady.png"), GetTexture("etherealLady_emission.png"))
                .SetPixelPortrait(GetTexture("etherealLady_pixel.png"))
                .AddAbilities(Ability.DeathShield, Ability.AllStrike)
                .AddAppearances(LadyAbility.CardAppearance)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck();

            CardInfo queen = CardManager.New(pluginPrefix, "eternalLady", "Our Eternal Lady", 1, 1, "UNDER HER CARE THERE WILL BE NO SUFFERING OR DEATH.")
                .SetDefaultPart1Card().AddGrimora().SetRare()
                .SetBonesCost(0).SetEnergyCost(6)
                .SetPortraitAndEmission(GetTexture("eternalLady.png"), GetTexture("eternalLady_emission.png"))
                .AddTraits(Trait.DeathcardCreationNonOption)
                .SetOnePerDeck();

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Sculptor");
                queen.AddAbilities(ability, Ability.DrawCopyOnDeath);
            }
            else
            {
                queen.AddAbilities(Ability.DrawCopyOnDeath, Ability.BuffNeighbours);
            }

            CardInfo bot = CardManager.New(pluginPrefix, "completeLady", "The Lady Complete", 2, 1)
                .SetDefaultPart3Card().AddP03().SetRare()
                .SetGemsCost(GemType.Green, GemType.Orange, GemType.Blue)
                .SetPortraitAndEmission(GetTexture("completeLady.png"), GetTexture("completeLady_decal.png"))
                .SetOnePerDeck();

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Purist With Blue");
                Ability ability2 = ScrybeCompat.GetP03Ability("Orange Mox Printer");
                Ability ability3 = ScrybeCompat.GetP03Ability("Emerald Blessing");
                bot.AddAbilities(ability3, ability2, ability).AddMetaCategories(ScrybeCompat.WizardRegion);
                ScrybeCompat.AddPart3Decal(bot, bot.GetEmissivePortrait().texture);
            }
            else
            {
                bot.AddAbilities(Ability.TriStrike, Ability.DebuffEnemy, Ability.MadeOfStone);
            }
        }
    }
}
