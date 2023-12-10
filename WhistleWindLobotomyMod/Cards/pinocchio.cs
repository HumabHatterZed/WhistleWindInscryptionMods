using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_Pinocchio_F01112()
        {
            const string pinocchio = "pinocchio";

            NewCard(pinocchio, "Pinocchio", "A wooden doll that mimics the beasts it encounters. Can you see through its lie?",
                attack: 0, health: 1, bones: 1, temple: CardTemple.Undead)
                .SetPortraits(pinocchio)
                .AddAbilities(Copycat.ability)
                .AddTribes(TribeBotanic)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Teth, ModCardType.Ruina);
        }
    }
}