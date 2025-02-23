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
        public const string namelessFetus = "wstl_namelessFetus";
        public const string namelessFetusAwake = "wstl_namelessFetusAwake";
        private static void NamelessFetus_O0115()
        {
            string fetusName = "Nameless Fetus";
            string textureName = "namelessFetusAwake";
            string textureName2 = "namelessFetus";
            Tribe[] tribes = new[] { TribeAnthropoid };

            CardManager.New(LobotomyPlugin.pluginPrefix, namelessFetusAwake, displayName: fetusName,
                attack: 0, health: 1)
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Aggravating.ability, Ability.PreventAttack, Ability.Sacrificial)
                .AddTribes(tribes)
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, namelessFetus, fetusName,
                attack: 0, health: 1, "A neverending supply of blood. Just don't wake it up.")
                .SetBonesCost(3)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName2)
                .AddAbilities(Ability.TripleBlood, Ability.Sacrificial)
                .AddSpecialAbilities(Syrinx.specialAbility)
                .AddTribes(tribes)
                .AddTraits(Trait.Goat)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}