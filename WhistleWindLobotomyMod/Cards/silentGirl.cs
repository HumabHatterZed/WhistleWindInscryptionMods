using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.Core.Helpers;
using InscryptionAPI.Card;
using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_SilentGirl_O010()
        {
            const string silentGirl = "silentGirl";

            CardInfo silentGirlCard = NewCard(
                silentGirl,
                "Silent Girl",
                "A girl wielding a hammer and nail.",
                attack: 2, health: 1, blood: 2)
                .SetPortraits(silentGirl)
                .AddAbilities(Ability.TriStrike)
                .AddTribes(TribeAnthropoid);

            CreateCard(silentGirlCard, CardHelper.ChoiceType.Rare, RiskLevel.Teth, ModCardType.Ruina);
        }
    }
}