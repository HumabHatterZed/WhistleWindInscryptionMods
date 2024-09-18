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
        private void Card_Nosferatu_F01113()
        {
            const string nosferatuName = "Nosferatu";
            const string nosferatu = "nosferatu", nosferatuBeast = "nosferatuBeast";
            Tribe[] tribes = new[] { TribeFae };

            CardInfo beast = CardManager.New(pluginPrefix, nosferatuBeast, nosferatuName,
                attack: 3, health: 1)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead).SetPortraits(ModAssembly, nosferatuBeast)
                .AddAbilities(Bloodfiend.ability, Bloodfiend.ability)
                .AddTribes(tribes)
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(pluginPrefix, nosferatu, nosferatuName,
                attack: 2, health: 1, "A creature of the night, noble and regal. Will you help sate its thirst?")
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(ModAssembly, nosferatu)
                .AddAbilities(Ability.Evolve, Bloodfiend.ability)
                .AddTribes(tribes)
                .SetEvolve(beast, 1)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}