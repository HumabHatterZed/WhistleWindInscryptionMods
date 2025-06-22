using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;

namespace BonniesBakingPack {
    public partial class BakingPlugin {
        private void CreatePhoneMice() {
            // Phone Mouse
            CardInfo phone = CardManager.New(pluginPrefix, "mousePhone", "Phone Mouse", 0, 2, "A chatty little rodent. When it dies, backup follows swiftly.")
                .SetDefaultPart1Card().AddAct1()
                .SetEnergyCost(3)
                .SetPortraitAndEmission(GetTexture("mousePhone.png"), GetTexture("mousePhone_emission.png"))
                .SetPixelPortrait(GetTexture("mousePhone_pixel.png"))
                .AddAbilities(Ability.DrawRandomCardOnDeath)
                .AddTraits(Trait.SatisfiesRingTrial);

            CardInfo killer = CardManager.New(pluginPrefixG, "killerMouse", "Killer Mouse", 1, 2, "DEATH FOLLOWS SWIFTLY WHEN THIS STABBY LITTLE MOUSE COMES A-CALLING.")
                .SetDefaultPart1Card().AddGrimora()
                .SetEnergyCost(3)
                .SetPortraitAndEmission(GetTexture("killerMouse.png"), GetTexture("killerMouse_emission.png"))
                .SetPixelPortrait(GetTexture("killerMouse_pixel.png"));

            CardInfo bot = CardManager.New(pluginPrefix3, "phoneMouse", "Mouse Phone", 0, 0)
                .SetDefaultPart3Card().AddP03()
                .SetEnergyCost(1)
                .SetPortrait(GetTexture("phoneMouse.png"))
                .SetPixelPortrait(GetTexture("phoneMouse_pixel.png"))
                .SetGlobalSpell()
                .AddAbilities(ScrybeCompat.GetP03Ability("Tinkerer", Ability.DrawRandomCardOnDeath));

            CardInfo gem = CardManager.New(pluginPrefixM, "witness", "Witness", 0, 1, "No misdeed nor demerit will go unseen by this rat's eternal gaze.")
                .SetDefaultPart1Card().AddMagnificus()
                .SetGemsCost(GemType.Blue)
                .SetPortrait(GetTexture("witness.png"))
                .SetPixelPortrait(GetTexture("witness_pixel.png"))
                .AddAbilities(ScrybeCompat.GetMagnificusAbility("Dead Draw", Ability.DrawRandomCardOnDeath));

            if (ScrybeCompat.GrimoraEnabled) {
                Ability ability = ScrybeCompat.GetGrimoraAbility("Slasher", Ability.None);
                Ability ability2 = ScrybeCompat.GetGrimoraAbility("Haunting Call", Ability.None);
                killer.AddAbilities(ability, ability2);
            }
            else {
                killer.AddAbilities(Ability.DoubleStrike);
            }

            if (ScrybeCompat.P03Enabled) {
                phone.AddMetaCategories(ScrybeCompat.NatureRegion);
                killer.AddMetaCategories(ScrybeCompat.UndeadRegion);
                bot.AddMetaCategories(ScrybeCompat.NeutralRegion);
            }
        }
    }
}
