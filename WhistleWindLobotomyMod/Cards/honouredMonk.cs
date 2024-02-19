using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

using static WhistleWind.AbnormalSigils.AbnormalPlugin;
using static WhistleWindLobotomyMod.Core.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_HonouredMonk_D01110()
        {
            const string honouredMonk = "honouredMonk";
            const string cloudedMonk = "cloudedMonk";
            Tribe[] tribes = new[] { TribeAnthropoid };

            CardInfo cloudedMonkCard = NewCard(cloudedMonk, "Clouded Monk",
                attack: 4, health: 2, blood: 2, temple: CardTemple.Wizard)
                .SetPortraits(cloudedMonk)
                .AddTribes(tribes)
                .Build(cardType: ModCardType.Donator);

            NewCard(honouredMonk, "Honoured Monk", "A monk seeking enlightenment through good deeds. But surely there's a quicker way to nirvana...",
                attack: 2, health: 1, blood: 2, temple: CardTemple.Wizard)
                .SetPortraits(honouredMonk)
                .AddAbilities(Ability.Evolve)
                .AddTribes(tribes)
                .SetEvolve(cloudedMonkCard, 1)
                .Build(CardHelper.ChoiceType.Common, RiskLevel.Waw, ModCardType.Donator);
        }
    }
}