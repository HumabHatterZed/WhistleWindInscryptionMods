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
        private void Card_SilentOrchestra_T0131()
        {
            const string silentOrchestra = "silentOrchestra";

            NewCard(silentOrchestra, "The Silent Orchestra", "Soon, the song none can hear but all can listen to will begin.",
                attack: 2, health: 6, blood: 3)
                .SetPortraits(silentOrchestra)
                .AddAbilities(Conductor.ability)
                .AddTribes(TribeAnthropoid)
                .AddTraits(Orchestral)
                .SetDefaultEvolutionName("The Grand Silent Orchestra")
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph);
        }
    }
}