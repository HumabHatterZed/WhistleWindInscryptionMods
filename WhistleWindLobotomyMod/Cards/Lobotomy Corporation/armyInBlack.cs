using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_ArmyInBlack_D01106()
        {
            const string blackName = "Army in Black";
            const string armyInBlack = "armyInBlack";

            CardManager.New(pluginPrefix, armyInBlack, blackName,
                attack: 3, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, armyInBlack)
                .AddAbilities(Ability.ExplodeOnDeath, Ability.Brittle)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(pluginPrefix, "armyInBlackSpell", blackName,
                attack: 0, health: 0)
                .SetPortraits(ModAssembly, armyInBlack)
                .AddAbilities(Ability.ExplodeOnDeath)
                .SetSpellType(SpellType.Targeted)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);
        }
    }
}