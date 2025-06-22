using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string lyingAdult = "wstl_lyingAdult";
        private static void AdultWhoTellsLies_F01117() {
            string textureName = "lyingAdult";
            CardManager.New(LobotomyPlugin.pluginPrefix, lyingAdult, "The Adult Who Tells Lies",
                attack: 1, health: 5)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(FalseThrone.ability)
                .AddSpecialAbilities(BoardEffects.specialAbility)
                .AddTribes(TribeAnthropoid)
                .AddAppearances(ForcedWhiteEmission.appearance)
                .SetOnePerDeck()
                .SetEventCard(true)
                .AddMetaCategories(RuinaCard)
                .Build();
        }
    }
}