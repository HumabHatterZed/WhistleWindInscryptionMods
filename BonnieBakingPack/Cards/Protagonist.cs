using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateProtagonists()
        {
            // ???
            CardManager.New(pluginPrefix, "protagonist", "???", 1, 2, "A mysterious person with a bad habit of sticking their nose where it shouldn't be.")
                .SetDefaultPart1Card().AddAct1()
                .SetBloodCost(2)
                .SetPortraitAndEmission(GetTexture("protagonist.png"), GetTexture("protagonist_emission.png"))
                .SetAltPortrait(GetTexture("protagonistAlt.png")).SetEmissiveAltPortrait(GetTexture("protagonistAlt_emission.png"))
                .SetPixelPortrait(GetTexture("protagonist_pixel.png"))
                .AddAbilities(Ability.Tutor);

            CardInfo warrior = CardManager.New(pluginPrefix, "unknownWarrior", "Unknown Soldier", 2, 2, "A MYSTERIOUS SOLDIER, KNOWN ONLY TO GOD.")
                .SetDefaultPart1Card().AddGrimora()
                .SetBonesCost(4)
                .SetPortraitAndEmission(GetTexture("unknownWarrior.png"), GetTexture("unknownWarrior_emission.png"));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Malnourishment");
                warrior.AddAbilities(Ability.ActivatedStatsUp, ability);
            }
            else
            {
                warrior.AddAbilities(Ability.DeathShield);
            }

            CardInfo bot = CardManager.New(pluginPrefix, "defaultUser", "Default User", 0, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("defaultUser.png"));

            if (ScrybeCompat.P03Enabled)
            {
                Ability ability = ScrybeCompat.GetP03Ability("Button Pusher");
                bot.AddAbilities(ability).AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
            else
            {
                bot.AddAbilities(Ability.DrawCopy);
            }
        }
    }
}
