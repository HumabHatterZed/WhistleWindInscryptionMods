using DiskCardGame;
using Infiniscryption.Spells.Sigils;
using InscryptionAPI.Card;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void XCard_Titania()
        {
            const string titania = "titania";
            const string oberon = "oberon";

            NewCard(titania, "Titania", "The queen of faeries, searching always for her traitorous husband.",
                attack: 1, health: 4, blood: 2)
                .SetPortraits(titania)
                .AddAbilities(Ability.StrafeSwap)
                .Build(CardHelper.ChoiceType.Rare, RiskLevel.Aleph, ModCardType.WonderLab);

            NewCard(oberon, "Oberon",
                attack: 1, health: 4, blood: 2)
                .SetPortraits(oberon)
                .AddAbilities(Ability.StrafeSwap)
                .Build(CardHelper.ChoiceType.Rare, cardType: ModCardType.EventCard, nonChoice: true);
        }
    }
}