using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string armyInBlack = "wstl_armyInBlack";
        public const string armyInBlackSpell = "wstl_armyInBlackSpell";
        private static void ArmyInBlack_D01106() {
            string blackName = "Army in Black";
            string textureName = "armyInBlack";
            CardManager.New(LobotomyPlugin.pluginPrefix, armyInBlack, blackName,
                attack: 3, health: 3)
                .SetBonesCost(4)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.ExplodeOnDeath, Ability.Brittle)
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);

            CardManager.New(LobotomyPlugin.pluginPrefix, armyInBlackSpell, blackName,
                attack: 0, health: 0)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(Ability.ExplodeOnDeath)
                .SetSpellType(SpellType.Targeted)
                .SetBoneless()
                .Build(CardHelper.CardType.Rare, overrideCardChoice: true);
        }
    }
}