using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod {
    public partial class Cards {
        public const string rudoltaSleigh = "wstl_rudoltaSleigh";
        public const string rudoltaSleigh_mule = "wstl_RUDOLTA_MULE";
        private static void Rudolta_F0249() {
            string sleighName = "Rudolta of the Sleigh";
            string textureName = "rudoltaSleigh";
            Ability[] abilities = new[] { Ability.Strafe, GiftGiver.ID };
            Tribe[] tribes = new[] { Tribe.Hooved };

            CardManager.New(LobotomyPlugin.pluginPrefix, rudoltaSleigh, sleighName,
                attack: 2, health: 3, "A grotesque effigy of a reindeer, laden with hate-filled gifts.")
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);

            CardManager.New(LobotomyPlugin.pluginPrefix, rudoltaSleigh_mule, sleighName,
                attack: 2, health: 3)
                .SetBloodCost(2)
                .SetPortraits(LobotomyPlugin.ModAssembly, textureName)
                .AddAbilities(abilities)
                .AddSpecialAbilities(SpecialTriggeredAbility.PackMule)
                .AddTribes(tribes)
                .AddTraits(Trait.Uncuttable)
                .Build();
        }
    }
}