using DiskCardGame;
using InscryptionAPI.Card;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowWhitesVine_F0442()
        {
            const string snowWhitesVine = "snowWhitesVine";
            CreateCard(MakeCard(
                cardName: snowWhitesVine,
                "Thorny Vines",
                attack: 0, health: 1)
                .SetPortraits(snowWhitesVine)
                .AddAbilities(Ability.Sharp)
                .AddTribes(TribeBotanic)
                .SetTerrain());
        }
    }
}