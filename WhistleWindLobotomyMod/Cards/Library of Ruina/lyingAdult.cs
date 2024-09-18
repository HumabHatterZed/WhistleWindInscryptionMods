using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_AdultWhoTellsLies_F01117()
        {
            const string lyingAdult = "lyingAdult";

            CardManager.New(pluginPrefix, lyingAdult, "The Adult Who Tells Lies",
                attack: 1, health: 5)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, lyingAdult)
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