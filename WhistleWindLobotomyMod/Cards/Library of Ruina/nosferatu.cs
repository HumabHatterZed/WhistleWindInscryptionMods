using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class Cards
    {
        public const string nosferatu = "wstl_nosferatu";
        public const string nosferatuBeast = "wstl_nosferatuBeast";
        private static void Nosferatu_F01113()
        {
            string name = "Nosferatu";
            string desc = "A creature of the night, noble and regal. Will you help sate its thirst?";
            string textureName = "nosferatuBeast";
            string textureName2 = "nosferatu";
            Tribe[] tribes = new[] { TribeFae };

            CardInfo beast = CardManager.New(LobotomyPlugin.pluginPrefix, nosferatuBeast, name,
                attack: 3, health: 1)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability, Bloodfiend.ability)
                .AddTribes(tribes)
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, nosferatu, name,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve, Bloodfiend.ability)
                .AddTribes(tribes)
                .SetEvolve(beast, 1)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw);

            CardInfo beast2 = CardManager.New(LobotomyPlugin.pixelPrefix, textureName, name,
                attack: 3, health: 1)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Bloodfiend.ability, Bloodfiend.ability)
                .AddTribes(tribes)
                .AddMetaCategories(RuinaCard)
                .Build();

            CardManager.New(LobotomyPlugin.pixelPrefix, textureName2, name,
                attack: 2, health: 1, desc)
                .SetBloodCost(2)
                .SetCardTemple(CardTemple.Undead)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.Evolve, Bloodfiend.ability)
                .AddTribes(tribes)
                .SetEvolve(beast, 1)
                .AddMetaCategories(RuinaCard)
                .Build(CardHelper.CardType.Common, RiskLevel.Waw, true);
        }
    }
}