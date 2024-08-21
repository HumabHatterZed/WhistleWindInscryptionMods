using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SwanBrothers_F0270()
        {
            const string brother1 = "dreamOfABlackSwanBrother1";
            const string brother2 = "dreamOfABlackSwanBrother2";
            const string brother3 = "dreamOfABlackSwanBrother3";
            const string brother4 = "dreamOfABlackSwanBrother4";
            const string brother5 = "dreamOfABlackSwanBrother5";
            const string brother6 = "dreamOfABlackSwanBrother6";

            CardManager.New(pluginPrefix, brother1, "First Brother", 0, 1)
                .SetPortraits(Assembly, brother1)
                .AddAbilities(Persistent.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, brother2, "Second Brother", 0, 1)
                .SetPortraits(Assembly, brother2)
                .AddAbilities(Piercing.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, brother3, "Third Brother", 0, 1)
                .SetPortraits(Assembly, brother3)
                .AddAbilities(Ability.Sharp)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, brother4, "Fourth Brother", 0, 1)
                .SetPortraits(Assembly, brother4)
                .AddAbilities(Ability.Deathtouch)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, brother5, "Fifth Brother", 0, 1)
                .SetPortraits(Assembly, brother5)
                .AddAbilities(BindingStrike.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, brother6, "Sixth Brother", 0, 1)
                .SetPortraits(Assembly, brother6)
                .AddAbilities(ThickSkin.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(SwanBrother)
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);
        }
    }
}