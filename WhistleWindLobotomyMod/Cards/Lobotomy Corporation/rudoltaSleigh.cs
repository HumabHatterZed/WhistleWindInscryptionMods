using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Rudolta_F0249()
        {
            const string sleighName = "Rudolta of the Sleigh";
            const string rudoltaSleigh = "rudoltaSleigh";
            Ability[] abilities = new[] { Ability.Strafe, GiftGiver.ability };
            Tribe[] tribes = new[] { Tribe.Hooved };

            CardManager.New(pluginPrefix, rudoltaSleigh, sleighName,
                attack: 2, health: 3, "A grotesque effigy of a reindeer. With its infinite hate, it bequeaths gifts onto you.")
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, rudoltaSleigh)
                .AddAbilities(abilities)
                .AddTribes(tribes)
                .Build(CardHelper.CardType.Common, RiskLevel.He, true);

            CardManager.New(pluginPrefix, "RUDOLTA_MULE", sleighName,
                attack: 2, health: 3)
                .SetBloodCost(2)
                .SetPortraits(ModAssembly, rudoltaSleigh)
                .AddAbilities(abilities)
                .AddSpecialAbilities(SpecialTriggeredAbility.PackMule)
                .AddTribes(tribes)
                .AddTraits(Trait.Uncuttable)
                .Build();
        }
    }
}