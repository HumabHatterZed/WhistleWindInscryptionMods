using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_Flower()
        {
            const string flower = "flower", flowerP = "flowerEphemeral";

            CardManager.New(pluginPrefix, flowerP, "Ephemeral Flower", 1, 1)
                .SetPortraits(Assembly, flower)
                .AddTribes(TribeBotanic)
                .AddAbilities(HealingStrike.ability, Ability.Brittle)
                .AddTraits(BloomingFlower)
                .SetBoneless()
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);

            CardManager.New(pluginPrefix, flower, "Flower", 0, 1)
                .SetPortraits(Assembly, flower)
                .AddTribes(TribeBotanic)
                .AddAbilities(Ethereal.ability)
                .AddTraits(BloomingFlower)
                .SetBoneless()
                .AddAppearances(CardAppearanceBehaviour.Appearance.TerrainLayout);
        }
    }
}