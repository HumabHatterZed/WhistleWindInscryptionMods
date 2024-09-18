using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_WhiteNight_T0346()
        {
            const string whiteNight = "whiteNight";

            CardManager.New(pluginPrefix, whiteNight, "WhiteNight",
                attack: 0, health: 66, "The time has come.")
                .SetPortraits(ModAssembly, whiteNight)
                .SetTitle(ModAssembly, "whiteNight_title.png")
                .AddAbilities(Ability.Flying, Idol.ability, TrueSaviour.ability)
                .AddTribes(TribeDivine)
                .AddTraits(ImmuneToInstaDeath, Trait.Uncuttable)
                .AddAppearances(ForcedWhiteEmission.appearance, CardAppearanceBehaviour.Appearance.TerrainLayout)
                .SetOnePerDeck()
                .SetEventCard(true)
                .Build();
        }
    }
}