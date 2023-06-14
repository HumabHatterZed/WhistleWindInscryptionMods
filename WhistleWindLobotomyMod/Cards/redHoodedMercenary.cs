using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_RedHoodedMercenary_F0157()
        {
            const string redHoodedMercenary = "redHoodedMercenary";

            CardInfo redHoodedMercenaryCard = NewCard(
                redHoodedMercenary,
                "Little Red Riding Hooded Mercenary",
                "A skilled mercenary with a bloody vendetta. Perhaps you can help her sate it.",
                attack: 2, health: 5, blood: 3)
                .SetPortraits(redHoodedMercenary)
                .AddAbilities(Ability.Sniper, Persistent.ability)
                .AddSpecialAbilities(CrimsonScar.specialAbility)
                .AddTribes(TribeAnthropoid)
                .SetEvolveInfo("[name]Red Riding Hooded Mercenary");

            CreateCard(redHoodedMercenaryCard, CardHelper.ChoiceType.Common, RiskLevel.Waw);
        }
    }
}