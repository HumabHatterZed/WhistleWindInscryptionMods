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
        private void Card_NamelessFetus_O0115()
        {
            const string fetusName = "Nameless Fetus";
            const string namelessFetus = "namelessFetus";
            const string namelessFetusAwake = "namelessFetusAwake";
            Tribe[] tribes = new[] { TribeAnthropoid };

            CardManager.New(pluginPrefix, namelessFetusAwake, displayName: fetusName,
                attack: 0, health: 1)
                .SetBonesCost(3)
                .SetPortraits(ModAssembly, namelessFetusAwake)
                .AddAbilities(Aggravating.ability, Ability.PreventAttack, Ability.Sacrificial)
                .AddTribes(tribes)
                .Build();

            CardManager.New(pluginPrefix, namelessFetus, fetusName,
                attack: 0, health: 1, "A neverending supply of blood. Just don't wake it up.")
                .SetBonesCost(3)
                .SetPortraits(ModAssembly, namelessFetus)
                .AddAbilities(Ability.TripleBlood, Ability.Sacrificial)
                .AddSpecialAbilities(Syrinx.specialAbility)
                .AddTribes(tribes)
                .AddTraits(Trait.Goat)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);
        }
    }
}