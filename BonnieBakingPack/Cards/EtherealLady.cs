using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateEtherealLadies()
        {
            // The Ethereal Lady
            CardInfo lady = CardManager.New(pluginPrefix, "etherealLady", "Ethereal Lady", 3, 1, "Under her protection, there will be no misery or strife.")
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
                .AddAbilities(Ability.DrawCopyOnDeath, ScrybeCompat.GetGrimoraAbility("Sculptor", Ability.BuffNeighbours))
                .SetOnePerDeck();

            CardInfo bot = CardManager.New(pluginPrefix, "completeLady", "The Lady Complete", 2, 1)
                .SetDefaultPart3Card().AddP03().SetRare()
                .SetGemsCost(GemType.Green, GemType.Orange, GemType.Blue)
                .SetPortraitAndEmission(GetTexture("completeLady.png"), GetTexture("completeLady_decal.png"))
                .AddAppearances(LadyAbility.CardAppearance)
                .AddTraits(Trait.DeathcardCreationNonOption)
                .AddAbilities(
                    ScrybeCompat.GetP03Ability("Purist With Blue", Ability.TriStrike),
                    ScrybeCompat.GetP03Ability("Orange Mox Printer", Ability.DebuffEnemy),
                    ScrybeCompat.GetP03Ability("Emerald Blessing", Ability.MadeOfStone))
                .SetOnePerDeck();

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    lady.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    queen.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.WizardRegion);
                ScrybeCompat.AddPart3Decal(bot, bot.GetEmissivePortrait().texture);
            }
        }
    }
}
