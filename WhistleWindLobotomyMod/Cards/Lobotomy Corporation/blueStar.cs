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
        private void Card_BlueStar_O0393()
        {
            const string starName = "Blue Star";
            const string blueStar = "blueStar";
            Tribe[] tribes = new[] { TribeDivine };

            CardInfo star3 = CardManager.New(pluginPrefix, "blueStar3", starName,
                attack: 4, health: 4)
                .SetBloodCost(4)
                .SetPortraits(ModAssembly, blueStar, pixelPortraitName: "blueStar2_pixel")
                .AddAbilities(Ability.Transformer, Ability.AllStrike)
                .AddSpecialAbilities(StarSound.specialAbility)
                .AddTribes(tribes)
                .AddAppearances(ForcedEmission.appearance)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardInfo star2 = CardManager.New(pluginPrefix, "blueStar2", starName,
                attack: 0, health: 4)
                .SetBloodCost(4)
                .SetPortraits(ModAssembly, blueStar)
                .AddAbilities(Ability.Transformer, Idol.ability)
                .AddTribes(tribes)
                .SetEvolve(star3, 1)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardInfo star = CardManager.New(pluginPrefix, blueStar, starName,
                attack: 0, health: 4, "When this is over, let's meet again as stars.")
                .SetBloodCost(3)
                .SetPortraits(ModAssembly, blueStar)
                .AddAbilities(Ability.Transformer)
                .AddTribes(tribes)
                .SetEvolve(star2, 1)
                .Build(CardHelper.CardType.Rare, RiskLevel.Aleph, true);

            star3.SetEvolve(star, 1); // set the evolve info here to prevent any errors with evolving cards and encounters
        }
    }
}