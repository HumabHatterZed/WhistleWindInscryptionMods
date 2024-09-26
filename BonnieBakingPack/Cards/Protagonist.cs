using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;

namespace BonniesBakingPack
{
    public partial class BakingPlugin
    {
        private void CreateProtagonists()
        {
            CardInfo protag = CardManager.New(pluginPrefix, "protagonist", "???", 1, 2, "A mysterious person with a bad habit of sticking their nose where it shouldn't be.")
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

            CardInfo bot = CardManager.New(pluginPrefix, "defaultUser", "Default User", 0, 1)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(3)
                .SetPortrait(GetTexture("defaultUser.png"))
                .AddAbilities(ScrybeCompat.GetP03Ability("Button Pusher", Ability.DrawCopy));

            if (ScrybeCompat.GrimoraEnabled)
            {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Malnourishment", Ability.None);
                warrior.AddAbilities(Ability.ActivatedStatsUp, ability);
            }
            else
            {
                warrior.AddAbilities(Ability.DeathShield);
            }

            if (ScrybeCompat.P03Enabled)
            {
                if (OverrideAct1.Value.HasFlag(ActOverride.Act3))
                    protag.AddMetaCategories(ScrybeCompat.NatureRegion);

                if (OverrideGrimora.Value.HasFlag(ActOverride.Act3))
                    warrior.AddMetaCategories(ScrybeCompat.UndeadRegion);

                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
